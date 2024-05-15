using Newtonsoft.Json;
using SDKService.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace SDKService
{
    public partial class SDKInstances : ServiceBase
    {
        private TCPServer server;
        public SDKInstances()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("SDKInstances"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "SDKInstances", "Application");
            }
                eventLog1.Source = "SDKInstances";
                eventLog1.Log = "Application";
            server = new TCPServer(42069, eventLog1);
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Iniciando Instancias del SDK de Contpaqi");
            Config config = ReadConfig("config.json");
            int lError = 0;
            SDK.SetCurrentDirectory(config.RutaBinarios);

            //si queremos iniciar sesion, debe ser antes del setnombre paq
            SDK.fInicioSesionSDK(config.User, config.Password);

            //indicar con que sistema se va a trabajar
            lError = SDK.fSetNombrePAQ(config.NombrePAQ);
            if (lError != 0)
            {
                SDK.rError(lError);
            }
            else
            {
                //indicar la ruta de la empresa a utilizar
                lError = SDK.fAbreEmpresa(config.RutaEmpresa);
                if (lError != 0)
                {
                    eventLog1.WriteEntry($"Error: {SDK.rError(lError)}");
                }
                else
                {
                    eventLog1.WriteEntry("Empresa abierta exitosamente");
                }
                eventLog1.WriteEntry("Starting tcp server...");
                server.Start();
            }
        }


        protected override void OnStop()
        {
            eventLog1.WriteEntry("Cerrando el changarro...");

            //complementarios (igual deben estar xD)
            SDK.fCierraEmpresa();
            eventLog1.WriteEntry("Empresa cerrada");
            SDK.fTerminaSDK();
            eventLog1.WriteEntry("SDK Liberado");
            server.Stop();
            eventLog1.WriteEntry("TCPServer stopped");
        }

        private Config ReadConfig(string filePath)
        {
            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(filePath))
                {
                    eventLog1.WriteEntry($"El archivo {filePath} no existe.");
                    return null;
                }

                // Leer el contenido del archivo
                string jsonString = File.ReadAllText(filePath);

                // Verificar que el contenido no sea nulo ni vacío
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    eventLog1.WriteEntry("El archivo de configuración está vacío.");
                    return null;
                }

                // Deserializar el contenido en un objeto Config
                Config config = JsonConvert.DeserializeObject<Config>(jsonString);

                return config;
            }
            catch (JsonException jsonEx)
            {
                eventLog1.WriteEntry($"Error de formato JSON: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry($"Error al leer el archivo de configuración: {ex.Message}");
                return null;
            }
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
