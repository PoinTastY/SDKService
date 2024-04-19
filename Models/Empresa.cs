using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKService.Models
{
    public class Empresa
    {
        private int _IdEmpresa;
        private StringBuilder _nombreEmpresa = new StringBuilder(255);
        private StringBuilder _dirEmpresa = new StringBuilder(255);

        public int PrimerEmpresa()
        {
            int lError = SDK.fPosPrimerEmpresa(ref _IdEmpresa, _nombreEmpresa, _dirEmpresa);
            return lError;
        }

        public int IdEmpresa {  get { return _IdEmpresa; } }
        public String NombreEmpresa { get { return _nombreEmpresa.ToString();  } }
        public String DirEmpresa { get { return _nombreEmpresa.ToString();  } }
    }
}
