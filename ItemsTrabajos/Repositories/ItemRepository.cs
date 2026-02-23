using ItemsTrabajos.Models;

namespace ItemsTrabajos.Repositories
{
    public class ItemRepository
    {
        // base de datos simulada en memoria
        public static List<ItemTrabajo> _items = new List<ItemTrabajo>
        {
            new ItemTrabajo { Id = 1, Titulo = "Item A1", Relevante = true, FechaEntrega = DateTime.Now.AddDays(10), UsuarioAsignadoId =1 },
            new ItemTrabajo { Id = 2, Titulo = "Item A2", Relevante = true, FechaEntrega = DateTime.Now.AddDays(15), UsuarioAsignadoId =1  },
            new ItemTrabajo { Id = 3, Titulo = "Item A3", Relevante = true, FechaEntrega = DateTime.Now.AddDays(20), UsuarioAsignadoId =1  },
            new ItemTrabajo { Id = 4, Titulo = "Item B4", Relevante = false, FechaEntrega = DateTime.Now.AddDays(30), UsuarioAsignadoId =1  },
            new ItemTrabajo { Id = 5, Titulo = "Item B5", Relevante = true, FechaEntrega = DateTime.Now.AddDays(10), UsuarioAsignadoId =2 },
            new ItemTrabajo { Id = 6, Titulo = "Item B6", Relevante = true, FechaEntrega = DateTime.Now.AddDays(20), UsuarioAsignadoId =2  },
            new ItemTrabajo { Id = 7, Titulo = "Item B7", Relevante = false, FechaEntrega = DateTime.Now.AddDays(30), UsuarioAsignadoId =2  },
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
        // metodo para obtener items pendientes por usuario, ordenados por relevancia y fecha de entrega
        public List<ItemTrabajo> ObtenerPendientesPorUsuario(int usuarioId)
        {
            return _items
                .Where(i => i.UsuarioAsignadoId == usuarioId && !i.Completado)
                .OrderByDescending(i => i.Relevante)
                .ThenBy(i => i.FechaEntrega)
                .ToList();
        }
        // metodo para contar items relevantes pendientes por usuario
        public int ContarItemsRelevantes(int usuarioId)
        {
            return _items.Count(i =>
                i.UsuarioAsignadoId == usuarioId &&
                i.Relevante &&
                !i.Completado);
        }
        // metodo para verificar si un usuario está saturado (más de 3 items relevantes pendientes)
        public bool UsuarioEstaSaturado(int usuarioId)
        {
            return ContarItemsRelevantes(usuarioId) > 3;
        }
        // metodo para agregar un nuevo item de trabajo
        public void Agregar(ItemTrabajo item)
        {
            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
            _items.Add(item);
        }
        // metodo para actualizar la asignación de un item a un usuario
        public void ActualizarAsignacion(int itemId, int usuarioId)
        {
            var item = ObtenerItemId(itemId);
            if (item != null)
                item.UsuarioAsignadoId = usuarioId;
        }
        // metodo para marcar un item como completado
        public void MarcarCompletado(int itemId)
        {
            var item = ObtenerItemId(itemId);
            if (item != null)
                item.Completado = true;
        }
    }
}
