using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Elements;

[TestFixture]
public class TableTests
{
    [Test]
    public void Constructor_ShouldInitializeCellsWithCorrectRowCount()
    {
        // Arrange & Act
        var table = new Table(3, 2);

        // Assert
        Assert.That(table.Cells, Has.Count.EqualTo(3));
    }

    [Test]
    public void Constructor_ShouldInitializeCellsWithCorrectColumnCount()
    {
        // Arrange & Act
        var table = new Table(3, 4);

        // Assert
        Assert.That(table.Cells[0], Has.Count.EqualTo(4));
        Assert.That(table.Cells[1], Has.Count.EqualTo(4));
        Assert.That(table.Cells[2], Has.Count.EqualTo(4));
    }

    [Test]
    public void Constructor_ShouldInitializeCellsWithDefaultText()
    {
        // Arrange & Act
        var table = new Table(2, 2);

        // Assert
        Assert.That(table.Cells[0][0], Is.EqualTo("Cell 0,0"));
        Assert.That(table.Cells[0][1], Is.EqualTo("Cell 0,1"));
        Assert.That(table.Cells[1][0], Is.EqualTo("Cell 1,0"));
        Assert.That(table.Cells[1][1], Is.EqualTo("Cell 1,1"));
    }

    [Test]
    public void Constructor_ShouldHandleSingleCell()
    {
        // Arrange & Act
        var table = new Table(1, 1);

        // Assert
        Assert.That(table.Cells, Has.Count.EqualTo(1));
        Assert.That(table.Cells[0], Has.Count.EqualTo(1));
        Assert.That(table.Cells[0][0], Is.EqualTo("Cell 0,0"));
    }

    [Test]
    public void Constructor_ShouldHandleLargeTable()
    {
        // Arrange & Act
        var table = new Table(10, 5);

        // Assert
        Assert.That(table.Cells, Has.Count.EqualTo(10));
        Assert.That(table.Cells[9], Has.Count.EqualTo(5));
        Assert.That(table.Cells[9][4], Is.EqualTo("Cell 9,4"));
    }

    [Test]
    public void Cells_ShouldBeModifiable()
    {
        // Arrange
        var table = new Table(2, 2);

        // Act
        table.Cells[0][0] = "Modified";

        // Assert
        Assert.That(table.Cells[0][0], Is.EqualTo("Modified"));
    }

    [Test]
    public void Accept_ShouldCallVisitorVisitMethod()
    {
        // Arrange
        var table = new Table(2, 2);
        var visitor = new WordCountVisitor();

        // Act
        table.Accept(visitor);

        // Assert
        // Each cell has "Cell x,y" which is 2 words
        // 2 rows * 2 columns * 2 words = 8 words
        Assert.That(visitor.TotalWords, Is.EqualTo(8));
    }

    [Test]
    public void Accept_ShouldWorkWithHtmlVisitor()
    {
        // Arrange
        var table = new Table(2, 2);
        var visitor = new HtmlExportVisitor();

        // Act
        table.Accept(visitor);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("<table>"));
        Assert.That(html, Does.Contain("<tr>"));
        Assert.That(html, Does.Contain("<td>"));
        Assert.That(html, Does.Contain("Cell 0,0"));
    }

    [Test]
    public void Accept_ShouldWorkWithValidationVisitor()
    {
        // Arrange
        var table = new Table(2, 2);
        var visitor = new ValidationVisitor();

        // Act
        table.Accept(visitor);

        // Assert
        Assert.That(visitor.IsValid, Is.True);
    }

    [Test]
    public void Constructor_WithZeroRows_ShouldCreateEmptyTable()
    {
        // Arrange & Act
        var table = new Table(0, 3);

        // Assert
        Assert.That(table.Cells, Is.Empty);
    }

    [Test]
    public void Constructor_WithZeroColumns_ShouldCreateRowsWithNoCells()
    {
        // Arrange & Act
        var table = new Table(3, 0);

        // Assert
        Assert.That(table.Cells, Has.Count.EqualTo(3));
        Assert.That(table.Cells[0], Is.Empty);
    }
}

