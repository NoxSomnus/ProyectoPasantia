using FerminToroMS.Application.Responses;
using System.ComponentModel.DataAnnotations;

namespace FerminToroWeb.Models
{
    public class UpdateEmployeeModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Introduzca su cedula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de Cédula debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de Cédula debe estar entre 7 y 8 caracteres.")]
        public string Cedula { get; set; } = null!;
        [Required(ErrorMessage = "Introduzca su nombre")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Introduzca su apellido")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string Correo { get; set; } = null!;
        [Required(ErrorMessage = "Introduzca su nombre de usuario")]
        public string Username { get; set; } = null!;
        public bool esAdmin { get; set; }
        public bool esDirector { get; set; }
        public bool esInstructor { get; set; }
        public List<PermissionResponse>? permisos_asignados { get; set; }
        public List<PermissionResponse> permisos { get; set; } = null!;
    }
}
