using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Threading;
using SDKService.Models;
using Newtonsoft.Json;
using System.IO;

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
                        SDK.tDocumento doc = GenerateRemision(eventLog, JsonConvert.DeserializeObject<SDK.tDocumento>(newreq.ObjectRequest));
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
    

    private static SDK.tDocumento GenerateRemision(EventLog log, SDK.tDocumento lDocto)
    {
        int lError = 0;
        StringBuilder serie = new StringBuilder("R");
        double folio = 0;
        string codConcepto = "3";
        //string codCte = "PUBLICO";
        //string codProducto = "ALEXA";
        int idDocto = 0;
        //int idMovto = 0;

        //SDK.tMovimiento lMovto = new SDK.tMovimiento();

        lError = SDK.fSiguienteFolio(codConcepto, serie, ref folio);
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
                log.WriteEntry($"Documento Generado Exitosamente: id doc: {idDocto}");
                return lDocto;
            }
        }
        return lDocto;

        
    }
    #endregion

    public void Stop()
    {
        listener.Stop();
    }
}
