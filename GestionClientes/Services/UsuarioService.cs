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
        public void AsignarItem(int usuarioId, ItemAsignado item)
        {
            _usuarioRepository.AsignarItem(usuarioId, item);
        }

        // metodo para obtener el usuario menos saturado
        public Usuario ObtenerUsuarioMenosSaturado()
        {
            return _usuarioRepository.ObtenerUsuarioMenosSaturado();
        }

        // metodo para marcar un item como completado
        public void MarcarCompletado(int usuarioId, int itemId) =>
       _usuarioRepository.MarcarCompletado(usuarioId, itemId);

        // metodo para verificar si un usuario está saturado
        public bool EstaSaturado(int usuarioId)
        {
            var usuario = _usuarioRepository.ObtenerUsuarioId(usuarioId);
            return usuario?.EstaSaturado ?? false;
        }
    }
}
