using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// Repository intarface to manage <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> presistence
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Find All <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> in database
        /// </summary>
        /// <returns>IEnumerable <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/></returns>
        public Task<IEnumerable<User>> GetAsync();

        /// <summary>
        /// Find a <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> by his/her Id
        /// </summary>
        /// <param name="id">Unique id of <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/></param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> or null</returns>
        public Task<User> GetByIdAsync(Guid id);

        /// <summary>
        /// Find a <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> by his/her Username
        /// </summary>
        /// <param name="userName">UserName of user searched</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> or null </returns>
        public Task<User> GetByUserNameAsync(string userName);

        /// <summary>
        /// Add a <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> to database
        /// </summary>
        /// <param name="user"><seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> to save</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> saved</returns>
        public Task<User> AddAsync(User user);

        /// <summary>
        /// Update an <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/>
        /// </summary>
        /// <param name="user"><seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> with updates</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> updated</returns>
        public Task<User> UpdateAsync(User user);

        /// <summary>
        /// Remove an <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> from database
        /// </summary>
        /// <param name="id">id of <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> to remove</param>
        /// <returns>True if <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/> is removed false if not</returns>
        public Task<bool> DeleteAsync(Guid id);

        //Favorites

        /// <summary>
        /// Find all favorite <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/>s of an <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/>
        /// </summary>
        /// <param name="idUser">Id of the <seealso cref="MyCocktail.Domain.Aggregates.UserAggregate.User"/></param>
        /// <returns>IEnumerable <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/></returns>
        public Task<IEnumerable<Drink>> GetFavoritesAsync(Guid idUser);
    }
}
