using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.DrinkAggregate
{
    public class DrinkTests
    {
        private readonly Fixture _fixture;

        public DrinkTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Constructor_WhenValidParameter_ShouldNotThrow()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var category = _fixture.Create<Category>();
            var glass = _fixture.Create<Glass>();

            //Act
            var ex = Record.Exception(() => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            });

            //Assert
            ex.Should().BeNull();

        }

        [Fact]
        public void Constructor_WithNullOrEmptyName_ShouldThrowArgumentException()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var category = _fixture.Create<Category>();
            var glass = _fixture.Create<Glass>();

            //Act
            Action act1 = () => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = null,
                UrlPicture = _fixture.Create<Uri>()
            };

            var ex2 = Record.Exception(() => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = "",
                UrlPicture = _fixture.Create<Uri>()
            });

            //Assert
            act1.Should().Throw<ArgumentException>().WithMessage("Name");
            Assert.IsType<ArgumentException>(ex2);
        }

        [Fact]
        public void Constructor_WhenIdOwnerAndIdSourceAreNullOrEmpty_ShouldThrowArgumentException()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var category = _fixture.Create<Category>();
            var glass = _fixture.Create<Glass>();

            //Act
            Action act1 = () => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdSource = null,
                IdOwner = null,
                Instruction = _fixture.Create<string>(),
                Name = null,
                UrlPicture = _fixture.Create<Uri>()
            };

            Action act2 = () => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdSource = "",
                IdOwner = null,
                Instruction = _fixture.Create<string>(),
                Name = null,
                UrlPicture = _fixture.Create<Uri>()
            };

            //Assert
            act1.Should().Throw<ArgumentException>().WithMessage("IdOwner can't be null if IdSource is null");
            act2.Should().Throw<ArgumentException>().WithMessage("IdOwner can't be null if IdSource is null");
        }

        [Fact]
        public void Constructor_WithValidInstruction_ShouldHaveTrimedInstruction()
        {
            //Arrange
            var instruction = " instructions not trimed ";
            var expected = instruction.Trim();

            //Act
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = instruction,
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Assert
            Assert.Equal(expected, drink.Instruction);
        }

        [Fact]
        public void GetIdOwner_WhenIdOwnerIsNotNull_ShouldReturnIdOwner()
        {
            //Arrange
            var idOwner = Guid.NewGuid();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = idOwner,
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Act
            var result = drink.IdOwner;

            //Assert
            Assert.Equal(idOwner, result);
        }

        [Fact]
        public void GetIdOwner_WhenIdOwnerIsNull_ShouldReturnNull()
        {
            //Arrange
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdSource = _fixture.Create<string>(),
                IdOwner = null,
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Act
            var result = drink.IdOwner;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetUrlPicture_WhenUrlPictureIsNotNull_ShouldReturnUrlPicture()
        {
            //Arrange
            var urlPicture = _fixture.Create<Uri>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = urlPicture
            };

            //Act
            var result = drink.UrlPicture;

            //Assert
            Assert.Equal(urlPicture, result);
        }

        [Fact]
        public void GetUrlPicture_WhenUrlPictureIsNull_ShouldReturnNull()
        {
            //Arrange
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdSource = _fixture.Create<string>(),
                IdOwner = null,
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = null
            };

            //Act
            var result = drink.UrlPicture;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void SetUrlPicture_WhenValueIsNull_ShouldNotThrowException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();

            //Act
            var ex = Record.Exception(() => drink.UrlPicture = null);

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void SetUrlPicture_WhenValueIsValid_ShouldNotThrowException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();

            //Act
            var ex = Record.Exception(() => drink.UrlPicture = _fixture.Create<Uri>());

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void GetGlass_WhenGlassIsValid_ShouldReturnACopyOfGlass()
        {
            //Arrange
            var glass = _fixture.Create<Glass>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };

            //Act
            var result = drink.Glass;

            //Assert
            Assert.Equal(glass.Id, result.Id);
            Assert.Equal(glass.Name, result.Name);
            Assert.False(glass == result);
        }

        [Fact]
        public void SetGlass_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();

            //Act
            var ex = Record.Exception(() => drink.Glass = null);

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void SetGlass_WhenValueIsValid_ShouldNotThrowException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var glass = _fixture.Create<Glass>();

            //Act
            var ex = Record.Exception(() => drink.Glass = glass);

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void GetCategory_WhenCategoryIsValid_ShouldReturnACopyOfCatagory()
        {
            //Arrange
            var category = _fixture.Create<Category>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = category,
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };

            //Act
            var result = drink.Category;

            //Assert
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.False(category == result);
        }

        [Fact]
        public void SetCategory_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();

            //Act
            var ex = Record.Exception(() => drink.Category = null);

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void SetCategory_WhenValueIsValid_ShouldNotThrowException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var category = _fixture.Create<Category>();

            //Act
            var ex = Record.Exception(() => drink.Category = category);

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void GetAlcoholic_WhenAlcoholicIsValid_ShouldReturnACopyOfAlcoholic()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };

            //Act
            var result = drink.Alcoholic;

            //Assert
            Assert.Equal(alcoholic.Id, result.Id);
            Assert.Equal(alcoholic.Name, result.Name);
            Assert.False(alcoholic == result);
        }

        [Fact]
        public void InitAlcoholic_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            Alcoholic alcoholic = null;

            //Act
            var ex = Record.Exception(() => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            }
            );

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void InitAlcoholic_WhenValueIsValid_ShouldNotThrowException()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();

            //Act
            var ex = Record.Exception(() => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            }
            );

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void GetDateModified_WhenDateModifiedIsNotNull_ShouldReturnDateModified()
        {
            //Arrange
            var dateModified = _fixture.Create<DateTime>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = dateModified,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Act
            var result = drink.DateModified;

            //Assert
            Assert.Equal(dateModified.Date, result.Date);
        }

        [Fact]
        public void AddMeasure_WhenDrinkContainOtherMeasures_ShouldNotThrowException()
        {
            //Arrange
            var measures = _fixture.CreateMany<Measure>(4);
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Act
            var ex = Record.Exception(() =>
            {
                foreach (var measure in measures)
                {
                    drink.AddMeasure(measure);
                }
            });

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void GetMeasures_WhenDrinkContainsMeasure_ShouldReturnAllMeasures()
        {
            //Arrange
            var measures = _fixture.CreateMany<Measure>(4);
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };
            foreach (var measure in measures)
            {
                drink.AddMeasure(measure);
            }

            //Act
            var result = drink.GetMeasures();


            //Assert
            Assert.Equal(measures.Count(), result.Count());
            Assert.True(!result.Except(measures).Any());
        }

        [Fact]
        public void GetMeasures_WhenDrinkNotContainsMeasure_ShouldReturnEmptyEnumerable()
        {
            //Arrange
            var measures = new List<Measure>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };
            foreach (var measure in measures)
            {
                drink.AddMeasure(measure);
            }

            //Act
            var result = drink.GetMeasures();


            //Assert
            Assert.Equal(measures.Count, result.Count());
        }

        [Fact]
        public void GetMeasuresByIngredientName_WhenDrinkContainsMeasureWithThisIngredient_ShouldReturnEnumerable()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() };
            var measuresWithRandomIngredient = _fixture.CreateMany<Measure>().ToList();
            var measuresWithIngredientSearch = _fixture.Build<Measure>().With(m => m.Ingredient, ingredient).CreateMany().ToList();

            measuresWithRandomIngredient.ForEach(m => drink.AddMeasure(m));
            measuresWithIngredientSearch.ForEach(m => drink.AddMeasure(m));

            //Act
            var result = drink.GetMeasureByIngredientName(ingredient.Name);

            //Assert
            Assert.Equal(measuresWithIngredientSearch.Count, result.Count());
        }

        [Fact]
        public void GetMeasuresByIngredientName_WhenDrinkDoesNotContainsMeasureWithThisIngredient_ShouldReturnEmptyEnumerable()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() };
            var measuresWithRandomIngredient = _fixture.CreateMany<Measure>().ToList();

            measuresWithRandomIngredient.ForEach(m =>
            {
                if (m.Ingredient.Name != ingredient.Name)
                {
                    drink.AddMeasure(m);
                }
            });
            //Act
            var result = drink.GetMeasureByIngredientName(ingredient.Name);

            //Assert
            Assert.False(result.Any());
        }

        [Fact]
        public void AddMeasure_WhenParametersAreValid_ShouldAddMeasureToDrink()
        {
            //Arrange
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };
            var ingredientName = _fixture.Create<string>();
            var quantity = _fixture.Create<string>();

            //Act
            drink.AddMeasure(ingredientName, quantity);
            var result = drink.GetMeasures().Select(m => m.Ingredient.Name == ingredientName && m.Quantity == quantity);

            //Assert
            Assert.True(result.Any());
        }

        [Fact]
        public void AddMeasure_WhenParametersIngredientNameIsNotValid_ShouldThrowArgumentException()
        {
            //Arrange
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };

            string ingredientName1 = null;
            var quantity1 = _fixture.Create<string>();

            string ingredientName2 = "";
            var quantity2 = _fixture.Create<string>();

            //Act
            Action act1 = () => drink.AddMeasure(ingredientName1, quantity1);
            var ex2 = Record.Exception(() => drink.AddMeasure(ingredientName2, quantity2));

            //Assert
            act1.Should().Throw<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void AddMeasure_WhenDrinkAlreadyContainsAMeasureWithSameParameters_ShouldNotAddNewMeasure()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();

            measures.ForEach(m => drink.AddMeasure(m));

            var ingredientName = "poudre de Perlinpinpi";
            var quantity = "Louche";
            var measureTracked = new Measure() { Id = Guid.NewGuid(), Ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName }, Quantity = quantity };

            drink.AddMeasure(measureTracked);

            //Act
            drink.AddMeasure(measureTracked.Ingredient.Name, measureTracked.Quantity);
            var result = drink.GetMeasures().Where(m => m.Ingredient.Name == measureTracked.Ingredient.Name && m.Quantity == measureTracked.Quantity);

            //Assert
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void ModifyMeasure_WhenMeasureWithUpdatesIsValid_ShouldModifyMeasure()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();

            measures.ForEach(m => drink.AddMeasure(m));

            var ingredientName = "poudre de Perlinpinpi";
            var quantity = "Louche";
            var measureTracked = new Measure() { Id = Guid.NewGuid(), Ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName }, Quantity = quantity };

            drink.AddMeasure(measureTracked);

            var modifiedMeasure = new Measure() { Ingredient = measureTracked.Ingredient, Quantity = _fixture.Create<string>() };

            //Act
            drink.ModifyMeasure(measureTracked, modifiedMeasure);
            var isOlderValueInDrink = drink.GetMeasures().Any(m => m.Ingredient.Name == measureTracked.Ingredient.Name && m.Quantity == measureTracked.Quantity);
            var isNewValueInDrink = drink.GetMeasures().Any(m => m.Ingredient.Name == modifiedMeasure.Ingredient.Name && m.Quantity == modifiedMeasure.Quantity);

            //Assert
            Assert.False(isOlderValueInDrink);
            Assert.True(isNewValueInDrink);
        }

        [Fact]
        public void ModifyMeasure_WhenMeasureWithUpdatesIsNull_ShouldNotThrowException()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();

            measures.ForEach(m => drink.AddMeasure(m));

            var ingredientName = "poudre de Perlinpinpi";
            var quantity = "Louche";
            var measureTracked = new Measure() { Id = Guid.NewGuid(), Ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName }, Quantity = quantity };

            drink.AddMeasure(measureTracked);

            Measure modifiedMeasure = null;

            //Act
            var ex = Record.Exception(() => drink.ModifyMeasure(measureTracked, modifiedMeasure));

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void ModifyMeasure_WhenMeasureWithUpdatesIsValidAndActualValueIsNull_ShouldNotThrowExceptionAndNotUpadteMeasure()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();

            measures.ForEach(m => drink.AddMeasure(m));

            var ingredientName = "poudre de Perlinpinpi";
            var quantity = "Louche";
            var measureTracked = new Measure() { Id = Guid.NewGuid(), Ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName }, Quantity = quantity };

            drink.AddMeasure(measureTracked);

            Measure modifiedMeasure = new Measure() { Ingredient = measureTracked.Ingredient, Quantity = _fixture.Create<string>() };

            //Act
            var ex = Record.Exception(() => drink.ModifyMeasure(null, modifiedMeasure));
            var result = drink.GetMeasures().Any(m => m.Ingredient.Name == measureTracked.Ingredient.Name && m.Quantity == measureTracked.Quantity);

            //Assert
            Assert.Null(ex);
            Assert.True(result);
        }

        [Fact]
        public void ModifyMeasure_WhenDrinkNotContainsActualValue_ShouldNotThrowExceptionAndNotUpadteMeasure()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();

            measures.ForEach(m => drink.AddMeasure(m));

            var ingredientName = "poudre de Perlinpinpi";
            var quantity = "Louche";
            var measureTracked = new Measure() { Id = Guid.NewGuid(), Ingredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName }, Quantity = quantity };
            drink.AddMeasure(measureTracked);
            Measure modifiedMeasure = new Measure() { Ingredient = measureTracked.Ingredient, Quantity = _fixture.Create<string>() };
            var actualValueNotInDrink = new Measure() { Ingredient = measureTracked.Ingredient, Quantity = "couillère Arthour" };

            //Act
            var ex = Record.Exception(() => drink.ModifyMeasure(actualValueNotInDrink, modifiedMeasure));
            var result = drink.GetMeasures().Any(m => m.Ingredient.Name == measureTracked.Ingredient.Name && m.Quantity == measureTracked.Quantity);

            //Assert
            Assert.Null(ex);
            Assert.True(result);
        }

        [Fact]
        public void DeleteMeasure_WhenDrinkContainsMeasureToDelete_ShouldNotThrowExceptionAndRemoveMeasure()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();
            var measureToRemove = measures.First();
            measures.ForEach(m => drink.AddMeasure(m));

            //Act
            var ex = Record.Exception(() => drink.DeleteMeasure(measureToRemove));
            var result = drink.GetMeasures().Any(m => m.Ingredient.Name == measureToRemove.Ingredient.Name && m.Quantity == measureToRemove.Quantity);

            //Assert
            Assert.Null(ex);
            Assert.False(result);
        }

        [Fact]
        public void DeleteMeasure_WhenDrinkNotContainsMeasureToDelete_ShouldNotThrowExceptionAndNotRemoveMeasure()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();
            var measureToRemove = _fixture.Create<Measure>();
            measures.ForEach(m => drink.AddMeasure(m));

            //Act
            var ex = Record.Exception(() => drink.DeleteMeasure(measureToRemove));

            //Assert
            Assert.Null(ex);
            Assert.Equal(measures.Count, drink.GetMeasures().Count());
        }

        [Fact]
        public void GetMeasures_WhenDrinkNotContainsMeasuresToDelete_ShouldRetunrEmptyEnumerable()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();

            //Act
            var result = drink.GetMeasures();

            //Assert
            Assert.False(result.Any());
        }

        [Fact]
        public void GetMeasures_WhenDrinkContainsMeasures_ShouldRetunrEnumerableWithAlldrink()
        {
            //Arrange
            var drink = _fixture.Create<Drink>();
            var measures = _fixture.CreateMany<Measure>().ToList();
            measures.ForEach(m => drink.AddMeasure(m));

            //Act
            var result = drink.GetMeasures();

            //Assert
            Assert.True(measures.All(m => result.Any(m2 => m.Equals(m2))));
        }

        [Fact]
        public void GetHascode_WhenDifferentDrink_ShouldReturnNonEqualHashCode()
        {
            //Arrange
            var drink1 = _fixture.Create<Drink>();
            var drink2 = _fixture.Create<Drink>();

            //Act
            var hashCode1 = drink1.GetHashCode();
            var hashCode2 = drink2.GetHashCode();

            //Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHascode_WhenSameDrinkPropertiesValue_ShouldReturnEqualHashCode()
        {
            //Arrange
            var drink1 = _fixture.Create<Drink>();
            var drink2 = new Drink() {
                Id = drink1.Id,
                Name = drink1.Name,
                Alcoholic = drink1.Alcoholic,
                Category = drink1.Category,
                DateModified = drink1.DateModified,
                Glass = drink1.Glass,
                IdSource = drink1.IdSource,
                IdOwner = drink1.IdOwner,
                Instruction = drink1.Instruction,
                UrlPicture = drink1.UrlPicture
            };

            //Act
            var hashCode1 = drink1.GetHashCode();
            var hashCode2 = drink2.GetHashCode();

            //Assert
            Assert.Equal(hashCode1, hashCode2);
        }
        
    }
}
