using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica_Elmer.DB.Entities;
using static Practica_Elmer.DB.Command.InsertarVenta;

namespace Practica_Elmer.DB.Querys
{
    public class GetSucursal
    {
        //Command
        public record ById(int Id) : IRequest<Response>;
        //Handler
        public class Handler : IRequestHandler<ById, Response>
        {
            private readonly ApplicationDbContext _context;
            public Handler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<Response> Handle(ById request, CancellationToken cancellationToken)
            {
                var sucursal = _context.Sucursales
                    .Include(x => x.RelProductoSucursals)
                    .ThenInclude(x => x.Producto)
                    .Where(x => x.Id == request.Id).FirstOrDefault();

                return new Response(sucursal);
            }
        }

        //Respuesta

        public record Response(Sucursale Sucursale);
    }
}
