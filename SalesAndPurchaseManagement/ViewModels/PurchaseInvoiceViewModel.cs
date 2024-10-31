using SalesAndPurchaseManagement.Models;

namespace SalesAndPurchaseManagement.ViewModels
{
    public class PurchaseInvoiceViewModel
    {
        public PurchaseInvoice Invoice { get; set; }
        public List<PurchaseInvoiceDetail> InvoiceDetails { get; set; }
    }
}
