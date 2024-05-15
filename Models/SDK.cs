using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SDKService.Models
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

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct ClienteProveedor
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoCliente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cRazonSocial;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string cFechaAlta;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongRFC)]
            public string cRFC;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCURP)]
            public string cCURP;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongDenComercial)]
            public string cDenComercial;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongRepLegal)]
            public string cRepLegal;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cNombreMoneda;
            public int cListaPreciosCliente;
            public double cDescuentoMovto;
            public int cBanVentaCredito;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente5;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente6;
            public int cTipoCliente;
            public int cEstatus;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string cFechaBaja;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string cFechaUltimaRevision;
            public double cLimiteCreditoCliente;
            public int cDiasCreditoCliente;
            public int cBanExcederCredito;
            public double cDescuentoProntoPago;
            public int cDiasProntoPago;
            public double cInteresMoratorio;
            public int cDiaPago;
            public int cDiasRevision;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongDesCorta)]
            public string cMensajeria;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongDescripcion)]
            public string cCuentaMensajeria;
            public int cDiasEmbarqueCliente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoAlmacen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoAgenteVenta;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoAgenteCobro;
            public int cRestriccionAgente;
            public double cImpuesto1;
            public double cImpuesto2;
            public double cImpuesto3;
            public double cRetencionCliente1;
            public double cRetencionCliente2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionProveedor1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionProveedor2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionProveedor3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionProveedor4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionProveedor5;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionProveedor6;
            public double cLimiteCreditoProveedor;
            public int cDiasCreditoProveedor;
            public int cTiempoEntrega;
            public int cDiasEmbarqueProveedor;
            public double cImpuestoProveedor1;
            public double cImpuestoProveedor2;
            public double cImpuestoProveedor3;
            public double cRetencionProveedor1;
            public double cRetencionProveedor2;
            public int cBanInteresMoratorio;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            public string cTextoExtra1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            public string cTextoExtra2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            public string cTextoExtra3;
            public double cImporteExtra1;
            public double cImporteExtra2;
            public double cImporteExtra3;
            public double cImporteExtra4;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct Producto
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoProducto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cNombreProducto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNombreProducto)]
            public string cDescripcionProducto;
            public int cTipoProducto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string cFechaAltaProducto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string cFechaBaja;
            public int cStatusProducto;
            public int cControlExistencia;
            public int cMetodoCosteo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoUnidadBase;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoUnidadNoConvertible;
            public double cPrecio1;
            public double cPrecio2;
            public double cPrecio3;
            public double cPrecio4;
            public double cPrecio5;
            public double cPrecio6;
            public double cPrecio7;
            public double cPrecio8;
            public double cPrecio9;
            public double cPrecio10;
            public double cImpuesto1;
            public double cImpuesto2;
            public double cImpuesto3;
            public double cRetencion1;
            public double cRetencion2;
            // N.D.8386 La estructura debe recibir el nombre de la caracter�stica padre. (ALRH)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cNombreCaracteristica1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cNomreCaracteristica2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cNomreCaracteristica3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacion1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacion2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacion3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacion4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacion5;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacion6;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            public string cTextoExtra1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            public string cTextoExtra2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            public string cTextoExtra3;
            public double cImporteExtra1;
            public double cImporteExtra2;
            public double cImporteExtra3;
            public double cImporteExtra4;
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

        //entrega el folio consecutivo para el concepto seleccionado
        [DllImport("MGWServicios.dll")]
        public static extern int fSiguienteFolio(string codConcepto, StringBuilder serie, ref double folio);

        [DllImport("MGWServicios.dll")]
        public static extern int fAltaDocumento(ref int idDocumento, ref tDocumento atDocumento);

        
        //        // Campos de la tablas de Documentos
        //#define kDocumento_IdDocumento              "cIdDocumento"
        //#define kDocumento_CodigoConcepto           "cCodigoConcepto"
        //#define kDocumento_Serie                    "cSerieDocumento"
        //#define kDocumento_Folio                    "cFolio"
        //#define kDocumento_Fecha                    "cFecha"
        //#define kDocumento_CodigoCteProv            "cCodigoCteProv"
        //#define kDocumento_RazonSocial              "cRazonSocial"
        //#define kDocumento_RFC                      "cRFC"
        //#define kDocumento_SerieOmision             "cSeriePorOmision"
        //#define kDocumento_CodigoAgente             "cCodigoAgente"
        //#define kDocumento_FechaVencimiento         "cFechaVencimiento"
        //#define kDocumento_FechaEntRecep            "cFechaEntregaRecepcion"
        //#define kDocumento_FechaProntoPago          "cFechaProntoPago"
        //#define kDocumento_FechaUltimoInteres       "cFechaUltimoInteres"
        //#define kDocumento_IdMoneda                 "cidMoneda"
        //#define kDocumento_TipoCambio               "cTipoCambio"
        //#define kDocumento_Referencia               "cReferencia"
        //#define kDocumento_Importe                  "cImporte"
        //#define kDocumento_Descuento1               "cDescuentoDoc1"
        //#define kDocumento_Descuento2               "cDescuentoDoc2"
        //#define kDocumento_DescProntoPago           "cDescuentoProntoPago"
        //#define kDocumento_InteresMoratorio         "cPorcentajeInteres"
        //#define kDocumento_SisOrigen                "cSistOrig"
        //#define kDocumento_Observaciones            "cObservaciones"
        //#define kDocumento_ConDireccionFiscal       "cConDireccionFiscal"
        //#define kDocumento_ConDireccionEnvio        "cConDireccionEnvio"
        //#define kDocumento_Gasto1                   "cGasto1"
        //#define kDocumento_Gasto2                   "cGasto2"
        //#define kDocumento_Gasto3                   "cGasto3"

        [DllImport("MGWServicios.dll")]
        public static extern int fSetDatoDocumento(string aCampo,string aValor);

        [DllImport("MGWServicios.dll")]
        public static extern int fSLeeDatoDocumento(string aCampo,StringBuilder aValor, int aLen);

        [DllImport("MGWServicios.dll")]
        public static extern int fFiltroDocumento(StringBuilder aFechaInicio, StringBuilder aFechaFin, StringBuilder aCodigoConcepto, StringBuilder aCodigoCteProv);

        [DllImport("MGWServicios.dll")]
        public static extern int fCancelaFiltroDocumento();

        [DllImport("MGWServicios.dll")]
        public static extern int fInsertarDocumento();

        [DllImport("MGWServicios.dll")]
        public static extern int fEditarDocumento();

        [DllImport("MGWServicios.dll")]
        public static extern int fGuardaDocumento();

        [DllImport("MGWServicios.dll")]
        public static extern int fCancelaDocumento();

        [DllImport("MGWServicios.dll")]
        public static extern int fCancelarModificacionDocumento();

        [DllImport("MGWServicios.dll")]
        public static extern int fBuscarIdDocumento(int aIdDocumento);//Busca

        [DllImport("MGWServicios.dll")]
        public static extern int fPosUltimoDocumento();


        #endregion

        #region Manejo de Movimientos
        [DllImport("MGWServicios.dll")]
        public static extern int fAltaMovimiento(int aIdDocumento, ref int aIdMovimiento, ref tMovimiento astMovimiento);
        #endregion

        #region Manejo de Clientes

        [DllImport("MGWServicios.dll")]
        public static extern int fBuscaIdCteProv(int aIdCteProv);

        [DllImport("MGWServicios.dll")]
        public static extern int fBuscaCteProv(string aBuscaCteProv);

        [DllImport("MGWServicios.dll")]
        public static extern int fAltaCteProv(ref int aIdCteProv, ClienteProveedor astCteProv);

        #endregion


        //  //Manejo de Errores
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
