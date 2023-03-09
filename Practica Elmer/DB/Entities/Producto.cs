using System;
using System.Collections.Generic;

namespace Practica_Elmer.DB.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            RelProductoSucursals = new HashSet<RelProductoSucursal>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CodigoBarras { get; set; }
        public int? EmpresaId { get; set; }
        public bool? Activo { get; set; }

        public virtual Empresa? Empresa { get; set; }
        public virtual ICollection<RelProductoSucursal> RelProductoSucursals { get; set; }
    }
}
