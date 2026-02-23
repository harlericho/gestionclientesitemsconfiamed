using GestionClientes.Models;

namespace GestionClientes.Repositories
{
    public class UsuarioRepository
    {
        // base de datos simulada en memoria
        private static List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Usuario A", itemsaAsigandos = new List<int> { 1, 2, 3, 4 } },
            new Usuario { Id = 2, Nombre = "Usuario B", itemsaAsigandos = new List<int> { 5, 6, 7 } },
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
        // metodo para obtener el usuario con menos items asignados
        public Usuario? ObtenerMenosItems()
        {
            return _usuarios.OrderBy(u => u.itemsaAsigandos.Count).FirstOrDefault();
        }
        // metodo para asignar un nuevo item a un usuario
        public void AsignarItem(int usuarioId, int itemId)
        {
            var usuario = ObtenerUsuarioId(usuarioId);
            if (usuario != null && !usuario.itemsaAsigandos.Contains(itemId))
            {
                usuario.itemsaAsigandos.Add(itemId);
            }
        }
    }
}
