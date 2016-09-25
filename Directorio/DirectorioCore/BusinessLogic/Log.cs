﻿using System;
using System.Configuration;
using System.IO;

namespace DirectorioCore.BusinessLogic
{
    public class Log
    {
        public static void EscribeLog(String Mensaje)
        {
            try
            {
                string LogDirectory = ConfigurationManager.AppSettings["LogDirectory"].ToString();
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);
                DateTime Fecha = DateTime.Now;
                string content = "[" + Fecha.ToString("yyyy/MM/dd HH:mm:ss") + "]" + " " + Mensaje;
                string ArchivoLog = LogDirectory + Fecha.ToShortDateString().Replace("/", "-") + ".txt";
                using (StreamWriter file = new System.IO.StreamWriter(ArchivoLog, true))
                {
                    file.WriteLine(content);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error en la escritura de la bitácora: " + exc.Message);
            }
        }
    }
}
