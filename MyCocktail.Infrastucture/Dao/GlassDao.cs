using System;
using System.Collections.Generic;

namespace MyCocktail.Infrastucture.Dao
{
    public class GlassDao
    {
        public Guid Id;
        public string Name;
        public ICollection<DrinkDao> Drinks;
    }
}
