using ItemsTrabajos.Models;
using ItemsTrabajos.Repositories;
using System.Text.Json;

namespace ItemsTrabajos.Services
{
    public class AsignacionService
    {
        // inyectar el repositorio de items y el cliente http
        private readonly ItemRepository _itemRepository;
        private readonly HttpClient _httpClient;

        // URL del microservicio GestionUsuarios
        private const string UrlUsuarios = "https://localhost:7111/api/usuario";

        // constructor para inyectar el repositorio y el cliente http
        public AsignacionService(ItemRepository itemRepository, IHttpClientFactory httpClientFactory)
        {
            _itemRepository = itemRepository;
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<string> AsignarItemAutomaticamente(ItemTrabajo itemTrabajo)
        {
            // verificar si el item cumple con los criterios para asignación automática
            bool fechaProxima = FechaLimiteCercana(itemTrabajo); // menos de 3 dias para la fecha de entrega
            bool asignarCarga = itemTrabajo.Relevante;
            if (!asignarCarga && !fechaProxima)
            {
                return "El item no cumple con los criterios para asignación automática.";
            }
            // obtener el usuario con menos items asignados del microservicio de gestion usuarios
            var response = await _httpClient.GetAsync($"{ UrlUsuarios}/mejor-disponible");
            if (!response.IsSuccessStatusCode)
            {
                return "Error al obtener usuarios del microservicio de gestion usuarios.";
            }
            // deserializar la respuesta para obtener el usuario
            var json = await response.Content.ReadAsStringAsync();
            // deserializar la respuesta para obtener el usuario
            var usuario = JsonSerializer.Deserialize<UsuarioDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            // verificar si se obtuvo un usuario válido
            if (usuario == null)
            {
                return "No se encontró un usuario disponible para asignar el item.";
            }

            // asignar el item al usuario obtenido y actualizar la base de datos
            _itemRepository.ActualizarAsignacion(itemTrabajo.Id, usuario.Id);
            // crear un objeto con los datos del item para enviar al microservicio de usuarios
            var itemAsignado = new
            {
                itemId = itemTrabajo.Id,
                esAltamenteRelevante = itemTrabajo.Relevante,
                fechaEntrega = itemTrabajo.FechaEntrega,
                completado = false
            };
            await _httpClient.PostAsync($"{ UrlUsuarios}/{usuario.Id}/asignar-item", null);
            return $"Item asignado automáticamente al usuario {usuario.Nombre}.";

        }
        // metodo verificar si la fecha de entrega del item es cercana (menos de 2 días)
        private bool FechaLimiteCercana(ItemTrabajo item)
        {
            var diasRestantes = (item.FechaEntrega - DateTime.Now).TotalDays;
            return diasRestantes <= 3;
        }
        // metodo dtos para enviar al microservicio de usuarios
        private class UsuarioDto
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
        }
    }
}
