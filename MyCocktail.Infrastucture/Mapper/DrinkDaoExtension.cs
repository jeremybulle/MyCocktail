using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class DrinkDaoExtension
    {
        public static DrinkDto ToDto(this DrinkDao drinkDao)
        {
            var measures = new List<MeasureDto>();

            var drinkToReturn = new DrinkDto
            {
                Id = drinkDao.Id.ToString(),
                IdSource = drinkDao.IdSource,
                Category = new CategoryDto() { Id = drinkDao.Category.Id.ToString(), Name = drinkDao.Category.Name },
                Alcoholic = new AlcoholicDto() { Id = drinkDao.Alcoholic.Id.ToString(), Name = drinkDao.Alcoholic.Name },
                Instruction = drinkDao.Instruction,
                Glass = new GlassDto() { Id = drinkDao.Glass.Id.ToString(), Name = drinkDao.Glass.Name },
                Name = drinkDao.Name,
                UrlPicture = drinkDao.UrlPicture != null ? drinkDao.UrlPicture.ToString() : null,
                DateModified = drinkDao.DateModified.ToString()
            };

            foreach (var measure in drinkDao.Measures)
            {
                measures.Add(new MeasureDto() { Id = measure.Id.ToString(), Ingedient = new IngredientDto() { Id = measure.IngredientId.ToString(), Name = measure.Ingredient.Name }, Quantity = measure.Quantity });
            }

            drinkToReturn.Measures = measures;

            return drinkToReturn;
        }

        public static DrinkPartialDto ToPartialDto(this DrinkDao drinkDao)
        {
            return new DrinkPartialDto
            {
                Id = drinkDao.Id.ToString(),
                Name = drinkDao.Name,
                AlcoholicName = drinkDao.Alcoholic.Name,
                CategoryName = drinkDao.Category.Name,
                GlassName = drinkDao.Glass.Name,
                UrlPicture = drinkDao.UrlPicture
            };
        }
    }
}
