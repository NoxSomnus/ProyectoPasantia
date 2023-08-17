namespace FerminToroWeb.Models
{
    public class AllPeriodsModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Año { get; set; }
        public string MesInicio { get; set; } = null!;
        public string MesFin { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
