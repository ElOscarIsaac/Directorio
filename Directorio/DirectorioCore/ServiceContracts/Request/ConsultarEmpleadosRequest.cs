using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Directorio.ServiceContracts.Request
{
    public class ConsultarEmpleadosRequest
    {
        public ConsultarEmpleadosRequest()
        {
            Empleados = new List<EmpleadoContract>();
        }
        public List<EmpleadoContract> Empleados { get; set; }
    }
    public class EmpleadoContract
    {
        public EmpleadoContract()
        {
            Nombre = string.Empty;
            Extension = string.Empty;
            Puesto = string.Empty;
            Area = string.Empty;
            Correo = string.Empty;
            Celular = string.Empty;
            Ubicacion = string.Empty;
            Division = string.Empty;
            Foto = string.Empty;
            NumeroDirecto = string.Empty;
        }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Puesto { get; set; }
        public string Area { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Ubicacion { get; set; }
        public string Division { get; set; }
        public string Foto { get; set; }
        public string NumeroDirecto { get; set; }
    }
}