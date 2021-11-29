using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCocktail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/<UsersController>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAsync();
            return result.IsNullOrEmpty() ? NoContent() : Ok(result);
        }

        // GET api/<UsersController>/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string idUser)
        {
            Guid id;
            try
            {
                id = Guid.Parse(idUser);
            }
            catch (Exception)
            {

                throw new ArgumentException(nameof(idUser));
            }
            var result = await _repo.GetByIdAsync(id);

            return result == null ? NotFound(idUser) : Ok(result);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userFromBody)
        {
            User userToSave;

            try
            {
                userToSave = new User()
                {
                    Email = userFromBody.Email,
                    FirstName = userFromBody.FirstName,
                    LastName = userFromBody.LastName,
                    Password = userFromBody.Password,
                    Role = UserRole.Reader,
                    UserName = userFromBody.UserName
                };
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(userFromBody));
            }

            var result = await _repo.AddAsync(userToSave);

            return result != null ? Ok(result) : BadRequest();

        }

        // PUT api/<UsersController>/5
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDto userFromBody)
        {
            Guid id;
            try
            {
                var idUser = HttpContext.User.GetUserId();
                id = Guid.Parse(idUser);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (id == Guid.Parse(userFromBody.Id) || HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    var userDomain = userFromBody.ToModel();
                    var userToSave = userDomain.ToDto();
                }
                catch (Exception)
                {
                    throw new ArgumentException(nameof(userFromBody));
                }

                var result = await _repo.UpdateAsync(userFromBody);

                return result ? Ok(userFromBody) : BadRequest(userFromBody);
            }

            return BadRequest();
        }

        // DELETE api/<UsersController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            var result = await _repo.DeleteAsync(id);

            return result ? Ok() : NotFound(id);
        }

        // GET api/<UsersController>/favorites
        [Authorize]
        [HttpGet]
        [Route("/api/Favorites")]
        public async Task<IActionResult> GetFavorits()
        {
            Guid id;
            try
            {
                var idUser = HttpContext.User.GetUserId();
                id = Guid.Parse(idUser);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var result = await _repo.GetFavorites(id);

            return result.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
    }
}
