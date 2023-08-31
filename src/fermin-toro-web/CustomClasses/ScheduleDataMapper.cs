namespace FerminToroWeb.CustomClasses
{
    public class ScheduleDataMapper
    {
        public static List<ScheduleDataToUpdate> ScheduleDataToUpdateMap(string PeriodoId, List<string> ScheduleId, List<string> programa, List<string> modulos,
            List<string> fechaInicio, List<string> fechaFin, List<string> regularidad,
            List<string> turno, List<string> horario, List<string> modalidad, List<int> duracion,
            List<int> vacantes, List<string> instructor, List<bool> habilitado)
        {
            var cronogramas = new List<ScheduleDataToUpdate>();
            int i = 1;
            for (; i < habilitado.Count; i++)
            {
                if (turno[i] == "Matutino") turno[i] = "0";
                if (turno[i] == "Vespertino") turno[i] = "1";
                if (turno[i] == "Nocturno") turno[i] = "2";
                if (turno[i] == "Sabatino") turno[i] = "3";
                if (modalidad[i] == "Presencial") modalidad[i] = "0";
                if (modalidad[i] == "Online") modalidad[i] = "1";
                if (regularidad[i] == "Regular") regularidad[i] = "0";
                if (regularidad[i] == "Intensivo") regularidad[i] = "1";
                if (regularidad[i] == "SemiIntensivo") regularidad[i] = "2";
                var cronograma = new ScheduleDataToUpdate();
                cronograma.ScheduleId = ScheduleId[i];
                cronograma.Programa = programa[i];
                cronograma.Modulo = modulos[i];
                cronograma.FechaInicio = fechaInicio[i];
                cronograma.FechaFin = fechaFin[i];
                cronograma.Regularidad = int.Parse(regularidad[i]);
                cronograma.Turno = int.Parse(turno[i]);
                cronograma.Horario = horario[i];
                cronograma.Modalidad = int.Parse(modalidad[i]);
                    cronograma.Duracion = duracion[i];
                cronograma.Vacantes = vacantes[i];
                cronograma.InstructorId = instructor[i];
                cronograma.Habilitado = habilitado[i];
                cronogramas.Add(cronograma);
            }
            for (; i < programa.Count; i++)
            {
                var cronograma = new ScheduleDataToUpdate();

                cronograma.ScheduleId = null;
                cronograma.Programa = programa[i];
                cronograma.Modulo = modulos[i];
                cronograma.FechaInicio = fechaInicio[i];
                cronograma.FechaFin = fechaFin[i];
                cronograma.Regularidad = int.Parse(regularidad[i]);
                cronograma.Turno = int.Parse(turno[i]);
                cronograma.Horario = horario[i];
                cronograma.Modalidad = int.Parse(modalidad[i]);
                cronograma.Duracion = duracion[i];
                cronograma.Vacantes = vacantes[i];
                cronograma.InstructorId = instructor[i];
                cronograma.Habilitado = true;
                cronogramas.Add(cronograma);
            }
            return cronogramas;
        }

        public static List<ScheduleDataToCreate> ScheduleDataToAddMap(List<string> programa, List<string> modulos,
            List<string> fechaInicio, List<string> fechaFin, List<int> regularidad,
            List<int> turno, List<string> horario, List<int> modalidad, List<int> duracion,
            List<int> vacantes, List<string> instructor)
        {
            var cronogramas = new List<ScheduleDataToCreate>();
            for (int i = 1; i < programa.Count; i++)
            {
                var cronograma = new ScheduleDataToCreate
                {
                    Programa = programa[i],
                    Modulo = modulos[i],
                    FechaInicio = fechaInicio[i],
                    FechaFin = fechaFin[i],
                    Regularidad = regularidad[i],
                    Turno = turno[i],
                    Horario = horario[i],
                    Modalidad = modalidad[i],
                    Duracion = duracion[i],
                    Vacantes = vacantes[i],
                    InstructorId = instructor[i]
                };
                cronogramas.Add(cronograma);
            }
            return cronogramas;
        }
    }
}
