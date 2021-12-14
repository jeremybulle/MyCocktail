using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Repository intarface to manage Drink and Ingredient presistence
    /// </summary>
    public interface IDrinkRepository
    {
        //Drink
        /// <summary>
        /// Add a <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> if not already present
        /// </summary>
        /// <param name="drink">Drink to save</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> added </returns>
        public Task<Drink> AddAsync(Drink drink);

        /// <summary>
        /// Update a <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> base on its id
        /// </summary>
        /// <param name="id"> drink's id to update</param>
        /// <param name="drink">drink containing updates</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> updated or null</returns>
        public Task<Drink> UpdateAsync(Guid id, Drink drink);

        /// <summary>
        /// Find a <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> with the id provided or return null 
        /// </summary>
        /// <param name="id">drink's id searched</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> or null</returns>
        public Task<Drink> GetByIdAsync(Guid id);

        /// <summary>
        /// Find a <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> by its name
        /// </summary>
        /// <param name="name">Drink's name searched</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> or null</returns>
        public Task<Drink> GetByNameAsync(string name);

        /// <summary>
        /// Get all <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> saved in Database
        /// </summary>
        /// <returns>IEnumerable of <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/></returns>
        public Task<IEnumerable<Drink>> GetAsync();

        /// <summary>
        /// Find the nbSearch last <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> updated
        /// </summary>
        /// <param name="nbSearch"> Is the number of last drink updated needed</param>
        /// <returns>IEnumerable of nbSearch <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/></returns>
        public Task<IEnumerable<Drink>> GetLastUpdatedAsync(int nbSearch);

        /// <summary>
        /// Remove drink from db
        /// </summary>
        /// <param name="id">drink's id to remove</param>
        public void Delete(Guid id);

        /// <summary>
        /// Find drink that include the list of ingredients discribed by ingredientIds parameter
        /// </summary>
        /// <param name="ingredientIds">list of ingredient id needed</param>
        /// <returns>IEnumerable of <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Drink"/> that have all ingredient with the ids provided in param</returns>
        public Task<IEnumerable<Drink>> GetDrinksByIngredient(IEnumerable<Guid> ingredientIds);

        //Ingredient

        /// <summary>
        /// Add <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> if not already present
        /// </summary>
        /// <param name="ingredient"><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> to save</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> added</returns>
        public Task<Ingredient> AddAsync(Ingredient ingredient);

        /// <summary>
        /// Get all <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> saved in Database
        /// </summary>
        /// <returns>IEnumerable <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/></returns>
        public Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();

        /// <summary>
        /// Find an <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> by its Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> or null</returns>
        public Task<Ingredient> GetIngredientByIdAsync(Guid id);

        /// <summary>
        /// Update an <seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> base on its id
        /// </summary>
        /// <param name="ingredient">Ingredient with updates</param>
        /// <returns><seealso cref="MyCocktail.Domain.Aggregates.DrinkAggregate.Ingredient"/> updated or null</returns>
        public Task<bool> UpdateIngredientAsync(Ingredient ingredient);
    }
}
