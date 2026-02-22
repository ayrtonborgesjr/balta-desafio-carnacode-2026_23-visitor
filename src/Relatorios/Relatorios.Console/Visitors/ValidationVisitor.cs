using Relatorios.Console.Elements;

namespace Relatorios.Console.Visitors;

public class ValidationVisitor : IDocumentVisitor
{
    public bool IsValid { get; private set; } = true;

    public void Visit(Paragraph paragraph)
    {
        if (string.IsNullOrWhiteSpace(paragraph.Text))
            IsValid = false;
    }

    public void Visit(Image image)
    {
        if (string.IsNullOrWhiteSpace(image.Url) ||
            image.Width <= 0 ||
            image.Height <= 0)
            IsValid = false;
    }

    public void Visit(Table table)
    {
        if (table.Cells.Count == 0)
            IsValid = false;
    }
}