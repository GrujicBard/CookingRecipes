import { RecipeType } from "./enums/recipeType"

export default class Category {
    id?: number
    recipeType: RecipeType

    constructor(recipeType: RecipeType) {
        this.recipeType = recipeType
    }
}