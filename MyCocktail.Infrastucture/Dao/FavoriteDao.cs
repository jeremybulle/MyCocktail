using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
