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
        private readonly IRecipeRepository _recipeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly RecipeController _recipeController;

        public RecipeControllerTests()
        {
            _recipeRepository = A.Fake<IRecipeRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _categoryRepository = A.Fake<ICategoryRepository>();
            _mapper = A.Fake<IMapper>();
            _recipeController = new RecipeController(_recipeRepository, _reviewRepository,_categoryRepository, _mapper);
        }

        [Fact]
        public async void RecipeController_GetRecipes_ReturnsOk()
        {
            #region Arrange
            var recipes = A.Fake<ICollection<RecipeDto>>();
            var recipesList = A.Fake<List<RecipeDto>>();
            A.CallTo(() => _mapper.Map<List<RecipeDto>>(recipes)).Returns(recipesList);
            #endregion

            #region Assert
            var result = await _recipeController.GetRecipes();
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void RecipeController_GetRecipeById_ReturnsOk(int recipeId)
        {
            #region Arrange
            var recipe = A.Fake<Recipe>();
            var recipeDto = A.Fake<RecipeDto>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _recipeRepository.GetRecipeById(recipeId)).Returns(recipe);
            A.CallTo(() => _mapper.Map<RecipeDto>(recipe)).Returns(recipeDto);
            #endregion

            #region Assert
            var result = await _recipeController.GetRecipeById(recipeId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Theory]
        [InlineData("recipe")]
        [InlineData("pancakes")]
        public async void RecipeController_GetRecipesByTitle_ReturnsOk(string title)
        {
            #region Arrange
            var recipe = A.Fake<Recipe>();
            var recipesDto = A.Fake<List<RecipeDto>>();
            A.CallTo(() => _mapper.Map<List<RecipeDto>>(recipe)).Returns(recipesDto);
            #endregion

            #region Assert
            var result = await _recipeController.GetRecipesByTitle(title);
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
        public async void RecipeController_CreateRecipe_ReturnsOk(int categoryId)
        {
            #region Arrange
            var recipe = A.Fake<Recipe>();
            var recipeMap = A.Fake<Recipe>();
            var recipeCreate = A.Fake<RecipeDto>();
            var recipes = A.Fake<ICollection<Recipe>>();
            var recipesList = A.Fake<IList<RecipeDto>>();
            A.CallTo(() => _recipeRepository.RecipeTitleExists(recipeCreate.Title)).Returns(false);
            A.CallTo(() => _mapper.Map<Recipe>(recipeCreate)).Returns(recipe);
            A.CallTo(() => _recipeRepository.CreateRecipe(recipe)).Returns(true);
            #endregion

            #region Assert
            var result = await _recipeController.CreateRecipe(recipeCreate);
            
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Theory]
        [InlineData(4)]
        [InlineData(14)]
        [InlineData(12)]
        public async void RecipeController_GetRecipesByCategory_ReturnsOk(int categoryId)
        {
            #region Arrange
            var recipesList = A.Fake<ICollection<Recipe>>();
            var recipesMap = A.Fake<List<RecipeDto>>();
            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _recipeRepository.GetRecipesByCategory(categoryId)).Returns(recipesList);
            A.CallTo(() => _mapper.Map<List<RecipeDto>>(recipesList)).Returns(recipesMap);
            #endregion

            #region Assert
            var result = await _recipeController.GetRecipesByCategory(categoryId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void RecipeController_UpdateRecipe_ReturnsNoContent(int recipeId)
        {
            #region Arrange
            var recipeMap = A.Fake<Recipe>();
            var updatedRecipe = A.Fake<RecipeDto>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _mapper.Map<Recipe>(updatedRecipe)).Returns(recipeMap);
            A.CallTo(() => _recipeRepository.UpdateRecipe(recipeMap)).Returns(true);
            #endregion

            #region Assert
            var result = await _recipeController.UpdateRecipe(recipeId, updatedRecipe);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void RecipeController_DeleteRecipe_ReturnsNoContent(int recipeId)
        {
            #region Arrange
            var reviewsToDelete = A.Fake<List<Review>>();
            var recipeToDelete = A.Fake<Recipe>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReviewsOfARecipe(recipeId)).Returns(reviewsToDelete);
            A.CallTo(() => _recipeRepository.GetRecipeById(recipeId)).Returns(recipeToDelete);
            A.CallTo(() => _reviewRepository.DeleteReviews(reviewsToDelete)).Returns(true);
            A.CallTo(() => _recipeRepository.DeleteRecipe(recipeToDelete)).Returns(true);
            #endregion

            #region Assert
            var result = await _recipeController.DeleteRecipe(recipeId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }
    }
}
