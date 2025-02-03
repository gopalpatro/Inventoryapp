using Microsoft.AspNetCore.Mvc;

namespace project_demo.Model.DTO
{
    public class newsaleDTO
    {
        public int customer_id { get; set; }
        public string customername { get; set; }
        public int productid { get; set; }
        public int quantity { get; set; }
    }
}
