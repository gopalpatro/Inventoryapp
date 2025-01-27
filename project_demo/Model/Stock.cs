namespace project_demo.Model
{
    public class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Last_modified { get; set; }
        public int max_Quantity { get; set; }
        public int min_Quantity { get; set; }
        //Navigation propety
        public Product Product { get; set; }//reference to product
    }
}
