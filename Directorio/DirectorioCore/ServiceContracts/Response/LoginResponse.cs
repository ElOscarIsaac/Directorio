using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Response
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            Id = 0;
            Nombre = string.Empty;
            Token = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Token { get; set; }
    }
}
