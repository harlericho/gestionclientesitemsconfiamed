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
        [HttpPost("{usuarioId}/asignar-item/{itemId}")]
        public IActionResult AsignarUsuarioItem(int usuarioId, int itemId)
        {
            var usuario = _usuarioService.ObtenerUsuarioId(usuarioId);
            if (usuario == null)
            {
                return NotFound($"Usuario con id {usuarioId} no encontrado.");
            }
            _usuarioService.AsignarItem(usuarioId, itemId);
            return Ok($"Item {itemId} asignado al usuario {usuario.Nombre}.");
        }
    }
}
