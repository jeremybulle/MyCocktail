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
        private Dictionary<string, AlcoholicDao> _alcoholicsCache = new Dictionary<string, AlcoholicDao>();
        private Dictionary<string, CategoryDao> _categoriesCache = new Dictionary<string, CategoryDao>();
        private Dictionary<string, GlassDao> _glassesCache = new Dictionary<string, GlassDao>();
        private Dictionary<string, DrinkDao> _drinksCache = new Dictionary<string, DrinkDao>();
        private Dictionary<string, IngredientDao> _ingredientsCache = new Dictionary<string, IngredientDao>();
        private Dictionary<(string, string, string), MeasureDao> _measureCache = new Dictionary<(string, string, string), MeasureDao>(); // key (drinkName, ingredientName, Quantity ) utiliser un Hashset?

        public DrinkRepository(DrinkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private Dictionary<Guid, Drink> _drinkCache = new Dictionary<Guid, Drink>();
        public async Task<Drink> AddAsync(Drink drink)
        {
            if (!_context.Drinks.Any(d => d.Name == drink.Name))
            {
                var drinkToSave = drink.ToDao();

                drinkToSave.Id = Guid.NewGuid();
                drinkToSave.Alcoholic = Add(drinkToSave.Alcoholic);
                drinkToSave.AlcoholicId = drinkToSave.Alcoholic.Id;
                drinkToSave.Category = Add(drinkToSave.Category);
                drinkToSave.CategoryId = drinkToSave.Category.Id;
                drinkToSave.Glass = Add(drinkToSave.Glass);
                drinkToSave.GlassId = drinkToSave.Glass.Id;

                await _context.Drinks.AddAsync(drinkToSave);

                foreach (var measureToSave in drink.GetMeasures())
                {
                    var ingredientCurrent = Add(new IngredientDao() { Id = Guid.NewGuid(), Name = measureToSave.Ingredient.Name });
                    Add(new MeasureDao() { Id = Guid.NewGuid(), Drink = drinkToSave, DrinkId = drinkToSave.Id, Ingredient = ingredientCurrent, IngredientId = ingredientCurrent.Id, Quantity = measureToSave.Quantity }, drinkToSave.Name);
                }

                await _context.Drinks.AddAsync(drinkToSave);

                await _context.SaveChangesAsync();
                return drinkToSave.ToModel();
            }
            return (await _context.Drinks.FirstOrDefaultAsync(d => d.Name == drink.Name)).ToModel();
        }

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

        public Task<Drink> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Drink>> GetAsync()
        {
            var drinksFind = new List<Drink>();
            var query = _context.Drinks.OrderBy(d => d.Name).Include(d => d.Glass).Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Measures).ThenInclude(m => m.Ingredient);
            foreach (var drinkDao in await query.ToListAsync())
            {
                drinksFind.Add(drinkDao.ToModel());
            }

            return drinksFind;
        }

        public async Task<Drink> GetByIdAsync(Guid id)
        {
            var drinkFound = await _context.Drinks.Include(d => d.Glass).Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Measures).ThenInclude(m => m.Ingredient).FirstOrDefaultAsync(d => d.Id == id);
            if (drinkFound != null)
            {
                return drinkFound.ToModel();
            }
            return null;
        }

        public async Task<IEnumerable<Drink>> GetLastUpdatedAsync(int nbSearch)
        {
            var drinkSorted = _context.Drinks.Include(d => d.Glass).Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Measures).ThenInclude(m => m.Ingredient).OrderByDescending(d => d.DateModified).Take(nbSearch);
            var listToReturn = new List<Drink>();
            foreach (var drink in await drinkSorted.ToListAsync())
            {
                listToReturn.Add(drink.ToModel());
            }
            return listToReturn;
        }

        //TODO 
        public async Task<Drink> UpdateAsync(Guid id, Drink drink)
        {
            var drinkToUpdate = await _context.Drinks
                .Include(d => d.Alcoholic)
                .Include(d => d.Category)
                .Include(d => d.Glass)
                .FirstOrDefaultAsync(d => d.Id == id);

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
                var alcoholicDao = await _context.Alcoholics.FirstOrDefaultAsync(a => a.Name == drink.Alcoholic.Name);

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
                    await _context.Alcoholics.AddAsync(alcoholicToSave);
                }
            }

            if (drinkToUpdate.Category.Name != drink.Category.Name)
            {
                var categoryDao = await _context.Categories.FirstOrDefaultAsync(c => c.Name == drink.Category.Name);

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
                    await _context.Categories.AddAsync(categoryToSave);
                }
            }

            if (drinkToUpdate.Glass.Name != drink.Category.Name)
            {
                var glassDao = await _context.Glasses.FirstOrDefaultAsync(g => g.Name == drink.Glass.Name);

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
                    await _context.Glasses.AddAsync(glassToSave);
                }
            }

            drinkToUpdate.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();

            var result = await _context.Drinks.FirstOrDefaultAsync(d => d.Id == drinkToUpdate.Id);

            return result != null ? result.ToModel() : null;
        }

        public async Task<IEnumerable<Drink>> GetDrinksByIngredient(IEnumerable<Guid> ingredientIds)
        {

            //var query = _context.Measures.Include(m => m.Drink).Include(m => m.Drink.Alcoholic).Include(m => m.Drink.Category).Include(m => m.Drink.Glass).Where(m => ingredientIds.Any(i => i == m.IngredientId)).Select(m => m.Drink); //=> Drink Contient au moins 1 des ingredients de la liste
            //var query = _context.Drinks.Include(d => d.Measures).Include(d => d.Alcoholic).Include(d => d.Category).Include(d => d.Glass).Where(d => d.Measures.All( m => ingredientIds.Contains(m.IngredientId))); => Drink contient tous les ingrétients de la liste pas plus pas moins

            var grp = await Task.FromResult(_context.Measures.Include(m => m.Drink).Include(m => m.Drink.Alcoholic).Include(m => m.Drink.Category).Include(m => m.Drink.Glass).Include(m => m.Ingredient).AsEnumerable().GroupBy(m => m.Drink, (key, g) => new { Drink = key, ingredients = g.Select(e => e.IngredientId) }));

            var grpPurged = grp.Where(g => ingredientIds.All(ingredientId => g.ingredients.Any(i => i == ingredientId)));

            return grpPurged.Select(e => e.Drink.ToModel());
        }


        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            var ingredientsDao = await _context.Ingredients.ToListAsync();
            var ingredientsToReturn = new List<Ingredient>();

            if (ingredientsDao.IsNullOrEmpty())
            {
                return ingredientsToReturn;
            }

            return ingredientsDao.ToModel();
        }

        public async Task<Ingredient> GetIngredientById(Guid id)
        {
            var ingredientDao = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);

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

        private AlcoholicDao Add(AlcoholicDao alcoholic)
        {
            if (!_alcoholicsCache.ContainsKey(alcoholic.Name))
            {
                if (!_context.Alcoholics.Any(a => a.Name == alcoholic.Name))
                {
                    _context.Alcoholics.Add(alcoholic);
                    //_context.SaveChanges();
                    _alcoholicsCache.Add(alcoholic.Name, alcoholic);
                }
                else
                {
                    var alcoholicFound = _context.Alcoholics.First(a => a.Name == alcoholic.Name);
                    _alcoholicsCache.Add(alcoholicFound.Name, alcoholicFound);
                    return alcoholicFound;
                }
            }
            return _alcoholicsCache[alcoholic.Name];
        }

        private CategoryDao Add(CategoryDao category)
        {
            if (!_categoriesCache.ContainsKey(category.Name))
            {
                if (!_context.Categories.Any(c => c.Name == category.Name))
                {
                    _context.Categories.Add(category);
                    //_context.SaveChanges();
                    _categoriesCache.Add(category.Name, category);
                }
                else
                {
                    var categoryFound = _context.Categories.First(c => c.Name == category.Name);
                    _categoriesCache.Add(categoryFound.Name, categoryFound);
                    return categoryFound;
                }
            }
            return _categoriesCache[category.Name];
        }

        private GlassDao Add(GlassDao glass)
        {
            if (!_glassesCache.ContainsKey(glass.Name))
            {
                if (!_context.Glasses.Any(g => g.Name == glass.Name))
                {
                    _context.Glasses.Add(glass);
                    //_context.SaveChanges();
                    _glassesCache.Add(glass.Name, glass);
                }
                else
                {
                    var glassFound = _context.Glasses.First(g => g.Name == glass.Name);
                    _glassesCache.Add(glassFound.Name, glassFound);
                    return glassFound;
                }
            }
            return _glassesCache[glass.Name];
        }

        private IngredientDao Add(IngredientDao ingredient)
        {
            if (!_ingredientsCache.ContainsKey(ingredient.Name))
            {
                if (!_context.Ingredients.Any(i => i.Name == ingredient.Name))
                {
                    _context.Ingredients.Add(ingredient);
                    //_context.SaveChanges();
                    _ingredientsCache.Add(ingredient.Name, ingredient);
                }
                else
                {
                    var ingredientFound = _context.Ingredients.First(i => i.Name == ingredient.Name);
                    _ingredientsCache.Add(ingredientFound.Name, ingredientFound);
                    return ingredientFound;
                }
            }
            return _ingredientsCache[ingredient.Name];
        }

        private MeasureDao Add(MeasureDao measure, string drinkName)
        {
            if (!_measureCache.ContainsKey((drinkName, measure.Ingredient.Name, measure.Quantity)))
            {
                if (_context.Measures.Where(m => m.Drink.Name == drinkName && m.Ingredient.Name == measure.Ingredient.Name && m.Quantity == measure.Quantity).Count() < 1)
                {
                    _context.Measures.Add(measure);
                    //_context.SaveChanges();
                    _measureCache.Add((drinkName, measure.Ingredient.Name, measure.Quantity), measure);
                }
                else
                {
                    var measureFound = _context.Measures.First(m => m.Drink.Name == drinkName && m.Ingredient.Name == measure.Ingredient.Name && m.Quantity == measure.Quantity);
                    _measureCache.Add((measureFound.Drink.Name, measureFound.Ingredient.Name, measureFound.Quantity), measureFound);
                    return measureFound;
                }
            }
            return _measureCache[(drinkName, measure.Ingredient.Name, measure.Quantity)];
        }

        public async Task<bool> UpdateIngredientAsync(Ingredient ingredient)
        {
            var ingredientDao = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredient.Id);

            if (ingredientDao != null)
            {
                ingredientDao.Name = ingredient.Name;
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }

            return false;
        }

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            var ingredientToSave = ingredient.ToDao();
            ingredientToSave.Id = Guid.NewGuid();
            await _context.Ingredients.AddAsync(ingredientToSave);
            return await _context.SaveChangesAsync() > 0 ? ingredientToSave.ToModel() : null;
        }


    }
}
