using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities.Usuarios
{
    public class EmpleadoEntity : PersonaEntity
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
