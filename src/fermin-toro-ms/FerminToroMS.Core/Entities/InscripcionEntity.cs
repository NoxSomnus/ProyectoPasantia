using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class InscripcionEntity : BaseEntity
    {
        public string? EstadoSolvencia { get; set; }
        public bool FueraVenezuela { get; set; }
        public string? EstadoVenezuela { get; set; }
        public string? NotaAcademica { get; set; }
        public Guid CronogramaId { get; set; }
        public CronogramaEntity Cronograma { get; set; } = null!;
        public Guid EstudianteId { get; set; }
        public EstudianteEntity Estudiante { get; set; } = null!;
        public int NroInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public ICollection<DeudasEntity>? Deudas { get; set; }
        public ICollection<PagoEntity>? Pagos { get; set; }
        public double? CantidadAPagar { get; set; }
    }
}
