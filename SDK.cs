using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SDKService
{
    public class SDK
    {

        #region Constantes
        public class constantes
        {
            public const int kLongFecha = 24;
            public const int kLongSerie = 12;
            public const int kLongCodigo = 31;
            public const int kLongNomre = 61;
            public const int kLongReferencia = 21;
            public const int kLongDescripcion = 61;
            public const int kLongCuenta = 101;
            public const int kLongMensaje = 3001;
            public const int kLongNombreProducto = 256;
            public const int kLongAbreviatura = 4;
            public const int kLongCodValorCasif = 4;
            public const int kLongDenComercial = 51;
            public const int kLongRepLegal = 51;
            public const int kLongTextoExtra = 51;
            public const int kLongRFC = 21;
            public const int kLongCURP = 21;
            public const int kLongDesCorta = 21;
            public const int kLongNumeroExtInt = 7;
            public const int kLongNumeroExpandido = 31;
            public const int kLongCodigoPostal = 7;
            public const int kLongTelefono = 16;
            public const int kLongEmailWeb = 51;
            public const int kLongSelloSat = 691;
            public const int kSDKLonSerieCertSAT = 190;
            public const int kLongFechaHora = 36;
            public const int kLongSelloCFDI = 691;
            public const int kSDKLongitudUUID = 206;
            public const int kLongitudRegimen = 101;
            public const int kLongitudMoneda = 61;
            public const int kLongitudFolio = 17;
            public const int kLongitudMonto = 31;
            public const int kLongitudLugarExpedicion = 401;
            public const int kLongitudNomBanExtrangero = 255;
        }
        #endregion

        #region Estructuras

        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tDocumento
        {
            public double aFolio;
            public int aNumMoneda;
            public double aTipoCambio;
            public double aImporte;
            public double aDescuentoDoc1;
            public double aDescuentoDoc2;
            public int aSistemaOrigen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodConcepto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongSerie)]
            public string aSerie;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string aFecha;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodigoCteProv;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodigoAgente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongReferencia)]
            public string aReferencia;
            public int aAfecta;
            public double aGasto1;
            public double aGasto2;
            public double aGasto3;

        }

        [StructLayout (LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tMovimiento
        {
            public int aConsecutivo;
            public double aUnidades;
            public double aPrecio;
            public double aCosto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodProdSer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodAlmacen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongReferencia)]
            public string aReferencia;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodClasificacion;

        }

        #endregion

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
