using DirectorioCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Response
{
    public class ConsultaEmpleadosResponse
    {
        public ConsultaEmpleadosResponse()
        {
            Empleados = new List<Empleado>();
        }
        public List<Empleado> Empleados { get; set; }
    }
}
