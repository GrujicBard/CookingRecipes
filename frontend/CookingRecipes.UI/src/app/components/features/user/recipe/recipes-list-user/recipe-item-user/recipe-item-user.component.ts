import { Component, OnInit } from '@angular/core';
import Recipe from 'src/app/models/recipe.model';

@Component({
  selector: 'app-recipe-item-user',
  templateUrl: './recipe-item-user.component.html',
  styleUrls: ['./recipe-item-user.component.css']
})
export class RecipeItemUserComponent implements OnInit {
  recipe!: Recipe;
  constructor() { }

  ngOnInit() {
  }

}
