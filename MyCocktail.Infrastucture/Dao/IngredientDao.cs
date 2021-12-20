using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class IngredientDao
    {
        public Guid Id;
        public string Name;

        public virtual ICollection<MeasureDao> Measures { get; set; }
    }
}
