using static GestionClientes.Models.Usuario;

namespace GestionClientes.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ItemAsignado> itemsaAsigandos { get; set; } = new List<ItemAsignado>();

        // propiedad calculada para obtener los items pendientes, ordenados por relevancia y fecha de entrega
        public List<ItemAsignado> ItemsPendientes =>
                itemsaAsigandos
                    .Where(i => !i.Completado)
                    .OrderByDescending(i => i.Relevante)
                    .ThenBy(i => i.FechaEntrega)
                    .ToList();

        // cantidad de items altamente relevantes y no completados
        public int ItemsAltamenteRelevantes =>
            itemsaAsigandos.Count(i => i.Relevante && !i.Completado);

        // propiedad calculada para determinar si el usuario está saturado
        public bool EstaSaturado => ItemsAltamenteRelevantes > 3;
    }
    // clase para representar un item asignado a un usuario 
    public class ItemAsignado
    {
        public int ItemId { get; set; }
        public bool Relevante { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool Completado { get; set; }
    }
}
