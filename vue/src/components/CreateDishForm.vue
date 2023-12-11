
<template>
  <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Recipe Form</title>
  </head>

  <body>
    <div id="app">
      <form @submit.prevent="submitRecipe">
        <!--   <div v-for="(ingredient, index) in ingredients" :key="index" class="ingredient-inputs">
                <input v-model="ingredient.name" type="text" placeholder="Ingredient" />
                <input v-model="ingredient.amount" type="text" placeholder="Amount" />
                <button @click.prevent="removeIngredient(index)" type="button">Remove</button>
            </div>

            <button @click.prevent="addIngredient" type="button">Add Ingredient</button> -->

            <label for="dishName">Dish Name</label>
            <input id="dishName" type="text" v-model="newDish.name">
        <div> <textarea v-model="newDish.recipe" placeholder="Enter directions" style="height: 200px;"></textarea></div>
        <div>
          <button @click.prevent="toggleSpecialDiets" type="button">{{ showSpecialDiets ? 'Hide Special Diets' : 'Special Diets ? ' }}</button>
          <div v-if="showSpecialDiets">
            <label>
              <input type="checkbox" value="vegan" v-model="newDish.diets" />
              Vegan
            </label>
            <label>
              <input type="checkbox" value="vegetarian" v-model="newDish.diets" />
              Vegetarian
            </label>
            <label>
              <input type="checkbox" value="halal" v-model="newDish.diets" />
              Halal
            </label>
            <label>
              <input type="checkbox" value="celiac" v-model="newDish.diets" />
              Celiac Friendly
            </label>
            <label>
              <input type="checkbox" value="lactose" v-model="newDish.diets" />
              Dairy-Free
            </label>
            <label>
              <input type="checkbox" value="kosher" v-model="newDish.diets" />
              Kosher
            </label>
          </div>

          <button @click.prevent="toggleAllergens" type="button">{{ showAllergens ? 'Hide Allergens' : 'Allergens?'
          }}</button>
          <div v-if="showAllergens">
            <label>
              <input type="checkbox" value="fish" v-model="newDish.allergens" />
              Fish
            </label>
            <label>
              <input type="checkbox" value="shellfish" v-model="newDish.allergens" />
              Shellfish
            </label>
            <label>
              <input type="checkbox" value="milk" v-model="newDish.allergens" />
              Milk
            </label>
            <label>
              <input type="checkbox" value="egg" v-model="newDish.allergens" />
              Eggs
            </label>
            <label>
              <input type="checkbox" value="soy" v-model="newDish.allergens" />
              Soy
            </label>
            <label>
              <input type="checkbox" value="sesame" v-model="newDish.allergens" />
              Sesame
            </label>
            <label>
              <input type="checkbox" value="treenuts" v-model="newDish.allergens" />
              Treenuts
            </label>
            <label>
              <input type="checkbox" value="peanuts" v-model="newDish.allergens" />
              Peanuts
            </label>
            <label>
              <input type="checkbox" value="wheat" v-model="newDish.allergens" />
              Wheat
            </label>
          </div>
        </div>



        <button type="submit">Submit Recipe</button>
      </form>
    </div>

    <!-- <script src="https://cdn.jsdelivr.net/npm/vue@2"></script>
    <script src="app.js"></script> -->
  </body>
</template>
<script>
import DishService from '@/services/DishService.js'
export default {

  data() {
    return {
      //  ingredients: [{ name: '', amount: '' }],
      newDish: {
        name: '',
        userId: this.$store.state.user.userId,
        creator: this.$store.state.user.username,
        recipe: '',
        diets: [],
        allergens: [],
        courseId: 1 //IS THIS OK?
      },
      
       showSpecialDiets: false,
       showAllergens: false,
     
    }
  },
  methods: {
    // addIngredient() {
    //   this.ingredients.push({ name: '', amount: '' });
    // },
    // removeIngredient(index) {
    //   this.ingredients.splice(index, 1);
    // },
    toggleSpecialDiets() {
      this.showSpecialDiets = !this.showSpecialDiets;
    },
    toggleAllergens() {
      this.showAllergens = !this.showAllergens;
    },
    submitRecipe() {

      DishService.postNewDish(this.newDish, this.$route.params.potluckId)
      .then()
      .catch()
      console.log('Recipe submitted:', this.newDish);
    }
  }
}
</script>

