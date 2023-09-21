using AutoMapper;
using CookingRecipes.Interfaces;
using CookingRecipes.Controllers;
using CookingRecipes.Repository;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoRecipes.Controllers;
using CookingRecipes.Dtos;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using CookingRecipes.Models;

namespace CookingRecipes.Tests.Controller
{
    public class CategoryControllerTests
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;
        private CategoryController _categoryController;

        public CategoryControllerTests()
        {
            _categoryRepository = A.Fake<ICategoryRepository>();
            _mapper = A.Fake<IMapper>();
            _categoryController = new CategoryController(_categoryRepository, _mapper);
        }

        [Fact]
        public async void RecipeController_GetCategories_ReturnsOk()
        {
            #region Arrange
            var categories = A.Fake<ICollection<CategoryDto>>();
            var categoriesList = A.Fake<List<CategoryDto>>();
            A.CallTo(() => _mapper.Map<List<CategoryDto>>(categories)).Returns(categoriesList);
            #endregion

            #region Assert
            var result = await _categoryController.GetCategories();
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
        public async void CategoryController_GetCategory_ReturnsOk(int categoryId)
        {
            #region Arrange
            var category = A.Fake<Category>();
            var categoryDto = A.Fake<CategoryDto>();
            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _categoryRepository.GetCategory(categoryId)).Returns(category);
            A.CallTo(() => _mapper.Map<CategoryDto>(category)).Returns(categoryDto);
            #endregion

            #region Assert
            var result = await _categoryController.GetCategory(categoryId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Fact]
        public async void CategoryController_CreateCategory_ReturnsOk()
        {
            #region Arrange
            var category = A.Fake<Category>();
            var categoryCreate = A.Fake<CategoryDto>();
            var recipeMap = A.Fake<Recipe>();
            var recipeCreate = A.Fake<RecipeDto>();
            var recipes = A.Fake<ICollection<Recipe>>();
            var recipesList = A.Fake<IList<RecipeDto>>();
            A.CallTo(() => _categoryRepository.CategoryTypeExists(categoryCreate.RecipeType)).Returns(false);
            A.CallTo(() => _mapper.Map<Category>(categoryCreate)).Returns(category);
            A.CallTo(() => _categoryRepository.CreateCategory(category)).Returns(true);
            #endregion

            #region Assert
            var result = await _categoryController.CreateCategory(categoryCreate);

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
        public async void CategoryController_UpdateCategory_ReturnsNoContentAsync(int categoryId)
        {
            #region Arrange

            var categoryMap = A.Fake<Category>();
            var updatedCategory = A.Fake<CategoryDto>();
            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _mapper.Map<Category>(updatedCategory)).Returns(categoryMap);
            A.CallTo(() => _categoryRepository.UpdateCategory(categoryMap)).Returns(true);
            #endregion

            #region Assert
            var result = await _categoryController.UpdateCategory(categoryId, updatedCategory);

            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async void CategoryController_DeleteCategory_ReturnsNoContentAsync(int categoryId)
        {
            #region Arrange
            var categoryToDelete = A.Fake<Category>();
            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _categoryRepository.GetCategory(categoryId)).Returns(categoryToDelete);
            #endregion

            #region Assert
            var result = await _categoryController.DeleteCategory(categoryId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }
    }
}