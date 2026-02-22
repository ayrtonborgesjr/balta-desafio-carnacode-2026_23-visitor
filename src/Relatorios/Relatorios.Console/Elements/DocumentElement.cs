using Relatorios.Console.Visitors;

namespace Relatorios.Console.Elements;

public abstract class DocumentElement
{
    public abstract void Accept(IDocumentVisitor visitor);
}