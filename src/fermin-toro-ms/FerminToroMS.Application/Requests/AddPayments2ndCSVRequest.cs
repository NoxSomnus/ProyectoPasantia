using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddPayments2ndCSVRequest
    {
        public string Cedula { get; set; } = null!;
        public int NroInscripcion { get; set; }
        public bool Cuota { get; set; }
        public string ReciboFacturaMonto { get; set; } = null!;
        public string Comentario { get; set; } = null!;

    }
}
