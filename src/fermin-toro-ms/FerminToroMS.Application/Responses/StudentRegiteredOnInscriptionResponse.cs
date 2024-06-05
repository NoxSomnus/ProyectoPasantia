using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class StudentRegiteredOnInscriptionResponse
    {
        public Guid InscriptionId { get; set; }
        public Guid StudentId { get; set; }
        public string Cedula { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CellPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int NroInscription { get; set; }
        public double TotalPaid { get; set; } = 0;
        public bool ByCuota { get; set; } = false;
        public bool EsJuridico { get; set; } = false;
        public bool HasPayment { get; set; } = true;
    }
}
