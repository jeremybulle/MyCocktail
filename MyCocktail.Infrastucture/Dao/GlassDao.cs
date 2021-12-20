using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class GlassDao
    {
        public Guid Id;
        public string Name;
        public virtual ICollection<DrinkDao> Drinks { get; set; }
    }
}
