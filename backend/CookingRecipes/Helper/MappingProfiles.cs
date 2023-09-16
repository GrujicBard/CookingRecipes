using AutoMapper;
using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Review, ReviewDto>();

        }
    }
}
