
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



        <div> <textarea v-model="directions" placeholder="Enter directions" style="height: 200px;"></textarea></div>
        <div>
          <button @click.prevent="toggleSpecialDiets" type="button">{{ showSpecialDiets ? 'Hide Special Diets' : 'Special Diets ? ' }}</button>
          <div v-if="showSpecialDiets">
            <label>
              <input type="checkbox" v-model="isVegan" />
              Vegan
            </label>
            <label>
              <input type="checkbox" v-model="isVegetarian" />
              Vegetarian
            </label>
            <label>
              <input type="checkbox" v-model="isHalal" />
              Halal
            </label>
            <label>
              <input type="checkbox" v-model="isCeliacFriendly" />
              Celiac Friendly
            </label>
            <label>
              <input type="checkbox" v-model="isDairyFree" />
              Dairy-Free
            </label>
            <label>
              <input type="checkbox" v-model="isKosher" />
              Kosher
            </label>
          </div>

          <button @click.prevent="toggleAllergens" type="button">{{ showAllergens ? 'Hide Allergens' : 'Allergens?'
          }}</button>
          <div v-if="showAllergens">
            <label>
              <input type="checkbox" v-model="hasFish" />
              Fish
            </label>
            <label>
              <input type="checkbox" v-model="hasShellfish" />
              Shellfish
            </label>
            <label>
              <input type="checkbox" v-model="hasMilk" />
              Milk
            </label>
            <label>
              <input type="checkbox" v-model="hasEggs" />
              Eggs
            </label>
            <label>
              <input type="checkbox" v-model="hasSoy" />
              Soy
            </label>
            <label>
              <input type="checkbox" v-model="hasSesame" />
              Sesame
            </label>
            <label>
              <input type="checkbox" v-model="hasTreenuts" />
              Treenuts
            </label>
            <label>
              <input type="checkbox" v-model="hasPeanuts" />
              Peanuts
            </label>
            <label>
              <input type="checkbox" v-model="hasWheat" />
              Wheat
            </label>
          </div>
        </div>



        <button type="submit">Submit Recipe</button>
      </form>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/vue@2"></script>
    <script src="app.js"></script>
  </body>
</template>
<script>
export default {

  data() {
    return {
      //  ingredients: [{ name: '', amount: '' }],
      directions: '',
      isVegan: false,
      isVegetarian: false,
      isHalal: false,
      isCeliacFriendly: false,
      isDairyFree: false,
      isKosher: false,
      showSpecialDiets: false,
      showAllergens: false,
      hasFish: false,
      hasShellfish: false,
      hasMilk: false,
      hasEggs: false,
      hasSoy: false,
      hasSesame: false,
      hasTreenuts: false,
      hasPeanuts: false,
      hasWheat: false
    }
  },
  methods: {
    addIngredient() {
      this.ingredients.push({ name: '', amount: '' });
    },
    removeIngredient(index) {
      this.ingredients.splice(index, 1);
    },
    toggleSpecialDiets() {
      this.showSpecialDiets = !this.showSpecialDiets;
    },
    toggleAllergens() {
      this.showAllergens = !this.showAllergens;
    },
    submitRecipe() {
      console.log('Recipe submitted:', {
        //  ingredients: this.ingredients,
        directions: this.directions,
        isVegan: this.isVegan,
        isVegetarian: this.isVegetarian,
        isHalal: this.isHalal,
        isCeliacFriendly: this.isCeliacFriendly,
        isDairyFree: this.isDairyFree,
        isKosher: this.isKosher,
        hasFish: this.hasFish,
        hasShellfish: this.hasShellfish,
        hasMilk: this.hasMilk,
        hasEggs: this.hasEggs,
        hasSoy: this.hasSoy,
        hasSesame: this.hasSesame,
        hasTreenuts: this.hasTreenuts,
        hasPeanuts: this.hasPeanuts,
        hasWheat: this.hasWheat
      });
    }
  }
}
</script>

