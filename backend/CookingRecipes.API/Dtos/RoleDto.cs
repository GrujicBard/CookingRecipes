using CookingRecipes.Data.Enums;

namespace CookingRecipes.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public RoleType? RoleType { get; set; }
    }
}
