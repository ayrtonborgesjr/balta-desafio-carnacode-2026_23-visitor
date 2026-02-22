using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Console;

public class Document
{
    public string Title { get; }
    public List<DocumentElement> Elements { get; }

    public Document(string title)
    {
        Title = title;
        Elements = new List<DocumentElement>();
    }

    public void AddElement(DocumentElement element)
    {
        Elements.Add(element);
    }

    public void Accept(IDocumentVisitor visitor)
    {
        foreach (var element in Elements)
        {
            element.Accept(visitor);
        }
    }
}