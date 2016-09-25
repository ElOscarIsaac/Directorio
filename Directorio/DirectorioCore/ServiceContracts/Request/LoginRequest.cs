using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Request
{
    public class LoginRequest
    {
        public LoginRequest()
        {
            usuario = string.Empty;
            password = string.Empty;
        }
        public string usuario { get; set; }
        public string password { get; set; }
    }
}
