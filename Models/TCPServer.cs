using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Threading;
using SDKService.Models;
using Newtonsoft.Json;

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

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                eventLog.WriteEntry("Mensaje recibido: " + request, EventLogEntryType.Information);

                // Procesar el mensaje recibido
                string response = JsonConvert.SerializeObject(GetEmpresas(eventLog));

                // Enviar la respuesta al cliente
                byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
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
    public static Empresa GetEmpresas(EventLog log)
    {
        var empresa = new Empresa();

        if (empresa.SiguienteEmpresa() != 0)
        {
            log.WriteEntry("Hubo un error seleccionando la siguiente empresa");
        }
        else
        {
            log.WriteEntry($"Siguiente Empresa: {empresa.IdEmpresa}, {empresa.NombreEmpresa}, {empresa.DirEmpresa}");
        }
        return empresa;
    }
    #endregion

    public void Stop()
    {
        listener.Stop();
    }
}
