using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Visitors;

[TestFixture]
public class ValidationVisitorTests
{
    [Test]
    public void IsValid_InitialValue_ShouldBeTrue()
    {
        // Arrange & Act
        var visitor = new ValidationVisitor();

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Visit_ValidParagraph_ShouldRemainValid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var paragraph = new Paragraph("Valid text");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Visit_ParagraphWithEmptyText_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var paragraph = new Paragraph("");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ParagraphWithWhitespace_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var paragraph = new Paragraph("   ");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ParagraphWithNull_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var paragraph = new Paragraph("test");
        paragraph.Text = null!;

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ValidImage_ShouldRemainValid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 800, 600);

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Visit_ImageWithEmptyUrl_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 100, 100);
        image.Url = "";

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ImageWithWhitespaceUrl_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 100, 100);
        image.Url = "   ";

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ImageWithNullUrl_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 100, 100);
        image.Url = null!;

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ImageWithZeroWidth_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 0, 100);

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ImageWithNegativeWidth_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", -100, 100);

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ImageWithZeroHeight_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 100, 0);

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ImageWithNegativeHeight_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var image = new Image("test.png", 100, -100);

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_ValidTable_ShouldRemainValid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var table = new Table(2, 2);

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Visit_TableWithZeroRows_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var table = new Table(0, 2);

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_TableWithEmptyCells_ShouldBeInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var table = new Table(2, 2);
        table.Cells.Clear();

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_MultipleValidElements_ShouldRemainValid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var paragraph = new Paragraph("Valid text");
        var image = new Image("test.png", 100, 100);
        var table = new Table(2, 2);

        // Act
        visitor.Visit(paragraph);
        visitor.Visit(image);
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Visit_OneInvalidElement_ShouldMakeDocumentInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var paragraph1 = new Paragraph("Valid text");
        var paragraph2 = new Paragraph(""); // Invalid
        var paragraph3 = new Paragraph("Also valid");

        // Act
        visitor.Visit(paragraph1);
        visitor.Visit(paragraph2);
        visitor.Visit(paragraph3);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_InvalidAfterValid_ShouldStayInvalid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var invalidParagraph = new Paragraph("");
        var validParagraph = new Paragraph("Valid text");

        // Act
        visitor.Visit(invalidParagraph);
        visitor.Visit(validParagraph);

        // Assert
        Assert.That(visitor.IsValid, Is.False);
    }

    [Test]
    public void Visit_SingleRow_Table_ShouldBeValid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var table = new Table(1, 5);

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Visit_LargeValidTable_ShouldBeValid()
    {
        // Arrange
        var visitor = new ValidationVisitor();
        var table = new Table(100, 50);

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }
}

