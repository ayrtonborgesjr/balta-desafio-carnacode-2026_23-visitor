using Relatorios.Console.Elements;

namespace Relatorios.Console.Visitors;

public interface IDocumentVisitor
{
    void Visit(Paragraph paragraph);
    void Visit(Image image);
    void Visit(Table table);
}