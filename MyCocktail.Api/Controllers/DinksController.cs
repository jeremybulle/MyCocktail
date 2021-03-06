using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyCocktail.Api.Dto;
using MyCocktail.Api.Dto.Extensions;
using MyCocktail.Api.Mapper;
using MyCocktail.Api.Services.Authentication;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCocktail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkRepository _repo;
        public DrinksController(IDrinkRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/<DrinksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drinksFind = await _repo.GetAsync();
            if (drinksFind.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(drinksFind.ToPartialDto());
        }

        // GET api/<DrinksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Guid idToGet;
            try
            {
                idToGet = Guid.Parse(id);
            }
            catch (Exception)
            {
                return BadRequest(id);
            }
            var drinkFound = await _repo.GetByIdAsync(idToGet);
            return drinkFound == null ? NotFound(id) : Ok(drinkFound.ToDto());
        }

        // GET api/<DrinksController>/LastUpdated/6
        [EnableCors]
        [HttpGet("LastUpdated/{nbSearch}")]
        public async Task<IActionResult> GetLastUpdated(string nbSearch)
        {
            int nbCocktail;
            try
            {
                nbCocktail = int.Parse(nbSearch) > 0 ? int.Parse(nbSearch) : throw new ArgumentException(nameof(nbSearch));
            }
            catch (Exception)
            {
                return BadRequest(nbSearch);
            }
            var drinkFound = await _repo.GetLastUpdatedAsync(nbCocktail);
            return drinkFound == null ? NotFound() : Ok(drinkFound.ToPartialDto());
        }

        // GET api/<DrinksController>/5/Ingredients/
        [HttpGet("{id}/Ingredients")]
        public async Task<IActionResult> GetIngredients(string id)
        {
            Guid idToGet;
            try
            {
                idToGet = Guid.Parse(id);
            }
            catch (Exception)
            {
                return BadRequest(id);
            }
            var drinkFound = await _repo.GetByIdAsync(idToGet);
            var ingredients = new List<IngredientDto>();
            foreach (var measure in drinkFound.GetMeasures())
            {
                ingredients.Add(measure.Ingredient.ToDto());
            }

            if (ingredients.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(ingredients);
        }

        // POST api/<DrinksController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DrinkDto drinkDto)
        {
            Drink result;
            try
            {
                var drinkModel = drinkDto.ToModel();
                result = await _repo.AddAsync(drinkModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return result == null ? BadRequest() : Ok(result.ToDto());
        }

        // POST api/Ingredients
        [Authorize]
        [HttpPost("Ingredients")]
        public async Task<IActionResult> GetDrinksByIndregient([FromBody] IEnumerable<string> ingredientIds)
        {
            IEnumerable<Drink> results;
            try
            {
                var ingredientGuids = ingredientIds.Select(id => Guid.Parse(id)).ToList();
                results = await _repo.GetDrinksByIngredient(ingredientGuids);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return results == null ? BadRequest() : Ok(results.ToPartialDto());
        }


        // PUT api/<DrinksController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DrinkDto drinkDtoToUpdate)
        {
            Guid idUser;
            try
            {
                var idUserFromToken = HttpContext.User.GetUserId();
                idUser = Guid.Parse(idUserFromToken);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (idUser == Guid.Parse(drinkDtoToUpdate.IdOwner) || HttpContext.User.IsInRole("Admin"))
            {
                Drink drinkToSave;
                try
                {
                    drinkToSave = drinkDtoToUpdate.ToModel();
                }
                catch (Exception)
                {
                    return BadRequest(drinkDtoToUpdate);
                }
                var result = await _repo.UpdateAsync(id, drinkToSave);
                return result != null ? Ok(result.ToDto()) : BadRequest(drinkDtoToUpdate);
            }
            return Unauthorized();
        }


        // DELETE api/<DrinksController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Guid idToDelete;
            try
            {
                idToDelete = Guid.Parse(id);
            }
            catch (Exception)
            {
                return BadRequest(id);
            }
            _repo.Delete(idToDelete);
            return Ok();
        }
    }
}
