using MyCocktail.Domain.Helper;
using System;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Ingredient used in <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Measure"/> to composed <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/>
    /// </summary>
    public class Ingredient : EntityBase
    {
        public override bool Equals(object obj)
        {
            return obj is Ingredient ingredient &&
                   Name == ingredient.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
