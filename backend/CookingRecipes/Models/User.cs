namespace CookingRecipes.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public Role? Role { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserFavoriteRecipe>? UserFavoriteRecipes { get; set; }

    }
}
