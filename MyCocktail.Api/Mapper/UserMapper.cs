using MyCocktail.Api.Dto;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToDtoNoPassword(this User user)
        {
            if(user == null)
            {
                return null;
            }
            return new UserDto()
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreationDate = user.CreationDate.ToString(),
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role.ToString()
            };
        }

        public static IEnumerable<UserDto> ToDtoNoPassword(this IEnumerable<User> users)
        {
            if (users.IsNullOrEmpty())
            {
                return null;
            }

            return users.Select(u => u.ToDtoNoPassword());

        }
    }
}
