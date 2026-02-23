using GestionClientes.Models;

namespace GestionClientes.Repositories
{
    public class UsuarioRepository
    {
        // base de datos simulada en memoria
        private static List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Usuario A" },
            new Usuario { Id = 2, Nombre = "Usuario B" },
        };
        // metodo para obtener todos los usuarios
        public List<Usuario> ObtenerUsuarios()
        {
            return _usuarios;
        }
        // metodo para obtener un usuario por su id
        public Usuario? ObtenerUsuarioId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }
        public Usuario ObtenerUsuarioMenosSaturado()
        {
            return _usuarios
                .Where(u => !u.EstaSaturado)
                .OrderBy(u => u.ItemsPendientes.Count)
                .FirstOrDefault();
        }
        // metodo para obtener el usuario con menos items asignados
        public Usuario? ObtenerMenosItems()
        {
            return _usuarios.OrderBy(u => u.itemsaAsigandos.Count).FirstOrDefault();
        }
        // metodo para asignar un nuevo item a un usuario
        public void AsignarItem(int usuarioId, ItemAsignado item)
        {
            var usuario = ObtenerUsuarioId(usuarioId);
            if (usuario == null) {
                return;
            }
            if (!usuario.itemsaAsigandos.Any(i => i.ItemId == item.ItemId))
                usuario.itemsaAsigandos.Add(item);
        }
        // metodo para marcar un item como completado   
        public void MarcarCompletado(int usuarioId, int itemId)
        {
            var usuario = ObtenerUsuarioId(usuarioId);
            var item = usuario?.itemsaAsigandos.FirstOrDefault(i => i.ItemId == itemId);
            if (item != null) item.Completado = true;
        }
    }
}
