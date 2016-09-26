using DirectorioCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Request
{
    public class AgregarEmpleadoRequest
    {
        public AgregarEmpleadoRequest()
        {
            Token = string.Empty;
            Entidad = new Empleado();
        }
        public string Token { get; set; }
        public Empleado Entidad { get; set; } 
    }
}
