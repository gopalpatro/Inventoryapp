using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_demo.Data;
using project_demo.Model;
using project_demo.Model.DTO;
using Microsoft.Extensions.Logging;

namespace project_demo.Repository
{
    public class mainRepository : Imainrepository
    {
       private readonly AppDbContext _context;
        private readonly ILogger<mainRepository> _logger;
        public mainRepository(AppDbContext context,ILogger<mainRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public  Purchase Newpurchaseasync(int user_id, int productid, string Invoiceno, int Quantity, string suppliername)
        {
            try
            {
                var test =  _context.products.FirstOrDefault(p => p.ProductId == productid);
                if (test == null)
                {
                    // return NotFound("The mentioned product is not in not in database ");
                    return null;
                }
                var res = new Purchase { USerId = user_id, quantity = Quantity, PurchaseDate = DateTime.Now, Invoice_Amount = (test.price * Quantity), Invoice_No = 1, Supplier_Name = suppliername, ProductId = productid };
                 _context.Purchases.Add(res);
                _context.SaveChanges();
                var st1 =  _context.stocks.FirstOrDefault(p => p.ProductId == productid);
                if (st1 != null)
                {
                    st1.Quantity += Quantity;
                    st1.Last_modified = DateTime.Now;
                }



                 _context.SaveChanges();
                _logger.LogInformation("Purchase for user {UserId} and product {ProductId} completed successfully.", user_id, productid);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing Newpurchaseasync."); 
                throw;
            }
            

        }
           Product Imainrepository.NewProduct(string ProductName, string product_detail, string category_Name, int category_id, string Category_Name, int amount)
        {
            var res = new Product { ProductName = ProductName, Product_detail = product_detail, Category_name = Category_Name, Category_id = category_id, CategoryName = Category_Name, price = amount };

             _context.products.Add(res);
             _context.SaveChanges();
            var res1 =  _context.products.FirstOrDefault(p => p.ProductName == ProductName);
            var st1 = new Stock { ProductId = res1.ProductId, Quantity = 0, Last_modified = DateTime.Now, min_Quantity = 10, max_Quantity = 50 };
             _context.stocks.Add(st1);
             _context.SaveChanges();
            return res;
        }

         public async Task<string> NewSaleAsync(int customer_id, string customername, int productid, int quantity)
        {
            var test = _context.products.FirstOrDefault(p => p.ProductId == productid);
            if (test == null)
            {
                return "The mentioned product is not in not in database ";
            }
            var st1 = _context.stocks.FirstOrDefault(p => p.ProductId == productid);
            if (st1.Quantity >= quantity)
            {
                var res = new Sale { USerId = customer_id, Sale_Date = DateTime.Now, Customer_name = customername, ProductId = productid, Quantity = quantity, amount = (test.price * quantity) };
                _context.Sales.Add(res);
            }

            if (st1 != null)
            {
                st1.Quantity -= quantity;
                st1.Last_modified = DateTime.Now;
            }
            _context.SaveChanges();
            return "OK";
        }

       

        public  List<dynamic> USerTransiction(int user_id)
        {
            var pur =  _context.Purchases.Where(p => p.USerId == user_id).Select(p => new
            {
                date = p.PurchaseDate,
                Type = "Debit",
                Amount = p.Invoice_Amount
            }).ToList();
            var sel =  _context.Sales.Where(p => p.USerId == user_id).Select(p => new
            {
                date = p.Sale_Date,
                Type = "Credit",
                Amount = p.amount
            }).ToList();
            var transictions = pur.Union(sel).OrderBy(t => t.date).ToList<dynamic>();
            return transictions;
        }

        

        public IQueryable<review_inventoryDto> Review_inventory()
        {
            var res1 = (from p in _context.stocks
                        join q in _context.products on p.ProductId equals q.ProductId
                        select new review_inventoryDto
                        {
                            ProductId = q.ProductId,
                            ProductName = q.ProductName,
                            quantity = p.Quantity,
                            minquantity = p.min_Quantity,
                            maxquantity = p.max_Quantity
                        });

            return res1;
        }

       public bool UpdatePriceById(int product_id, float new_price)
        {
            var res_product = _context.products.FirstOrDefault(r => r.ProductId == product_id);
            if (res_product == null)
            {
                return false;
            }
            res_product.price = new_price;
            _context.SaveChanges();
            return true;
        }

       public List<item_quantityDTO> Min_Quantity()
        {

            return (from p in _context.stocks 
                    join q in _context.products on
                    p.ProductId equals q.ProductId where
                    p.Quantity < p.min_Quantity select new
                    item_quantityDTO { ProductId = q.ProductId, productName = q.ProductName, Quantity = p.Quantity, Last_modified = p.Last_modified }).ToList();
            //var res = _context.stocks.Where(r => r.Quantity >= r.min_Quantity).ToList();

            
        }

        List<item_quantityDTO> Imainrepository.Max_Quantity()
        {
            return (from p in _context.stocks
                    join q in _context.products on
                    p.ProductId equals q.ProductId
                    where
                    p.Quantity > p.max_Quantity
                    select new
                    item_quantityDTO
                    { ProductId = q.ProductId, productName = q.ProductName, Quantity = p.Quantity, Last_modified = p.Last_modified }).ToList();

        }

        
    }
    
       
}

