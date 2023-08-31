using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class StudentSignUpRequest
    {
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Fecha_Nac { get; set; } = null!;
        public string Rango_Edad { get; set; } = null!;
        public string Telefono { get; set; } = null!;
    }
}
