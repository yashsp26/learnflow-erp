using LearnFlowERP.Application.Features.Payments.DTOs;

namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IReceiptPdfService
    {
        byte[] GenerateReceiptPdf(ReceiptDto receipt);
    }
}
