﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKService.Models
{
    public class Response
    {
        public Response() 
        {
            ResponseCode = 0;
        }
        public double ResponseCode { get; set; }

        public string ResponseContent { get; set; }
    }
}
