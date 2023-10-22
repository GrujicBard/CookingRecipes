using AutoMapper;
using CookingRecipes.API.Dtos;
using CookingRecipes.API.Models.Jwt;
using CookingRecipes.Data.Enums;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using CookingRecipes.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet, Authorize(Roles = nameof(RoleType.Admin))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{id}"), Authorize(Roles = nameof(RoleType.Admin))]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(id));

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(user);
        }

        [HttpGet("recipes/{userId}"), Authorize(Roles = nameof(RoleType.User) + "," + nameof(RoleType.Admin))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetFavoriteRecipesByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var recipes = _mapper.Map<List<RecipeDto>>(await _userRepository.GetFavoriteRecipesByUser(userId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);

        }

        [HttpGet("reviews/{userId}"), Authorize(Roles = nameof(RoleType.User) + "," + nameof(RoleType.Admin))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var reviews = _mapper.Map<List<ReviewDto>>(await _userRepository.GetReviewsByUser(userId));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);

        }

        [HttpPost("register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register(RegisterDto userRegister)
        {
            if (_userRepository.UserNameExists(userRegister.UserName))
            {
                ModelState.AddModelError("", "Username already exists.");
                return StatusCode(422, ModelState);
            }

            if (_userRepository.EmailExists(userRegister.Email))
            {
                ModelState.AddModelError("", "Email already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _userRepository.Register(userRegister))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("User successfuly registered.");
        }

        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> Login(LoginDto loginUser)
        {
            if (!_userRepository.EmailExists(loginUser.Email))
            {
                ModelState.AddModelError("", "Invalid login.");
                return StatusCode(422, ModelState);
            }
            var user = await _userRepository.GetUserByEmail(loginUser.Email);

            if (!_userRepository.VerifyPasswordHash(loginUser.Password, user.PasswordHash, user.PasswordSalt))
            {
                ModelState.AddModelError("", "Invalid login.");
                return StatusCode(422, ModelState);
            }

            string token = _userRepository.CreateToken(user);
            var refreshToken = _userRepository.GenerateRefreshToken();
            User updatedUser = SetRefreshToken(user, refreshToken);

            if (!await _userRepository.UpdateUser(updatedUser))
            {
                ModelState.AddModelError("", "Something went wrong updating user.");
                return StatusCode(500, ModelState);
            }

            return Ok(token);
        }


        [HttpPost("recipes/{userId}"), Authorize(Roles = nameof(RoleType.User))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddFavoriteRecipe(int userId, [FromQuery] int recipeId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            if (!await _userRepository.AddFavoriteRecipe(userId, recipeId))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly favorited recipe.");
        }

        [HttpPut("{userId}"), Authorize(Roles = nameof(RoleType.Admin))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(updatedUser);

            if (!await _userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}"), Authorize(Roles = nameof(RoleType.Admin))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var userToDelete = await _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _userRepository.RemoveFavoriteRecipes(userId))
            {
                ModelState.AddModelError("", "Something went wrong favorite recipes.");
            }

            if (!await _userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user.");
            }

            return NoContent();
        }

        [HttpDelete("recipes/{userId}"), Authorize(Roles = nameof(RoleType.User))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveFavoriteRecipe(int userId, [FromQuery] int recipeId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            if (!await _userRepository.RemoveFavoriteRecipe(userId, recipeId))
            {
                ModelState.AddModelError("", "Something went wrong removing favorite recipe.");
            }

            return NoContent();
        }

        [HttpPost("refresh-token/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<string>> RefreshToken(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var user = await _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = _userRepository.CreateToken(user);
            var newRefreshToken = _userRepository.GenerateRefreshToken();
            User updatedUser = SetRefreshToken(user, newRefreshToken);

            if (!await _userRepository.UpdateUser(updatedUser))
            {
                ModelState.AddModelError("", "Something went wrong updating user.");
                return StatusCode(500, ModelState);
            }

            return Ok(token);
        }

        private User SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpiresAt
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.CreatedAt;
            user.TokenExpires = newRefreshToken.ExpiresAt;

            return user;
        }
    }
}
