namespace FerminToroWeb.Models
{
    public class Schedule
    {
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        public string Horario { get; set; } = null!;
        public int Regularidad { get; set; }
        public int Modalidad { get; set; }
        public int Turno { get; set; }
        public int Duracion { get; set; }
        public int Vacantes { get; set; }
        public string Programa { get; set; } = null!;
        public string Modulo { get; set; } = null!;
    }
}
