using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace FerminToroWeb.Models
{
    public class AddEmployeeModel
    {
        [Required(ErrorMessage = "Introduzca la cedula del empleado")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de Cédula debe ser un valor numérico.")]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "La longitud del número de Cédula debe estar entre 7 y 8 caracteres.")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca el nombre del empleado")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca el apellido empleado")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca el nombre de usuario del empleado")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca la contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener minimo 8 caracteres")]
        public string Password { get; set; } = null!;

        public bool esAdmin { get; set; }
        public bool esInstructor { get; set; }
        public bool esDirector { get; set; }
        public List<PermissionResponse> permisos { get; set; } = null!;
        public bool Error { get; set; }

        public AddEmployeeModel() 
        {
            Cedula = "";
            Name = "";
            LastName = "";
            Email = "";
            UserName = "";
            Password = "";
            esAdmin = false;
            esDirector = false;
            esInstructor = false;
            permisos = new List<PermissionResponse>();
            Error = false;
        }

    }
}
