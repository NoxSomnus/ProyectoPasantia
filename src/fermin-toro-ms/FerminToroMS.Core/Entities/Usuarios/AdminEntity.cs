using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities.Usuarios
{
    public class AdminEntity : EmpleadoEntity
    {
        public bool EsDirector { get; set; }
    }
}
