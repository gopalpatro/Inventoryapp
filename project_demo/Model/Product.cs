using System.ComponentModel.DataAnnotations;

namespace project_demo.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Product_detail { get; set; }
        public string CategoryName { get; set; }
        public int Category_id { get; set; }
        public string Category_name { get; set; }
        public float price { get; set; }

        //Navigation propety
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Stock> Stocks{ get; set; }

    }
}
