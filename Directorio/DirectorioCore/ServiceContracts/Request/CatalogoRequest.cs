using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Request
{
    public class CatalogoRequest
    {
        public CatalogoRequest()
        {
            Token = string.Empty;
        }
        public string Token { get; set; }
    }
}
