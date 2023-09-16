using AutoMapper;
using CookingRecipes.Dto;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
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
        public ActionResult CreateRecipe()
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateRecipe()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteRecipe()
        {
            return Ok();
        }
    }
}
