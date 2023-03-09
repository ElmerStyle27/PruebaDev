using MediatR;
using Microsoft.AspNetCore.Mvc;
using Practica_Elmer.DB.Command;
using Practica_Elmer.DB.DTO;
using Practica_Elmer.DB.Entities;
using Practica_Elmer.DB.Querys;
using static Practica_Elmer.DB.Command.CommandProductos;

namespace Practica_Elmer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : Controller, IGenericMethods<Producto>
    {
        private readonly IMediator _mediator;
        public ProductosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Petición para actualizar un producto
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Actualizar(Producto modelo)
        {
            var response = _mediator.Send(new ActualizarProductoCommand(modelo)).GetAwaiter().GetResult();
            var result = response.Producto;
            return Ok(result);
        }
        /// <summary>
        /// Peticion para eliminar un producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("EliminarProducto/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = _mediator.Send(new EliminarProductoCommand(id)).GetAwaiter().GetResult();
            var resultado = response.result;
            return Ok(resultado);
        }
        /// <summary>
        /// Inserta un producto
        /// </summary>
        /// <param name="modelo">The modelo.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insertar(Producto modelo)
        {
            var response = _mediator.Send(new CommandProductos.Command(modelo)).GetAwaiter().GetResult();
            var prod = response.Producto;

            if (prod.Id != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        /// <summary>
        /// Petición para obtener un producto por su nombre
        /// </summary>
        /// <param name="nombreProducto"></param>
        /// <returns></returns>
        [HttpGet("Producto/{nombreProducto}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombreProducto)
        {
            var response = _mediator.Send(new QueryProductos.ObtenerPorNombre(nombreProducto)).GetAwaiter().GetResult();
            var producto = response.Producto;

            return Ok(producto);
        }
        /// <summary>
        /// Petición para obtener un producto por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{ProductoId}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = _mediator.Send(new QueryProductos.ObtenerPorId(id)).GetAwaiter().GetResult();
            var producto = response.Producto;

            return Ok(producto);
        }

        /// <summary>
        /// Petición para obtener todos los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObtenerTodos")]
        public async Task<List<Producto>> ObtenerTodos()
        {
            var response = _mediator.Send(new QueryProductos.ObtenerTodos()).GetAwaiter().GetResult();
            var producto = response.Producto;

            return (producto);
        }

        /// <summary>
        /// Petición para crear un producto
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("CrearProducto")]
        public async Task<IActionResult> CrearProducto(ProductoDTO modelo)
        {
            var response = _mediator.Send(new CommandProductos.CrearProductoCommand(modelo)).GetAwaiter().GetResult();
            var prod = response.result;
            return Ok(prod);
        }

    }
}
