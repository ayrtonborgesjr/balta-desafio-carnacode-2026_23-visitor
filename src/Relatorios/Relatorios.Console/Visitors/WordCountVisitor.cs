using Relatorios.Console.Elements;

namespace Relatorios.Console.Visitors;

public class WordCountVisitor : IDocumentVisitor
{
    public int TotalWords { get; private set; }

    public void Visit(Paragraph paragraph)
    {
        TotalWords += paragraph.Text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Length;
    }

    public void Visit(Image image)
    {
        // imagens não contam palavras
    }

    public void Visit(Table table)
    {
        foreach (var row in table.Cells)
        {
            foreach (var cell in row)
            {
                TotalWords += cell
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Length;
            }
        }
    }
}