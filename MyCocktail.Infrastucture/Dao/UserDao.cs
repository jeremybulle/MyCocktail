using MyCocktail.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class UserDao
    {
        public Guid Id;
        public string Password;
        public string UserName;
        public string FirstName;
        public string LastName;
        public string Email;
        public DateTime CreationDate;
        public UserRole Role;

        public virtual ICollection<FavoriteDao> Favorites { get; set; }
        public virtual ICollection<DrinkDao> DrinksOwned { get; set; }
    }
}
