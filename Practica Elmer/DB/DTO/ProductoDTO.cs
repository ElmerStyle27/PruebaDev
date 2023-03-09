namespace Practica_Elmer.DB.DTO
{
    public class ProductoDTO
    {

        public string Nombre { get; set; } = null!;
        public int CodigoBarras { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int SucursalId { get; set; }

    }
}
