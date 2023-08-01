using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class EstudianteEntity : PersonaEntity
    {
        public string Direccion_Hab { get; set; } = null!;
        public DateOnly Fecha_Nac { get; set; }
        public string Rango_Edad { get; set; } = null!;
        public bool Es_Regular { get; set; }
        public int Porcentaje_Beca { get; set; }
        public string Codigo_Verificacion { get; set; } = null!;
    }
}
