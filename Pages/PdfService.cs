using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using static AttendanceSystem.Pages.AttendanceSummary;

public class PdfService
{
    public byte[] GenerateAttendanceSummaryPdf(List<AttendanceSummaryModel> summaries)
    {
        using var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Verdana", 12, XFontStyle.Regular);

        int yPosition = 100;
        gfx.DrawString("Attendance Summary", new XFont("Verdana", 20, XFontStyle.Bold), XBrushes.Black, new XPoint(40, 60));

        foreach (var summary in summaries)
        {
            gfx.DrawString($"Student Name: {summary.StudentName}", font, XBrushes.Black, new XPoint(40, yPosition));
            gfx.DrawString($"Total Present Days: {summary.TotalPresentDays}", font, XBrushes.Black, new XPoint(40, yPosition + 20));
            gfx.DrawString($"Total Absent Days: {summary.TotalAbsentDays}", font, XBrushes.Black, new XPoint(40, yPosition + 40));
            gfx.DrawString($"Attendance Percentage: {summary.AttendancePercentage:F2}%", font, XBrushes.Black, new XPoint(40, yPosition + 60));
            yPosition += 100;
        }

        using var stream = new MemoryStream();
        document.Save(stream, false);
        return stream.ToArray();
    }
}