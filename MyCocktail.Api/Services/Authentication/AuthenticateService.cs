using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Services.Authentication
{
    public interface IAuthenticateService
    {
        public Task<LoginResponse> Authenticate(LoginRequest request);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private IUserRepository _repo;
        private IOptions<AuthOptions> _authOptions;

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
        private string GenerateJwtToken(UserDto user)
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
                    new Claim(ClaimTypes.Role, user.Role) }),
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
