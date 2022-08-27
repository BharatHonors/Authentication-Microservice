using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationMicroservice.Model;
using AuthenticationMicroservice.Repository;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _Config;
        private IPensionRepo pensionRepo;
        public AuthenticationController(IConfiguration conf,IPensionRepo repo)
        {
            _Config = conf;
            pensionRepo = repo;
        }
        // GET: api/<AuthenticationController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]PentionCredentials pentionCredentials)
        {

            IActionResult response = Unauthorized();
            var user = pensionRepo.GetPentionCredentials(pentionCredentials);
            if (user != null)
            {
                var tockenString = GenarateJWTocken(user);
                return Ok(new { tocken = tockenString });
            }
            return response;
        }

        private string GenarateJWTocken (PentionCredentials user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
            };
            var tocken = new JwtSecurityToken(_Config["Jwt:Issuer"],
                _Config["Jwt:Issuer"],
                claims,
                expires: System.DateTime.Now.AddMinutes(120),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(tocken);
        }
        // GET api/<AuthenticationController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromBody]PentionCredentials id)
        {
            return new OkObjectResult(id);
        }

        // POST api/<AuthenticationController>
        

        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
