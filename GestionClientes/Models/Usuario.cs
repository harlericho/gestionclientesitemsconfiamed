namespace GestionClientes.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<int> itemsaAsigandos { get; set; } = new List<int>();
    }
}
