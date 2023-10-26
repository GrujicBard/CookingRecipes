import Category from "./category.model";

export default class RecipeCategory {
    category?: Category

    constructor(category: Category) {
        this.category = category
    } 
}