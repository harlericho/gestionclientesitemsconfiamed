using ItemsTrabajos.Models;
using ItemsTrabajos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemsTrabajos.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        // inyectar el servicio de items
        private readonly ItemService _itemService;
        private readonly AsignacionService _asignacionService;
        // constructor para inyectar el servicio
        public ItemController(ItemService itemService, AsignacionService asignacionService)
        {
            _itemService = itemService;
            _asignacionService = asignacionService;
        }
        [HttpGet]
        public IActionResult ObtenerItemsTodos()
        {
            var items = _itemService.ObtenerItems();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public IActionResult ObtenerItemId(int id)
        {
            var item = _itemService.ObtenerItemId(id);
            if (item == null)
            {
                return NotFound($"Item con id {id} no encontrado.");
            }
            return Ok(item);
        }
        [HttpGet("pendientes/{usuarioId}")]
        public IActionResult ObtenerPendientes(int usuarioId) =>
         Ok(_itemService.ObtenerPendientesOrdenados(usuarioId));

        [HttpGet("saturado/{usuarioId}")]
        public IActionResult VerificarSaturacion(int usuarioId) =>
           Ok(new
           {
               usuarioId,
               saturado = _itemService.UsuarioEstaSaturado(usuarioId),
               mensaje = _itemService.UsuarioEstaSaturado(usuarioId)
                   ? "Usuario saturado, no apto para nuevas asignaciones relevantes."
                   : "Usuario disponible para asignación."
           });

        [HttpPut("{itemId}/asignar-usuario/{usuarioId}")]
        public IActionResult AsignarUsuario(int itemId, int usuarioId)
        {
            var item = _itemService.ObtenerItemId(itemId);
            if (item == null)
            {
                return NotFound($"Item con id {itemId} no encontrado.");
            }
            _itemService.ActualizarAsignacion(itemId, usuarioId);
            return Ok($"Usuario {usuarioId} asignado al item {item.Titulo}.");
        }
        [HttpPost("crear-asignar")]
        public async Task<IActionResult> CrearAsignarItem([FromBody] ItemTrabajo item)
        {
            _itemService.AgregarItem(item);
            var resultadoAsignacion = await _asignacionService.AsignarItemAutomaticamente(item);
            return Ok(new
            {
                item,
                mensaje = resultadoAsignacion
            });

        }
        [HttpPut("{itemId}/completar")]
        public IActionResult MarcarCompletado(int itemId)
        {
            _itemService.MarcarCompletado(itemId);
            return Ok("Ítem marcado como completado.");
        }
    }
}
