using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Dao;
using System;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class UserExtension
    {
       public static UserDao ToDao(this User userToMap)
        {
            if(userToMap == null)
            {
                return null;
            }

            return new UserDao()
            {
                Id = userToMap.Id ?? Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Email = userToMap.Email,
                FirstName = userToMap.FirstName,
                LastName = userToMap.LastName,
                Password = PasswordHasher.Hash(userToMap.Password),
                UserName = userToMap.UserName,
                Role = userToMap.Role,
            };
        }
    }
}
