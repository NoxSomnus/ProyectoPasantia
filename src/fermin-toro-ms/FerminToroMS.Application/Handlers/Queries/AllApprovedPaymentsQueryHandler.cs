using Automatonymous;
using FerminToroMS.Application.CustomClasses;
using FerminToroMS.Application.Factories;
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
    internal class AllApprovedPaymentsQueryHandler : IRequestHandler<AllApprovedPaymentsQuery, AllApprovedPaymentsResponse>
    {
        private readonly IFerminToroDbContext _dbContext;
        private readonly ILogger<AllApprovedPaymentsQueryHandler> _logger;

        /// <summary>
        /// Constructor de la clase AllApprovedPaymentsQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllApprovedPaymentsQueryHandler(IFerminToroDbContext dbContext, ILogger<AllApprovedPaymentsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Manejador de la consulta que busca todos los pagos aprobados.
        /// </summary>
        /// <param name="request">La consulta AllApprovedPaymentsQuery que especifica la busqueda de todos los pagos aprobados.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de todos los pagos aprobados del sistema.</returns>
        public Task<AllApprovedPaymentsResponse> Handle(AllApprovedPaymentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("AllApprovedPaymentsQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync();
                }
            }
            catch
            {
                _logger.LogWarning("AllApprovedPaymentsQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método asincronico que maneja la consulta de todos los pagos aprobados registrados.
        /// </summary>      
        /// <returns>Una lista de pagos aprobados del sistema.</returns>
        private async Task<AllApprovedPaymentsResponse> HandleAsync()
        {
            try
            {
                _logger.LogInformation("AllApprovedPaymentsQueryHandler.HandleAsync");
                var approvedPayments = await _dbContext.Pagos_Aprobados
                    .Include(p=>p.Pago).ThenInclude(m=>m.MetodoPago)
                    .Include(p=>p.Pago).ThenInclude(i=>i.Inscripcion).ThenInclude(c=>c.Cronograma)
                    .OrderByDescending(c => c.FechaTransaccion).ToListAsync();
                var response = new AllApprovedPaymentsResponse();
                response.PagosPaypal = new List<PaypalApprovedPayments>();
                response.PagosMercantil = new List<MercantilApprovedPayments>();
                response.PagosBNC = new List<BNCApprovedPayments>();
                response.PagosZelle = new List<ZelleApprovedPayments>();
                response.PagosEfectivo = new List<EfectivoApprovedPayments>();
                response.Periodos = _dbContext.Periodos.OrderByDescending(c => c.Año).ThenByDescending(c => c.CreatedAt).Select(c => new PeriodResponse
                    {
                       PeriodId = c.Id,
                       PeriodName = c.NombrePeriodo,
                       Año = c.Año,
                       MesFin = c.MesFin,
                       MesInicio = c.MesInicio
                    }).ToList();
                if(approvedPayments != null)
                foreach (var ApprovedPayment in approvedPayments)
                {
                    if (ApprovedPayment.Pago.MetodoPago.NombreMetodo == "Banco Mercantil")
                    {                        
                        response.PagosMercantil.Add(ApprovedPaymentFactory.CreateMercantilApprovedPayments(ApprovedPayment));
                    }
                    if (ApprovedPayment.Pago.MetodoPago.NombreMetodo == "Banco Nacional de Crédito")
                    {
                        response.PagosBNC.Add(ApprovedPaymentFactory.CreateBNCApprovedPayments(ApprovedPayment));
                    }
                    if (ApprovedPayment.Pago.MetodoPago.NombreMetodo == "Paypal")
                    {
                        response.PagosPaypal.Add(ApprovedPaymentFactory.CreatePaypalApprovedPayments(ApprovedPayment));
                    }
                    if (ApprovedPayment.Pago.MetodoPago.NombreMetodo == "Zelle")
                    {
                        response.PagosZelle.Add(ApprovedPaymentFactory.CreateZelleApprovedPayments(ApprovedPayment));
                    }
                    if (ApprovedPayment.Pago.MetodoPago.NombreMetodo == "Efectivo")
                    {
                        response.PagosEfectivo.Add(ApprovedPaymentFactory.CreateEfectivoApprovedPayments(ApprovedPayment));
                    }
                    }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllApprovedPaymentsQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}
