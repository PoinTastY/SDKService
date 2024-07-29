using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SDKService.Models
{
    public class Config
    {
        public string RutaBinarios {  get; set; }
        public string NombrePAQ { get; set; }
        public string RutaEmpresa { get; set; }
        public string User {  get; set; }
        public string Password { get; set; }
        public string CodAlmacen {  get; set; }
        public string CodProdSer {  get; set; }
        public double Precio {  get; set; }
        public double Unidades {  get; set; }
    }
}
