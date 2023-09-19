using AutoMapper;
using ContosoRecipes.Controllers;
using CookingRecipes.Controllers;
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
    public class ReviewControllerTests
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ReviewController _reviewController;

        public ReviewControllerTests()
        {
            _recipeRepository = A.Fake<IRecipeRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _reviewController = new ReviewController(_reviewRepository, _userRepository, _recipeRepository, _mapper);
        }

        [Fact]
        public void ReviewController_GetReviews_ReturnsOk()
        {
            #region Arrange
            var reviews = A.Fake<ICollection<ReviewDto>>();
            var reviewsList = A.Fake<List<ReviewDto>>();
            A.CallTo(() => _mapper.Map<List<ReviewDto>>(reviews)).Returns(reviewsList);
            #endregion

            #region Assert
            var result = _reviewController.GetReviews();
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
        public void ReviewController_GetReview_ReturnsOk(int reviewId)
        {
            #region Arrange
            var review = A.Fake<Review>();
            var reviewDto = A.Fake<ReviewDto>();
            A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReview(reviewId)).Returns(review);
            A.CallTo(() => _mapper.Map<ReviewDto>(review)).Returns(reviewDto);
            #endregion

            #region Assert
            var result = _reviewController.GetReview(reviewId);
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
        public void ReviewController_GetReviewsOfARecipe_ReturnsOk(int recipeId)
        {
            #region Arrange
            var reviews = A.Fake<ICollection<Review>>();
            var reviewDtosList= A.Fake<List<ReviewDto>>();
            A.CallTo(() => _recipeRepository.RecipeExists(recipeId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReviewsOfARecipe(recipeId)).Returns(reviews);
            A.CallTo(() => _mapper.Map<List<ReviewDto>>(reviews)).Returns(reviewDtosList);
            #endregion

            #region Assert
            var result = _reviewController.GetReviewsOfARecipe(recipeId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            #endregion
        }

        [Theory]
        [InlineData(3, 4)]
        [InlineData(33, 44)]
        [InlineData(333, 444)]
        public void ReviewController_CreateReview_ReturnsOk(int userId, int recipeId)
        {
            #region Arrange
            var review = A.Fake<Review>();
            var reviewMap = A.Fake<Review>();
            var reviewCreate = A.Fake<ReviewPostDto>();
            A.CallTo(() => _reviewRepository.GetUserReviewOfARecipe(userId, recipeId)).Returns(null);
            A.CallTo(() => _mapper.Map<Review>(reviewCreate)).Returns(reviewMap);
            A.CallTo(() => _recipeRepository.GetRecipe(recipeId)).Returns(reviewMap.Recipe);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(reviewMap.User);
            A.CallTo(() => _reviewRepository.CreateReview(reviewMap)).Returns(true);
            #endregion

            #region Assert
            var result = _reviewController.CreateReview(userId, recipeId, reviewCreate);

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
        public void ReviewController_UpdateReview_ReturnsNoContent(int reviewId)
        {
            #region Arrange

            var reviewMap = A.Fake<Review>();
            var updatedReview = A.Fake<ReviewDto>();
            A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
            A.CallTo(() => _mapper.Map<Review>(updatedReview)).Returns(reviewMap);
            A.CallTo(() => _reviewRepository.UpdateReview(reviewMap)).Returns(true);
            #endregion

            #region Assert
            var result = _reviewController.UpdateReview(reviewId, updatedReview);
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
        public void ReviewController_DeleteReview_ReturnsNoContent(int reviewId)
        {
            #region Arrange

            var reviewToDelete = A.Fake<Review>();
            A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReview(reviewId)).Returns(reviewToDelete);
            A.CallTo(() => _reviewRepository.DeleteReview(reviewToDelete)).Returns(true);
            #endregion

            #region Assert
            var result = _reviewController.DeleteReview(reviewId);
            #endregion

            #region Act
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }


    }
}
