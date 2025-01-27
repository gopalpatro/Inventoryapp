
namespace project_demo.Model
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime Sale_Date { get; set; }
        public string Customer_name { get; set; }
        //public int Invoice_No { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float amount { get; set; }
        public int USerId { get; set; }

        //Navigation propety
        public Product Product { get; set; }//reference to product
    }
}
