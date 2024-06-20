using GamarraPlus.Models;
using TiaVeneno_API.Repositorio.DAO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;

namespace TiaVeneno_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {


        private readonly VentaDAO _ventaDAO;

        public VentaController()
        {
            _ventaDAO = new VentaDAO();
        }


        [HttpPost("RegistrarVenta")]
        public async Task<IActionResult> RegistrarVenta(Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _ventaDAO.RegistrarVenta(venta));
            return Ok(mensaje);
        }


        [HttpGet("VerDetalleVenta/{numeroDocumento}")]
        public async Task<IActionResult> DetalleVenta(string numeroDocumento)
        {
            var detalle = await Task.Run(() => _ventaDAO.VerDetalleVenta(numeroDocumento));
            if (detalle == null)
            {
                return NotFound();
            }
            return Ok(detalle);
        }



    }
}

