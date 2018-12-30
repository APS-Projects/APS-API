using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APS_API.Data;
using APS_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace APS_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context, UserManager<User> user)
        {
            _userManager = user;
            _context = context;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUser()
        {
            string userName = User.Identity.Name;
            User user = _context.User.Single(u => u.UserName == userName);
            return Ok(user);
        }

        //PUT: api/Users/id
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutEdit([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            string userName = User.Identity.Name;
            User defaultUser = _context.User.Single(u => u.UserName == userName);

            if (user.IsActive == false)
            {
                defaultUser.FirstName = "Anonymous";
                defaultUser.LastName = null;
                defaultUser.AvatarUrl = null;
                defaultUser.UserName = null;
                defaultUser.NormalizedUserName = null;
                defaultUser.Email = null;
                defaultUser.NormalizedEmail = null;
            }
            else
            {
                defaultUser.FirstName = (user.FirstName != null) ? user.FirstName : defaultUser.FirstName;
                defaultUser.LastName = (user.LastName != null) ? user.LastName : defaultUser.LastName;
                defaultUser.AvatarUrl = (user.AvatarUrl != null) ? user.AvatarUrl : defaultUser.AvatarUrl;
            }


            _context.Entry(defaultUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UserExists(string id)
        {
            string userName = User.Identity.Name;
            User user = _context.User.Single(u => u.UserName == userName);
            return (user != null) ? true : false;
        }

    }
}