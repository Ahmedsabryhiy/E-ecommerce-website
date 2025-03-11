namespace UI.ModelViews
{
    public class OrderViewModel
    {
        public int InvoiceId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // Add status tracking to TbSalesInvoice if needed
        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}
