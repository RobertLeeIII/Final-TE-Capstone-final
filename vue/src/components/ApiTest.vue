<template>
  <div>
    <button @click="getRecipe">Recipe Suggestion</button>
    <div v-if="currentRecipe">
      <h2>{{ currentRecipe.strMeal }}</h2>
      <img :src="currentRecipe.strMealThumb" :alt="currentRecipe.strMeal" />
      <p>{{ currentRecipe.strInstructions }}</p>
    </div>
  </div>
</template>

<script>
import MealDBService from "../services/MealDBService.js";

export default {
  data() {
    return {
      currentRecipe: {},
    };
  },
  methods: {
    getRecipe() {
      MealDBService.getRandomRecipe()
        .then((response) => {
          this.currentRecipe = response.data;
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
  },
};
</script>
