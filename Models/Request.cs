using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKService.Models
{
    public class Request
    {
        public Request() 
        {
            Work = 2;
        }
        public int Work { get; set; }
        public string SerialDocto { get; set; }
        public string Observaciones { get; set; }
        public string cTextoExtra1 { get; set; }
        public string cTextoExtra2 { get; set; }
        public string cTextoExtra3 { get; set; }
    }
}
