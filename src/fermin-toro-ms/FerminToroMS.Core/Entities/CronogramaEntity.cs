using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class CronogramaEntity : BaseEntity
    {
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string Horario_Dias { get; set; } = null!;
        public RegularidadEnum Regularidad { get; set; }
        public ModalidadEnum Modalidad { get; set; }
        public TurnosEnum Turno { get; set; }
        public int Duracion_Semanas { get; set; }
        public int NroHoras { get; set; }
        //Relacion de modulo, periodo, instructor
        public Guid ModuloId { get; set; }
        public ModuloEntity Modulo { get; set; } = null!;
        public Guid PeriodoId { get; set; }
        public PeriodoEntity Periodo { get; set; } = null!;
        public Guid? InstructorId { get; set; }
        public EmpleadoEntity? Instructor { get; set; }
        //Relacion con inscripciones y Promociones
        public ICollection<InscripcionEntity>? Inscripciones { get; set; }
        public ICollection<PromocionEntity>? Promociones { get; set; } 
        public int? NroVacantes { get; set; }
    }
}
