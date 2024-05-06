using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKService
{
    public class SDK
    {
        #region Funciones Generales
        
        [DllImport("KERNEL32")]
        public static extern int SetCurrentDirectory(string pPtrDirActual);

        [DllImport("MGWServicios.dll")]
        public static extern int fSetNombrePAQ(String aNombrePAQ);

        [DllImport("MGWServicios.dll")]
        public static extern void fTerminaSDK();

        [DllImport("MGWServicios.dll")]
        public static extern void fError(int NumerError, StringBuilder Mensaje, int Longitud);

        
        #endregion//////////////////////////////////////

        #region Funciones de Empresa
        [DllImport("MGWServicios.dll")]
        public static extern int fAbreEmpresa(string Directorio);

        [DllImport("MGWServicios.dll")]
        public static extern void fCierraEmpresa();

        [DllImport("MGWServicios.dll")]
        public static extern void fInicioSesionSDK(string usuario, string pass);

        [DllImport("MGWServicios.dll")]
        public static extern int fPosPrimerEmpresa(ref int idEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirEmpresa); 
        
        [DllImport("MGWServicios.dll")]
        public static extern int fPosSiguienteEmpresa(ref int idEmpresa, StringBuilder aNombreEmpresa, StringBuilder aDirEmpresa);

        #endregion

        #region Manejo de Documentos

        [DllImport("MGWServicios.dll")]
        public static extern int fSiguienteFolio(string codConcepto, StringBuilder serie, ref double folio);

        [DllImport("MGWServicios.dll")]
        public static extern int fAltaDocumento(ref int idDocumento, ref tDocumento atDocumento);

        [DllImport("MGWServicios.dll")]
        public static extern int fAltaMovimiento(int aIdDocumento, ref int aIdMovimiento, ref tMovimiento astMovimiento);
        #endregion

        //Manejo de Errores
        public static string rError(int iError)
        {
            StringBuilder msj = new StringBuilder(512);
            if(iError != 0)
            {
                fError(iError, msj, 512);
                Console.WriteLine("Numero de error: " + iError.ToString() + "Error: " + msj.ToString());
            }
            return msj.ToString();
        }

    }
}
