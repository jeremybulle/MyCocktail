using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastucture.Dao;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class UserDaoExtension
    {
        public static User ToModel(this UserDao userToConvert)
        {
            return new User()
            {
                Id = userToConvert.Id,
                FirstName = userToConvert.FirstName,
                LastName = userToConvert.LastName,
                UserName = userToConvert.UserName,
                Email = userToConvert.Email,
                Password = userToConvert.Password,
                Role = userToConvert.Role,
                CreationDate = userToConvert.CreationDate,
            };
        }

        public static IEnumerable<User> ToModel(this IEnumerable<UserDao> usersToConvert)
        {
            return usersToConvert.Select(u => u.ToModel());
        }
    }
}
