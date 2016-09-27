using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Response
{
    public class AgregarEmpleadoResponse
    {
        public AgregarEmpleadoResponse()
        {
            Message = string.Empty;
            Success = false;
        }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
