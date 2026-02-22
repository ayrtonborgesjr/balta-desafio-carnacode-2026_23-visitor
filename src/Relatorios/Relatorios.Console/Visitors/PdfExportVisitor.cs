using System.Text;
using Relatorios.Console.Elements;

namespace Relatorios.Console.Visitors;

public class PdfExportVisitor : IDocumentVisitor
{
    private readonly StringBuilder _pdf = new();

    public void Visit(Paragraph paragraph)
    {
        _pdf.Append($"PDF_TEXT({paragraph.Text}) ");
    }

    public void Visit(Image image)
    {
        _pdf.Append($"PDF_IMAGE({image.Url}) ");
    }

    public void Visit(Table table)
    {
        _pdf.Append($"PDF_TABLE({table.Cells.Count}) ");
    }

    public string GetPdf(string title)
    {
        return $"PDF_DOCUMENT({title}) {_pdf}";
    }
}