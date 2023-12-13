<template>
  <div class="main">
  <div class="card">
    <form @submit.prevent="submitRecipe">
      <div class="card-body">
        <label for="dishName">Dish Name</label>
        <input id="dishName" type="text" v-model="newDish.name">

        <textarea v-model="newDish.recipe" placeholder="Enter directions"></textarea>

        <div class="toggle-section">
          <button @click.prevent="toggleSpecialDiets" type="button">{{ showSpecialDiets ? 'Hide Special Diets' : 'Special Diets' }}</button>
          <div v-if="showSpecialDiets" class="checkbox-group">
            <label v-for="diet in specialDiets" :key="diet">
              <input type="checkbox" :value="diet" v-model="newDish.diets" />
              {{ diet }}
            </label>
          </div>

          <button @click.prevent="toggleAllergens" type="button">{{ showAllergens ? 'Hide Allergens' : 'Allergens' }}</button>
          <div v-if="showAllergens" class="checkbox-group">
            <label v-for="allergen in allergens" :key="allergen">
              <input type="checkbox" :value="allergen" v-model="newDish.allergens" />
              {{ allergen }}
            </label>
          </div>
        </div>

        <button type="submit">Submit Recipe</button>
      </div>
    </form>
  </div>
  </div>
</template>

<script>
import DishService from '@/services/DishService.js';

export default {
  data() {
    return {
      newDish: {
        name: '',
        userId: this.$store.state.user.userId,
        creator: this.$store.state.user.username,
        recipe: '',
        diets: [],
        allergens: [],
        courseId: this.$route.query.course
      },
      showSpecialDiets: false,
      showAllergens: false,
      specialDiets: ['Vegan', 'Vegetarian', 'Halal', 'Celiac Friendly', 'Dairy-Free', 'Kosher'],
      allergens: ['Fish', 'Shellfish', 'Milk', 'Eggs', 'Soy', 'Sesame', 'Treenuts', 'Peanuts', 'Wheat']
    };
  },
  methods: {
    toggleSpecialDiets() {
      this.showSpecialDiets = !this.showSpecialDiets;
    },
    toggleAllergens() {
      this.showAllergens = !this.showAllergens;
    },
    submitRecipe() {
      DishService.postNewDish(this.newDish, this.$route.params.potluckId)
        .then(response => {
          this.$router.push({ name: 'potluck-details', params: { potluckId: this.$route.potluckId } });
        })
        .catch(error => {
          this.handleErrorResponse(error);
        });
      console.log('Recipe submitted:', this.newDish);
    },
    handleErrorResponse(error) {
      if (error.response) {
        if (error.response.status === 404) {
          console.log('404 PROBLEM');
        }
        else if(error.response.status === 401) {
          this.$router.push({name: 'login'})
        }
      }
    }
  }
};
</script>

<style scoped>
.main {
  background-image: url('/public\7660462.jpg');
  background-size: cover;
  background-position: center;
  min-height: 90vh;
  display: flex;
  justify-content: center;
  align-items: center;
}
.card {
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border: 1px solid #ccc;
  max-width: 600px;
  min-height: 80vh;
  margin: auto;
}

.card-body {
  padding: 20px;
}.recipe-form {
  max-width: 600px;
  margin: auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

label {
  display: block;
  margin-bottom: 8px;
}

input[type="text"],
textarea {
  width: 100%;
  padding: 8px;
  margin-bottom: 12px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.toggle-section {
  margin-top: 20px;
}

button {
  padding: 8px 16px;
  margin-right: 10px;
  margin-top: 10px;
  background-color: #007bff;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}

.checkbox-group {
  margin-top: 10px;
}

.checkbox-group label {
  display: block;
  margin-bottom: 6px;
}
</style>
