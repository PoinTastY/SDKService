﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using SDKService.Models;
using System.Text;
using System.Threading.Tasks;

namespace SDKService
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SDKInstances()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
