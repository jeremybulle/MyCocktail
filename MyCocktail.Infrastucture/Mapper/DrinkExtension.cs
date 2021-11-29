using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class DrinkExtension
    {
        public static DrinkDao ToDao(this Drink drink)
        {
            var categoryToAdd = new CategoryDao() { Id = Guid.NewGuid(), Name = drink.Category.Name };
            var glassToAdd = new GlassDao() { Id = Guid.NewGuid(), Name = drink.Glass.Name };
            var alcoholicToAdd = new AlcoholicDao() { Id = Guid.NewGuid(), Name = drink.Alcoholic.Name };

            var drinkToReturn = new DrinkDao()
            {
                Id = Guid.NewGuid(),
                IdSource = drink.IdSource ?? throw new ArgumentNullException(nameof(drink.IdSource)),
                Instruction = drink.Instruction ?? throw new ArgumentNullException(nameof(drink.Instruction)),
                Name = drink.Name ?? throw new ArgumentNullException(nameof(drink.Name)),
                UrlPicture = drink.UrlPicture.ToString(),
                Category = categoryToAdd,
                CategoryId = categoryToAdd.Id,
                Glass = glassToAdd,
                GlassId = glassToAdd.Id,
                Alcoholic = alcoholicToAdd,
                AlcoholicId = alcoholicToAdd.Id,
                DateModified = drink.DateModified,
                OwnerId = drink.IdOwner
            };

            return drinkToReturn;
        }
    }
}
