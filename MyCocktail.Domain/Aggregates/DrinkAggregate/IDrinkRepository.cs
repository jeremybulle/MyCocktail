using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    public interface IDrinkRepository
    {
        //Drink
        public Task<Drink> AddAsync(Drink drink);
        public Task<Drink> UpdateAsync(Guid id, Drink drink);
        public Task<Drink> GetByIdAsync(Guid id);
        public Task<Drink> GetByNameAsync(string name);
        public Task<IEnumerable<Drink>> GetAsync();
        public Task<IEnumerable<Drink>> GetLastUpdatedAsync(int nbSearch);
        public void Delete(Guid id);
        public Task<IEnumerable<Drink>> GetDrinksByIngredient(IEnumerable<Guid> ingredientIds);

        //Ingredient
        public Task<Ingredient> AddAsync(Ingredient ingredient);
        public Task<IEnumerable<Ingredient>> GetAllIngredients();
        public Task<Ingredient> GetIngredientById(Guid id);
        public Task<bool> UpdateIngredientAsync(Ingredient ingredient);
    }
}
