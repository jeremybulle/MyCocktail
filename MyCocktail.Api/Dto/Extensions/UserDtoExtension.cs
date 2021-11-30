using MyCocktail.Domain.Aggregates.UserAggregate;
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
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
            };
        }
    }
}
