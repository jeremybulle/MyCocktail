using Microsoft.EntityFrameworkCore;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly DrinkDbContext _context;
        private readonly Dictionary<string, AlcoholicDao> _alcoholicsCache = new Dictionary<string, AlcoholicDao>();
        private readonly Dictionary<string, CategoryDao> _categoriesCache = new Dictionary<string, CategoryDao>();
        private readonly Dictionary<string, GlassDao> _glassesCache = new Dictionary<string, GlassDao>();
        private readonly Dictionary<string, DrinkDao> _drinksCache = new Dictionary<string, DrinkDao>();
        private readonly Dictionary<string, IngredientDao> _ingredientsCache = new Dictionary<string, IngredientDao>();
        private readonly Dictionary<(string, string, string), MeasureDao> _measureCache = new Dictionary<(string, string, string), MeasureDao>(); // key (drinkName, ingredientName, Quantity ) utiliser un Hashset?

        public DrinkRepository(DrinkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

       
        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.AddAsync(Drink)"/>
        public async Task<Drink> AddAsync(Drink drink)
        {
            if (!_context.Drinks.Any(d => d.Name == drink.Name))
            {
                var drinkToSave = drink.ToDao();

                drinkToSave.Id = Guid.NewGuid();
                drinkToSave.Alcoholic = await AddAsync(drinkToSave.Alcoholic).ConfigureAwait(false);
                drinkToSave.AlcoholicId = drinkToSave.Alcoholic.Id;
                drinkToSave.Category = await AddAsync(drinkToSave.Category).ConfigureAwait(false);
                drinkToSave.CategoryId = drinkToSave.Category.Id;
                drinkToSave.Glass = await AddAsync(drinkToSave.Glass).ConfigureAwait(false);
                drinkToSave.GlassId = drinkToSave.Glass.Id;

                await _context.Drinks.AddAsync(drinkToSave);

                foreach (var measureToSave in drink.GetMeasures())
                {
                    var ingredientCurrent = await AddAsync(new IngredientDao() { Id = Guid.NewGuid(), Name = measureToSave.Ingredient.Name });
                    await AddAsync(new MeasureDao() { Id = Guid.NewGuid(), Drink = drinkToSave, DrinkId = drinkToSave.Id, Ingredient = ingredientCurrent, IngredientId = ingredientCurrent.Id, Quantity = measureToSave.Quantity }, drinkToSave.Name);
                }

                await _context.Drinks.AddAsync(drinkToSave).ConfigureAwait(false);

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return drinkToSave.ToModel();
            }
            return (await _context.Drinks.FirstOrDefaultAsync(d => d.Name == drink.Name).ConfigureAwait(false)).ToModel();
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.Delete(Guid)"/>
        public void Delete(Guid id)
        {
            var drinkToDelete = _context.Drinks.Include(d => d.Measures).FirstOrDefault(d => d.Id == id);
            if (drinkToDelete != null)
            {
                _context.Measures.RemoveRange(drinkToDelete.Measures);
                _context.Drinks.Remove(drinkToDelete);
                _context.SaveChanges();
            }
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetByNameAsync(string)"/>
        public async Task<Drink> GetByNameAsync(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return null;
            }
            var result = await _context.Drinks.FirstOrDefaultAsync(d => d.Name == name).ConfigureAwait(false);
            return result != null ? result.ToModel() : null;
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetAsync()"/>
        public async Task<IEnumerable<Drink>> GetAsync()
        {
            var drinksFind = new List<Drink>();
            var query = _context.Drinks.OrderBy(d => d.Name).Include(d => d.Glass).Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Measures).ThenInclude(m => m.Ingredient);
            foreach (var drinkDao in await query.ToListAsync().ConfigureAwait(false))
            {
                drinksFind.Add(drinkDao.ToModel());
            }

            return drinksFind;
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetByIdAsync(Guid)"/>
        public async Task<Drink> GetByIdAsync(Guid id)
        {
            var drinkFound = await _context.Drinks.Include(d => d.Glass).Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Measures).ThenInclude(m => m.Ingredient).FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);
            if (drinkFound != null)
            {
                return drinkFound.ToModel();
            }
            return null;
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetLastUpdatedAsync(int)"/>
        public async Task<IEnumerable<Drink>> GetLastUpdatedAsync(int nbSearch)
        {
            var drinkSorted = _context.Drinks.Include(d => d.Glass).Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Measures).ThenInclude(m => m.Ingredient).OrderByDescending(d => d.DateModified).Take(nbSearch);
            var listToReturn = new List<Drink>();
            foreach (var drink in await drinkSorted.ToListAsync().ConfigureAwait(false))
            {
                listToReturn.Add(drink.ToModel());
            }
            return listToReturn;
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.UpdateAsync(Guid, Drink)"/>
        public async Task<Drink> UpdateAsync(Guid id, Drink drink)
        {
            var drinkToUpdate = await _context.Drinks
                .Include(d => d.Alcoholic)
                .Include(d => d.Category)
                .Include(d => d.Glass)
                .FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);

            if (drinkToUpdate == null)
            {
                return null;
            }

            drinkToUpdate.IdSource = drink.IdSource;
            drinkToUpdate.OwnerId = drink.IdOwner;
            drinkToUpdate.Instruction = drink.Instruction;
            drinkToUpdate.Name = drink.Name;
            drinkToUpdate.UrlPicture = drink.UrlPicture.ToString();

            if (drinkToUpdate.Alcoholic.Name != drink.Alcoholic.Name)
            {
                var alcoholicDao = await _context.Alcoholics.FirstOrDefaultAsync(a => a.Name == drink.Alcoholic.Name).ConfigureAwait(false);

                if (alcoholicDao != null)
                {
                    drinkToUpdate.AlcoholicId = alcoholicDao.Id;
                }
                else
                {
                    var alcoholicToSave = new AlcoholicDao()
                    {
                        Id = Guid.NewGuid(),
                        Name = drink.Alcoholic.Name,
                        Drinks = new List<DrinkDao> { drinkToUpdate }
                    };
                    await _context.Alcoholics.AddAsync(alcoholicToSave).ConfigureAwait(false);
                }
            }

            if (drinkToUpdate.Category.Name != drink.Category.Name)
            {
                var categoryDao = await _context.Categories.FirstOrDefaultAsync(c => c.Name == drink.Category.Name).ConfigureAwait(false);

                if (categoryDao != null)
                {
                    drinkToUpdate.CategoryId = categoryDao.Id;
                }
                else
                {
                    var categoryToSave = new CategoryDao()
                    {
                        Id = Guid.NewGuid(),
                        Name = drink.Category.Name,
                        Drinks = new List<DrinkDao> { drinkToUpdate }
                    };
                    await _context.Categories.AddAsync(categoryToSave).ConfigureAwait(false);
                }
            }

            if (drinkToUpdate.Glass.Name != drink.Category.Name)
            {
                var glassDao = await _context.Glasses.FirstOrDefaultAsync(g => g.Name == drink.Glass.Name).ConfigureAwait(false);

                if (glassDao != null)
                {
                    drinkToUpdate.GlassId = glassDao.Id;
                }
                else
                {
                    var glassToSave = new GlassDao()
                    {
                        Id = Guid.NewGuid(),
                        Name = drink.Glass.Name,
                        Drinks = new List<DrinkDao> { drinkToUpdate }
                    };
                    await _context.Glasses.AddAsync(glassToSave).ConfigureAwait(false);
                }
            }

            drinkToUpdate.DateModified = DateTime.Now;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            var result = await _context.Drinks.FirstOrDefaultAsync(d => d.Id == drinkToUpdate.Id).ConfigureAwait(false);

            return result != null ? result.ToModel() : null;
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetDrinksByIngredient(IEnumerable{Guid})"/>
        public async Task<IEnumerable<Drink>> GetDrinksByIngredient(IEnumerable<Guid> ingredientIds)
        {

            //var query = _context.Measures.Include(m => m.Drink).Include(m => m.Drink.Alcoholic).Include(m => m.Drink.Category).Include(m => m.Drink.Glass).Where(m => ingredientIds.Any(i => i == m.IngredientId)).Select(m => m.Drink); //=> Drink Contient au moins 1 des ingredients de la liste
            //var query = _context.Drinks.Include(d => d.Measures).Include(d => d.Alcoholic).Include(d => d.Category).Include(d => d.Glass).Where(d => d.Measures.All( m => ingredientIds.Contains(m.IngredientId))); => Drink contient tous les ingrétients de la liste pas plus pas moins

            //var grp = await Task.FromResult(_context.Measures.Include(m => m.Drink).Include(m => m.Drink.Alcoholic).Include(m => m.Drink.Category).Include(m => m.Drink.Glass).Include(m => m.Ingredient).AsEnumerable().GroupBy(m => m.Drink, (key, g) => new { Drink = key, ingredients = g.Select(e => e.IngredientId) })).ConfigureAwait(false);
            
            var query = await _context.Measures.Include(m => m.Drink).Include(m => m.Drink.Alcoholic).Include(m => m.Drink.Category).Include(m => m.Drink.Glass).Include(m => m.Ingredient).ToListAsync();

            var grp = query.GroupBy(m => m.Drink, (key, g) => new { Drink = key, ingredients = g.Select(e => e.IngredientId) });

            var grpPurged = grp.Where(g => ingredientIds.All(ingredientId => g.ingredients.Any(i => i == ingredientId)));

            return grpPurged.Select(e => e.Drink.ToModel());
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetAllIngredientsAsync()"/>
        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            var ingredientsDao = await _context.Ingredients.ToListAsync().ConfigureAwait(false);

            if (ingredientsDao.IsNullOrEmpty())
            {
                return new List<Ingredient>();
            }

            return ingredientsDao.ToModel();
        }

        /// <inheritdoc cref="MyCocktail.Domain.Aggregates.DrinkAggregate.IDrinkRepository.GetIngredientByIdAsync(Guid)"/>
        public async Task<Ingredient> GetIngredientByIdAsync(Guid id)
        {
            var ingredientDao = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);

            if (ingredientDao == null)
            {
                return null;
            }
            return ingredientDao.ToModel();
        }

        public void ClearCaches()
        {
            _alcoholicsCache.Clear();
            _categoriesCache.Clear();
            _glassesCache.Clear();
            _drinksCache.Clear();
            _ingredientsCache.Clear();
            _measureCache.Clear();
        }

        private async Task<AlcoholicDao> AddAsync(AlcoholicDao alcoholic)
        {
            if (!_alcoholicsCache.ContainsKey(alcoholic.Name))
            {
                if (!_context.Alcoholics.Any(a => a.Name == alcoholic.Name))
                {
                    await _context.Alcoholics.AddAsync(alcoholic).ConfigureAwait(false);
                    _alcoholicsCache.Add(alcoholic.Name, alcoholic);
                }
                else
                {
                    var alcoholicFound = await _context.Alcoholics.FirstAsync(a => a.Name == alcoholic.Name).ConfigureAwait(false);
                    _alcoholicsCache.Add(alcoholicFound.Name, alcoholicFound);
                    return alcoholicFound;
                }
            }
            return _alcoholicsCache[alcoholic.Name];
        }

        private async Task<CategoryDao> AddAsync(CategoryDao category)
        {
            if (!_categoriesCache.ContainsKey(category.Name))
            {
                if (!_context.Categories.Any(c => c.Name == category.Name))
                {
                    await _context.Categories.AddAsync(category).ConfigureAwait(false);
                    _categoriesCache.Add(category.Name, category);
                }
                else
                {
                    var categoryFound = await _context.Categories.FirstAsync(c => c.Name == category.Name).ConfigureAwait(false);
                    _categoriesCache.Add(categoryFound.Name, categoryFound);
                    return categoryFound;
                }
            }
            return _categoriesCache[category.Name];
        }

        private async Task<GlassDao> AddAsync(GlassDao glass)
        {
            if (!_glassesCache.ContainsKey(glass.Name))
            {
                if (!_context.Glasses.Any(g => g.Name == glass.Name))
                {
                    await _context.Glasses.AddAsync(glass).ConfigureAwait(false);
                    _glassesCache.Add(glass.Name, glass);
                }
                else
                {
                    var glassFound = await _context.Glasses.FirstAsync(g => g.Name == glass.Name).ConfigureAwait(false);
                    _glassesCache.Add(glassFound.Name, glassFound);
                    return glassFound;
                }
            }
            return _glassesCache[glass.Name];
        }

        private async Task<IngredientDao> AddAsync(IngredientDao ingredient)
        {
            if (!_ingredientsCache.ContainsKey(ingredient.Name))
            {
                if (!_context.Ingredients.Any(i => i.Name == ingredient.Name))
                {
                    await _context.Ingredients.AddAsync(ingredient).ConfigureAwait(false);
                    _ingredientsCache.Add(ingredient.Name, ingredient);
                }
                else
                {
                    var ingredientFound = await _context.Ingredients.FirstAsync(i => i.Name == ingredient.Name).ConfigureAwait(false);
                    _ingredientsCache.Add(ingredientFound.Name, ingredientFound);
                    return ingredientFound;
                }
            }
            return _ingredientsCache[ingredient.Name];
        }

        private async Task AddAsync(MeasureDao measure, string drinkName)
        {
            if (!_measureCache.ContainsKey((drinkName, measure.Ingredient.Name, measure.Quantity)))
            {
                if (_context.Measures.Where(m => m.Drink.Name == drinkName && m.Ingredient.Name == measure.Ingredient.Name && m.Quantity == measure.Quantity).Count() < 1)
                {
                    await _context.Measures.AddAsync(measure).ConfigureAwait(false);
                    _measureCache.Add((drinkName, measure.Ingredient.Name, measure.Quantity), measure);
                }
                else
                {
                    var measureFound = await _context.Measures.FirstAsync(m => m.Drink.Name == drinkName && m.Ingredient.Name == measure.Ingredient.Name && m.Quantity == measure.Quantity).ConfigureAwait(false);
                    _measureCache.Add((measureFound.Drink.Name, measureFound.Ingredient.Name, measureFound.Quantity), measureFound);
                }
            }
        }

        public async Task<bool> UpdateIngredientAsync(Ingredient ingredient)
        {
            var ingredientDao = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredient.Id).ConfigureAwait(false);

            if (ingredientDao != null)
            {
                ingredientDao.Name = ingredient.Name;
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            var ingredientToSave = ingredient.ToDao();
            ingredientToSave.Id = Guid.NewGuid();
            await _context.Ingredients.AddAsync(ingredientToSave).ConfigureAwait(false);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0 ? ingredientToSave.ToModel() : null;
        }


    }
}
