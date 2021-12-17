using System;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class FavoriteDao
    {
        public Guid Id;

        public Guid IdUser;
        public UserDao User;

        public Guid IdDrink;
        public DrinkDao Drink;
    }
}
