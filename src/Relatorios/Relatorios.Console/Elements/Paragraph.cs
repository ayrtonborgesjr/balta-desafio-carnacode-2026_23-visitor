using Relatorios.Console.Visitors;

namespace Relatorios.Console.Elements;

public class Paragraph : DocumentElement
{
    public string Text { get; set; }
    public string FontFamily { get; set; }
    public int FontSize { get; set; }

    public Paragraph(string text)
    {
        Text = text;
        FontFamily = "Arial";
        FontSize = 12;
    }

    public override void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}