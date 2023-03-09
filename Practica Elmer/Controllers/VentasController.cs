using MediatR;
using Microsoft.AspNetCore.Mvc;
using Practica_Elmer.DB.Command;
using Practica_Elmer.DB.DTO;
using Practica_Elmer.DB.Entities;
using Practica_Elmer.DB.Querys;

namespace Practica_Elmer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private readonly IMediator _mediator;
        public VentasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Petición para crear una venta
        /// </summary>
        /// <param name="carrito"></param>
        /// <returns></returns>
        [HttpPost("CrearVenta")]
        public IActionResult Insert([FromBody] List<VentaDTO> carrito)
        {
            var response = _mediator.Send(new InsertarVenta.Command(carrito)).GetAwaiter().GetResult();
            var historicoInsertado = response.Historico;
            return Json(new { historicoInsertado });
        }

        /// <summary>
        /// Obtiene la sucursal por su id
        /// </summary>
        /// <param name="SucursalId"></param>
        /// <returns></returns>
        [HttpGet("{SucursalId}")]
        public IActionResult Get(int SucursalId)
        {
            var response = _mediator.Send(new GetSucursal.ById(SucursalId)).GetAwaiter().GetResult();
            var sucursal = response.Sucursale;

            return Ok(sucursal);
        }

        /// <summary>
        /// Devuelve el historico de ventas
        /// </summary>
        /// <returns></returns>
        [HttpGet("HistoricoVentas")]
        public IActionResult Get()
        {
            var response = _mediator.Send(new QueryVentas.ObtenerTodos()).GetAwaiter().GetResult();
            var ventas = response.Historicos;
            return Ok(ventas);
        }

        /// <summary>
        /// Devuelve el historico de ventas por id
        /// </summary>
        /// <param name="SucursalId"></param>
        /// <returns></returns>
        [HttpGet("HistoricoVentas/{SucursalId}")]
        public IActionResult GetHistorico(int SucursalId)
        {
            var response = _mediator.Send(new QueryVentas.ObtenerPorSucursal(SucursalId)).GetAwaiter().GetResult();
            var ventas = response.Historicos;
            return Ok(ventas);
        }

    }
}
