using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;

namespace Shop.Presentation.Pages
{
    public partial class ExportToysWindow : Window
    {
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly List<ProductEntity> _products;
        private readonly List<ProductEntity> _productsByCategory;
        private readonly CategoryRepository _categoryRepository;
        public ExportToysWindow()
        {
            InitializeComponent();
            _products = _context.Products.ToList();
            _categoryRepository = new CategoryRepository(_context);
            _productsByCategory = GetProductsByCateg("Toys").ToList();
        }
        private IEnumerable<ProductEntity> GetProductsByCateg(string categoryName)
        {
            foreach (var product in _products)
            {
                if (_categoryRepository.GetCategoryById(product.CategoryId)?.Name.ToLower() == categoryName.ToLower())
                {
                    yield return product;
                }
            }
        }
        private void ExportWord_Click(object sender, RoutedEventArgs e)
        {
            string filepath = "WordReport.docx";

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Table table = new Table();

                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                    )
                );

                table.AppendChild(tblProp);

                TableRow headerRow = new TableRow();
                foreach (string header in new[] { "ID", "Name", "Description", "Price", "Age Restrict", "Country", "CategoryID" })
                {
                    headerRow.Append(CreateTableCell(header, true));
                }
                table.Append(headerRow);
                
                foreach (var product in _productsByCategory)
                {
                    TableRow newRow = new TableRow();

                    newRow.Append(CreateTableCell(product.Id.ToString()));
                    newRow.Append(CreateTableCell(product.Name));
                    newRow.Append(CreateTableCell(product.Description));
                    newRow.Append(CreateTableCell(product.Price.ToString()));
                    newRow.Append(CreateTableCell(product.AgeRestrict.ToString()));
                    newRow.Append(CreateTableCell(product.Country));
                    newRow.Append(CreateTableCell(_categoryRepository.GetCategoryById(product.CategoryId).Name));

                    table.Append(newRow);
                }

                body.Append(table);
            }
        }

        private TableCell CreateTableCell(string text, bool isHeader = false)
        {
            TableCell cell = new TableCell(new Paragraph(new Run(new Text(text))));
            if (isHeader)
            {
                cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }, new TableHeader()));
            }
            return cell;
        }

    }
}