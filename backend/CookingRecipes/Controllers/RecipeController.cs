using AutoMapper;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using CookingRecipes.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int id)
        {
            if (!_recipeRepository.RecipeExists(id))
            {
                return NotFound();
            }

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(id));

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(recipe);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeRating(int id)
        {
            if (!_recipeRepository.RecipeExists(id))
            {
                return NotFound();
            }

            var recipe = _recipeRepository.GetRecipeRating(id);

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromQuery] int categoryId, [FromBody] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
            {
                return BadRequest(ModelState);
            }

            var recipe = _recipeRepository.GetRecipes()
                .Where(r => r.Title.Trim().ToUpper() == recipeCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (recipe != null)
            {
                ModelState.AddModelError("", "Recipe already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);

            if (!_recipeRepository.CreateRecipe(categoryId, recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created.");
        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(int recipeId, [FromBody] RecipeDto updatedRecipe)
        {
            if (updatedRecipe == null)
            {
                return BadRequest(ModelState);
            }

            if (recipeId != updatedRecipe.Id)
                if (recipeId != updatedRecipe.Id)
                {
                    return BadRequest(ModelState);
                }

            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeMap = _mapper.Map<Recipe>(updatedRecipe);
  
            if (!_recipeRepository.UpdateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating recipe.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
