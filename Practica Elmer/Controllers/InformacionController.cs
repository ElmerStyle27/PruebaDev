using MediatR;
using Microsoft.AspNetCore.Mvc;
using Practica_Elmer.DB.Querys;

namespace Practica_Elmer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformacionController : Controller
    {
        private readonly IMediator _mediator;
        public InformacionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Devuelve las Sucursales
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sucursales")]
        public IActionResult GetSucursales()
        {
            var response = _mediator.Send(new QueryInformacion.Sucursales()).GetAwaiter().GetResult();
            var sucursal = response.Sucursale;
            return Ok(sucursal);
        }


        /// <summary>
        /// Devuelve los productos por Sucursal
        /// </summary>
        /// <param name="SucursalId"></param>
        /// <returns></returns>
        [HttpGet("Productos/Sucursal/{SucursalId}")]
        public IActionResult GetProductos(int SucursalId)
        {
            var response = _mediator.Send(new QueryInformacion.SucursalById(SucursalId)).GetAwaiter().GetResult();
            var sucursal = response.Sucursale;

            return Ok(sucursal);
        }

        /// <summary>
        /// Devuelve el historico de ventas por Sucursal
        /// </summary>
        /// <returns></returns>
        [HttpGet("HistoricoVentas/{SucursalId}")]
        public IActionResult GetHistoricoVentas(int SucursalId)
        {
            var response = _mediator.Send(new QueryInformacion.HistoricoVentasSucursal(SucursalId)).GetAwaiter().GetResult();
            var historico = response.Historicos;
            return Ok(historico);
        }
    }
}
