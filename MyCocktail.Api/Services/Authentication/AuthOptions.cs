namespace MyCocktail.Api.Services.Authentication
{
    public class AuthOptions
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtExpireDays { get; set; }
    }
}
