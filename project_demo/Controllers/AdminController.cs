using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_demo.Data;
using project_demo.Model;
using project_demo.NewFolder2;

namespace project_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ItokenRepository tokenrepository;
        private readonly AppDbContext _Context;
        public AdminController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _Context = context;
            this.userManager = userManager;
            this.tokenrepository = tokenrepository;
        }



        //private readonly AppDbContext _Context;

        [HttpPost("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsApproved);
            if (user == null)
            {
                return Unauthorized("invalid creditial or approval pending");
            }
            return Ok("login successful");
        }

        [HttpGet("Pending-request")]
        public IActionResult GetPendingRequest()
        {
            var PendingRequests=_Context.Registrationrequests.Where(r=>r.IsApproved==false).ToList();
            List<dynamic> var = new List<dynamic>();
            foreach(var x in PendingRequests)
            {
                var.Add(new { id = x.Id, name = x.Username, Isapproved = x.IsApproved });
            }
            
            return Ok(var);
        }
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveRequest(int id, [FromQuery] bool approve)
        {
            var request=_Context.Registrationrequests.FirstOrDefault(r=>r.Id == id);
            if (request == null)
            {
                return NotFound("registration request not found");
            }
            // request.IsProcessed = true;
            request.IsApproved = approve;
            
            // return Ok(new { message = approve ? "USer approved and registered" : "user registration denied" });

            if (approve)
            {
                var IdentityUser = new IdentityUser
                {
                    UserName = request.Username,
                    Email = request.Username
                };
                var identityResult = await userManager.CreateAsync(IdentityUser, request.Password);
                if (identityResult.Succeeded)
                {
                    //add roles to the user
                    if (request.Role.Any())
                    {
                        identityResult = await userManager.AddToRoleAsync(IdentityUser, request.Role);
                    }
                    if (approve)
                    {
                        var user = new User
                        {
                            Username = request.Username,
                            Password = request.Password,
                            IsApproved = true,
                        };
                        _Context.Users.Add(user);
                    }
                    await _Context.SaveChangesAsync();
                    if (identityResult.Succeeded)
                    {
                        return Ok("User registration sucesful");
                    }

                }
            }
            return BadRequest("something went wrong");

        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleterequest(int id)
        {
            var user = _Context.Users.FirstOrDefault(u => u.USerId == id);
            if(user==null)
            {
                return NotFound("User not found");
            }
            _Context.Users.Remove(user);
            await _Context.SaveChangesAsync();
            return Ok($"user {user.Username} deleted successfully");
        }

    }
}
