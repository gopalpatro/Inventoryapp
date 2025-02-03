using Microsoft.AspNetCore.Mvc;

namespace project_demo.Model.DTO
{
    public class newproductDTO
    {
        public string ProductName { get; set; }
        public string product_detail { get; set; } 
        public int category_id { get; set; }
        public string Category_Name { get; set; }
        public int amount { get; set; }

    }
}
