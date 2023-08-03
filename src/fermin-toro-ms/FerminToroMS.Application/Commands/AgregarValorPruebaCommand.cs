using MediatR;
using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;

namespace FerminToroMS.Application.Commands
{
    public class AgregarValorPruebaCommand : IRequest<Guid>
    {
        public ValoresRequest _request { get; set; }

        public AgregarValorPruebaCommand(ValoresRequest request)
        {
            _request = request;
        }
    }
}
