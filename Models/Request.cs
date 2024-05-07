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
        public string ObjectRequest { get; set; }
        public string RawRequest {  get; set; }
    }
}
