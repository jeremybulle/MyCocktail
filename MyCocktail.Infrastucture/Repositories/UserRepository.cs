using Microsoft.EntityFrameworkCore;
using MyCocktail.Domain.Aggregates.User;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DrinkDbContext _context;

        public UserRepository(DrinkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserDto> AddAsync(User user)
        {
            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var userToSave = user.ToDao();
                await _context.Users.AddAsync(userToSave);
                await _context.SaveChangesAsync();

                return userToSave.ToDto();
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            var query = _context.Users.OrderBy(u => u.LastName).OrderBy(u => u.FirstName);
            var result = await query.ToListAsync();

            return result.ToDtoWithoutPassword();
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return result == null ? null : result.ToDtoWithoutPassword();
        }

        public async Task<UserDto> GetByUserNameAsync(string userName)
        {
            var query = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            var result = query?.ToDto();

            return result == null ? null : result;
        }

        public async Task<bool> UpdateAsync(UserDto user)
        {
            var userDao = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(user.Id));

            if (userDao == null)
            {
                return false;
            }

            userDao.FirstName = user.FirstName;
            userDao.LastName = user.LastName;
            userDao.Email = user.Email;
            userDao.Role = (UserRole)UInt32.Parse(user.Role);
            userDao.UserName = user.UserName;
            userDao.Password = PasswordHasher.Hash(user.Password);

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<IEnumerable<DrinkPartialDto>> GetFavorites(Guid idUser)
        {
            var query = _context.Favorites.Include(f => f.Drink).Where(f => f.IdUser == idUser).Select(f => f.Drink);
            var result = await query.ToListAsync();
            return result.IsNullOrEmpty() ? null : result.Select(d => d.ToPartialDto());
        }
    }
}
