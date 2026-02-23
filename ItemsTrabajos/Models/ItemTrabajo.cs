namespace ItemsTrabajos.Models
{
    public class ItemTrabajo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Relevante { get; set; }
        public DateTime FechaEntrega { get; set; }
        // null = sin asignar
        public int? UsuarioAsignadoId { get; set; }

        // Pendiente = false, Completado = true
        public bool Completado { get; set; } = false;

        // Verifica si la fecha está próxima a vencer (menos de 3 días)
        public bool FechaProximaAVencer =>
            (FechaEntrega.Date - DateTime.Now.Date).TotalDays < 3;
    }
}
