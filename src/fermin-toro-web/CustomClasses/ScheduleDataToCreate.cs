namespace FerminToroWeb.CustomClasses
{
    public class ScheduleDataToCreate
    {
        public string Programa { get; set; } = null!;
        public string Modulo { get; set; } = null!;
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        public int Regularidad { get; set; }
        public int Turno { get; set; }
        public string Horario { get; set; } = null!;
        public int Modalidad { get; set; }
        public int Duracion { get; set; }
        public int Vacantes { get; set; }
    }
}
