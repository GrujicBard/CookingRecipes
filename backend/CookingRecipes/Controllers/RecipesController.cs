using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoRecipes.Controllers
{
    public class RecipesController : Controller
    {
        string[] recipes = { "Oxtail", "Curry Chicken", "Dumplings" };
        [HttpGet]
        public ActionResult GetRecipes()
        {
            if (!recipes.Any())
            {
                return NotFound();
            }
            return Ok(recipes);
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
