using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class IngredientExtension
    {
        public static IngredientDao ToDao(this Ingredient ingredientToMap)
        {
            return new IngredientDao()
            {
                Name = ingredientToMap.Name
            };
        }
    }
}
