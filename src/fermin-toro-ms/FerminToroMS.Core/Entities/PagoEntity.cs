using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Entities
{
    public class PagoEntity : BaseEntity
    {
        public DateTime Fecha { get; set; } //posiblemente se cambie a nulleable para la migracion
        public double Monto { get; set; }
        public bool PorCuotas { get; set; }
        public bool EnDivisa { get; set; }
        public string? URLComprobante { get; set; }
        public bool? EsAprobado { get; set; }
        public string? Comentarios { get; set; }
        public string? FechaPagoEfectivo { get; set; }
        public TimeOnly? HoraPagoEfectivo { get; set; }
        public bool EsJuridico { get; set; }
        public bool? CheckRetencion { get; set; }
        public int? NroRetencion { get; set; }
        public int? NroFactura { get; set; }
        public int? NroRecibo { get; set; }
        public bool? EsPagoDeAbono { get; set; }
        public Guid? PrimeraCuotaId { get; set; }
        public PagoEntity? PrimeraCuota { get; set; }
        public Guid InscripcionId { get; set; }
        public InscripcionEntity Inscripcion { get; set; } = null!;
        public Guid? EmpresaJuridicaId { get; set; }
        public DatoEmpresaJuridicaEntity? EmpresaJuridica { get; set; }
        public Guid MetodoPagoId { get; set; }
        public Metodo_PagoEntity MetodoPago { get; set; } = null!;
        public ICollection<AbonoEntity>? Abonos { get; set; }
    }
}
