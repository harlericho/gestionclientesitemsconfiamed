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
            bool asignarCarga = AltamenteRelevante(itemTrabajo) || FechaLimiteCercana(itemTrabajo);
            if (!asignarCarga)
            {
                return "No se asignara automaticamente el item, no cumple con los criterios.";
            }
            // obtener el usuario con menos items asignados del microservicio de gestion usuarios
            var response = await _httpClient.GetAsync($"{ UrlUsuarios}/menos-items");
            if (!response.IsSuccessStatusCode)
            {
                return "Error al obtener usuarios del microservicio de gestion usuarios.";
            }
            // deserializar la respuesta para obtener el usuario
            var json = await response.Content.ReadAsStringAsync();
            // deserializar la respuesta para obtener el usuario
            var usuario = JsonSerializer.Deserialize<UsuarioDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            // asignar el item al usuario obtenido y actualizar la base de datos
            _itemRepository.ActualizarAsignacion(itemTrabajo.Id, usuario.Id);
            await _httpClient.PostAsync($"{ UrlUsuarios}/asignar-item/{usuario.Id}", null);
            return $"Item asignado automáticamente al usuario {usuario.Nombre}.";

        }
        // metodo para asignar automáticamente un item a un usuario
        private bool AltamenteRelevante(ItemTrabajo item)
        {
            return item.Relevante;
        }
        // metodo verificar si la fecha de entrega del item es cercana (menos de 2 días)
        private bool FechaLimiteCercana(ItemTrabajo item)
        {
            var diasRestantes = (item.FechaEntrega - DateTime.Now).TotalDays;
            return diasRestantes <= 2;
        }
        // metodo dtos para enviar al microservicio de usuarios
        private class UsuarioDto
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
        }
    }
}
