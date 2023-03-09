using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica_Elmer.DB.Entities;

namespace Practica_Elmer.DB.Querys
{
    public class QueryVentas
    {
        //Command
        public record ObtenerTodos() : IRequest<ResponseObtenerTodos>;
        //Handler
        public class ObtenerTodosHandler : IRequestHandler<ObtenerTodos, ResponseObtenerTodos>
        {
            private readonly ApplicationDbContext _context;
            public ObtenerTodosHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<ResponseObtenerTodos> Handle(ObtenerTodos request, CancellationToken cancellationToken)
            {
                var HistoricoVentas = _context.Historicos.ToList();

                return new ResponseObtenerTodos(HistoricoVentas);
            }
        }


        //Command
        public record ObtenerPorSucursal(int SucursalId) : IRequest<ResponseObtenerTodos>;
        //Handler
        public class ObtenerPorSucursalHandler : IRequestHandler<ObtenerPorSucursal, ResponseObtenerTodos>
        {
            private readonly ApplicationDbContext _context;
            public ObtenerPorSucursalHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<ResponseObtenerTodos> Handle(ObtenerPorSucursal request, CancellationToken cancellationToken)
            {
                var HistoricoVentas = _context.Historicos.Where(x => x.SucursalId == request.SucursalId).ToList();

                return new ResponseObtenerTodos(HistoricoVentas);
            }
        }

        //Respuesta
        public record ResponseObtenerTodos(List<Historico> Historicos);
    }
}
