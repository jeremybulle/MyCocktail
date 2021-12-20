using System;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.Dao
{
    [ExcludeFromCodeCoverage]
    public class MeasureDao
    {
        public Guid Id;
        public string Quantity;

        public Guid DrinkId;
        public virtual DrinkDao Drink { get; set; }

        public Guid IngredientId;
        public virtual IngredientDao Ingredient { get; set; }
    }
}
