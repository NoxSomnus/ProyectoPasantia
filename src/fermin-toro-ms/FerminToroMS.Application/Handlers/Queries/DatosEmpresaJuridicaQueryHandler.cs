using FerminToroMS.Application.Exceptions;
using FerminToroMS.Application.Queries;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Handlers.Queries
{
    internal class DatosEmpresaJuridicaQueryHandler : IRequestHandler<DatosEmpresaJuridicaQuery, EmpresaJuridicaResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<DatosEmpresaJuridicaQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase DatosEmpresaJuridicaQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public DatosEmpresaJuridicaQueryHandler(IFerminToroDbContext dbContext, ILogger<DatosEmpresaJuridicaQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca los datos de una empresa juridica por Id.
        /// </summary>
        /// <param name="request">La consulta DatosEmpresaJuridicaQuery que especifica la busqueda de la empresa por Id.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto JSON que contiene toda la informacion de la empresa juridica.</returns>
        public Task<EmpresaJuridicaResponse> Handle(DatosEmpresaJuridicaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("DatosEmpresaJuridicaQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("DatosEmpresaJuridicaQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de la empresa juridica por Id.
        /// </summary>
        /// <param name="request">La consulta DatosEmpresaJuridicaQuery que especifica la busqueda de la empresa por Id.</param>
        /// <returns>Un objeto JSON que contiene toda la informacion de la empresa juridica.</returns>
        private async Task<EmpresaJuridicaResponse> HandleAsync(DatosEmpresaJuridicaQuery request)
        {
            try
            {
                _logger.LogInformation("EmployeeByIdQueryHandler.HandleAsync");
                var company = await _dbContext.Datos_Empresa_Juridica.FirstOrDefaultAsync(c => c.Id == request.EmpresaJuridicaId);
                if (company == null)
                {
                    throw new IdNotFoundException("No se encontro los datos de la empresa juridica");
                }  
                var response = new EmpresaJuridicaResponse
                {
                    Nombre_Empresa = company.NombreEmpresa,
                    CorreoAdministrativo = company.Correo_Administrativo,
                    TelefonoAdministrativo = company.Telefono_Administrativo,
                    UrlRif = company.URL_Rif
                };
                return response;
            }
            catch (IdNotFoundException ex)
            {
                _logger.LogError(ex, "Error DatosEmpresaJuridicaQueryHandler.HandleAsync.", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error DatosEmpresaJuridicaQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
