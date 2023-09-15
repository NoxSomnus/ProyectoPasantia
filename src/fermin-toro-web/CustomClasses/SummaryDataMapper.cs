using FerminToroMS.Application.Responses;
using FerminToroWeb.Models;
using static Google.Apis.Requests.BatchRequest;

namespace FerminToroWeb.CustomClasses
{
    public class SummaryDataMapper
    {
        public static PeriodSummaryModel SummaryMap(PeriodSummaryResponse Response) 
        {
            var model = new PeriodSummaryModel
            {
                PeriodName = Response.PeriodName,
                TotalOnline = Response.TotalOnline,
                TotalOnPeriod = Response.TotalOnPeriod,
                TotalPresencial = Response.TotalPresencial,
                TotalOnModulsWithTurns = new List<TotalOnModulsWithTurns>(),
                TotalOnModuls = new List<TotalOnModuls>()
            };
            foreach (var modul in Response.TotalOnModulsWithTurns)
            {
                var modularray = modul.ModulCode.Split('-');
                modul.ModulCode = modularray[0];
                if (modul.ModulCode == "M1") modul.ModulCode = "Modulo I";
                if (modul.ModulCode == "M2") modul.ModulCode = "Modulo II";
                if (modul.ModulCode == "M3") modul.ModulCode = "Modulo III";
                model.TotalOnModulsWithTurns.Add(modul);
                var Procesado = model.TotalOnModuls.Any(x => x.ModuloId == modul.ModulId);
                if (!Procesado)
                {
                    var Modul = Response.TotalOnModulsWithTurns.Where(x => x.ModulId == modul.ModulId).ToList();
                    var totalOnModul = new TotalOnModuls
                    {
                        Modalidad = modul.Modalidad,
                        ModuloId = modul.ModulId,
                        Total = 0
                    };
                    var nombremoduloarray = modul.ModulCode.Split('-');
                    totalOnModul.NombreModulo = nombremoduloarray[0];
                    if (totalOnModul.NombreModulo == "M1") totalOnModul.NombreModulo = "Modulo I";
                    if (totalOnModul.NombreModulo == "M2") totalOnModul.NombreModulo = "Modulo II";
                    if (totalOnModul.NombreModulo == "M3") totalOnModul.NombreModulo = "Modulo III";
                    foreach (var modulo in Modul)
                    {
                        totalOnModul.Total = totalOnModul.Total + modulo.Total;
                    }
                    model.TotalOnModuls.Add(totalOnModul);
                }
                else
                {
                    continue;
                }
            }
            return model;
        }
    }
}
