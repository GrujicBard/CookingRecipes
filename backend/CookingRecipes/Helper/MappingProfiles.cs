using AutoMapper;
using CookingRecipes.Dtos;
using CookingRecipes.Models;

namespace CookingRecipes.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap(); ;
            CreateMap<Role, RoleDto>().ReverseMap(); ;
            CreateMap<User, UserDto>().ReverseMap(); ;
            CreateMap<Review, ReviewDto>().ReverseMap(); ;
            CreateMap<Review, ReviewPostDto>().ReverseMap(); ;
        }
    }
}
