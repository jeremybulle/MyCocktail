using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyCocktail.Api.Services.Authentication;
using MyCocktail.Api.Services.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCocktail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IAuthenticateService _authService;

        public LoginController(IOptions<AuthOptions> authOptions, IAuthenticateService authService)
        {
            _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return NotFound("Empty Request");
            }

            var response = await _authService.Authenticate(request);

            return response != null ? Ok(response) : NotFound(response);
        }
    }
}
