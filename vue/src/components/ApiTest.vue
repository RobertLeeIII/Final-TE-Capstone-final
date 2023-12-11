<template>
  <div>
    <div class="page-container">
      <button @click="getRecipe" class="button">Recipe Suggestion</button>
      <br><br>
      <div v-if="currentRecipe" class="recipe-container">
        <h2 class="recipe-title">{{ currentRecipe.strMeal }}</h2>
        <img :src="currentRecipe.strMealThumb" :alt="currentRecipe.strMeal" class="recipe-image" />
        <p class="recipe-instructions">{{ currentRecipe.strInstructions }}</p>
      </div>
      <br>
      <div v-if="showIngredients" class="ingredients">List of Ingredients:</div>
      <br>
      <ul v-if="showIngredients" class="ingredient-list">
        <li v-for="(ingredient, index) in getIngredients()" :key="index" class="ingredient-item">
          {{ ingredient }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import MealDBService from "../services/MealDBService.js";

export default {
  data() {
    return {
      currentRecipe: {},
      showIngredients: false,
    };
  },
  methods: {
    getRecipe() {
      MealDBService.getRandomRecipe()
        .then((response) => {
          this.currentRecipe = response.data.meals[0];
          this.showIngredients = true; // Show ingredients when recipe is loaded
        })
        .catch((error) => {
          this.handleErrorResponse(error);
        });
    },
    handleErrorResponse(error) {
      if (error.response) {
        if (error.response.status == 404) {
          console.log("404 PROBLEM");
        } else if (error.request) {
          console.log("OTHER PROBLEM");
        } else {
          console.log("ANOTHER PROBLEM");
        }
      }
    },
    getIngredients() {
      const ingredients = [];
      for (let i = 1; i < 1000; i++) {
        // Assuming a maximum of 20 ingredients
        const ingredientKey = `strIngredient${i}`;
        const measureKey = `strMeasure${i}`;
        const ingredient = this.currentRecipe[ingredientKey];
        const measure = this.currentRecipe[measureKey];

        if (ingredient) {
          ingredients.push(`${measure} ${ingredient}`);
        }
      }
      return ingredients;
    },
  },
};
</script>

<style>
/* body {
  background-color: #cef0c3;
  margin: 0;
}
 */
.button {
  background-color: #4CAF50;
  border: none;
  color: white;
  padding: 10px 20px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
}

.page-container {
  background-color: #cef0c3;
  padding: 20px; /* Add padding as needed */
}

.recipe-container {
  text-align: center;
  padding: 15px; /* Add padding as needed */
  margin-top: 20px; /* Adjust margin as needed */
}

.recipe-title {
  font-size: 24px;
  margin-bottom: 10px;
  border-bottom: 2px solid #4CAF50; /* Add a line below the recipe title */
  padding-bottom: 10px; /* Add padding as needed */
}

.recipe-image {
  max-width: 100%;
  border-radius: 8px;
  margin: 20px 0; /* Adjust margin as needed */
}

.recipe-instructions {
  font-size: 16px;
  margin-bottom: 20px;
  border-bottom: 2px solid #4CAF50; /* Add a line below the instructions */
  padding-bottom: 10px; /* Add padding as needed */
}

.ingredients {
  font-size: 18px;
  font-weight: bold;
  border-bottom: 2px solid #4CAF50; /* Add a line below the ingredients title */
  padding-bottom: 10px; /* Add padding as needed */
}

.ingredient-list {
  list-style-type: none;
  padding: 0;
  margin-bottom: 20px;
}

.ingredient-item {
  font-size: 16px;
  margin-bottom: 5px;
  padding-left: 20px;
  position: relative;
}

.ingredient-item::before {
  content: "•";
  position: absolute;
  left: 0; /* Adjusted position */
}
</style>
