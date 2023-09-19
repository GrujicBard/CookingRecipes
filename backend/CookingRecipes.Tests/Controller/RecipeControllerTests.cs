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
        public void RecipeController_GetRecipes_ReturnsOk()
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
        [InlineData(2)]
        [InlineData(3)]
        public void RecipeController_GetRecipe_ReturnsOk(int recipeId)
        {
            #region Arrange
            var recipe = A.Fake<Recipe>();
            var recipeDto = A.Fake<RecipeDto>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _recipeRepository.GetRecipe(recipeId)).Returns(recipe);
            A.CallTo(() => _mapper.Map<RecipeDto>(recipe)).Returns(recipeDto);
            var controller = new RecipeController(_recipeRepository, _reviewRepository, _mapper);
            #endregion

            #region Assert
            var result = controller.GetRecipe(recipeId);
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
        public void RecipeController_CreateRecipe_ReturnsOk(int categoryId)
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
            var result = controller.GetRecipes();
            
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
        public void RecipeController_GetRecipeRating_ReturnsDecimal(int recipeId)
        {
            #region Arrange

            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _recipeRepository.GetRecipeRating(recipeId)).Returns(1.5M);
            var controller = new RecipeController(_recipeRepository, _reviewRepository, _mapper);
            #endregion

            #region Assert
            var result = controller.GetRecipeRating(recipeId);
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
        public void RecipeController_UpdateRecipe_ReturnsNoContent(int recipeId)
        {
            #region Arrange

            var recipeMap = A.Fake<Recipe>();
            var updatedRecipe = A.Fake<RecipeDto>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _mapper.Map<Recipe>(updatedRecipe)).Returns(recipeMap);
            A.CallTo(() => _recipeRepository.UpdateRecipe(recipeMap)).Returns(true);
            var controller = new RecipeController(_recipeRepository, _reviewRepository, _mapper);
            #endregion

            #region Assert
            var result = controller.UpdateRecipe(recipeId, updatedRecipe);

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
        public void RecipeController_DeleteRecipe_ReturnsNoContent(int recipeId)
        {
            #region Arrange

            var reviewsToDelete = A.Fake<ICollection<Review>>();
            var recipeToDelete = A.Fake<Recipe>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReviewsOfARecipe(recipeId)).Returns(reviewsToDelete);
            A.CallTo(() => _recipeRepository.GetRecipe(recipeId)).Returns(recipeToDelete);
            var controller = new RecipeController(_recipeRepository, _reviewRepository, _mapper);
            #endregion

            #region Assert
            var result = controller.DeleteRecipe(recipeId);

            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }
    }
}
