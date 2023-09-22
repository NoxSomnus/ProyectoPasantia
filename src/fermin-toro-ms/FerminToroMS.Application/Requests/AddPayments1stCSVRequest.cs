using FerminToroMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class AddPayments1stCSVRequest
    {
        public int NroInscripcion { get; set; }
        public DateTime FechaPago { get; set; } 
        public bool Cuota { get; set; }
        public bool Divisa { get; set; }
        public string MetodoPago { get; set; } = null!;
        public string? URLComprobante { get; set; }
        public bool Juridico { get; set; }
        public string? UrlRif { get; set; }
    }
}
