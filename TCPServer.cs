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
    private bool interrupt;
    private Config config;
    private int lError;

    public TCPServer(int port, EventLog eventLog)
    {
        this.listener = new TcpListener(IPAddress.Any, port);
        this.eventLog = eventLog;
        this.serverThread = new Thread(ListenForClients);
        this.interrupt = false;
    }

    public void Start(Config settings)
    {
        try
        {
            serverThread.Start();
            config = settings;
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

            while (!interrupt)
            {
                if (listener.Pending())
                {
                    TcpClient client = listener.AcceptTcpClient();
                    eventLog.WriteEntry("Cliente conectado desde " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), EventLogEntryType.Information);

                    Stream stream = client.GetStream();
                    MemoryStream memory = new MemoryStream();
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memory.Write(buffer, 0, bytesRead);
                        if (bytesRead < buffer.Length)
                            break;

                        byte[] newBuffer = new byte[buffer.Length * 2];
                        Array.Copy(buffer, newBuffer, bytesRead);
                        buffer = newBuffer;
                    }
                    string RawRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Request newreq = JsonConvert.DeserializeObject<Request>(RawRequest);

                    // Procesar el mensaje recibido
                    Response response = new Response();
                    eventLog.WriteEntry($"Trabajo recibido: {newreq.Work}");
                    switch (newreq.Work)
                    {
                        case 0:

                            break;
                        case 1://agenerar remision
                            SDK.tDocumento doc = GenerateRemision(eventLog, newreq);
                            if (doc.aFolio != 0)
                                eventLog.WriteEntry($"Remision Generada Exitosamente, folio: {doc.aFolio}");
                            else
                                eventLog.WriteEntry("Parece que hubo un problema generando la remision :(");
                            response.ResponseCode = doc.aFolio;
                            response.ResponseContent = JsonConvert.SerializeObject(doc);
                            
                            break;
                        default:
                            break;
                    }

                    // serializar y enviar la respuesta al cliente
                    byte[] responseBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                    stream.Write(responseBuffer, 0, responseBuffer.Length);

                    client.Close();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            listener.Stop();
            eventLog.WriteEntry("Listener Apagado");
        }
        catch (Exception ex)
        {
            // Captura cualquier excepción y registra el mensaje en el EventLog
            eventLog.WriteEntry("Error en el servidor TCP: " + ex.Message, EventLogEntryType.Error);
            serverThread.Interrupt();
        }


    }

    #region Funciones


    private SDK.tDocumento GenerateRemision(EventLog log, Request req)
    {
        SDK.tDocumento lDocto = new SDK.tDocumento
        {
            aFolio = 0,
        };
        #region UnpackJson
        try
        {
            log.WriteEntry("Desempaquetando JSON...");
            int attempts = 0;
            while (true)
            {
                try
                {
                    lDocto = JsonConvert.DeserializeObject<SDK.tDocumento>(req.SerialDocto);
                    log.WriteEntry("Json Deserializado");
                    break;
                }
                catch
                {
                    log.WriteEntry("Reintentando desempaquetado...");
                    Thread.Sleep(1000);
                    if (attempts++ > 4)
                    {
                        log.WriteEntry("No se pudo desempaquetar");
                        return lDocto;
                    }
                }
            }
            var intentoInicioSesion = 0;
            while (true)
            {
                lError = SDK.fAbreEmpresa(config.RutaEmpresa);
                if (lError != 0)
                {
                    log.WriteEntry($"Reintentando abrir empresa ({++intentoInicioSesion})...");
                    if (intentoInicioSesion > 4)
                    {
                        log.WriteEntry($"No se pudo abrir la empresa: {SDK.rError(lError)}");
                        return lDocto;
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            #region Find or Create Client
            try
            {
                //buscandoCliente
                log.WriteEntry("Buscando Habitacion para asignar cliente:");
                lError = SDK.fBuscaCteProv(lDocto.aCodigoCteProv);

                if (lError != 0)
                {
                    //asumimos que no existe, entonces creamos 
                    log.WriteEntry($"No se encontro la habitacion: {lDocto.aCodigoCteProv}, Error: {SDK.rError(lError)}");
                    SDK.fCierraEmpresa();
                    return lDocto;
                }
                else
                {
                    log.WriteEntry($"Se  encontro una coincidencia del cliente: {lDocto.aCodigoCteProv} en Contpaqi");
                }
            }
            catch (Exception ex)
            {
                log.WriteEntry($"Excepcion en Gestion de cliente: {ex}");
                //lDocto.aCodigoCteProv = new StringBuilder(SDK.constantes.kLongCodigo).ToString();
                SDK.fCierraEmpresa();
                return lDocto;
            }



            #endregion

            #region Build Document
            StringBuilder serie = new StringBuilder(lDocto.aSerie); //we need a string builder
            double folio = 0;
            int idDocto = 0;

            //trying to retrieve next Folio
            log.WriteEntry($"Intentando conseguir folio...");
            lError = SDK.fSiguienteFolio(lDocto.aCodConcepto, serie, ref folio);
            if (lError != 0)
            {
                log.WriteEntry($"Problema en obtencion de siguiente folio: {SDK.rError(lError)}");
                lDocto.aFolio = folio;
            }
            else
            {
                log.WriteEntry($"Folio Obtenido: {folio}");
                lDocto.aFolio = folio;

                lError = SDK.fAltaDocumento(ref idDocto, ref lDocto);
                if (lError != 0)
                {
                    log.WriteEntry($"Problema en Alta de Documento: {SDK.rError(lError)}");
                    lDocto.aFolio = 0;
                    SDK.fCierraEmpresa();
                    return lDocto;
                }
                else
                {
                    #region Generar Movimiento

                    //este es requerido, ya que sin el, el documento no aparece en la lista de pendientes, por default se generara un movimiento con cargo de hospitalizacion.
                    SDK.tMovimiento lMovimiento = new SDK.tMovimiento();

                    lMovimiento.aCodAlmacen = config.CodAlmacen;
                    lMovimiento.aCodProdSer = config.CodProdSer;
                    lMovimiento.aPrecio = config.Precio;
                    lMovimiento.aUnidades = config.Unidades;

                    int idMovto = 0;

                    lError = SDK.fAltaMovimiento(idDocto, ref idMovto, ref lMovimiento);
                    if (lError != 0)
                    {
                        log.WriteEntry($"Error al generar Movimiento: {SDK.rError(lError)}");
                    }
                    else
                    {
                        log.WriteEntry($"Movimiento Generado y asignado Exitosamente, id: {idMovto}");
                    }
                    #endregion
                    log.WriteEntry($"Documento Generado Exitosamente: id doc: {lDocto.aFolio}");
                    //posicionar puntero en campo objetivo para actualizar los campos
                    lError = SDK.fPosUltimoDocumento();//probablemente es mejor buscarlo por su identificador, pero asi no deberia haber bronca
                    if (lError != 0)
                    {
                        log.WriteEntry($"No se encontro el documento para actualizar las tablas con los datos proporcionados de Observaciones: {SDK.rError(lError)}");
                    }
                    else
                    {
                        SDK.fEditarDocumento();//may need to add another filter here just in case
                        lError = SDK.fSetDatoDocumento("cObservaciones", req.Observaciones);
                        if (lError != 0)
                        {
                            log.WriteEntry($"Error Actualizando el campo: Observaciones: {SDK.rError(lError)}");
                            SDK.fCancelarModificacionDocumento();
                        }
                        else
                        {
                            lError = SDK.fSetDatoDocumento("cTextoExtra1", req.cTextoExtra1);
                            if (lError != 0)
                            {
                                log.WriteEntry($"Error al modificar cTextoExtra1: {SDK.rError(lError)}");

                            }
                            else
                            {
                                SDK.fSetDatoDocumento("cTextoExtra2", req.cTextoExtra2);
                                SDK.fSetDatoDocumento("cTextoExtra3", req.cTextoExtra3);

                                lError = SDK.fGuardaDocumento();
                                if (lError != 0)
                                {
                                    log.WriteEntry($"Hubo un error al guardar los cambios: {SDK.rError(lError)}");
                                }
                                else
                                {
                                    log.WriteEntry("Campos extras actualizados correctamente");
                                }
                            }

                        }
                    }
                    SDK.fCierraEmpresa();
                    return lDocto;
                }
            }
            #endregion
            SDK.fCierraEmpresa();
            return lDocto;
            
        }
        catch (JsonSerializationException jsonEx)
        {
            log.WriteEntry($"Error de serialización JSON: {jsonEx.Message}");
            lDocto.aFolio = 0;
            return lDocto;
        }
        catch (DivideByZeroException divEx)
        {
            log.WriteEntry($"Error de división por cero: {divEx.Message}");
            return lDocto;
        }
        catch (Exception ex)
        {
            log.WriteEntry($"Ocurrió un error general: {ex.Message}");
            SDK.fCierraEmpresa();
            return lDocto;
        }

        #endregion

    }
    #endregion

    public void Stop()
    {
        interrupt = true;
        serverThread.Join();
    }
}
