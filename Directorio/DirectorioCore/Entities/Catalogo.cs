using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.Entities
{
    public class Catalogo
    {
        public Catalogo()
        {
            id = 0;
            codigo = "";
            descripcion = "";
        }
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}
