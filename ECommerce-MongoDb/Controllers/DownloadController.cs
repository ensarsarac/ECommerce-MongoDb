using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using ECommerce_MongoDb.Dtos.ReportDtos;
using ECommerce_MongoDb.Entities;
using ECommerce_MongoDb.Settings;
using MongoDB.Driver;
using OfficeOpenXml;
using ECommerce_MongoDb.Dtos.ProductDtos;
using static iTextSharp.text.pdf.AcroFields;

namespace ECommerce_MongoDb.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public DownloadController(IDatabaseSetting _databaseSetting)
        {
            var client = new MongoClient(_databaseSetting.ConnectionString);
            var database = client.GetDatabase(_databaseSetting.DatabaseName);
            _orderCollection = database.GetCollection<Order>(_databaseSetting.OrderCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSetting.ProductCollectionName);
            _customerCollection = database.GetCollection<Customer>(_databaseSetting.CustomerCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSetting.CategoryCollectionName);
        }
        public async Task<IActionResult> DownloadPdf()
        {
            var list = new List<ResultReportDto>();
            var values = await _orderCollection.Find(x => true).ToListAsync();
            foreach (var item in values)
            {
                var product = await _productCollection.Find(x => x.ProductId == item.ProductId).FirstOrDefaultAsync();
                var customer = await _customerCollection.Find(x => x.CustomerId == item.CustomerId).FirstOrDefaultAsync();
                list.Add(new ResultReportDto
                {
                    CustomerId = item.CustomerId,
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    CustomerAddress = customer.Address,
                    CustomerNameSurname = customer.CustomerNameSurname,
                    CustomerPhone = customer.Phone,
                    OrderId = item.OrderId,
                    ProductImage = product.ImageUrl,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductStock = product.Stock,
                    TotalPrice = item.TotalPrice
                });
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Reports/SiparisRapor.pdf");
            Document document = new Document(PageSize.A4);
            var fs = new FileStream(path, FileMode.Create);
            PdfWriter.GetInstance(document, fs);
            document.Open();

            PdfPTable table = new PdfPTable(8);

            table.AddCell("Ürün Adı");
            table.AddCell("Ürün Birim Fiyatı");
            table.AddCell("Ürün Stok");
            table.AddCell("Müşteri Adı");
            table.AddCell("Müşteri Adresi");
            table.AddCell("Müşteri Telefon");
            table.AddCell("Sipariş Miktarı");
            table.AddCell("Fiyat");

            foreach (var item in list)
            {
                table.AddCell(item.ProductName);
                table.AddCell(item.ProductPrice.ToString());
                table.AddCell(item.ProductStock.ToString());
                table.AddCell(item.CustomerNameSurname);
                table.AddCell(item.CustomerAddress);
                table.AddCell(item.CustomerPhone);
                table.AddCell(item.Amount.ToString());
                table.AddCell(item.TotalPrice.ToString());
            }
            document.Add(table);
            document.Close();
            return File("/Reports/SiparisRapor.pdf", "application/json", "SiparisRapor.pdf");
        }
        public async Task<IActionResult> DownloadExcel()
        {
            var list = new List<ResultProductDto>();
            var values = await _productCollection.Find(x => true).ToListAsync();
            foreach (var item in values)
            {
                var category = await _categoryCollection.Find(x => x.CategoryId == item.CategoryId).FirstOrDefaultAsync();
                list.Add(new ResultProductDto
                {
                    CategoryId = item.CategoryId,
                    CategoryName = category.CategoryName,
                    ImageUrl = item.ImageUrl,
                    Name = item.Name,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Stock = item.Stock,
                });

            }
            ExcelPackage excel = new ExcelPackage();
            var work = excel.Workbook.Worksheets.Add("Ürün Listesi");
            work.Cells[1, 1].Value = "Ürün Id";
            work.Cells[1, 2].Value = "Ürün Adı";
            work.Cells[1, 3].Value = "Fiyatı";
            work.Cells[1, 4].Value = "Kategori";
            work.Cells[1, 5].Value = "Stok";

            for (int i = 2; i < list.Count() + 2; i++)
            {
                    work.Cells[i, 1].Value = list[i-2].ProductId;
                    work.Cells[i, 2].Value = list[i - 2].Name;
                    work.Cells[i, 3].Value = list[i - 2].Price;
                    work.Cells[i, 4].Value = list[i - 2].CategoryName;
                    work.Cells[i, 5].Value = list[i - 2].Stock;
            }


            var bytes = excel.GetAsByteArray();
            return File(bytes, "application/vdn.openxmlformats-officedocument.spreadsheetml.sheet", "Ürünler.xlsx");
        }
    }
}
