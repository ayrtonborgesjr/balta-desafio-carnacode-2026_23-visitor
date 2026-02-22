using Relatorios.Console;
using Relatorios.Console.Elements;
using Relatorios.Console.Visitors;

var doc = new Document("Relatório Anual");

doc.AddElement(new Paragraph("Este é o relatório anual da empresa."));
doc.AddElement(new Image("grafico.png", 800, 600));
doc.AddElement(new Paragraph("Resultados financeiros do ano."));
doc.AddElement(new Table(2, 3));

// HTML
var htmlVisitor = new HtmlExportVisitor();
doc.Accept(htmlVisitor);
Console.WriteLine(htmlVisitor.GetHtml(doc.Title));

// PDF
var pdfVisitor = new PdfExportVisitor();
doc.Accept(pdfVisitor);
Console.WriteLine(pdfVisitor.GetPdf(doc.Title));

// Contagem
var wordVisitor = new WordCountVisitor();
doc.Accept(wordVisitor);
Console.WriteLine($"Total palavras: {wordVisitor.TotalWords}");

// Validação
var validationVisitor = new ValidationVisitor();
doc.Accept(validationVisitor);
Console.WriteLine($"Documento válido: {validationVisitor.IsValid}");