using System;
using System.Collections.Generic;

namespace Practica_Elmer.DB.Entities
{
    public partial class Empresa
    {
        public Empresa()
        {
            Productos = new HashSet<Producto>();
            Sucursales = new HashSet<Sucursale>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public bool? Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Sucursale> Sucursales { get; set; }
    }
}
