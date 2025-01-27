using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_demo.NewFolder2
{
    public class tokenRepository:ItokenRepository
    {
        private readonly IConfiguration configuration;
        public tokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user,string roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email,user.Email));

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token= new JwtSecurityToken(
                configuration["jwt:Issuer"],
                configuration["jwt:Audience"],
                claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
