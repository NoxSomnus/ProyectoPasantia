using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class PaymentDetailsQuery : IRequest<PaymentDetails>
    {
        public Guid InscriptionId { get; set; }
        public PaymentDetailsQuery(Guid inscriptionId)
        {
            InscriptionId = inscriptionId;
        }
    }
}
