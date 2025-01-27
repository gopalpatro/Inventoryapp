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
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ItokenRepository tokenrepository;
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context, UserManager<IdentityUser> userManager, ItokenRepository tokenrepository)
        {
            _context = context;
            this.userManager = userManager;
            this.tokenrepository = tokenrepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( [FromQuery] string username, [FromQuery] string password, [FromQuery] string role)
        //public async Task<IActionResult> Register([FromBody] Registrationrequest request)
        {
            if(_context.Users.Any(u=>u.Username==username))
            {
                return BadRequest("Email already exist");
            }
            var request= new Registrationrequest
            {
               // Id = id,
                Username =username,
                Password = password,
                Role=role,
                IsApproved=false,
                
                // BCrypt.Net.BCrypt.HashPassword(//for encryption of password
            };

            _context.Registrationrequests.Add(request);
           // _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("registration request submitted for admin approval");
        }
        //[HttpGet("login")]

        //public async Task<IActionResult> login([FromQuery] string username, [FromQuery] string password)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        //    if (user == null)
        //    {
        //        return Unauthorized(new { message = "Invalid username or password" });
        //    }
        //    return Ok(new { message = "login successful" });

        //    if (user != null)
        //    {
        //        var token = JwtTokenGenerator.GenerateToken(
        //            login.UserName,
        //            _configuration["Jwt:Key"],
        //            _configuration["Jwt:Issuer"],
        //            _configuration["Jwt:Audience"]
        //            );
        //        return Ok(new { Token = token });
        //    }
        //    return Unauthorized();
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password)
        {
            var user = await userManager.FindByEmailAsync(username);
            if (user != null)
            {
                var res = await userManager.CheckPasswordAsync(user, password);
                if (res)
                {
                    //get role of user
                    var roles = await userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault();

                    //Create Token

                    var jwtToken = tokenrepository.CreateJwtToken(user, role);

                    return Ok($"Token:{jwtToken}");
                }
            }
            return BadRequest("invalid credentials");
        }

    }
}
