using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Visitors;

[TestFixture]
public class HtmlExportVisitorTests
{
    [Test]
    public void Visit_Paragraph_ShouldGenerateHtmlParagraph()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var paragraph = new Paragraph("Test text");

        // Act
        visitor.Visit(paragraph);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("<p"));
        Assert.That(html, Does.Contain("Test text"));
        Assert.That(html, Does.Contain("</p>"));
    }

    [Test]
    public void Visit_Paragraph_ShouldIncludeFontFamily()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var paragraph = new Paragraph("Test") { FontFamily = "Times New Roman" };

        // Act
        visitor.Visit(paragraph);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("font-family:Times New Roman"));
    }

    [Test]
    public void Visit_Paragraph_ShouldIncludeFontSize()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var paragraph = new Paragraph("Test") { FontSize = 16 };

        // Act
        visitor.Visit(paragraph);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("font-size:16px"));
    }

    [Test]
    public void Visit_Image_ShouldGenerateHtmlImage()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var image = new Image("test.png", 800, 600);

        // Act
        visitor.Visit(image);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("<img"));
        Assert.That(html, Does.Contain("src='test.png'"));
        Assert.That(html, Does.Contain("width='800'"));
        Assert.That(html, Does.Contain("height='600'"));
    }

    [Test]
    public void Visit_Image_ShouldIncludeAltText()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var image = new Image("test.png", 100, 100) { Alt = "Test image" };

        // Act
        visitor.Visit(image);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("alt='Test image'"));
    }

    [Test]
    public void Visit_Table_ShouldGenerateHtmlTable()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var table = new Table(2, 2);

        // Act
        visitor.Visit(table);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("<table>"));
        Assert.That(html, Does.Contain("</table>"));
        Assert.That(html, Does.Contain("<tr>"));
        Assert.That(html, Does.Contain("</tr>"));
        Assert.That(html, Does.Contain("<td>"));
        Assert.That(html, Does.Contain("</td>"));
    }

    [Test]
    public void Visit_Table_ShouldIncludeAllCells()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var table = new Table(2, 2);

        // Act
        visitor.Visit(table);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("Cell 0,0"));
        Assert.That(html, Does.Contain("Cell 0,1"));
        Assert.That(html, Does.Contain("Cell 1,0"));
        Assert.That(html, Does.Contain("Cell 1,1"));
    }

    [Test]
    public void GetHtml_ShouldIncludeTitle()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();

        // Act
        var html = visitor.GetHtml("Test Title");

        // Assert
        Assert.That(html, Does.Contain("<title>Test Title</title>"));
    }

    [Test]
    public void GetHtml_ShouldGenerateCompleteHtmlDocument()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        visitor.Visit(new Paragraph("Test"));

        // Act
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.StartWith("<html>"));
        Assert.That(html, Does.EndWith("</html>"));
        Assert.That(html, Does.Contain("<head>"));
        Assert.That(html, Does.Contain("</head>"));
        Assert.That(html, Does.Contain("<body>"));
        Assert.That(html, Does.Contain("</body>"));
    }

    [Test]
    public void Visit_MultipleElements_ShouldIncludeAllInOrder()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();
        var paragraph = new Paragraph("First");
        var image = new Image("test.png", 100, 100);
        var table = new Table(1, 1);

        // Act
        visitor.Visit(paragraph);
        visitor.Visit(image);
        visitor.Visit(table);
        var html = visitor.GetHtml("Document");

        // Assert
        var firstIndex = html.IndexOf("First");
        var imageIndex = html.IndexOf("<img");
        var tableIndex = html.IndexOf("<table>");

        Assert.That(firstIndex, Is.LessThan(imageIndex));
        Assert.That(imageIndex, Is.LessThan(tableIndex));
    }

    [Test]
    public void GetHtml_EmptyDocument_ShouldGenerateValidHtml()
    {
        // Arrange
        var visitor = new HtmlExportVisitor();

        // Act
        var html = visitor.GetHtml("Empty");

        // Assert
        Assert.That(html, Does.Contain("<html>"));
        Assert.That(html, Does.Contain("</html>"));
        Assert.That(html, Does.Contain("<title>Empty</title>"));
    }
}

