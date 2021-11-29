using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.Drink
{
    public interface IDrinkRepository
    {
        //Drink
        public Task<DrinkDto> AddAsync(Drink drink);
        public Task<DrinkDto> UpdateAsync(Guid id, Drink drink);
        public Task<DrinkDto> GetByIdAsync(Guid id);
        public Task<DrinkDto> GetByNameAsync(string name);
        public Task<IEnumerable<DrinkPartialDto>> GetAsync();
        public Task<IEnumerable<DrinkPartialDto>> GetLastUpdatedAsync(int nbSearch);
        public void Delete(Guid id);
        public Task<IEnumerable<DrinkPartialDto>> GetDrinksByIngredient(IEnumerable<Guid> ingredientIds);

        //Ingredient
        public Task<IngredientDto> AddAsync(Ingredient ingredient);
        public Task<IEnumerable<IngredientDto>> GetAllIngredients();
        public Task<IngredientDto> GetIngredientById(Guid id);
        public Task<bool> UpdateIngredientAsync(IngredientDto ingredient);
    }
}
