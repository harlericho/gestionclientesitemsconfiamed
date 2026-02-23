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
        // metodo para actualizar al usuario asignado a un item
        public void ActualizarAsignacion(int itemId, int usuarioId)
        {
            _itemRepository.ActualizarAsignacion(itemId, usuarioId);
        }
    }
}
