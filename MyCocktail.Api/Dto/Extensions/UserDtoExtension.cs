using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using System;
namespace MyCocktail.Api.Dto.Extensions
{
    public static class UserDtoExtension
    {
        public static User ToModel(this UserDto userDto)
        {
            return new User()
            {
                Role = (UserRole)Enum.Parse(typeof(UserRole), userDto.Role),
                Id = new Guid(userDto.Id),
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password.IsNullOrEmpty()? throw new ArgumentException("User Password can not be null or empty") : PasswordHasher.Hash(userDto.Password),
            };
        }
    }
}
