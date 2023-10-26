import { CuisineType } from "../models/enums/cuisineType"
import { DishType } from "../models/enums/dishType"

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