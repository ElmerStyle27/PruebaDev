using MediatR;
using Practica_Elmer.DB.DTO;
using Practica_Elmer.DB.Entities;

namespace Practica_Elmer.DB.Command
{
    public class InsertarVenta
    {
        //command

        public record Command(List<VentaDTO> Historico) : IRequest<Response>;

        //Handler

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ApplicationDbContext _context;
            public Handler(ApplicationDbContext dbContext)
            {
                _context = dbContext;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {

                bool productoSinStock = false;

                List<Historico> lstHistorico = new List<Historico>();

                foreach (var i in request.Historico)
                {
                    var prod = _context.RelProductoSucursals.FirstOrDefault(p => p.ProductoId == i.ProductoId && p.SucursalId == i.sucursalId);

                    if (prod != null)
                    {
                        if (prod.Cantidad > i.cantidad)
                        {
                            DateTime fecha = DateTime.Now;

                            Historico objVenta = new()
                            {
                                ProductoId = i.ProductoId,
                                SucursalId = i.sucursalId,
                                Cantidad = i.cantidad,
                                TotalPagado = i.costo * i.cantidad,
                                FechaVenta = fecha
                            };
                            lstHistorico.Add(objVenta);

                            prod.Cantidad -= i.cantidad;
                            _context.SaveChanges();

                        }
                        else
                        {
                            productoSinStock = true;
                        }
                    }
                }

                _context.Historicos.AddRange(lstHistorico);
                _context.SaveChanges();

                bool result = (productoSinStock == false) ? true : false;

                return new Response(result);
            }
        }

        //Respuesta

        public record Response(bool Historico);

    }
}
