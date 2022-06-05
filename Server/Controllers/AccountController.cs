using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WebAppBlazorLogin.Server.Data;
using WebAppBlazorLogin.Server.Entities;
using WebAppBlazorLogin.Shared;

namespace WebAppBlazorLogin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDetailDto userDetailDto)
        {
            var hmac = new HMACSHA512();

            var newUserDetail = new UserDetail
            {
                UserName = userDetailDto.UserName,
                PasswordHash = hmac.ComputeHash(userDetailDto.Password),
                PasswordSalt = hmac.Key
            };

            _dataContext.UserDetails.Add(newUserDetail);
            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success) return BadRequest();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDetailDto userDetailDto)
        {
            // Retrieve user from db
            var userDetail = _dataContext.UserDetails.Where(userDetail => userDetail.UserName == userDetailDto.UserName).FirstOrDefault();

            // new hmac using password salt
            var hmac = new HMACSHA512(userDetail.PasswordSalt);

            // Check dto password with db password
            for (int i = 0; i < userDetailDto.Password.Length; i++)
            {
                if (hmac.ComputeHash(userDetailDto.Password)[i] != userDetail.PasswordHash[i])
                {
                    return BadRequest("Invalid Credentials");
                }
            }

            return Ok();
        }
    }
}
