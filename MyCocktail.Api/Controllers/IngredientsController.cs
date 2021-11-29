using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCocktail.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {

        private IDrinkRepository _repo;
        public IngredientsController(IDrinkRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/<IngredientsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAllIngredients();

            if (result.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            IngredientDto result;

            try
            {
                var idConverted = Guid.Parse(id);
                result = await _repo.GetIngredientById(idConverted);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }

        // POST api/<IngredientsController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IngredientDto ingredientFromBody)
        {
            Ingredient ingredientToSave;
            try
            {
                ingredientToSave = ingredientFromBody.ToModel();
            }
            catch (Exception)
            {

                throw new ArgumentException(nameof(ingredientFromBody));
            }

            var result = await _repo.AddAsync(ingredientToSave);

            return result != null ? Ok(result) : BadRequest(ingredientFromBody);
        }

        // PUT api/<IngredientsController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] IngredientDto ingredientDto)
        {
            try
            {
                var ingredientDomain = ingredientDto.ToModel();
                ingredientDto.Name = ingredientDomain.Name;
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(ingredientDto));
            }

            var result = await _repo.UpdateIngredientAsync(ingredientDto);

            return result ? Ok(ingredientDto) : BadRequest(ingredientDto);
        }

        //// DELETE api/<IngredientsController>/5
        //[Authorize(Roles ="Admin")]
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
