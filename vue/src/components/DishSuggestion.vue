<template>
  <section class="signup-section">
    <p>Welcome to the potluck signup!</p>
    <h1>Thanks for volunteering to bring a {{ courseName }} to our potluck!</h1>
    <router-link :to="{
      name: 'dish-signup',
      params: { potluckId: $route.params.potluckId },
      query: { action: 'signup', course: currentCourse }
    }">
      <button class="signup-button">Bring Your Dish</button>
    </router-link>
    <button @click="getNewDish" class="new-dish-button">Get a New Dish</button>
    <div class="grid-container">
      <div v-for="meal in shuffledMeals" :key="meal.idMeal" class="meal-item" >
          <img :src="meal.strMealThumb" alt="Meal Image" class="meal-image" @click="showSuggestion"/>
          <suggestion-info :course="currentCourse" :currentMealId="meal.idMeal" v-show="show" ></suggestion-info>
      </div>
    </div>
  </section>
</template>
  
<script>
import MealDBService from '@/services/MealDBService.js';
import SuggestionInfo from '@/components/SuggestionInfo.vue'

export default {
  props: {
    course: Number,
  },
  components: {
    SuggestionInfo
  },
  data() {
    return {
      mealSuggestions: [],
      currentCourse: 0,
      show: false
    };
  },
  computed: {
    courseName() {
      return this.course === 1 ? 'appetizer' : this.course === 2 ? 'side' : this.course === 3 ? 'main dish' : 'dessert';
    },
    shuffledMeals() {
      let mealsCopy = this.mealSuggestions;

      let shuffledCopy =  mealsCopy.sort(() => Math.random() - 0.5);
      return shuffledCopy.slice(0,6);
    }
  },
  methods: {
    async getNewDish() {
      this.mealSuggestions = [];
      try {
        const response = await MealDBService.getRecipeByCourseId(this.currentCourse);
        this.mealSuggestions = response.data.meals;
      } catch (error) {
        console.error('Error fetching meal suggestions:', error);
      }
    },
    showDishDetails() {
      this.show = true;
    }
  },
  mounted() {
    this.currentCourse = this.course;
    this.getNewDish(); // Fetch initial meal suggestions on component creation
  }
};
</script>
  
<style scoped>
.signup-section {
  text-align: center;
  padding: 20px;
}

.signup-button,
.new-dish-button {
  padding: 10px 20px;
  margin: 10px;
  background-color: #3498db;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.signup-button:hover,
.new-dish-button:hover {
  background-color: #2980b9;
}

.grid-container {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-template-rows: auto auto;
  gap: 20px;
  grid-auto-rows: auto; /* Set to auto to adjust height based on content */
  justify-items: center;
  margin-bottom: 20px; /* Add some margin at the bottom */
}
.meal-item {
  margin: 0;
}

.meal-image {
  width: 100%;
  height: auto;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
</style>
  