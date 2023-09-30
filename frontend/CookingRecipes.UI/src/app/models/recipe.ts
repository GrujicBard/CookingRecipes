import { CuisineType } from "./enums/cuisineType"
import { DishType } from "./enums/dishType"

export default interface Recipe {
    id?: number
    title: string
    instructions: string
    imageName: string
    ingredients: string
    difficulty: number
    dishType: DishType
    cuisineType: CuisineType
}