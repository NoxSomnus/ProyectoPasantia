using FerminToroMS.Core.Entities;
using FerminToroMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class PricesRequest
    {
        public string CourseName { get; set; } = null!;
        public ModalidadEnum Modalidad { get; set; }
        public TurnosEnum Turno { get; set; }
        public float Precio { get; set; }
    }
}
