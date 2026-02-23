using ItemsTrabajos.Models;

namespace ItemsTrabajos.Repositories
{
    public class ItemRepository
    {
        // base de datos simulada en memoria
        public static List<ItemTrabajo> _items = new List<ItemTrabajo>
        {
            new ItemTrabajo { Id = 1, Titulo = "Item A1", Relevante = true, FechaEntrega = DateTime.Now.AddDays(10), UsuarioId =1 },
            new ItemTrabajo { Id = 2, Titulo = "Item A2", Relevante = true, FechaEntrega = DateTime.Now.AddDays(15), UsuarioId =1  },
            new ItemTrabajo { Id = 3, Titulo = "Item A3", Relevante = true, FechaEntrega = DateTime.Now.AddDays(20), UsuarioId =1  },
            new ItemTrabajo { Id = 4, Titulo = "Item B4", Relevante = false, FechaEntrega = DateTime.Now.AddDays(30), UsuarioId =1  },
            new ItemTrabajo { Id = 5, Titulo = "Item B5", Relevante = true, FechaEntrega = DateTime.Now.AddDays(10), UsuarioId =2 },
            new ItemTrabajo { Id = 6, Titulo = "Item B6", Relevante = true, FechaEntrega = DateTime.Now.AddDays(20), UsuarioId =2  },
            new ItemTrabajo { Id = 7, Titulo = "Item B7", Relevante = false, FechaEntrega = DateTime.Now.AddDays(30), UsuarioId =2  },
        };
        // metodo para obtener todos los items
        public List<ItemTrabajo> ObtenerItems()
        {
            return _items;
        }
        // metodo para obtener un item por su id
        public ItemTrabajo? ObtenerItemId(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }
        // metodo para agregar un nuevo item
        public void AgregarItem(ItemTrabajo item)
        {
            item.Id = _items.Max(i => i.Id) + 1; // asignar un nuevo id
            _items.Add(item);
        }
        // metodo para Actualizar asignacion de item a usuario
        public void ActualizarAsignacion(int itemId, int usuarioId)
        {
            var item = ObtenerItemId(itemId);
            if (item != null)
            {
                item.UsuarioId = usuarioId;
            }
        }
    }
}
