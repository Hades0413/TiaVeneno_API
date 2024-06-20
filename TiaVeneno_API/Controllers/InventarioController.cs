using TiaVeneno_API.Repositorio.DAO;
using GamarraPlus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TiaVeneno_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly CategoriaDAO _categoriaDAO;
        private readonly ProductoDAO _productoDAO;

        public InventarioController()
        {
            _categoriaDAO = new CategoriaDAO();
            _productoDAO = new ProductoDAO();
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> ObtenerCategorias()
        {
            var lista = await Task.Run(() => _categoriaDAO.ObtenerCategorias());
            return Ok(lista);
        }

        [HttpGet("categorias/{id}")]
        public async Task<IActionResult> ObtenerCategoria(int id)
        {
            var categoria = await Task.Run(() => _categoriaDAO.ObtenerCategoriaPorId(id));
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost("categorias")]
        public async Task<IActionResult> RegistrarCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _categoriaDAO.RegistrarCategoria(categoria));
            return Ok(mensaje);
        }

        [HttpPut("categorias/{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _categoriaDAO.ActualizarCategoria(categoria));
            return Ok(mensaje);
        }

        [HttpDelete("categorias/{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var mensaje = await Task.Run(() => _categoriaDAO.EliminarCategoria(id));
            return Ok(mensaje);
        }

        [HttpGet("productos")]
        public async Task<IActionResult> ObtenerProductos()
        {
            var lista = await Task.Run(() => _productoDAO.ObtenerProductos());
            return Ok(lista);
        }

        [HttpGet("productos/{id}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var producto = await Task.Run(() => _productoDAO.ObtenerProductoPorId(id));
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost("productos")]
        public async Task<IActionResult> RegistrarProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _productoDAO.RegistrarProducto(producto));
            return Ok(mensaje);
        }

        [HttpPut("productos/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _productoDAO.ActualizarProducto(producto));
            return Ok(mensaje);
        }

        [HttpDelete("productos/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var mensaje = await Task.Run(() => _productoDAO.EliminarProducto(id));
            return Ok(mensaje);
        }
    }
}

