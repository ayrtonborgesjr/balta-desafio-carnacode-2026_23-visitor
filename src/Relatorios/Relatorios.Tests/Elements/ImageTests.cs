using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

namespace Relatorios.Tests.Elements;

[TestFixture]
public class ImageTests
{
    [Test]
    public void Constructor_ShouldInitializeUrl()
    {
        // Arrange & Act
        var image = new Image("test.png", 100, 200);

        // Assert
        Assert.That(image.Url, Is.EqualTo("test.png"));
    }

    [Test]
    public void Constructor_ShouldInitializeWidth()
    {
        // Arrange & Act
        var image = new Image("test.png", 100, 200);

        // Assert
        Assert.That(image.Width, Is.EqualTo(100));
    }

    [Test]
    public void Constructor_ShouldInitializeHeight()
    {
        // Arrange & Act
        var image = new Image("test.png", 100, 200);

        // Assert
        Assert.That(image.Height, Is.EqualTo(200));
    }

    [Test]
    public void Constructor_ShouldSetDefaultAlt()
    {
        // Arrange & Act
        var image = new Image("test.png", 100, 200);

        // Assert
        Assert.That(image.Alt, Is.EqualTo(""));
    }

    [Test]
    public void Url_ShouldBeSettable()
    {
        // Arrange
        var image = new Image("initial.png", 100, 100);

        // Act
        image.Url = "modified.png";

        // Assert
        Assert.That(image.Url, Is.EqualTo("modified.png"));
    }

    [Test]
    public void Width_ShouldBeSettable()
    {
        // Arrange
        var image = new Image("test.png", 100, 100);

        // Act
        image.Width = 500;

        // Assert
        Assert.That(image.Width, Is.EqualTo(500));
    }

    [Test]
    public void Height_ShouldBeSettable()
    {
        // Arrange
        var image = new Image("test.png", 100, 100);

        // Act
        image.Height = 600;

        // Assert
        Assert.That(image.Height, Is.EqualTo(600));
    }

    [Test]
    public void Alt_ShouldBeSettable()
    {
        // Arrange
        var image = new Image("test.png", 100, 100);

        // Act
        image.Alt = "Test description";

        // Assert
        Assert.That(image.Alt, Is.EqualTo("Test description"));
    }

    [Test]
    public void Accept_ShouldCallVisitorVisitMethod()
    {
        // Arrange
        var image = new Image("test.png", 100, 100);
        var visitor = new WordCountVisitor();

        // Act
        image.Accept(visitor);

        // Assert - Images don't contribute to word count
        Assert.That(visitor.TotalWords, Is.EqualTo(0));
    }

    [Test]
    public void Accept_ShouldWorkWithHtmlVisitor()
    {
        // Arrange
        var image = new Image("test.png", 800, 600);
        image.Alt = "Test image";
        var visitor = new HtmlExportVisitor();

        // Act
        image.Accept(visitor);
        var html = visitor.GetHtml("Document");

        // Assert
        Assert.That(html, Does.Contain("<img"));
        Assert.That(html, Does.Contain("test.png"));
        Assert.That(html, Does.Contain("800"));
        Assert.That(html, Does.Contain("600"));
    }
}

