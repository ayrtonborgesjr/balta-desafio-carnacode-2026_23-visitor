using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Visitors;

[TestFixture]
public class WordCountVisitorTests
{
    [Test]
    public void TotalWords_InitialValue_ShouldBeZero()
    {
        // Arrange & Act
        var visitor = new WordCountVisitor();

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }

    [Test]
    public void Visit_Paragraph_ShouldCountWords()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph = new Paragraph("Hello world");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(2));
    }

    [Test]
    public void Visit_ParagraphWithSingleWord_ShouldCountOne()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph = new Paragraph("Hello");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(1));
    }

    [Test]
    public void Visit_ParagraphWithMultipleSpaces_ShouldCountCorrectly()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph = new Paragraph("Hello    world    test");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(3));
    }

    [Test]
    public void Visit_EmptyParagraph_ShouldCountZero()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph = new Paragraph("");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }

    [Test]
    public void Visit_ParagraphWithOnlySpaces_ShouldCountZero()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph = new Paragraph("   ");

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }

    [Test]
    public void Visit_Image_ShouldNotCountWords()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var image = new Image("test.png", 100, 100);

        // Act
        visitor.Visit(image);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }

    [Test]
    public void Visit_Table_ShouldCountWordsInCells()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var table = new Table(2, 2);
        // Default cells: "Cell 0,0", "Cell 0,1", "Cell 1,0", "Cell 1,1"
        // Each cell has 2 words, total = 8 words

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(8));
    }

    [Test]
    public void Visit_TableWithCustomText_ShouldCountCorrectly()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var table = new Table(2, 2);
        table.Cells[0][0] = "One";
        table.Cells[0][1] = "Two Three";
        table.Cells[1][0] = "Four Five Six";
        table.Cells[1][1] = "";

        // Act
        visitor.Visit(table);

        // Assert
        // "One" = 1, "Two Three" = 2, "Four Five Six" = 3, "" = 0
        Assert.That(visitor.TotalWords, Is.EqualTo(6));
    }

    [Test]
    public void Visit_MultipleParagraphs_ShouldAccumulateCount()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph1 = new Paragraph("Hello world");
        var paragraph2 = new Paragraph("Testing counts");

        // Act
        visitor.Visit(paragraph1);
        visitor.Visit(paragraph2);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(4));
    }

    [Test]
    public void Visit_MixedElements_ShouldCountCorrectly()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var paragraph = new Paragraph("Hello world"); // 2 words
        var image = new Image("test.png", 100, 100); // 0 words
        var table = new Table(1, 1); // "Cell 0,0" = 2 words

        // Act
        visitor.Visit(paragraph);
        visitor.Visit(image);
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(4));
    }

    [Test]
    public void Visit_LongParagraph_ShouldCountAllWords()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var text = "This is a long paragraph with many words to test the counting functionality";
        var paragraph = new Paragraph(text);

        // Act
        visitor.Visit(paragraph);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(13));
    }

    [Test]
    public void Visit_TableWithEmptyCells_ShouldHandleCorrectly()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var table = new Table(2, 2);
        table.Cells[0][0] = "";
        table.Cells[0][1] = "";
        table.Cells[1][0] = "";
        table.Cells[1][1] = "";

        // Act
        visitor.Visit(table);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }

    [Test]
    public void Visit_MultipleImages_ShouldNotIncrementCount()
    {
        // Arrange
        var visitor = new WordCountVisitor();
        var image1 = new Image("test1.png", 100, 100);
        var image2 = new Image("test2.png", 200, 200);
        var image3 = new Image("test3.png", 300, 300);

        // Act
        visitor.Visit(image1);
        visitor.Visit(image2);
        visitor.Visit(image3);

        // Assert
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }
}

