using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.UserAggregate
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAsync();
        public Task<User> GetByIdAsync(Guid id);
        public Task<User> GetByUserNameAsync(string userName);
        public Task<User> AddAsync(User user);
        public Task<bool> UpdateAsync(User user);
        public Task<bool> DeleteAsync(Guid id);

        //Favorites
        public Task<IEnumerable<Drink>> GetFavoritesAsync(Guid idUser);
    }
}
