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
        public DrinkDao Drink;

        public Guid IngredientId;
        public IngredientDao Ingredient;
    }
}
