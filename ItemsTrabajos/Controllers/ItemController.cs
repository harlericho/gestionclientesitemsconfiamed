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
        [HttpPost]
        public IActionResult AgregarItem([FromBody] ItemTrabajo item)
        {
            _itemService.AgregarItem(item);
            return Ok("Item agregado correctamente.");
        }
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
            return Ok(resultadoAsignacion);

        }
    }
}
