using System;
using System.Collections.Generic;

namespace MyCocktail.Infrastucture.Dao
{
    public class IngredientDao
    {
        public Guid Id;
        public string Name;

        public ICollection<MeasureDao> Measures;
    }
}
