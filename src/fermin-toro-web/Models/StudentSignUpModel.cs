using System.ComponentModel.DataAnnotations;

namespace FerminToroWeb.Models
{
    public class StudentSignUpModel
    {
        [Required(ErrorMessage = "Introduzca su cédula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de Cédula debe ser un valor numérico.")]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "La longitud del número de Cédula debe estar entre 7 y 9 caracteres.")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca su nombre")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca su apellido")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca su numero de teléfono - Sin guiones - Ejemplo (0424xxxxxx)")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de teléfono debe ser solo numeros.")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca su dirección de habitación")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "Introduzca su fecha de nacimiento")]
        public string Fecha_Nac { get; set; } = null!;

        [Required(ErrorMessage = "Elija su rango de edad")]
        public string Rango_Edad { get; set; } = null!;
        public bool Error { get; set; }

        public StudentSignUpModel() 
        {
            Cedula = "";
            Nombre = "";
            Apellido = "";
            Correo = "";
            Telefono = "";
            Direccion = "";
            Fecha_Nac = "";
            Rango_Edad = "";
            Error = false;
        }
    }
}
