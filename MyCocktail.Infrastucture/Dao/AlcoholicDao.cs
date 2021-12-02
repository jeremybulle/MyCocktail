using System;
using System.Collections.Generic;

namespace MyCocktail.Infrastucture.Dao
{
    public class AlcoholicDao
    {
        public Guid Id;
        public string Name;
        public ICollection<DrinkDao> Drinks;
    }
}
