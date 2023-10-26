import { RecipeType } from "./enums/recipe.enum"

export default class Category {
    id?: number
    recipeType: RecipeType

    constructor(recipeType: RecipeType) {
        this.recipeType = recipeType
    }
}