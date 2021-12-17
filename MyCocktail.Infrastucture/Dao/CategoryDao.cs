using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class CategoryDao
    {
        public Guid Id;
        public string Name;
        public ICollection<DrinkDao> Drinks;
    }
}
