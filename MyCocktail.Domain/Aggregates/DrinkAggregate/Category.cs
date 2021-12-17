using System;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Category of <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/>, for exemple : coffe/tea, beer, shot, etc...
    /// </summary>
    public class Category : EntityBase
    {
        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Name == category.Name &&
                   Id == category.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
