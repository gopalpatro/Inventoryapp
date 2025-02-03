namespace project_demo.Model.DTO
{
    public class item_quantityDTO
    {
        public int ProductId { get; set; }
        public string? productName { get; set; }
        public int Quantity { get; set; }
        public DateTime Last_modified { get; set; }
        public int minquantity { get; set; }
    }
}
