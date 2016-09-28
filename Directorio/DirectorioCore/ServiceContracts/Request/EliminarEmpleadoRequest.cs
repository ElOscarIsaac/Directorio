using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Request
{
    public class EliminarEmpleadoRequest
    {
        public EliminarEmpleadoRequest()
        {
            Token = string.Empty;
            EmpleadoId = 0;
        }
        public string Token { get; set; }
        public int EmpleadoId { get; set; }
    }
}
