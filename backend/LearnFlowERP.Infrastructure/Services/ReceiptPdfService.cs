using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Payments.DTOs;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace LearnFlowERP.Infrastructure.Services
{
    public class ReceiptPdfService : IReceiptPdfService
    {
        public byte[] GenerateReceiptPdf(
            ReceiptDto receipt)
        {
            var document = new Document();

            var section = document.AddSection();

            var title = section.AddParagraph();
            title.AddFormattedText(
                "LearnFlow ERP Receipt",
                TextFormat.Bold);

            title.Format.Font.Size = 16;
            title.Format.SpaceAfter = "1cm";

            section.AddParagraph(
                $"Receipt No: {receipt.ReceiptNumber}");

            section.AddParagraph(
                $"Payment Id: {receipt.PaymentId}");

            section.AddParagraph(
                $"Student: {receipt.StudentName}");

            section.AddParagraph(
                $"Fee Type: {receipt.FeeType}");

            section.AddParagraph(
                $"Amount Paid: ₹{receipt.AmountPaid:N2}");

            section.AddParagraph(
                $"Payment Method: {receipt.PaymentMethod}");

            section.AddParagraph(
                $"Transaction Ref: {receipt.TransactionRef}");

            section.AddParagraph(
                $"Payment Date: {receipt.PaymentDate:dd-MMM-yyyy}");

            section.AddParagraph();

            section.AddParagraph(
                "This is a system generated receipt.");

            var renderer =
                new PdfDocumentRenderer(true)
                {
                    Document = document
                };

            renderer.RenderDocument();

            using var stream =
                new MemoryStream();

            renderer.PdfDocument.Save(
                stream,
                false);

            return stream.ToArray();
        }
    }
}