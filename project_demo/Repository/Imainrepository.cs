using Microsoft.AspNetCore.Mvc;
using project_demo.Model;
using project_demo.Model.DTO;

namespace project_demo.Repository
{
    public interface Imainrepository
    {
        Purchase Newpurchaseasync(int user_id, int productid, string Invoiceno, int Quantity, string suppliername);
        Product NewProduct(string ProductName, string product_detail, string category_Name, int category_id, string Category_Name, int amount);
        string NewSaleAsync(int customer_id, string customername, int productid, int quantity);
        IQueryable<review_inventoryDto> Review_inventory();
        List<dynamic> USerTransiction(int user_id);
        bool UpdatePriceById(int product_id, float new_price);
        List<item_quantityDTO> Min_Quantity();
        List<item_quantityDTO> Max_Quantity();


    }
        
}
