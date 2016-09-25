using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.Entities
{
    public class Empleado
    {
        public Empleado()
        {

        }
        public int Id { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }

    }
}
