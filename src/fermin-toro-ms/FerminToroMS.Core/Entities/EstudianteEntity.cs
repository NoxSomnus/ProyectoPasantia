using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class EstudianteEntity : BaseEntity
    {
        [Column(Order = 2)]
        public string Cedula { get; set; } = null!;
        [Column(Order = 3)]
        public string Nombre { get; set; } = null!;
        [Column(Order = 4)]
        public string Apellido { get; set; } = null!;
        [Column(Order = 5)]
        public string Correo { get; set; } = null!;
        [Column(Order = 6)]
        public string Telefono { get; set; } = null!;
        public string? CorreoSecundario { get; set; }
        public string Direccion_Hab { get; set; } = null!;
        public DateOnly? Fecha_Nac { get; set; }
        public int? Edad { get; set; }
        public string? Rango_Edad { get; set; }
        public bool Es_Regular { get; set; }
        public int Porcentaje_Beca { get; set; }
        public string? Codigo_Verificacion { get; set; }
        public ICollection<ModuloEntity>? ModulosAprobados { get; set; }
        public ICollection<DeudasEntity>? Deudas { get; set; }
        public ICollection<AbonoEntity>? Abonos { get; set; }
        public Guid? RepresentanteId { get; set; }
        public RepresentanteEntity? Representante { get; set; }
    }
}
