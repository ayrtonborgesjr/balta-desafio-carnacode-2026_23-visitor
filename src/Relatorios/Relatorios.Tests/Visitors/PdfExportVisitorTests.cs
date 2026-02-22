using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Visitors;

[TestFixture]
public class PdfExportVisitorTests
{
    [Test]
    public void Visit_Paragraph_ShouldGeneratePdfText()
    {
        // Arrange
        var visitor = new PdfExportVisitor();
        var paragraph = new Paragraph("Test text");

        // Act
        visitor.Visit(paragraph);
        var pdf = visitor.GetPdf("Document");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_TEXT(Test text)"));
    }

    [Test]
    public void Visit_Image_ShouldGeneratePdfImage()
    {
        // Arrange
        var visitor = new PdfExportVisitor();
        var image = new Image("test.png", 800, 600);

        // Act
        visitor.Visit(image);
        var pdf = visitor.GetPdf("Document");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_IMAGE(test.png)"));
    }

    [Test]
    public void Visit_Table_ShouldGeneratePdfTable()
    {
        // Arrange
        var visitor = new PdfExportVisitor();
        var table = new Table(3, 2);

        // Act
        visitor.Visit(table);
        var pdf = visitor.GetPdf("Document");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_TABLE(3)"));
    }

    [Test]
    public void GetPdf_ShouldIncludeTitle()
    {
        // Arrange
        var visitor = new PdfExportVisitor();

        // Act
        var pdf = visitor.GetPdf("Test Title");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_DOCUMENT(Test Title)"));
    }

    [Test]
    public void Visit_MultipleParagraphs_ShouldIncludeAll()
    {
        // Arrange
        var visitor = new PdfExportVisitor();
        var paragraph1 = new Paragraph("First paragraph");
        var paragraph2 = new Paragraph("Second paragraph");

        // Act
        visitor.Visit(paragraph1);
        visitor.Visit(paragraph2);
        var pdf = visitor.GetPdf("Document");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_TEXT(First paragraph)"));
        Assert.That(pdf, Does.Contain("PDF_TEXT(Second paragraph)"));
    }

    [Test]
    public void Visit_MultipleElements_ShouldIncludeAllInOrder()
    {
        // Arrange
        var visitor = new PdfExportVisitor();
        var paragraph = new Paragraph("Text");
        var image = new Image("image.png", 100, 100);
        var table = new Table(2, 2);

        // Act
        visitor.Visit(paragraph);
        visitor.Visit(image);
        visitor.Visit(table);
        var pdf = visitor.GetPdf("Document");

        // Assert
        var textIndex = pdf.IndexOf("PDF_TEXT(Text)");
        var imageIndex = pdf.IndexOf("PDF_IMAGE(image.png)");
        var tableIndex = pdf.IndexOf("PDF_TABLE(2)");

        Assert.That(textIndex, Is.LessThan(imageIndex));
        Assert.That(imageIndex, Is.LessThan(tableIndex));
    }

    [Test]
    public void GetPdf_EmptyDocument_ShouldGenerateValidPdf()
    {
        // Arrange
        var visitor = new PdfExportVisitor();

        // Act
        var pdf = visitor.GetPdf("Empty");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_DOCUMENT(Empty)"));
    }

    [Test]
    public void Visit_TableWithDifferentSizes_ShouldReflectRowCount()
    {
        // Arrange
        var visitor1 = new PdfExportVisitor();
        var visitor2 = new PdfExportVisitor();
        var table1 = new Table(5, 3);
        var table2 = new Table(10, 2);

        // Act
        visitor1.Visit(table1);
        visitor2.Visit(table2);
        var pdf1 = visitor1.GetPdf("Doc1");
        var pdf2 = visitor2.GetPdf("Doc2");

        // Assert
        Assert.That(pdf1, Does.Contain("PDF_TABLE(5)"));
        Assert.That(pdf2, Does.Contain("PDF_TABLE(10)"));
    }

    [Test]
    public void Visit_ParagraphWithEmptyText_ShouldStillGenerate()
    {
        // Arrange
        var visitor = new PdfExportVisitor();
        var paragraph = new Paragraph("");

        // Act
        visitor.Visit(paragraph);
        var pdf = visitor.GetPdf("Document");

        // Assert
        Assert.That(pdf, Does.Contain("PDF_TEXT()"));
    }

    [Test]
    public void Visit_ImageWithDifferentUrls_ShouldIncludeCorrectUrl()
    {
        // Arrange
        var visitor1 = new PdfExportVisitor();
        var visitor2 = new PdfExportVisitor();
        var image1 = new Image("photo.jpg", 100, 100);
        var image2 = new Image("diagram.svg", 200, 200);

        // Act
        visitor1.Visit(image1);
        visitor2.Visit(image2);
        var pdf1 = visitor1.GetPdf("Doc1");
        var pdf2 = visitor2.GetPdf("Doc2");

        // Assert
        Assert.That(pdf1, Does.Contain("PDF_IMAGE(photo.jpg)"));
        Assert.That(pdf2, Does.Contain("PDF_IMAGE(diagram.svg)"));
    }
}

