using FerminToroMS.Application.Responses;
using FerminToroWeb.Models;
using static Google.Apis.Requests.BatchRequest;

namespace FerminToroWeb.Mappers
{
    public class MapperResponseToModels
    {
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
                permisos_nuevos = employee.permisos_asignados,
                permisos = _permisos
            };
            return model;
        }
    }
}
