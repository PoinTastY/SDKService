using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Threading;
using SDKService.Models;
using Newtonsoft.Json;
using System.IO;
using StareMedic.Models.Entities;

public class TCPServer
{
    private TcpListener listener;
    private EventLog eventLog;
    private Thread serverThread;

    public TCPServer(int port, EventLog eventLog)
    {
        this.listener = new TcpListener(IPAddress.Any, port);
        this.eventLog = eventLog;
        this.serverThread = new Thread(ListenForClients);
    }

    public void Start()
    {
        try
        {
            serverThread.Start();
            eventLog.WriteEntry("Servidor TCP iniciado. Esperando conexiones...", EventLogEntryType.Information);
        }
        catch (Exception ex)
        {
            // Captura cualquier excepción y registra el mensaje en el EventLog
            eventLog.WriteEntry("Error al iniciar el servidor TCP: " + ex.Message, EventLogEntryType.Error);
        }
    }

    private void ListenForClients()
    {
        try
        {
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                eventLog.WriteEntry("Cliente conectado desde " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), EventLogEntryType.Information);

                Stream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string RawRequest = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Request newreq = JsonConvert.DeserializeObject<Request>(RawRequest);


                Response response = new Response();

                switch (newreq.Work)
                {
                    case 0:
                        
                        break;
                    case 1://agenerar remision
                        SDK.tDocumento doc = GenerateRemision(eventLog, newreq);
                        response.ResponseCode = doc.aFolio;
                        response.ResponseContent = JsonConvert.SerializeObject(doc);
                        break;
                    default:
                        break;
                }


                
                eventLog.WriteEntry("Mensaje recibido: " + EventLogEntryType.Information);

                // Procesar el mensaje recibido

                // serializar y enviar la respuesta al cliente
                byte[] responseBuffer = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(response));
                stream.Write(responseBuffer, 0, responseBuffer.Length);

                client.Close();
            }
        }
        catch (Exception ex)
        {
            // Captura cualquier excepción y registra el mensaje en el EventLog
            eventLog.WriteEntry("Error en el servidor TCP: " + ex.Message, EventLogEntryType.Error);
        }
    }

    #region Funciones
    

    private static SDK.tDocumento GenerateRemision(EventLog log, Request req)
    {
        #region UnpackJson

        SDK.tDocumento lDocto = JsonConvert.DeserializeObject<SDK.tDocumento>(req.SerialDocto);
        CasoClinico casoClinico = JsonConvert.DeserializeObject<CasoClinico>(req.SerialEntity1);
        Patient patient = JsonConvert.DeserializeObject<Patient>(req.SerialEntity2);
        Rooms room = JsonConvert.DeserializeObject<Rooms>(req.SerialEntity3);
        Medic medic = JsonConvert.DeserializeObject<Medic>(req.SerialEntity4);
        Diagnostico diagnostico = JsonConvert.DeserializeObject<Diagnostico>(req.SerialEntity5);

        #endregion

        int lError = 0;//4 error management

        #region Find or Create Client

        lError = SDK.fBuscaCteProv(lDocto.aCodigoCteProv);

        if(lError != 0)
        {
            //asumimos que no existe, entonces creamos 
            log.WriteEntry($"No se encontro la habitacion: {room.Nombre}, Intentando crearlo");
            int aIdCteProv = 0;
            SDK.ClienteProveedor cliente = new SDK.ClienteProveedor();
            cliente.cRazonSocial = room.Nombre;
            cliente.cCodigoCliente = lDocto.aCodigoCteProv;
            lError = SDK.fAltaCteProv(ref aIdCteProv, cliente);
        }

        #endregion

        #region Build Document
        StringBuilder serie = new StringBuilder(lDocto.aSerie); //we need a string builder
        double folio = 0;
        int idDocto = 0;

        lError = SDK.fSiguienteFolio(lDocto.aCodConcepto, serie, ref folio);
        if (lError != 0)
        {
            log.WriteEntry($"Problema en obtencion de siguiente folio: {SDK.rError(lError)}");
            lDocto.aFolio = folio;
        }
        else
        {
            lDocto.aFolio = folio;
           

            lError = SDK.fAltaDocumento(ref idDocto, ref lDocto);
            if(lError != 0)
            {
                log.WriteEntry($"Problema en Alta de Documento: {SDK.rError(lError)}");
            }
            else
            {
                log.WriteEntry($"Documento Generado Exitosamente: id doc: {lDocto.aFolio}");
                //posicionar puntero en campo objetivo para actualizar los campos
                lError = SDK.fPosUltimoDocumento();
                if(lError != 0)
                {
                    log.WriteEntry($"No se encontro el documento para actualizar las tablas con los datos proporcionados: {SDK.rError(lError)}");
                }
                else
                {
                    SDK.fEditarDocumento();//may need to add another filter here just in case
                    string observaciones = $"Paciente: {patient.Nombre}\nMedico: {medic.Nombre}\nDiagnostico: {diagnostico.Contenido}";
                    lError = SDK.fSetDatoDocumento("cObservaciones", observaciones);
                    if(lError != 0)
                    {
                        log.WriteEntry($"Error Actualizando el campo: Observaciones: {SDK.rError(lError)}");
                        SDK.fCancelarModificacionDocumento();
                    }
                    else
                    {
                        SDK.fGuardaDocumento();
                    }
                }
                return lDocto;
            }
        }
        #endregion
        return lDocto;

        
    }
    #endregion

    public void Stop()
    {
        listener.Stop();
    }
}
