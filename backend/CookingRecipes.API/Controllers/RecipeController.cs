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
        private readonly IReviewRepository _reviewRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IReviewRepository reviewRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _reviewRepository = reviewRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(await _recipeRepository.GetRecipes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            if (!_recipeRepository.RecipeExists(id))
            {
                return NotFound();
            }

            var recipe = _mapper.Map<RecipeDto>(await _recipeRepository.GetRecipeById(id));

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(recipe);
        }

        [HttpGet("title/{title}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRecipeByTitle(string title)
        {
            var recipe = _mapper.Map<RecipeDto>(await _recipeRepository.GetRecipeByTitle(title));

            if (recipe == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(recipe);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRecipeRating(int id)
        {
            if (!_recipeRepository.RecipeExists(id))
            {
                return NotFound();
            }

            var avgRating = await _recipeRepository.GetRecipeRating(id);

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(avgRating);
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRecipesByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var recipes = _mapper.Map<List<RecipeDto>>(await _recipeRepository.GetRecipesByCategory(categoryId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRecipe([FromQuery] int categoryId, [FromBody] RecipeDto recipeCreate)
        {
            Response.ContentType = "text/plain";
            if (recipeCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (_recipeRepository.RecipeTitleExists(recipeCreate.Title))
            {
                ModelState.AddModelError("", "Recipe already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);

            if (!await _recipeRepository.CreateRecipe(categoryId, recipeMap))
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
        public async Task<IActionResult> UpdateRecipe(int recipeId, [FromBody] RecipeDto updatedRecipe)
        {
            if (updatedRecipe == null)
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

            if (!await _recipeRepository.UpdateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating recipe.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }
            var reviewsToDelete = await _reviewRepository.GetReviewsOfARecipe(recipeId);
            var recipeToDelete = await _recipeRepository.GetRecipeById(recipeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviews");
            }

            if (!await _recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting recipe.");
            }

            return NoContent();

        }

    }
}
