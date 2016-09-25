using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Id = 0;
            NombreEmpleado = string.Empty;
            NombreUsuario = string.Empty;
            Password = string.Empty;
            CorreoElectronico = string.Empty;
            FechaRegistro = new DateTime();
            Activo = false;
        }
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}
