using MediatR;
using Practica_Elmer.DB.DTO;
using Practica_Elmer.DB.Entities;

namespace Practica_Elmer.DB.Command
{
    public class CommandProductos
    {
        //command
        public record Command(Producto Producto) : IRequest<Response>;

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
                _context.Productos.Add(request.Producto);
                _context.SaveChanges();
                return new Response(request.Producto);
            }
        }



        public record ActualizarProductoCommand(Producto Product) : IRequest<Response>;

        //Handler
        public class ActualizarHandler : IRequestHandler<ActualizarProductoCommand, Response>
        {
            private readonly ApplicationDbContext _context;
            public ActualizarHandler(ApplicationDbContext dbContext)
            {
                _context = dbContext;
            }
            public async Task<Response> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
            {
                var producto = _context.Productos.FirstOrDefault(x => x.Id == request.Product.Id);

                if (producto != null)
                {
                    producto.Nombre = request.Product.Nombre;
                    producto.CodigoBarras = request.Product.CodigoBarras;
                    producto.Activo = request.Product.Activo;
                    producto.RelProductoSucursals = request.Product.RelProductoSucursals;

                    _context.SaveChanges();
                    return new Response(producto);
                }



                return new Response(request.Product);
            }
        }


        //Respuesta
        public record Response(Producto Producto);




        //command
        public record EliminarProductoCommand(int ProductoId) : IRequest<ResponseEliminar>;

        //Handler
        public class EliminarHandler : IRequestHandler<EliminarProductoCommand, ResponseEliminar>
        {
            private readonly ApplicationDbContext _context;
            public EliminarHandler(ApplicationDbContext dbContext)
            {
                _context = dbContext;
            }
            public async Task<ResponseEliminar> Handle(EliminarProductoCommand request, CancellationToken cancellationToken)
            {

                var EliminarProducto = _context.Productos.FirstOrDefault(x => x.Id == request.ProductoId);
                if (EliminarProducto != null)
                {
                    EliminarProducto.Activo = false;
                    _context.SaveChanges();
                    return new ResponseEliminar(true);
                }

                return new ResponseEliminar(false);

            }
        }


        //Respuesta
        public record ResponseEliminar(bool result);




        //command
        public record CrearProductoCommand(ProductoDTO Producto) : IRequest<ResponseInsertar>;
        //Handler
        public class CrearProductoHandler : IRequestHandler<CrearProductoCommand, ResponseInsertar>
        {
            private readonly ApplicationDbContext _context;
            public CrearProductoHandler(ApplicationDbContext dbContext)
            {
                _context = dbContext;
            }
            public async Task<ResponseInsertar> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
            {

                var sucursal = _context.Sucursales.FirstOrDefault(x => x.Id == request.Producto.SucursalId);

                if (sucursal != null)
                {
                    RelProductoSucursal r = new RelProductoSucursal();
                    r.SucursalId = sucursal.Id;
                    r.Cantidad = request.Producto.Cantidad;
                    r.PrecioUnitario = request.Producto.PrecioUnitario;


                    Producto pro = new Producto();
                    pro.Nombre = request.Producto.Nombre;
                    pro.CodigoBarras = request.Producto.CodigoBarras;
                    pro.EmpresaId = sucursal.EmpresaId;
                    pro.Activo = true;
                    pro.RelProductoSucursals.Add(r);

                    _context.Add(pro);
                    _context.SaveChanges();
                };


                return new ResponseInsertar(true);
            }
        }
        //Respuesta
        public record ResponseInsertar(bool result);

    }
}
