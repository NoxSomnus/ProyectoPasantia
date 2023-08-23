using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class DatoEmpresaJuridicaEntity : BaseEntity
    {
        public string NombreEmpresa { get; set; } = "No registrado";
        public string URL_Rif { get; set; } = null!;
        public string Correo_Administrativo { get; set; } = "No registrado";
        public string Telefono_Administrativo { get; set; } = "No registrado";

        public ICollection<PagoEntity>? PagosJuridicos { get; set; }
    }
}
