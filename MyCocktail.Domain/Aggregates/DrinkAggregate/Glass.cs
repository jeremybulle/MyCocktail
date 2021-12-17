using System;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Glass needed to serve a <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/>
    /// </summary>
    public class Glass : EntityBase
    {
        public override bool Equals(object obj)
        {
            return obj is Glass glass &&
                   Name == glass.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
