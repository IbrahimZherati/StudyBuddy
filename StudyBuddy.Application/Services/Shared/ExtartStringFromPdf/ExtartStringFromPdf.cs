using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Utils;
using System;
using System.Text;
namespace StudyBuddy.Application.Services.Shared.ExtartStringFromPdf;

public class ExtartStringFromPdf : IExtartStringFromPdf
{

    public string ExtractTextFromPdf(byte[] pdfBytes)
    {
        try
        {
            // Convert Base64 string to byte array

            // Create a memory stream from the bytes
            using (var memoryStream = new MemoryStream(pdfBytes))
            {
                // Open the PDF document
                using (var pdfReader = new PdfReader(memoryStream))
                using (var pdfDocument = new PdfDocument(pdfReader))
                {
                    var textBuilder = new StringBuilder();

                    // Loop through all pages
                    for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
                    {
                        var pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page));
                        textBuilder.AppendLine(pageText);
                    }

                    return textBuilder.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error extracting text from PDF: {ex.Message}", ex);
        }
    }
}
