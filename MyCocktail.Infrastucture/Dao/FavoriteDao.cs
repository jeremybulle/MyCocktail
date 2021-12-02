using System;

namespace MyCocktail.Infrastucture.Dao
{
    public class FavoriteDao
    {
        public Guid Id;

        public Guid IdUser;
        public UserDao User;

        public Guid IdDrink;
        public DrinkDao Drink;
    }
}
