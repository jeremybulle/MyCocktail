using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class UserDaoExtension
    {
        public static UserDao ToDao(this User userToConvert)
        {
            return new UserDao
            {
                CreationDate = DateTime.Now,
                Id = userToConvert.Id ?? Guid.NewGuid(),
                Password = PasswordHasher.Hash(userToConvert.Password),
                Role = UserRole.User,
                UserName = userToConvert.UserName,
                Email = userToConvert.Email,
                FirstName = userToConvert.FirstName,
                LastName = userToConvert.LastName,
            };
        }
        //public static UserDto ToDto(this UserDao userDao)
        //{
        //    return new UserDto()
        //    {
        //        Id = userDao.Id.ToString(),
        //        UserName = userDao.UserName,
        //        Password = userDao.Password,
        //        Email = userDao.Email,
        //        CreationDate = userDao.CreationDate.ToString(),
        //        Role = userDao.Role.ToString()
        //    };
        //}

        //public static UserDto ToDtoWithoutPassword(this UserDao userDao)
        //{
        //    return new UserDto()
        //    {
        //        Id = userDao.Id.ToString(),
        //        UserName = userDao.UserName,
        //        Email = userDao.Email,
        //        CreationDate = userDao.CreationDate.ToString(),
        //        Role = userDao.Role.ToString()
        //    };
        //}

        //public static IEnumerable<UserDto> ToDtoWithoutPassword(this IEnumerable<UserDao> userDaos)
        //{
        //    return userDaos.Select(u => u.ToDtoWithoutPassword());
        //}

    }
}
