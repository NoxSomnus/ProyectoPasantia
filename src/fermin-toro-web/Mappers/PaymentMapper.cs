using FerminToroMS.Application.Responses;
using FerminToroWeb.CustomClasses;
using FerminToroWeb.Models;
using System.Globalization;

namespace FerminToroWeb.Mappers
{
    public class PaymentMapper
    {
        public static List<UpdatePaymentState> PaymentDataToUpdateMap(List<string> PaymentId,
            List<string> Estado, List<double> Monto, List<string> NroTransaccion, List<string> NroCuenta,
            List<string> Correo, List<string> FechaTransaccion,
            List<string> comprobanteIVA, List<string> TasaBCV) 
        {
            var payments = new List<UpdatePaymentState>();
            for (int i=0; i < PaymentId.Count; i++)
            {
                var payment = new UpdatePaymentState
                {
                    Monto = Monto[i],
                    PaymentId = PaymentId[i],
                    State = Estado[i],
                    AccountNumber = NroCuenta[i],
                    ComprobanteIVA = comprobanteIVA[i],
                    Email = Correo[i],
                    TransactionDate = FechaTransaccion[i],
                    TransactionNumber = NroTransaccion[i] == null ? string.Empty : NroTransaccion[i],
                };
                if (TasaBCV[i].Contains("."))
                {
                    payment.TasaBCV = double.Parse(TasaBCV[i], CultureInfo.GetCultureInfo("en-US"));
                }
                else 
                {
                    payment.TasaBCV = double.Parse(TasaBCV[i], CultureInfo.GetCultureInfo("es-ES"));
                }
                payments.Add(payment);
            }
            return payments;
        }

        public static int ObtenerValorMetodoPago(AllComprobantes cadena)
        {
            if (cadena.MetodoPago == "Banco Mercantil") return 1;
            if (cadena.MetodoPago == "Banco Nacional de Crédito") return 2;
            if (cadena.MetodoPago == "Paypal") return 3;
            if (cadena.MetodoPago == "Zelle") return 4;
            if (cadena.MetodoPago == "Bank of America") return 5;
            return 100;
        }

        public static CheckComprobantesModel MapResponseToCheckComprobantesModel(AllComprobantesByScheduleIdResponse response) 
        {
            var model = new CheckComprobantesModel
            {
                CourseCompleteName = response.CourseCompleteName,
                Horario = response.Horario,
                Code = response.Code,
                StartDate = response.StartDate,
                EndDate = response.EndDate,
                Instructor = response.Instructor,
                Modalidad = response.Modalidad,
                ModulCompleteName = response.ModulCompleteName,
                ModulId = response.ModulId,
                ModulName = response.ModulName,
                Regularidad = response.Regularidad,
                Turno = response.Turno,
                BNC = new List<AllComprobantes>(),
                Mercantil = new List<AllComprobantes>(),
                Paypal = new List<AllComprobantes>(),
                Zelle = new List<AllComprobantes>(),
            };
            foreach (var comprobante in response.Comprobantes) 
            {
                if (comprobante.MetodoPago == "Banco Mercantil") model.Mercantil.Add(comprobante);
                if (comprobante.MetodoPago == "Banco Nacional de Crédito") model.BNC.Add(comprobante); 
                if (comprobante.MetodoPago == "Paypal") model.Paypal.Add(comprobante); 
                if (comprobante.MetodoPago == "Zelle") model.Zelle.Add(comprobante); 
            }
            return model;
        }
    }
}
