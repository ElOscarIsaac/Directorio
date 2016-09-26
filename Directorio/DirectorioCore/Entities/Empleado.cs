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
            Id = 0;
            Nombre = string.Empty;
            Extension = string.Empty;
            Puesto = string.Empty;
            Departamento = new Catalogo();
            Division = new Catalogo();
            Ubicacion = new Catalogo();
            Correo = string.Empty;
            Celular = string.Empty;
            NumeroDirecto = string.Empty;
            Foto = string.Empty;
            Activo = false;
            AUDUSUARIO = 0;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Puesto { get; set; }
        public Catalogo Departamento { get; set; }
        public Catalogo Division { get; set; }
        public Catalogo Ubicacion { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string NumeroDirecto { get; set; }
        public string Foto { get; set; }
        public bool Activo { get; set; }
        public int AUDUSUARIO { get; set; }
    }
}
