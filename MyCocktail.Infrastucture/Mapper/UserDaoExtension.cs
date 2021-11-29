using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class UserDaoExtension
    {
        public static UserDto ToDto(this UserDao userDao)
        {
            return new UserDto()
            {
                Id = userDao.Id.ToString(),
                UserName = userDao.UserName,
                Password = userDao.Password,
                Email = userDao.Email,
                CreationDate = userDao.CreationDate.ToString(),
                Role = userDao.Role.ToString()
            };
        }

        public static UserDto ToDtoWithoutPassword(this UserDao userDao)
        {
            return new UserDto()
            {
                Id = userDao.Id.ToString(),
                UserName = userDao.UserName,
                Email = userDao.Email,
                CreationDate = userDao.CreationDate.ToString(),
                Role = userDao.Role.ToString()
            };
        }

        public static IEnumerable<UserDto> ToDtoWithoutPassword(this IEnumerable<UserDao> userDaos)
        {
            return userDaos.Select(u => u.ToDtoWithoutPassword());
        }

    }
}
