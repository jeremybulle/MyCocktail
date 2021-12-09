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
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAllIngredients();

            if (result.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(result.ToDto());
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Ingredient result;

            try
            {
                var idConverted = Guid.Parse(id);
                result = await _repo.GetIngredientById(idConverted);
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

            return result != null ? Ok(result.ToDto()) : BadRequest(ingredientFromBody);
        }

        // PUT api/<IngredientsController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] IngredientDto ingredientDto)
        {
            Ingredient ingredientToUpdate;
            try
            {
                ingredientToUpdate = ingredientDto.ToModel();
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(ingredientDto));
            }

            var result = await _repo.UpdateIngredientAsync(ingredientToUpdate);

            return result ? Ok(result) : BadRequest(ingredientDto);
        }
    }
}
