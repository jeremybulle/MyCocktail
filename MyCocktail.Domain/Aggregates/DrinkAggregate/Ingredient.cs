using MyCocktail.Domain.Helper;
using System;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Ingredient used in <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Measure"/> to composed <seealso cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Drink"/>
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
