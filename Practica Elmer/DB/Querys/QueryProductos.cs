using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica_Elmer.DB.Entities;
using static Practica_Elmer.DB.Querys.QueryProductos.ObtenerTodosHandler;

namespace Practica_Elmer.DB.Querys
{
    public class QueryProductos
    {
        //Command
        public record ObtenerPorNombre(string Nombre) : IRequest<Response>;
        //Handler
        public class ObtenerPorNombreHandler : IRequestHandler<ObtenerPorNombre, Response>
        {
            private readonly ApplicationDbContext _context;
            public ObtenerPorNombreHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<Response> Handle(ObtenerPorNombre request, CancellationToken cancellationToken)
            {
                var Produc = _context.Productos
                    .Include(x => x.RelProductoSucursals)
                    .Where(x => x.Nombre.Contains(request.Nombre)).FirstOrDefault();

                return new Response(Produc);
            }

        }


        public record ObtenerPorId(int Id) : IRequest<Response>;
        //Handler
        public class ObtenerPorIdHandler : IRequestHandler<ObtenerPorId, Response>
        {
            private readonly ApplicationDbContext _context;
            public ObtenerPorIdHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<Response> Handle(ObtenerPorId request, CancellationToken cancellationToken)
            {
                var Produc = _context.Productos.Include(x => x.RelProductoSucursals).Where(x => x.Id == request.Id).FirstOrDefault();

                return new Response(Produc);
            }


        }

        //Respuesta
        public record Response(Producto Producto);


        public record ObtenerTodos() : IRequest<ResponseTodos>;
        //Handler
        public class ObtenerTodosHandler : IRequestHandler<ObtenerTodos, ResponseTodos>
        {
            private readonly ApplicationDbContext _context;
            public ObtenerTodosHandler(ApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<ResponseTodos> Handle(ObtenerTodos request, CancellationToken cancellationToken)
            {
                var Productos = _context.Productos.ToList();
                return new ResponseTodos(Productos);
            }

            //Respuesta
            public record ResponseTodos(List<Producto> Producto);

        }
    }
}
