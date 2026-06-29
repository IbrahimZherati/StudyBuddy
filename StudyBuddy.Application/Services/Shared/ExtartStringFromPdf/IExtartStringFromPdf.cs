namespace StudyBuddy.Application.Services.Shared.ExtartStringFromPdf;

public interface IExtartStringFromPdf
{
    string ExtractTextFromPdf(byte[] pdfBytes);
}
