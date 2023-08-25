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
        public static CronogramaEntity MapRequestToEntitySchedule(ScheduleDataToCreate schedule,
            Guid modulId, Guid periodId)
        {
            var response = new CronogramaEntity
            {
                Id = Guid.NewGuid(),
                FechaInicio = DateOnly.ParseExact(schedule.FechaInicio, "yyyy-MM-dd", null),
                FechaFin = DateOnly.ParseExact(schedule.FechaFin, "yyyy-MM-dd", null),
                Regularidad = schedule.Regularidad,
                Turno = schedule.Turno,
                Horario_Dias = schedule.Horario,
                Modalidad = schedule.Modalidad,
                Duracion_Semanas = schedule.Duracion,
                NroVacantes = schedule.Vacantes,
                NroHoras = 80,
                PeriodoId = periodId,
                ModuloId = modulId
            };
            return response;
        }
        public static CronogramaEntity MapRequestToEntitySchedule(ScheduleDataToCreate schedule, 
            Guid modulId, Guid periodId, Guid instrutorId)
        {
            var response = new CronogramaEntity
            {
                Id = Guid.NewGuid(),
                FechaInicio = DateOnly.ParseExact(schedule.FechaInicio, "yyyy-MM-dd", null),
                FechaFin = DateOnly.ParseExact(schedule.FechaFin, "yyyy-MM-dd", null),
                Regularidad = schedule.Regularidad,
                Turno = schedule.Turno,
                Horario_Dias = schedule.Horario,
                Modalidad = schedule.Modalidad,
                Duracion_Semanas = schedule.Duracion,
                NroVacantes = schedule.Vacantes,
                NroHoras = 80,
                PeriodoId = periodId,
                ModuloId = modulId,
                InstructorId = instrutorId
            };           
            return response;
        }
    }
}
