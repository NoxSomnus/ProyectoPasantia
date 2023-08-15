using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public bool Success { get; set; }// indica si la conexion fue exitosa o no
        public bool IsDirector { get; set; }
        public bool IsAdmin { get; set; }
    }
}
