using Microsoft.AspNetCore.Mvc;

namespace project_demo.Model.DTO
{
    public class new_purchaseDTO
    {
        public int user_id { get; set; }
       public  int productid { get; set; }
        public string Invoiceno { get; set; }
        public int Quantity { get; set; }
        public string suppliername { get; set; }
    }
}
