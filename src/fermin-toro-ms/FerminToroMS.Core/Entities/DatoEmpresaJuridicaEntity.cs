using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class DatoEmpresaJuridicaEntity : BaseEntity
    {
        public string NombreEmpresa { get; set; } = null!;
        public string URL_Rif { get; set; } = null!;
        public string Correo_Administrativo { get; set; } = null!;
        public string Telefono_Administrativo { get; set; } = null!;

        public ICollection<PagoEntity>? PagosJuridicos { get; set; }
    }
}
