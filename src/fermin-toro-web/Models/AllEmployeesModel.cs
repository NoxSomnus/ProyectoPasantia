namespace FerminToroWeb.Models
{
    public class AllEmployeesModel
    {
        public Guid Id { get; set; }
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
