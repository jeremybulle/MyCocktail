using Microsoft.AspNetCore.Mvc;
using MyCocktail.Api.Services.Authentication;
using MyCocktail.Domain.Helper;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCocktail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/<TestController>
        [HttpGet]
        public IActionResult Get()
        {
            var id = HttpContext.User.GetUserId();

            return id.IsNullOrEmpty() ? NotFound() : Ok(id);
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }
    }
}
