using FerminToroMS.Application.Responses;
using FerminToroMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Mappers
{
    public class CronogramasMapper
    {
        public static List<ScheduleResponse> MapListEntityToResponse(List<CronogramaEntity> schedules) 
        {
            var response = new List<ScheduleResponse>();
            foreach (var schedule in schedules) 
            {
                var scheduleResponse = new ScheduleResponse
                {
                    PeriodId = schedule.PeriodoId,
                    PeriodName = schedule.Periodo.NombrePeriodo,
                    CourseName = schedule.Modulo.Nombre,
                    ModulName = schedule.Modulo.Nombre,
                    Fecha_Inicio = schedule.FechaInicio.ToString(),
                    Fecha_Fin = schedule.FechaFin.ToString(),
                    Horario = schedule.Horario_Dias,
                    Turno = schedule.Turno.ToString(),
                    Regularidad = schedule.Regularidad.ToString(),
                    Modalidad = schedule.Modalidad.ToString(),
                    Duracion = schedule.Duracion_Semanas,
                    NroVacantes = schedule.NroVacantes,
                    Horas = schedule.NroHoras,
                    //InstructorAsignado = schedule.Instructor.Nombre
                };
                response.Add(scheduleResponse);
            }
            return response;
        }
    }
}
