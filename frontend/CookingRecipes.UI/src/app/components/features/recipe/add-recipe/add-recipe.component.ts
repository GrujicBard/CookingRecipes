import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Recipe from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.css']
})
export class AddRecipeComponent implements OnInit {

  createRecipe: Recipe = {
    title: "",
    ingredients: "",
    instructions: "",
    imageName: "",
    dishType: 0,
    difficulty: 0,
  };

  categoryId: number = 2;

  dishTypes: string[] = [
    "Breakfast",
    "Brunch",
    "Lunch",
    "Dinner",
  ];

  constructor(
    private recipeService: RecipeService,
    private router: Router,
  ) { }

  ngOnInit() {
  }
  addRecipe() {
    this.recipeService.addRecipe(this.categoryId, this.createRecipe)
      .subscribe({
        next: () => {
          console.log("test")
          this.router.navigate(['recipes']);
        },
        error:(response)=>{
          console.log(response);
          this.router.navigate(['recipes']);
        },
      });
  }

}
