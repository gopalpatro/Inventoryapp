using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_demo.Data;
using project_demo.Model;
using project_demo.Model.DTO;
using project_demo.Repository;

namespace project_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mainController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Imainrepository _mainrepository;

        public mainController(AppDbContext context,Imainrepository mainrepo)
        {
            _context = context;
            _mainrepository = mainrepo;

        }

        //add a new purchase i.e invoice
        [HttpPost("NewPurchase")]
        //[Authorize(Roles ="USer")]
        public async Task<IActionResult> newpurchase([FromBody] new_purchaseDTO prch) 
       
        {
            
            var res = _mainrepository.Newpurchaseasync(prch.user_id,prch.productid,prch.Invoiceno,prch.Quantity,prch.suppliername);
            if(res==null)
            {
                return BadRequest("product does not exist at first add product");
            }
           
            return Ok(new { message = "New invoice added successfully" }); 
        }

        //Add a new product
        [HttpPost("Newproduct")]
        //[Authorize(Roles ="User")]
        public IActionResult newProduct([FromBody] newproductDTO pd)
        {
            var res = _mainrepository.NewProduct( pd.ProductName, pd.product_detail,pd.Category_Name, pd.category_id,pd.Category_Name,pd.amount );
            return Ok( new {message= "new product added successfully" });
        }

        //to sell a item
        [HttpPost("NewSale")]
        //[Authorize]
        public IActionResult newsale([FromBody] newsaleDTO saledto )
        {

            var res = _mainrepository.NewSaleAsync(saledto.customer_id, saledto. customername, saledto.productid, saledto.quantity);
            if(res== "insufficent quantity")
            {
                return Ok(new {message= "insufficient quantity" });
            }
            
            return Ok(new {message= "sale transicition is  successful" });
        }

        //to see the inventory size
        [HttpGet("review_inventory")]
        //[Authorize]
        public async Task<IActionResult> GetStockdetail()
        {

          // var res =   _mainrepository.Review_inventory();

            var res1 = (from p in _context.stocks
                        join q in _context.products on p.ProductId equals q.ProductId
                        select new stockshow
                        {
                            ProductId = q.ProductId,
                            ProductName = q.ProductName,
                            quantity = p.Quantity,
                            minquantity = p.min_Quantity,
                            maxquantity = p.max_Quantity
                        });

            //intiate inventory dto and send it to client
            //List<review_inventoryDto> li1 = new List<review_inventoryDto>();
            //foreach (var item in res)
            //{
            //    li1.Add(new review_inventoryDto()
            //    {
            //        ProductId = item.ProductId,
            //        ProductName = item.ProductName,
            //        quantity = item.quantity,
            //        minquantity = item.minquantity,
            //        maxquantity = item.maxquantity
            //    });
            //}


            //return  inventory Dto to client
            return Ok(res1);
        }
        
        //get transictions of user by userId
        [HttpGet("usertransiction")]
        
        public ActionResult<dynamic> usertransitions([FromQuery] int user_id)
        {

            var transictions = _mainrepository.USerTransiction(user_id);
            //var pur = _context.Purchases.Where(p => p.USerId == user_id).Select(p => new
            //{
            //    date = p.PurchaseDate,
            //    Type = "Debit",
            //    Amount = p.Invoice_Amount
            //});
            //var sel = _context.Sales.Where(p => p.USerId == user_id).Select(p => new
            //{
            //    date = p.Sale_Date,
            //    Type = "Credit",
            //    Amount = p.amount
            //});
            //var transictions = pur.Union(sel).OrderBy(t => t.date).ToList<dynamic>();
            return Ok(transictions);
        }
        [HttpGet("get_min_quantity")]
        public IActionResult get_min_quantity()
        {
            //var res = _context.stocks.FirstOrDefault(r => r.Quantity <= r.min_Quantity);
            //if(res==null)
            //{
            //    return NotFound(" No worry,all products are above minimum quantity" );
            //}
            var res = _mainrepository.Min_Quantity();
            return Ok(res);
            
        }
        [HttpGet("get_max_quantity")]
        public IActionResult get_max_quantity()
        {
            //var res = _context.stocks.FirstOrDefault(r => r.Quantity >= r.min_Quantity);
            //if (res == null)
            //{
            //    return NotFound(" No worry,all products are below maximum quantity");
            //}
            var res = _mainrepository.Max_Quantity();
            return Ok(res);
        }
        [HttpPost("update stock price by id")]
        public IActionResult Update_price_by_id([FromQuery] int product_id, [FromQuery] float new_price)
        {
            //var res_product = _context.products.FirstOrDefault(r => r.ProductId == product_id);
            //if (res_product==null)
            //{
            //    return BadRequest("no product found");
            //}
            //res_product.price = new_price;
            //return Ok("new price updated");
            var res = _mainrepository.UpdatePriceById(product_id, new_price);
            if(res)
            {
                return Ok("NEW PRICE UPDATED SUCCESSFULLY");
            }
            return BadRequest("PRODUCT NOT FOUND");

        }
        //[HttpPost("Discount to max quantity products")]
        //public IActionResult Discount_to_max_Quantity([FromQuery] float discount_percentage)
        //{
        //    var res = _context.stocks.Where(r => r.Quantity >= r.min_Quantity).ToList();
        //    //var res1=from p in _context.stocks select new { id = _context.stocks.ProductId }
        //    foreach (var r in res)
        //    {
        //        var tes = _context.products.FirstOrDefault(r => r.ProductId == res);

        //    }
        //}


    }
}
