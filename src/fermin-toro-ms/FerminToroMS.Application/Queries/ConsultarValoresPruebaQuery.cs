using MediatR;
using FerminToroMS.Application.Responses;

namespace FerminToroMS.Application.Queries
{
    public class ConsultarValoresPruebaQuery : IRequest<List<ValoresResponse>>
    { }
}

