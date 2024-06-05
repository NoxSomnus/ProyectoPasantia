namespace FerminToroWeb.Models
{
    public class UpdatePeriodModel
    {
        public Guid Id { get; set; }
        public string NombrePeriodo { get; set; } = null!;
        public int Año { get; set; }
        public string MesInicio { get; set; } = null!;
        public string MesFin { get; set; } = null!;
    }
}
