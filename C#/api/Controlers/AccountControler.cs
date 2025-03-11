using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controlers
{
    [Route("api/account")]
    public class AccountControler: ControllerBase
    {
        private readonly UserManager<Reader> _userManager;
        public AccountControler(UserManager<Reader> usermanager)
        {
            _userManager = usermanager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([Frombody] RegisteDTO registeDTO)
        {
            var user = new Reader
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Username = user.UserName, Email = user.Email });
            }
            return BadRequest(result.Errors);
        }
    }
}