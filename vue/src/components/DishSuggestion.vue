<template>
    <section class="signup-section">
      <p>Welcome to the potluck signup!</p>
      <h1>Thanks for volunteering to bring a {{ courseName }} to our potluck!</h1>
      <router-link
        :to="{
          name: 'dish-signup',
          params: { potluckId: $route.params.potluckId },
          query: { action: 'signup', course: currentCourse }
        }"
      >
        <button class="signup-button">Bring Your Dish</button>
      </router-link>
      <button @click="getNewDish" class="new-dish-button">Get a New Dish</button>
      <div class="grid-container">
        <div v-for="meal in shuffledMeals" :key="meal.idMeal" class="meal-item">
          <img :src="meal.strMealThumb" alt="Meal Image" class="meal-image" />
        </div>
      </div>
    </section>
  </template>
  
  <script>
  import { ref, computed, onMounted } from 'vue';
  import MealDBService from '@/services/MealDBService.js';
  
  export default {
    props: ['course'],
    setup(props) {
      const mealSuggestions = ref([]);
      const currentCourse = ref(0);
  
      const courseName = computed(() => {
        return props.course === 1 ? 'Starter' : props.course === 2 ? 'Main Course' : 'Dessert';
      });
  
      const shuffledMeals = computed(() => {
        const mealsCopy = mealSuggestions.value.slice(); // Create a copy of the array
        return mealsCopy.sort(() => Math.random() - 0.5);
      });
  
      const getNewDish = async () => {
        mealSuggestions.value = [];
        try {
          const response = await MealDBService.getRecipeByCourseId(currentCourse.value);
          mealSuggestions.value = response.data.meals;
        } catch (error) {
          console.error('Error fetching meal suggestions:', error);
        }
      };
  
      onMounted(() => {
        currentCourse.value = props.course;
        getNewDish(); // Fetch initial meal suggestions on component creation
      });
  
      return {
        currentCourse,
        courseName,
        shuffledMeals,
        getNewDish
      };
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
  grid-auto-rows: 0px;
  justify-items: center;
  max-height: 750px;
  overflow: hidden;

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