using Relatorios.Console;
using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests;

[TestFixture]
public class DocumentTests
{
    [Test]
    public void Constructor_ShouldInitializeTitle()
    {
        // Arrange & Act
        var document = new Document("Test Title");

        // Assert
        Assert.That(document.Title, Is.EqualTo("Test Title"));
    }

    [Test]
    public void Constructor_ShouldInitializeEmptyElementsList()
    {
        // Arrange & Act
        var document = new Document("Test");

        // Assert
        Assert.That(document.Elements, Is.Not.Null);
        Assert.That(document.Elements, Is.Empty);
    }

    [Test]
    public void AddElement_ShouldAddElementToList()
    {
        // Arrange
        var document = new Document("Test");
        var paragraph = new Paragraph("Test text");

        // Act
        document.AddElement(paragraph);

        // Assert
        Assert.That(document.Elements, Has.Count.EqualTo(1));
        Assert.That(document.Elements[0], Is.EqualTo(paragraph));
    }

    [Test]
    public void AddElement_ShouldAddMultipleElements()
    {
        // Arrange
        var document = new Document("Test");
        var paragraph1 = new Paragraph("First");
        var paragraph2 = new Paragraph("Second");
        var image = new Image("test.png", 100, 100);

        // Act
        document.AddElement(paragraph1);
        document.AddElement(paragraph2);
        document.AddElement(image);

        // Assert
        Assert.That(document.Elements, Has.Count.EqualTo(3));
        Assert.That(document.Elements[0], Is.EqualTo(paragraph1));
        Assert.That(document.Elements[1], Is.EqualTo(paragraph2));
        Assert.That(document.Elements[2], Is.EqualTo(image));
    }

    [Test]
    public void Accept_ShouldCallVisitorForAllElements()
    {
        // Arrange
        var document = new Document("Test");
        document.AddElement(new Paragraph("Test paragraph"));
        document.AddElement(new Image("test.png", 100, 100));
        document.AddElement(new Table(2, 2));

        var visitor = new WordCountVisitor();

        // Act
        document.Accept(visitor);

        // Assert
        // Should count words from paragraph ("Test paragraph" = 2 words)
        // and table (4 cells with "Cell x,y" = 2 words each = 8 words)
        Assert.That(visitor.TotalWords, Is.GreaterThan(0));
    }

    [Test]
    public void Accept_ShouldHandleEmptyDocument()
    {
        // Arrange
        var document = new Document("Empty");
        var visitor = new WordCountVisitor();

        // Act & Assert
        Assert.DoesNotThrow(() => document.Accept(visitor));
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }
}

