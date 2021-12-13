using Microsoft.EntityFrameworkCore;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<User> AddAsync(User user)
        {
            if (user != null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userToSave = user.ToDao();
            if (userToSave.Password.IsNullOrEmpty())
            {
                throw new ArgumentException("Can not Create an user wihtout password");
            }

            return AddInternalAsync(userToSave);

        }

        private async Task<User> AddInternalAsync(UserDao userTosave)
        {
            
            await _context.Users.AddAsync(userTosave).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return userTosave.ToModel();
          
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);

            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            var query = _context.Users.OrderBy(u => u.LastName).OrderBy(u => u.FirstName);
            var result = await query.ToListAsync().ConfigureAwait(false);

            return result.ToModel();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
            return result == null ? null : result.ToModel();
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var query = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName).ConfigureAwait(false);
            var result = query?.ToModel();

            return result == null ? null : result;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var userDao = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id).ConfigureAwait(false);

            if (userDao == null)
            {
                return false;
            }

            userDao.FirstName = user.FirstName;
            userDao.LastName = user.LastName;
            userDao.Email = user.Email;
            userDao.Role = user.Role;
            userDao.UserName = user.UserName;
            userDao.Password = user.Password.IsNullOrEmpty() ? PasswordHasher.Hash(user.Password) : throw new ArgumentNullException(nameof(user),"Can not be Updated with null or empty password");

            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<IEnumerable<Drink>> GetFavoritesAsync(Guid idUser)
        {
            var query = _context.Favorites.Include(f => f.Drink).Where(f => f.IdUser == idUser).Select(f => f.Drink);
            var result = await query.ToListAsync().ConfigureAwait(false);
            return result.IsNullOrEmpty() ? null : result.Select(d => d.ToModel());
        }
    }
}
