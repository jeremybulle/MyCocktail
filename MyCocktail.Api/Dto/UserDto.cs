using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Api.Dto
{
    [ExcludeFromCodeCoverage]
    public class UserDto
    {
        public string Id;
        public string UserName;
        public string Email;
        public string CreationDate;
        public string Password;
        public string Role;
        public string FirstName;
        public string LastName;
    }
}
