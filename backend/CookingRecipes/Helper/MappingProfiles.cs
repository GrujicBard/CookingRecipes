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
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

        }
    }
}
