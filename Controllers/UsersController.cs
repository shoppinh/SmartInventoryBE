using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Enums;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;

namespace SmartInventoryBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new User
                {
                    UserName = dto.Username,
                    Email = dto.Email,

                };

                var createdUser = await _userManager.CreateAsync(appUser, dto.Password);

                if (!createdUser.Succeeded) return BadRequest(createdUser.Errors);

                var roleResult = await _userManager.AddToRoleAsync(appUser, Role.User.ToString());

                return roleResult.Succeeded ? Ok("User created") : StatusCode(500, roleResult.Errors);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName != null && x.UserName.Equals(dto.UserName.ToLower()));
                if (user == null) return Unauthorized("Invalid username");
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (!result.Succeeded) return Unauthorized("Username not found and/or password is not correct");


                return Ok(new UserResponse()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
