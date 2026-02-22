using System.Text;
using Relatorios.Console.Elements;

namespace Relatorios.Console.Visitors;

public class HtmlExportVisitor : IDocumentVisitor
{
    private readonly StringBuilder _html = new();

    public void Visit(Paragraph paragraph)
    {
        _html.Append(
            $"<p style='font-family:{paragraph.FontFamily};font-size:{paragraph.FontSize}px'>" +
            $"{paragraph.Text}</p>");
    }

    public void Visit(Image image)
    {
        _html.Append(
            $"<img src='{image.Url}' width='{image.Width}' height='{image.Height}' alt='{image.Alt}' />");
    }

    public void Visit(Table table)
    {
        _html.Append("<table>");

        foreach (var row in table.Cells)
        {
            _html.Append("<tr>");
            foreach (var cell in row)
                _html.Append($"<td>{cell}</td>");
            _html.Append("</tr>");
        }

        _html.Append("</table>");
    }

    public string GetHtml(string title)
    {
        return $"<html><head><title>{title}</title></head><body>{_html}</body></html>";
    }
}