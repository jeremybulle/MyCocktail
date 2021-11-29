using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class UserExtension
    {
        public static UserDao ToDao(this User userToConvert)
        {
            return new UserDao
            {
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Password = PasswordHasher.Hash(userToConvert.Password),
                Role = UserRole.Reader,
                UserName = userToConvert.UserName,
                Email = userToConvert.Email,
                FirstName = userToConvert.FirstName,
                LastName = userToConvert.LastName,
            };
        }
    }
}
