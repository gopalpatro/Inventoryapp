//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using project_demo.Data;
//using project_demo.Model.DTO;
//using project_demo.NewFolder2;
//using System.Data;

//namespace project_demo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NewAuthController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> userManager;
//        private readonly ItokenRepository tokenrepository;
//        //private readonly AppDbContext _co

//        public NewAuthController(UserManager<IdentityUser> userManager,ItokenRepository tokenrepository)
//        {
//            this.userManager = userManager;
//            this.tokenrepository = tokenrepository;
//        }
//        [HttpPost("Register")]
//        public async Task<IActionResult> Register([FromQuery] string username, [FromQuery] string password, [FromQuery] string roles)
//        {
//            var r1 = new RegisterRequestDto()
//            {
//                Username = username,
//                Password = password,
//                Roles=roles
//            };
//            var IdentityUser = new IdentityUser
//            {
//                UserName = username,
//                Email = username
//            };
//            var identityResult=await userManager.CreateAsync(IdentityUser,r1.Password);
//            if (identityResult.Succeeded) 
//            {
//                //add roles to the user
//                if (r1.Roles.Any())
//                {
//                    identityResult = await userManager.AddToRoleAsync(IdentityUser, r1.Roles);
//                }
//                if (identityResult.Succeeded)
//                {
//                    return Ok("User registration sucesful");
//                }

//            }
//            return BadRequest("something went wrong");


//        }
        
//        [HttpPost("Login")]
//        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password)
//        {
//            var user=await userManager.FindByEmailAsync(username);
//            if(user!=null)
//            {
//                var res=await userManager.CheckPasswordAsync(user, password);
//                if(res)
//                {
//                    //get role of user
//                    var roles=await userManager.GetRolesAsync(user);
//                    string role = roles.FirstOrDefault();

//                    //Create Token

//                    var jwtToken= tokenrepository.CreateJwtToken(user, role);

//                    return Ok($"Token:{jwtToken}");
//                }
//            }
//            return BadRequest("invalid credentials");
//        }
//    }
//}
