using AutoMapper;
using ContosoRecipes.Controllers;
using CookingRecipes.Controllers;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using CookingRecipes.Repository;
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
    public class UserControllerTests
    {
        private IUserRepository _userRepository;
        private IRecipeRepository _recipeRepository;
        private IMapper _mapper;
        private UserController _userController;

        public UserControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _recipeRepository = A.Fake<IRecipeRepository>();
            _mapper = A.Fake<IMapper>();
            _userController = new UserController(_userRepository, _recipeRepository, _mapper);
        }

        [Fact]
        public async void UserController_GetUsers_ReturnsOk()
        {
            #region Arrange
            var Users = A.Fake<ICollection<UserDto>>();
            var usersList = A.Fake<List<UserDto>>();
            A.CallTo(() => _mapper.Map<List<UserDto>>(Users)).Returns(usersList);
            #endregion

            #region Assert
            var result = await _userController.GetUsers();
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
        public async void UserController_GetUser_ReturnsOk(int userId)
        {
            #region Arrange
            var user = A.Fake<User>();
            var userDto = A.Fake<UserDto>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(user);
            A.CallTo(() => _mapper.Map<UserDto>(user)).Returns(userDto);
            #endregion

            #region Assert
            var result = await _userController.GetUser(userId);
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
        public async void UserController_GetFavoriteRecipesByUser_ReturnsOk(int userId)
        {
            #region Arrange
            var recipes = A.Fake<ICollection<Recipe>>();
            var recipesList = A.Fake<List<RecipeDto>>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _userRepository.GetFavoriteRecipesByUser(userId)).Returns(recipes);
            A.CallTo(() => _mapper.Map<List<RecipeDto>>(recipes)).Returns(recipesList);
            #endregion

            #region Assert
            var result = await _userController.GetFavoriteRecipesByUser(userId);
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
        public async void UserController_GetReviewsByUser_ReturnsOk(int userId)
        {
            #region Arrange
            var reviews = A.Fake<ICollection<Review>>();
            var usersList = A.Fake<List<UserDto>>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _userRepository.GetReviewsByUser(userId)).Returns(reviews);
            A.CallTo(() => _mapper.Map<List<UserDto>>(reviews)).Returns(usersList);
            #endregion

            #region Assert
            var result = await _userController.GetReviewsByUser(userId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        //[Theory]
        //[InlineData(1)]
        //[InlineData(12)]
        //[InlineData(300)]
        //public async void UserController_CreateUser_ReturnsOk(int roleId)
        //{
        //    #region Arrange
        //    var userMap = A.Fake<User>();
        //    var userCreate = A.Fake<UserDto>();
        //    A.CallTo(() => _userRepository.UserNameExists(userCreate.UserName)).Returns(false);
        //    A.CallTo(() => _userRepository.EmailExists(userCreate.Email)).Returns(false);
        //    A.CallTo(() => _mapper.Map<User>(userCreate)).Returns(userMap);
        //    A.CallTo(() => _userRepository.CreateUser(userMap)).Returns(true);
        //    #endregion

        //    #region Assert
        //    var result = await _userController.CreateUser(userCreate);
        //    #endregion

        //    #region Act
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType(typeof(OkObjectResult));
        //    #endregion
        //}

        [Theory]
        [InlineData(1,43)]
        [InlineData(12,23)]
        [InlineData(30,19)]
        public async void UserController_AddFavoriteRecipe_ReturnsOk(int userId, int recipeId)
        {
            #region Arrange
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _userRepository.AddFavoriteRecipe(userId, recipeId)).Returns(true);
            #endregion

            #region Assert
            var result = await _userController.AddFavoriteRecipe(userId, recipeId);
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
        public async void RecipeController_UpdateUser_ReturnsNoContent(int userId)
        {
            #region Arrange
            var userMap = A.Fake<User>();
            var updatedUser = A.Fake<UserDto>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _mapper.Map<User>(updatedUser)).Returns(userMap);
            A.CallTo(() => _userRepository.UpdateUser(userMap)).Returns(true);
            #endregion

            #region Assert
            var result = await _userController.UpdateUser(userId, updatedUser);
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
        public async void UserController_DeleteUser_ReturnsNoContent(int userId)
        {
            #region Arrange
            var userToDelete = A.Fake<User>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(userToDelete);
            A.CallTo(() => _userRepository.DeleteUser(userToDelete)).Returns(true);
            #endregion

            #region Assert
            var result = await _userController.DeleteUser(userId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(3, 1)]
        public async void UserController_RemoveFavoriteRecipe_ReturnsNoContent(int userId, int recipeId)
        {
            #region Arrange
            var userToDelete = A.Fake<User>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _userRepository.RemoveFavoriteRecipe(userId, recipeId)).Returns(true);
            #endregion

            #region Assert
            var result = await _userController.RemoveFavoriteRecipe(userId, recipeId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }

    }

}
