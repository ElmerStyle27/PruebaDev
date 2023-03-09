using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica_Elmer.DB.Entities;

namespace Practica_Elmer.DB.Querys
{
    public class QueryInformacion
    {

        //Command
        public record SucursalById(int Id) : IRequest<Response>;
        //Handler
        public class Handler : IRequestHandler<SucursalById, Response>
        {
            private readonly ApplicationDbContext _context;
            public Handler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<Response> Handle(SucursalById request, CancellationToken cancellationToken)
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




        public record Sucursales() : IRequest<ResponseSucursales>;
        //Handler
        public class SucursalesHandler : IRequestHandler<Sucursales, ResponseSucursales>
        {
            private readonly ApplicationDbContext _context;
            public SucursalesHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<ResponseSucursales> Handle(Sucursales request, CancellationToken cancellationToken)
            {
                var sucursal = _context.Sucursales.ToList();

                return new ResponseSucursales(sucursal);
            }
        }
        //Respuesta
        public record ResponseSucursales(List<Sucursale> Sucursale);



        //Command
        public record HistoricoVentasSucursal(int Id) : IRequest<ResponseHistorico>;
        //Handler
        public class HistoricoVentasSucursalHandler : IRequestHandler<HistoricoVentasSucursal, ResponseHistorico>
        {
            private readonly ApplicationDbContext _context;
            public HistoricoVentasSucursalHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<ResponseHistorico> Handle(HistoricoVentasSucursal request, CancellationToken cancellationToken)
            {
                var historico = _context.Historicos
                    .Include(x => x.producto)
                    .Where(x => x.SucursalId == request.Id).ToList();

                return new ResponseHistorico(historico);
            }
        }
        //Respuesta
        public record ResponseHistorico(List<Historico> Historicos);

    }
}
