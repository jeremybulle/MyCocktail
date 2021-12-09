using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyCocktail.Api.Services.Authentication.Models;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Api.Services.Authentication
{
    public interface IAuthenticateService
    {
        public Task<LoginResponse> Authenticate(LoginRequest request);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _repo;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthenticateService(IUserRepository repo, IOptions<AuthOptions> authOptions)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
        }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            var user = await _repo.GetByUserNameAsync(request.UserName);

            if (user != null && PasswordHasher.Check(user.Password, request.Password))
            {
                return new LoginResponse()
                {
                    UserName = user.UserName,
                    Token = GenerateJwtToken(user)
                };
            }

            return null;
        }

        // helper methods
        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();

            var myIssuer = "https://localhost:44337";
            var myAudience = "http://localhost:3000";

            var key = Encoding.ASCII.GetBytes(_authOptions.Value.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()) }),
                Issuer = myIssuer,
                Audience = myAudience,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
