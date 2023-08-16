using FerminToroMS.Application.Requests;
using FerminToroMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Mappers
{
    public class EstudiantesMapper
    {
        //Metodo que transforma un request EmployeeSignUpRequest a un EmpleadoEntity
        public static EstudianteEntity MapRequestToEntity(StudentSignUpRequest request)
        {
            var entity = new EstudianteEntity()
            {
                Cedula = request.Cedula,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Correo = request.Correo,
                Direccion_Hab = request.Direccion,
                Es_Regular = false,
                Fecha_Nac = DateOnly.ParseExact(request.Fecha_Nac, "yyyy-MM-dd", null),
                Rango_Edad = request.Rango_Edad,
                Telefono = request.Telefono,
                Porcentaje_Beca = 0
            };
            return entity;
        }
    }
}
