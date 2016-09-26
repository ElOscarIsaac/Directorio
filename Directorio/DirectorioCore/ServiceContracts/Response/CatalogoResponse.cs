using DirectorioCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.ServiceContracts.Response
{
    public class CatalogoResponse
    {
        public CatalogoResponse()
        {
            Lista = new List<Catalogo>();
        }
        public List<Catalogo> Lista { get; set; }
    }
}
