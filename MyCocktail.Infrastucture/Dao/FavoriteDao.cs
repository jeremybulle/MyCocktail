using System;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class FavoriteDao
    {
        public Guid Id;

        public Guid IdUser;
        public virtual UserDao User { get; set; }

        public Guid IdDrink;
        public virtual DrinkDao Drink { get; set; }
    }
}
