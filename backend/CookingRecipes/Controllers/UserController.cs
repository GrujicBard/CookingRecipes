using AutoMapper;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using CookingRecipes.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetRecipes()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(user);
        }

        [HttpGet("recipes/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetFavoriteRecipesByUserId(int userId)
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_userRepository.GetFavoriteRecipesByUser(userId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);

        }

        [HttpGet("reviews/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByUserId(int userId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_userRepository.GetReviewsByUser(userId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);

        }

    }
}
