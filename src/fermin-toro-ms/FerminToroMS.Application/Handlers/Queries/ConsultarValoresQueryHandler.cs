using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace FerminToroMS.Application.Handlers.Queries
{
    public class ConsultarValoresQueryHandler : IRequestHandler<ConsultarValoresPruebaQuery, List<ValoresResponse>>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<ConsultarValoresQueryHandler> _logger;

        public ConsultarValoresQueryHandler(IFerminToroDbContext dbContext, ILogger<ConsultarValoresQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ValoresResponse>> Handle(ConsultarValoresPruebaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync();
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<ValoresResponse>> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarValoresQueryHandler.HandleAsync");

                var result = _dbContext.Valores.Where(c=>c.Nombre == "Carlos").Select(c => new ValoresResponse()
                {
                    Id = c.Id,
                    Nombre = c.Nombre + " " + c.Apellido,
                    Identificacion = c.Identificacion,
                });

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }
}
