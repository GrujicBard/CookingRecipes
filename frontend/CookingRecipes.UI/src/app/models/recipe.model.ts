import { CuisineType } from "./enums/cuisine.enum"
import { DishType } from "./enums/dish.enum"
import RecipeCategory from "./recipe-category.model"

export default class Recipe {
    id?: number
    title: string
    instructions: string
    imageName: string
    ingredients: string
    difficulty: number
    dishType: DishType
    cuisineType: CuisineType
    recipeCategories: RecipeCategory[]
     
    constructor(
        id: number,
        title: string,
        instructions: string,
        imageName: string,
        ingredients: string,
        difficulty: number,
        dishType: DishType,
        cuisineType: CuisineType,
        recipeCategories: RecipeCategory[]
    ) {
        this.id = id
        this.title = title
        this.instructions = instructions
        this.imageName = imageName
        this.ingredients = ingredients
        this.difficulty = difficulty
        this.dishType = dishType
        this.cuisineType = cuisineType
        this.recipeCategories = recipeCategories
    } 
}