using Relatorios.Console.Visitors;

namespace Relatorios.Console.Elements;

public class Image : DocumentElement
{
    public string Url { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Alt { get; set; }

    public Image(string url, int width, int height)
    {
        Url = url;
        Width = width;
        Height = height;
        Alt = "";
    }

    public override void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}