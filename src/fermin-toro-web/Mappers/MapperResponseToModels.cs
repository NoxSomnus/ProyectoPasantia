using FerminToroMS.Application.Responses;
using FerminToroWeb.Models;
using System.Globalization;
using static Google.Apis.Requests.BatchRequest;

namespace FerminToroWeb.Mappers
{
    public class MapperResponseToModels
    {
        public string DeterminatePeriodColor(AllPeriodsModel _period)
        {
            string color = "gray";
            string fechainicio = "01/"+_period.MesInicio+"/"+_period.Año;
            string formato = "dd/MMMM/yyyy";
            CultureInfo culture = new CultureInfo("es-ES");
            DateTime fechaInicio = DateTime.ParseExact(fechainicio, formato, culture);
            string fechafin = "30/" + _period.MesFin + "/" + _period.Año;
            if (_period.MesFin == "febrero") 
            {
                fechafin = "28/" + _period.MesFin + "/" + _period.Año;
            }
            
            DateTime fechaFin = DateTime.ParseExact(fechafin, formato, culture);
            if (fechaInicio > fechaFin) 
            {
                int año = _period.Año - 1;
                fechainicio = "01/"+_period.MesInicio + "/" + año.ToString();
                fechaInicio = DateTime.ParseExact(fechainicio, formato, culture);
            }
            if (_period.Año == DateTime.Now.Year)
            {
                if (DateTime.Now >= fechaInicio && DateTime.Now <= fechaFin) 
                {
                    return "green";
                }
                if (fechaInicio > DateTime.Now)
                {
                    return "yellow";
                }
            }
            if (_period.Año > DateTime.Now.Year) return "yellow";
            return color;
        }
        public List<AllPeriodsModel> MapPeriodResponseToModel(List<PeriodResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var ViewModel = new List<AllPeriodsModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var period in response)
            {
                var model = new AllPeriodsModel
                {
                    Id = period.PeriodId,
                    Nombre = period.PeriodName,
                    Año = period.Año,
                    MesFin = period.MesFin,
                    MesInicio = period.MesInicio,
                };
                model.Color = DeterminatePeriodColor(model);
                ViewModel.Add(model);
            }
            return ViewModel;
        }
        public List<AllEmployeesModel> MapAllEmployeesResponseToModel(List<AllEmployeesResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var employeesViewModel = new List<AllEmployeesModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var employees in response)
            {
                var model = new AllEmployeesModel
                {
                    Id = employees.Id,
                    Cedula = employees.Cedula,
                    Nombre = employees.Nombre,
                    Username = employees.Username
                };
                employeesViewModel.Add(model);
            }
            return employeesViewModel;
        }

        public UpdateEmployeeModel MapResponseToUpdateEmployeeModel(EmployeeResponse employee, List<PermissionResponse> _permisos) 
        {
            var model = new UpdateEmployeeModel
            {
                Id = employee.Id,
                Cedula = employee.Cedula,
                Nombre = employee.Nombre,
                Apellido = employee.Apellido,
                Correo = employee.Correo,
                Username = employee.Username,
                esAdmin = employee.esAdmin,
                esDirector = employee.esDirector,
                esInstructor = employee.esInstructor,
                permisos_asignados = employee.permisos_asignados,
                permisos = _permisos
            };
            return model;
        }
    }
}
