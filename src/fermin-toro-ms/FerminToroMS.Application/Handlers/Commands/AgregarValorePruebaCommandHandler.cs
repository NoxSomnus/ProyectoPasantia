﻿using MediatR;
using Microsoft.Extensions.Logging;
using FerminToroMS.Application.Commands;
using FerminToroMS.Application.Handlers.Queries;
using FerminToroMS.Application.Mappers;
using FerminToroMS.Core.Database;

namespace FerminToroMS.Application.Handlers.Commands
{
    public class AgregarValorePruebaCommandHandler : IRequestHandler<AgregarValorPruebaCommand, Guid>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<ConsultarValoresQueryHandler> _logger;

        public AgregarValorePruebaCommandHandler(IFerminToroDbContext dbContext, ILogger<ConsultarValoresQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(AgregarValorPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(AgregarValorPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}" , request);
                var entity = ValoresMapper.MapRequestEntity(request._request);
                _dbContext.Valores.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
