using GestionClientes.Models;
using GestionClientes.Repositories;

namespace GestionClientes.Services
{
    public class UsuarioService
    {
        // inyectar el repositorio de usuarios
        private readonly UsuarioRepository _usuarioRepository;
        // constructor para inyectar el repositorio
        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        // metodo para obtener todos los usuarios
        public List<Usuario> ObtenerUsuarios()
        {
            return _usuarioRepository.ObtenerUsuarios();
        }
        // metodo para obtener un usuario por su id
        public Usuario? ObtenerUsuarioId(int id)
        {
            return _usuarioRepository.ObtenerUsuarioId(id);
        }
        // metodo para obtener el usuario con menos items asignados
        public Usuario? ObtenerMenosItems()
        {
            return _usuarioRepository.ObtenerMenosItems();
        }
        // metodo para asignar un nuevo item a un usuario
        public void AsignarItem(int usuarioId, int itemId)
        {
            _usuarioRepository.AsignarItem(usuarioId, itemId);
        }

    }
}
