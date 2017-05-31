using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using GarageWeb.Models.ViewModel;
using GarageWeb.Models;
using System.Web.Mvc;

namespace GarageWeb.Infrastructure
{
    public static class Receipt
    {
        static readonly BaseFont baseFont = BaseFont.CreateFont($"{ System.AppDomain.CurrentDomain.BaseDirectory}/fonts/OpenSans-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        static readonly Font cellStyle = new Font(baseFont, 16, 0, BaseColor.BLACK);
        public static FileStreamResult GetPdfReceipt(Order order)
        {
            string path = $"{System.AppDomain.CurrentDomain.BaseDirectory}/{order.Id}.pdf";
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Document doc = new Document(PageSize.A5);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                //Order ID
                Paragraph p1 = new Paragraph();
                p1.Alignment = Element.ALIGN_CENTER;
                p1.Add(new Chunk(("Замовлення #" + order.Id).ToUpper(), new Font(baseFont, 20, 1, BaseColor.BLACK)));
                p1.SpacingAfter = 20;
                doc.Add(p1);

                //Table of dishes
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.DefaultCell.PaddingTop = 10;
                table.DefaultCell.PaddingBottom = 10;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                
                table.AddCell(GetCell("Страва"));
                table.AddCell(GetCell("Кількість"));
                table.AddCell(GetCell("Сумма"));
                foreach (var item in order.DishOrder)
                {
                    table.AddCell(GetCell(item.Dish.Name));
                    table.AddCell(GetCell(item.Count.ToString()));
                    table.AddCell(GetCell((item.Dish.Price * item.Count).ToString()));
                }
                doc.Add(table);
                //Total worth
                p1 = new Paragraph();
                p1.SpacingBefore = 50;
                p1.Alignment = Element.ALIGN_RIGHT;
                Phrase ph = new Phrase();
                ph.Add(new Chunk("Сума:", new Font(baseFont, 16, 0, BaseColor.BLACK)));
                ph.Add(new Chunk(order.TotalWorth.ToString()+ " грн", new Font(baseFont, 18, 1, BaseColor.BLACK)));
                p1.Add(ph);
                doc.Add(p1);
                //Paid 
                p1 = new Paragraph();
                p1.SpacingBefore = 10;
                p1.Alignment = Element.ALIGN_RIGHT;
                ph = new Phrase();
                ph.Add(new Chunk("Сплачена сума:", new Font(baseFont, 16, 0, BaseColor.BLACK)));
                ph.Add(new Chunk(order.ToPay.ToString() +" грн", new Font(baseFont, 18, 1, BaseColor.BLACK)));
                p1.Add(ph);
                doc.Add(p1);
                doc.Close();
                writer.Close();
            }

            var file = new FileStream(path,
                                        FileMode.Open,
                                        FileAccess.Read
                                    );
            var pdfResult = new FileStreamResult(file, "application/pdf");
            return pdfResult;
        }
        private static PdfPCell GetCell(string text)
        {
            return new PdfPCell(new Phrase(text, cellStyle)) { BorderWidth = 0, PaddingTop = 10, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER };
        }
    }
}