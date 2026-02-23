using ItemsTrabajos.Models;
using ItemsTrabajos.Repositories;

namespace ItemsTrabajos.Services
{
    public class ItemService
    {
        // inyectar el repositorio de items
        private readonly ItemRepository _itemRepository;
        // constructor para inyectar el repositorio
        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        // metodo para obtener todos los items
        public List<ItemTrabajo> ObtenerItems()
        {
            return _itemRepository.ObtenerItems();
        }
        // metodo para obtener un item por su id
        public ItemTrabajo? ObtenerItemId(int id)
        {
            return _itemRepository.ObtenerItemId(id);
        }
        // metodo para agregar un nuevo item
        public void AgregarItem(ItemTrabajo item)
        {
            _itemRepository.AgregarItem(item);
        }
        // metodo para obtener los items pendientes de un usuario ordenados por relevancia y fecha de entrega
        public List<ItemTrabajo> ObtenerPendientesOrdenados(int usuarioId) =>
            _itemRepository.ObtenerPendientesPorUsuario(usuarioId);

        // metodo para verificar si un usuario esta saturado (tiene 5 o mas items pendientes)
        public bool UsuarioEstaSaturado(int usuarioId) =>
            _itemRepository.UsuarioEstaSaturado(usuarioId);

        // metodo para actualizar al usuario asignado a un item
        public string ActualizarAsignacion(int itemId, int usuarioId)
        {
            // Verifica saturación antes de asignar
            if (_itemRepository.UsuarioEstaSaturado(usuarioId))
                return $"El usuario {usuarioId} está saturado, no se puede asignar.";

            _itemRepository.ActualizarAsignacion(itemId, usuarioId);
            return "Ítem asignado. Lista de pendientes reordenada.";
        }
        // metodo para marcar un item como completado
        public void MarcarCompletado(int itemId) =>
          _itemRepository.MarcarCompletado(itemId);
    }
}
