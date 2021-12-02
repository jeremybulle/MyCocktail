using System;

namespace MyCocktail.Infrastucture.Dao
{
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
