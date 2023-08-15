using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool esAdmin { get; set; }
        public bool esDirector { get; set; }
        public bool esInstructor { get; set; }
        public List<PermissionResponse>? permisos_asignados { get; set; }
    }
}
