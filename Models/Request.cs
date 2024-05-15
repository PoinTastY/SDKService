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
        public string SerialEntity1 { get; set; }
        public string SerialEntity2 { get; set; }
        public string SerialEntity3 {  get; set; }
        public string SerialEntity4 {  get; set; }
        public string SerialEntity5 { get; set; }
        public string SerialEntity6 { get; set; }
        public string RawRequest {  get; set; }
    }
}
