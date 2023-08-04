using FerminToroMS.Application.Requests;
using FerminToroMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Mappers
{
    public class EmpleadosMapper
    {
        //Metodo que transforma un request EmployeeSignUpRequest a un EmpleadoEntity
        public static EmpleadoEntity MapRequestToEntity(EmployeeSignUpRequest request, List<Empleado_PermisoEntity> permisos, Guid UserId)
        {
            var entity = new EmpleadoEntity()
            {
                Id = UserId,
                Cedula = request.Cedula,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Correo = request.Correo,
                esAdmin = request.esAdmin,
                esDirector = request.esDirector,
                esInstructor = request.esInstructor,
                Username = request.Username,
                Password = request.Password,
                Permisos = permisos
            };
            return entity;
        }
    }
}
