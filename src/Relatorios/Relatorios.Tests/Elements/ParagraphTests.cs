using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Elements;

[TestFixture]
public class ParagraphTests
{
    [Test]
    public void Constructor_ShouldInitializeText()
    {
        // Arrange & Act
        var paragraph = new Paragraph("Test text");

        // Assert
        Assert.That(paragraph.Text, Is.EqualTo("Test text"));
    }

    [Test]
    public void Constructor_ShouldSetDefaultFontFamily()
    {
        // Arrange & Act
        var paragraph = new Paragraph("Test");

        // Assert
        Assert.That(paragraph.FontFamily, Is.EqualTo("Arial"));
    }

    [Test]
    public void Constructor_ShouldSetDefaultFontSize()
    {
        // Arrange & Act
        var paragraph = new Paragraph("Test");

        // Assert
        Assert.That(paragraph.FontSize, Is.EqualTo(12));
    }

    [Test]
    public void Text_ShouldBeSettable()
    {
        // Arrange
        var paragraph = new Paragraph("Initial");

        // Act
        paragraph.Text = "Modified";

        // Assert
        Assert.That(paragraph.Text, Is.EqualTo("Modified"));
    }

    [Test]
    public void FontFamily_ShouldBeSettable()
    {
        // Arrange
        var paragraph = new Paragraph("Test");

        // Act
        paragraph.FontFamily = "Times New Roman";

        // Assert
        Assert.That(paragraph.FontFamily, Is.EqualTo("Times New Roman"));
    }

    [Test]
    public void FontSize_ShouldBeSettable()
    {
        // Arrange
        var paragraph = new Paragraph("Test");

        // Act
        paragraph.FontSize = 16;

        // Assert
        Assert.That(paragraph.FontSize, Is.EqualTo(16));
    }

    [Test]
    public void Accept_ShouldCallVisitorVisitMethod()
    {
        // Arrange
        var paragraph = new Paragraph("Test text");
        var visitor = new WordCountVisitor();

        // Act
        paragraph.Accept(visitor);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(2));
    }

    [Test]
    public void Accept_ShouldWorkWithHtmlVisitor()
    {
        // Arrange
        var paragraph = new Paragraph("Test");
        var visitor = new HtmlExportVisitor();

        // Act
        paragraph.Accept(visitor);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("<p"));
        Assert.That(html, Does.Contain("Test"));
    }
}

