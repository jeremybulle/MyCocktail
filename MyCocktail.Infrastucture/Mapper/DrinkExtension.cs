using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Dao;
using System;

namespace MyCocktail.Infrastucture.Mapper
{
    public static class DrinkExtension
    {
        public static DrinkDao ToDao(this Drink drink)
        {
            var categoryToAdd = new CategoryDao() { Id = (Guid)(drink.Category.Id != null ? drink.Category.Id : Guid.NewGuid()), Name = drink.Category.Name };
            var glassToAdd = new GlassDao() { Id = (Guid)(drink.Glass.Id != null ? drink.Glass.Id : Guid.NewGuid()), Name = drink.Glass.Name };
            var alcoholicToAdd = new AlcoholicDao() { Id = (Guid)(drink.Alcoholic.Id != null ? drink.Alcoholic.Id : Guid.NewGuid()), Name = drink.Alcoholic.Name };

            var drinkToReturn = new DrinkDao()
            {
                Id = drink.Id ?? Guid.NewGuid(),
                IdSource = drink.IdSource,
                Instruction = drink.Instruction ?? throw new Exception($"{nameof(drink.Instruction)} can not be null"),
                Name = drink.Name ?? throw new Exception($"{nameof(drink.Name)} can not be null"),
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
