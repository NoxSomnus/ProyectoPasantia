namespace FerminToroWeb.Models
{
    public class AddPeriodModel
    {
        public string NombrePeriodo { get; set; } = null!;
        public int Año { get; set; }
        public string MesInicio { get; set; } = null!;
        public string MesFin { get; set; } = null!;
    }
}
