using System;
using System.Collections.Generic;

namespace Practica_Elmer.DB.Entities
{
    public partial class Historico
    {
        public int Id { get; set; }
        public int? ProductoId { get; set; }
        public int? SucursalId { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime? FechaVenta { get; set; }

        public Producto producto { get; set; }
    }
}
