namespace ItemsTrabajos.Models
{
    public class ItemTrabajo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Relevante { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int? UsuarioId { get; set; }
    }
}
