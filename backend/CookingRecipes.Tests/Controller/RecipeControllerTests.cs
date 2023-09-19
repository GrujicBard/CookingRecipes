using AutoMapper;
using ContosoRecipes.Controllers;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingRecipes.Tests.Controller
{
    public class RecipeControllerTests
    {
        private IRecipeRepository _recipeRepository;
        private IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public RecipeControllerTests()
        {
            _recipeRepository = A.Fake<IRecipeRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void RecipeController_GetRecipes_ReturnOk()
        {
            #region Arrange
            var recipes = A.Fake<ICollection<RecipeDto>>();
            var recipesList = A.Fake<List<RecipeDto>>();
            A.CallTo(() => _mapper.Map<List<RecipeDto>>(recipes)).Returns(recipesList);
            var controller = new RecipeController(_recipeRepository, _reviewRepository, _mapper);
            #endregion

            #region Assert
            var result = controller.GetRecipes();
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(300)]
        public void RecipeController_CreateRecipe_ReturnOk(int categoryId)
        {
            #region Arrange
            var recipe = A.Fake<Recipe>();
            var recipeMap = A.Fake<Recipe>();
            var recipeCreate = A.Fake<RecipeDto>();
            var recipes = A.Fake<ICollection<Recipe>>();
            var recipesList = A.Fake<IList<RecipeDto>>();
            A.CallTo(() => _recipeRepository.GetRecipeTrimToUpper(recipeCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Recipe>(recipeCreate)).Returns(recipe);
            A.CallTo(() => _recipeRepository.CreateRecipe(categoryId, recipe)).Returns(true);
            var controller = new RecipeController(_recipeRepository, _reviewRepository, _mapper);
            #endregion

            #region Assert
            var result = controller.CreateRecipe(categoryId, recipeCreate);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }
    }
}
