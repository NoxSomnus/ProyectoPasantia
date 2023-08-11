using FerminToroMS.Application.Responses;
using FerminToroWeb.ApiUrlConfig;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FerminToroWeb.Models
{
    public class AddEmployeeModel
    {
        [Required(ErrorMessage = "Introduzca su cedula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de Cédula debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de Cédula debe estar entre 7 y 8 caracteres.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Introduzca su nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Introduzca su apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Introduzca su nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduzca la contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener minimo 8 caracteres")]
        public string Password { get; set; }

        public bool esAdmin { get; set; }
        public bool esInstructor { get; set; }
        public bool esDirector { get; set; }
        public List<PermissionResponse> permisos { get; set; }

        /*public AddEmployeeModel()
        {
            Email = "";
            Password = "";
            Name = "";
            Cedula = "";
            LastName = "";
            UserName = "";
            esAdmin = false;
            esDirector = false;
            esInstructor = false;
            permisos = new List<PermissionResponse>();
        }*/
    }
}
