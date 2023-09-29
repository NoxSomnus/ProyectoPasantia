using FerminToroMS.Application.CustomClasses;
using FerminToroMS.Core.Entities;
using GreenPipes.Caching.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Factories
{
    public class ApprovedPaymentFactory
    {
        public static MercantilApprovedPayments CreateMercantilApprovedPayments(PagosAprobadosEntity ApprovedPayment) 
        {
            var mercantil = new MercantilApprovedPayments
            {
                PagoAprobadoId = ApprovedPayment.Id,
                PagoId = ApprovedPayment.PagoId,
                ComprobanteIVA = ApprovedPayment.ComprobanteIVA,
                Divisa = ApprovedPayment.Pago.EnDivisa,
                FechaConciliacion = ApprovedPayment.FechaConciliacion.ToString("dd/MM/yyyy"),
                Empleado_Conciliador = ApprovedPayment.Nombre_Empleado,
                FechaTransaccion = ApprovedPayment.FechaTransaccion.ToString("dd/MM/yyyy"),
                Monto = ApprovedPayment.Pago.Monto,
                NroCuentaPagoMovil = ApprovedPayment.NroCuentaPagoMovil,
                NroTransaccion = ApprovedPayment.NroTransaccion,
                TasaBCV = (double)(ApprovedPayment.TasaBCV == null ? 0 : ApprovedPayment.TasaBCV),
                CodigoCronograma = ApprovedPayment.Pago.Inscripcion.Cronograma.Codigo
            };
            return mercantil;
        }

        public static BNCApprovedPayments CreateBNCApprovedPayments(PagosAprobadosEntity ApprovedPayment)
        {
            var BNC = new BNCApprovedPayments
            {
                CodigoCronograma = ApprovedPayment.Pago.Inscripcion.Cronograma.Codigo,
                PagoAprobadoId = ApprovedPayment.Id,
                PagoId = ApprovedPayment.PagoId,
                Divisa = ApprovedPayment.Pago.EnDivisa,
                FechaConciliacion = ApprovedPayment.FechaConciliacion.ToString("dd/MM/yyyy"),
                Empleado_Conciliador = ApprovedPayment.Nombre_Empleado,
                FechaTransaccion = ApprovedPayment.FechaTransaccion.ToString("dd/MM/yyyy"),
                Monto = ApprovedPayment.Pago.Monto,
                NroTransaccion = ApprovedPayment.NroTransaccion,
                TasaBCV = (double)(ApprovedPayment.TasaBCV == null ? 0 : ApprovedPayment.TasaBCV),
                NroCuenta = ApprovedPayment.NroCuentaPagoMovil != null ? ApprovedPayment.NroCuentaPagoMovil : " ",
                ComprobanteIVA = ApprovedPayment.ComprobanteIVA
            };
            return BNC;
        }

        public static PaypalApprovedPayments CreatePaypalApprovedPayments(PagosAprobadosEntity ApprovedPayment)
        {
            var paypal = new PaypalApprovedPayments
            {
                CodigoCronograma = ApprovedPayment.Pago.Inscripcion.Cronograma.Codigo,
                PagoAprobadoId = ApprovedPayment.Id,
                PagoId = ApprovedPayment.PagoId,
                Divisa = ApprovedPayment.Pago.EnDivisa,
                FechaConciliacion = ApprovedPayment.FechaConciliacion.ToString("dd/MM/yyyy"),
                Empleado_Conciliador = ApprovedPayment.Nombre_Empleado,
                FechaTransaccion = ApprovedPayment.FechaTransaccion.ToString("dd/MM/yyyy"),
                Monto = ApprovedPayment.Pago.Monto,
                NroTransaccion = ApprovedPayment.NroTransaccion,
                Correo = ApprovedPayment.Correo != null ? ApprovedPayment.Correo : " "
            };
            return paypal;
        }

        public static ZelleApprovedPayments CreateZelleApprovedPayments(PagosAprobadosEntity ApprovedPayment)
        {
            var zelle = new ZelleApprovedPayments
            {
                CodigoCronograma = ApprovedPayment.Pago.Inscripcion.Cronograma.Codigo,
                PagoAprobadoId = ApprovedPayment.Id,
                PagoId = ApprovedPayment.PagoId,
                Divisa = ApprovedPayment.Pago.EnDivisa,
                FechaConciliacion = ApprovedPayment.FechaConciliacion.ToString("dd/MM/yyyy"),
                Empleado_Conciliador = ApprovedPayment.Nombre_Empleado,
                FechaTransaccion = ApprovedPayment.FechaTransaccion.ToString("dd/MM/yyyy"),
                Monto = ApprovedPayment.Pago.Monto,
                NroTransaccion = ApprovedPayment.NroTransaccion,
                Correo = ApprovedPayment.Correo != null ? ApprovedPayment.Correo : " "
            };
            return zelle;
        }
        public static EfectivoApprovedPayments CreateEfectivoApprovedPayments(PagosAprobadosEntity ApprovedPayment) 
        {
            var efectivo = new EfectivoApprovedPayments 
            {
                PagoAprobadoId = ApprovedPayment.Id, 
                CodigoCronograma = ApprovedPayment.Pago.Inscripcion.Cronograma.Codigo,
                PagoId = ApprovedPayment.PagoId,
                Divisa = ApprovedPayment.Pago.EnDivisa,
                FechaConciliacion = ApprovedPayment.FechaConciliacion.ToString("dd/MM/yyyy"),
                Empleado_Conciliador = ApprovedPayment.Nombre_Empleado,
                FechaTransaccion = ApprovedPayment.FechaTransaccion.ToString("dd/MM/yyyy"),
                Monto = ApprovedPayment.Pago.Monto,
                NroTransaccion = ApprovedPayment.NroTransaccion                
            };
            return efectivo;
        }
    }
}
