using System.ComponentModel.DataAnnotations;

namespace project_demo.Model
{
    public class Purchase
    {
        [Key]
        public int purchaseID { get; set; }
        public int quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int ProductId { get; set; }
        public float Invoice_Amount {  get; set; }
        public int Invoice_No { get; set; }
        public string Supplier_Name { get; set; }
        public int USerId { get; set; }
        //NAvigation propety
        public Product Product { get; set; }//reference to product
    }
}
