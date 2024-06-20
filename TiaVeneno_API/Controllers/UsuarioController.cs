using GamarraPlus.Models;
using TiaVeneno_API.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TiaVeneno_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDAO _usuarioDAO;

        public UsuarioController()
        {
            _usuarioDAO = new UsuarioDAO();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var lista = await Task.Run(() => _usuarioDAO.obtenerUsuarios());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorID(int id)
        {
            var usuario = await Task.Run(() => _usuarioDAO.ObtenerUsuarioPorId(id));
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _usuarioDAO.RegistrarUsuario(usuario));
            return Ok(mensaje);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mensaje = await Task.Run(() => _usuarioDAO.ActualizarUsuario(usuario));
            return Ok(mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var mensaje = await Task.Run(() => _usuarioDAO.EliminarUsuario(id));
            return Ok(mensaje);
        }
    }
}
