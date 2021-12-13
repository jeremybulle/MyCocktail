using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCocktail.Api.Dto;
using MyCocktail.Api.Dto.Extensions;
using MyCocktail.Api.Mapper;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCocktail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {

        private readonly IDrinkRepository _repo;
        public IngredientsController(IDrinkRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/<IngredientsController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _repo.GetAllIngredientsAsync().ConfigureAwait(false);

            if (result.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(result.ToDto());
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Ingredient result;

            try
            {
                var idConverted = Guid.Parse(id);
                result = await _repo.GetIngredientByIdAsync(idConverted).ConfigureAwait(false);
            }
            catch (Exception)
            {

                return BadRequest(id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.ToDto());

        }

        // POST api/<IngredientsController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] IngredientDto ingredientFromBody)
        {
            Ingredient result;
            try
            {
                if(ingredientFromBody == null)
                {
                    throw new ArgumentNullException(nameof(ingredientFromBody));
                }

                 var ingredientToSave = ingredientFromBody.ToModel();
                if(ingredientToSave == null)
                {
                    return BadRequest(ingredientFromBody);
                }
                 result = await _repo.AddAsync(ingredientToSave).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(ingredientFromBody));
            }

            return result != null ? Ok(result.ToDto()) : StatusCode(500);
        }
        

        // PUT api/<IngredientsController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] IngredientDto ingredientDto)
        {
            bool result;
            try
            {
                var ingredientToUpdate = ingredientDto.ToModel();
                if(ingredientToUpdate.Id != id)
                {
                    return BadRequest(ingredientDto);
                }
                result = await _repo.UpdateIngredientAsync(ingredientToUpdate).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(ingredientDto));
            }

            return result ? Ok() : NotFound(ingredientDto);
        }
    }
}
