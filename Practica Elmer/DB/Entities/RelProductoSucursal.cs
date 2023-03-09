using System;
using System.Collections.Generic;

namespace Practica_Elmer.DB.Entities
{
    public partial class RelProductoSucursal
    {
        public int Id { get; set; }
        public int? ProductoId { get; set; }
        public int? SucursalId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public virtual Producto? Producto { get; set; }
        public virtual Sucursale? Sucursal { get; set; }
    }
}
