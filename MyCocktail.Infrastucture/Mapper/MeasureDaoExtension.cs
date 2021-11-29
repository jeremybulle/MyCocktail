using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class MeasureDaoExtension
    {
        public static Measure ToModel(this MeasureDao measureDao)
        {
            return new Measure() { IngredientName = measureDao.Ingredient.Name, Quantity = measureDao.Quantity };
        }
    }
}
