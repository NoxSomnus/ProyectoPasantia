using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class EmpresaJuridicaResponse
    {
        public string? Nombre_Empresa { get; set; }
        public string UrlRif { get; set; } = null!;
        public string? TelefonoAdministrativo { get; set; }
        public string? CorreoAdministrativo { get; set; }
    }
}
