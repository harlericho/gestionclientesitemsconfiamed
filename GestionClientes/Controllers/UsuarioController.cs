using GestionClientes.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionClientes.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        // inyectar el servicio de usuarios
        private readonly Services.UsuarioService _usuarioService;
        // constructor para inyectar el servicio
        public UsuarioController(Services.UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public IActionResult ObtenerUsuariosTodos()
        {
            var usuarios = _usuarioService.ObtenerUsuarios();
            return Ok(usuarios);
        }
        [HttpGet("mejor-disponible")]
        public IActionResult ObtenerMejorDisponible()
        {
            var usuario = _usuarioService.ObtenerUsuarioMenosSaturado();
            if (usuario == null)
            {
                return BadRequest("Todos los usuarios están saturados.");
            }
            return Ok(usuario);
        }
        [HttpGet("{id}/saturado")]
        public IActionResult EstaSaturado(int id) =>
            Ok(new { saturado = _usuarioService.EstaSaturado(id) });

        [HttpGet("menos-items")]
        public IActionResult ObtenerUsuarioMenosItems()
        {
            var usuario = _usuarioService.ObtenerMenosItems();
            if (usuario == null)
            {
                return NotFound("No se encontraron usuarios.");
            }
            return Ok(usuario);

        }
        [HttpPost("{usuarioId}/asignar-item")]
        public IActionResult AsignarUsuarioItem(int usuarioId, ItemAsignado item)
        {
            var usuario = _usuarioService.ObtenerUsuarioId(usuarioId);
            if (usuario == null)
            {
                return NotFound($"Usuario con id {usuarioId} no encontrado.");
            }
            _usuarioService.AsignarItem(usuarioId, item);
            return Ok($"Item {item} asignado al usuario {usuario.Nombre}.");
        }
        [HttpPut("{usuarioId}/completar/{itemId}")]
        public IActionResult MarcarCompletado(int usuarioId, int itemId)
        {
            _usuarioService.MarcarCompletado(usuarioId, itemId);
            return Ok("Ítem marcado como completado.");
        }
    }
}
