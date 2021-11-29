using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
