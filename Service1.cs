using SDKService.Models;
using System.Diagnostics;
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
            string rutaBinarios = @"C:\Program Files (x86)\Compac\COMERCIAL";
            string nombrePAQ = "CONTPAQ I COMERCIAL";
            string rutaEmpresa = "C:\\Compac\\Empresas\\adIndex_Computacion";
            int lError = 0;
            SDK.SetCurrentDirectory(rutaBinarios);

            //si queremos iniciar sesion, debe ser antes del setnombre paq
            SDK.fInicioSesionSDK("KEVIN", "index");

            //indicar con que sistema se va a trabajar
            lError = SDK.fSetNombrePAQ(nombrePAQ);
            if (lError != 0)
            {
                SDK.rError(lError);
            }
            else
            {
                //indicar la ruta de la empresa a utilizar
                lError = SDK.fAbreEmpresa(rutaEmpresa);
                if (lError != 0)
                {
                    eventLog1.WriteEntry($"Error: {SDK.rError(lError)}");
                }
                else
                {
                    eventLog1.WriteEntry("Empresa abierta exitosamente");
                    Empresa empresa = new Empresa();
                    if (empresa.PrimerEmpresa() != 0)
                    {
                        eventLog1.WriteEntry("Hubo un error seleccionando la primer empresa");
                    }
                    else
                    {
                        eventLog1.WriteEntry($"Primer Empresa: {empresa.IdEmpresa}, {empresa.NombreEmpresa}, {empresa.DirEmpresa}");

                    }
                }
                server.Start();
                eventLog1.WriteEntry("Server should be running");

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

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
