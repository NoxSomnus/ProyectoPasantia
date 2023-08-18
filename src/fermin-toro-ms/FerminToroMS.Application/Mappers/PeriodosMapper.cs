using FerminToroMS.Application.Requests;
using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Mappers
{
    public class PeriodosMapper
    {
        public static PeriodResponse MapEntityToResponse(PeriodoEntity request)
        {
            var response = new PeriodResponse()
            {
                PeriodId = request.Id,
                Año = request.Año,
                MesInicio = request.MesInicio,
                MesFin = request.MesFin,
                PeriodName = request.NombrePeriodo
            };
            return response;
        }

        public static PeriodoEntity MapRequestToEntity(AddPeriodRequest request)
        {
            var period = new PeriodoEntity
            {
                NombrePeriodo = request.PeriodName,
                Año = request.Year,
                MesInicio = request.StartMonth,
                MesFin = request.EndMonth
            };
            return period;
        }
    }
}
