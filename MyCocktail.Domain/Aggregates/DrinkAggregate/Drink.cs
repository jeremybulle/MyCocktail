using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Domain.Aggregates.DrinkAggregate
{
    /// <summary>
    /// Represent a Cockatail. Contains all personal informations like Name, Measures, Instructions etc...
    /// </summary>
    public class Drink
    {
        #region Id
        private Guid? _id;
        public Guid? Id
        {
            get
            {
                if (_id == null)
                {
                    return null;
                }

                return _id == null ? null : new Guid(_id.ToString());
            }
            init
            {
                _id = value;
            }
        }
        #endregion
        #region IdSource
        private string _idSource;

        /// <summary>
        /// Id from original Cocktail Db
        /// </summary>
        public string IdSource
        {
            get
            {
                return new string(_idSource);
            }
            init
            {
                _idSource = value;
            }
        }
        #endregion

        #region Name
        private string _name;

        /// <summary>
        /// Name of the cocktail. Is Trimed, LowCased and can not be null or empty
        /// </summary>
        public string Name
        {
            get
            {
                return new string(_name);
            }
            init
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(Name));
                }
                _name = value.Trim();
                _name = Name.ToLower();
            }
        }
        #endregion

        #region IdOwner
        private Guid? _idOwner;

        /// <summary>
        /// Guid of the owner. If null owner this is from original Cocktail Db
        /// </summary>
        public Guid? IdOwner
        {
            get
            {
                return _idOwner == null ? null : new Guid(_idOwner.ToString());
            }
            set
            {
                if (value == null && IdSource.IsNullOrEmpty())
                {
                    throw new ArgumentException($"{nameof(IdOwner)} can't be null if IdSource is null");
                }
                _idOwner = value;
            }
        }
        #endregion

        #region Instruction
        private string _instruction;

        /// <summary>
        /// Instructions for realize this cocktail
        /// </summary>
        public string Instruction
        {
            get
            {
                return new string(_instruction);
            }
            set
            {
                _instruction = value.Trim();
            }
        }
        #endregion

        #region UrlPicture
        private Uri _urlPicture;

        /// <summary>
        /// Url for display this cocktail
        /// </summary>
        public Uri UrlPicture
        {
            get
            {
                if (_urlPicture == null)
                {
                    return null;
                }
                return new Uri(_urlPicture.ToString());
            }
            set
            {
                Uri url;
                try
                {
                    if (value != null)
                    {
                        url = new Uri(value.ToString());
                        _urlPicture = url;
                    }
                    else
                    {
                        _urlPicture = null;
                    }

                }
                catch
                {
                    throw new ArgumentException(nameof(UrlPicture));
                }
            }
        }
        #endregion

        #region Glass
        private Glass _glass;

        /// <summary>
        /// <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Glass"/> used in this recipe
        /// </summary>
        public Glass Glass
        {
            get
            {
                return new Glass() { Id = _glass.Id, Name = _glass.Name };
            }
            set
            {
                _glass = new Glass() { Id = value.Id, Name = value.Name };
            }
        }
        #endregion

        #region Category
        private Category _category;

        /// <summary>
        /// The <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Category"/> of this drink like shot, beer, coffee/tea etc...
        /// </summary>
        public Category Category
        {
            get
            {
                return new Category() { Id = _category.Id, Name = _category.Name };
            }
            set
            {
                _category = new Category() { Id = value.Id, Name = value.Name };
            }
        }
        #endregion

        #region Alcoholic
        private Alcoholic _alcoholic;

        /// <summary>
        /// Represent if this recipe contains alcohol, or not, or optional
        /// see : <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Alcoholic"/>
        /// </summary>
        public Alcoholic Alcoholic
        {
            get
            {
                return new Alcoholic() { Id = _alcoholic.Id, Name = _alcoholic.Name };
            }
            init
            {
                _alcoholic = new Alcoholic() { Id = value.Id, Name = value.Name };
            }
        }
        #endregion

        #region DateModified
        private DateTime _dateModified = DateTime.Now;

        /// <summary>
        /// Date of last updated in Db
        /// </summary>
        public DateTime DateModified
        {
            get
            {
                return new DateTime(_dateModified.Year, _dateModified.Month, _dateModified.Day);
            }
            set
            {
                _dateModified = new DateTime(value.Year, value.Month, value.Day);
            }
        }
        #endregion


        private HashSet<Measure> _measures = new HashSet<Measure>();

        /// <summary>
        /// Get all <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Measure"/>s related to this drink
        /// </summary>
        /// <returns>IEumerable of measure's copy</returns>
        public IEnumerable<Measure> GetMeasures()
        {
            List<Measure> measuresToReturn = new List<Measure>();
            foreach (var m in _measures)
            {
                measuresToReturn.Add(new Measure() { Ingredient = new Ingredient() { Id = m.Ingredient.Id, Name = m.Ingredient.Name }, Quantity = m.Quantity });
            }

            return measuresToReturn;
        }

        /// <summary>
        /// Search a Measure by Ingredient Name
        /// </summary>
        /// <param name="ingredientName">Measure's name searched</param>
        /// <returns>Measure or null</returns>
        public Measure GetMeasureByIngredientName(string ingredientName)
        {
            var ingredientNameHandled = ingredientName.ToLower().Trim();
            var measure = _measures.FirstOrDefault(m => m.Ingredient.Name == ingredientNameHandled);
            return measure != null ? new Measure() { Ingredient = new Ingredient() { Id = measure.Ingredient.Id, Name = ingredientNameHandled }, Quantity = measure.Quantity } : null;
        }

        /// <summary>
        /// Add a <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Measure"/>
        /// </summary>
        /// <param name="ingedientName">Ingredient's name of measure</param>
        /// <param name="quantity"></param>
        public void AddMeasure(string ingedientName, string quantity)
        {
            var ingredientNameHandled = ingedientName.ToLower().Trim();
            var isMeasureForIngredient = _measures.Any(m => m.Ingredient.Name == ingredientNameHandled);
            if (!isMeasureForIngredient)
            {
                _measures.Add(new Measure() { Ingredient = new Ingredient() { Name = ingredientNameHandled }, Quantity = quantity });
            }
        }

        public void AddMeasure(Measure measure)
        {
            var isMeasureForIngredient = _measures.Any(m => m.Ingredient.Name == measure.Ingredient.Name);
            if (!isMeasureForIngredient)
            {
                _measures.Add(measure);
            }
        }

        /// <summary>
        /// Update a <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Measure"/> with the same Name
        /// </summary>
        /// <param name="measureModified">New value for the measure to update</param>
        public void ModifyMeasure(Measure measureModified)
        {
            if (measureModified == null || !_measures.Any(m => m.Ingredient.Name == measureModified.Ingredient.Name))
            {
                return;
            }
            var measureToUpdate = _measures.RemoveWhere(m => m.Ingredient.Name == measureModified.Ingredient.Name);
            _measures.Add(measureModified);
        }

        /// <summary>
        /// Delete a <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Measure"/> with the specifiedName
        /// </summary>
        /// <param name="IngredientName"></param>
        public void DeleteMeasure(Measure measureToDelete)
        {
            var measureToDrop = _measures.FirstOrDefault(m => m.Ingredient.Name == measureToDelete.Ingredient.Name && m.Quantity == measureToDelete.Ingredient.Name);
            if (measureToDelete != null)
            {
                _measures.Remove(measureToDelete);
            }
        }

        /// <summary>
        /// Get all <see cref="MyCocktailDDD.Domain.AggregatesModel.DrinkAggregate.Ingredient"/>s needed for this Cocktail
        /// </summary>
        /// <returns>IEnumerable of Ingredient</returns>
        public IEnumerable<Ingredient> GetIngredients()
        {
            var ingredients = new List<Ingredient>();
            foreach (var measure in _measures)
            {
                ingredients.Add(new Ingredient() { Name = measure.Ingredient.Name });
            }
            return ingredients;
        }

        public override bool Equals(object obj)
        {
            return obj is Drink drink &&
                   IdSource == drink.IdSource &&
                   Name == drink.Name &&
                   Instruction == drink.Instruction &&
                   DateModified == drink.DateModified &&
                   IdOwner == drink.IdOwner &&
                   EqualityComparer<Uri>.Default.Equals(UrlPicture, drink.UrlPicture) &&
                   EqualityComparer<Glass>.Default.Equals(Glass, drink.Glass) &&
                   EqualityComparer<Category>.Default.Equals(Category, drink.Category) &&
                   EqualityComparer<Alcoholic>.Default.Equals(Alcoholic, drink.Alcoholic);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdSource, Name, DateModified, UrlPicture, Glass, Category, Alcoholic, IdOwner);
        }
    }
}
