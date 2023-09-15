using FerminToroMS.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FerminToroWeb.PdfGenerator
{
    public class PaymentPdfGenerator
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PaymentPdfGenerator(IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public FileStreamResult GenerateComprobantesPdf(AllComprobantesByScheduleIdResponse response)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imagesPath = Path.Combine(wwwrootPath, "img");
                        string logoPath = Path.Combine(imagesPath, "logos");
                        string filePath = Path.Combine(logoPath, "Logo_dark_letras.png");

                        byte[] imgdata = File.ReadAllBytes(filePath);
                        //row.ConstantItem(140).Height(60).Placeholder();
                        row.ConstantItem(150).Image(imgdata);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Academia Fermín Toro").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Comprobantes de pago").FontSize(12);
                            col.Item().AlignCenter().Text("Nombre del Curso: "+ response.Code).FontSize(10);
                            col.Item().AlignCenter().Text("Reporte Generado en: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).FontSize(12);

                        });


                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {

                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("Datos del Programa").Underline().Bold();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Programa: ").SemiBold().FontSize(10);
                                txt.Span(response.CourseCompleteName).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Modalidad: ").SemiBold().FontSize(10);
                                txt.Span(response.Modalidad).FontSize(10);
                            });
                            col2.Item().Text(txt =>
                            {
                                txt.Span("Turno: ").SemiBold().FontSize(10);
                                txt.Span(response.Turno).FontSize(10);
                            });
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Nro Inscripcion").FontColor("#fff");
                                header.Cell().Background("#257272")
                               .Padding(2).Text("Estudiante").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Metodo de Pago").FontColor("#fff");
                                header.Cell().Background("#257272")
                               .Padding(2).Text("Modalidad de Pago").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Comprobante").FontColor("#fff");
                            });
                            var total = 0;
                            foreach (var comprobante in response.Comprobantes)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(comprobante.NroInscripcion).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(comprobante.StudentName).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(comprobante.MetodoPago).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(comprobante.Cuota ? "Cuotas" : "Completo").FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(comprobante.UrlComprobante).FontSize(10);
                                total++;
                            }

                            col1.Item().AlignRight().Text("Cantidad de Comprobantes No Revisados: " + total).FontSize(12);
                            col1.Spacing(10);

                        });
                        col1.Spacing(10);
                    });


                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();


            Stream stream = new MemoryStream(document);
            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = response.Code+"_comprobantes_"+DateTime.Now.ToString("dd/MM/yyyy")+".pdf"
            };
        }
    }
}
