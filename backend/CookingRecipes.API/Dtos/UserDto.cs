﻿using CookingRecipes.Models;

namespace CookingRecipes.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}