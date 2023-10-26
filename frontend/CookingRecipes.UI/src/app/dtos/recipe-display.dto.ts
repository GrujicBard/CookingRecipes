import { CuisineType } from "../models/enums/cuisine.enum"
import { DishType } from "../models/enums/dish.enum"

export default interface RecipeDisplayDto{
    id?: number
    title: string
    instructions: string
    imageName: string
    ingredients: string
    difficulty: number
    dishType: DishType
    cuisineType: CuisineType
    categories?: string[]
}