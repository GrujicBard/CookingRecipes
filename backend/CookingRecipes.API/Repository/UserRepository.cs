using Azure;
using CookingRecipes.API.Dtos;
using CookingRecipes.API.Models.Jwt;
using CookingRecipes.Data;
using CookingRecipes.Data.Enums;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CookingRecipes.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AddFavoriteRecipe(int userId, int recipeId)
        {

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefaultAsync();

            var userFavoriteRecipe = new UserFavoriteRecipe()
            {
                User = user,
                Recipe = recipe,
            };

            await _context.AddAsync(userFavoriteRecipe);
            return await Save();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Remove(user);
            return await Save();
        }

        public async Task<ICollection<Recipe>> GetFavoriteRecipesByUser(int userId)
        {
            return await _context.UserFavoriteRecipes.Where(u => u.UserId == userId).Select(r => r.Recipe).ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsByUser(int userId)
        {
            return await _context.Reviews.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.OrderBy(u => u.UserName).ToListAsync();
        }

        public async Task<bool> RemoveFavoriteRecipe(int userId, int recipeId)
        {
            var favoriteRecipe = await _context.UserFavoriteRecipes.Where(f => f.UserId == userId && f.RecipeId == recipeId).FirstOrDefaultAsync();
            _context.Remove(favoriteRecipe);
            return await Save();
        }

        public async Task<bool> RemoveFavoriteRecipes(int userId)
        {
            var favoriteRecipes = await _context.UserFavoriteRecipes.Where(f => f.UserId == userId).ToListAsync();
            _context.RemoveRange(favoriteRecipes);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Update(user);
            return await Save();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
        public bool UserNameExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public async Task<bool> Register(RegisterDto registerUser)
        {
            CreatePasswordHash(registerUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User userCreate = new()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _context.AddAsync(userCreate);
            return await Save();
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Role, Enum.GetName(typeof(RoleType), user.Role))
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.Now.AddDays(7),
            };

            return refreshToken;
        }
    }
}

