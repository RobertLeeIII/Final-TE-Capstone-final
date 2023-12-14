<template>

    <h1 class="mealTitle">{{ currentMeal.strMeal }}</h1>
<a :href="currentMeal.strMealThumb"></a>
<router-link class="button" :to="
        {name: 'dish-signup',
        params: { potluckId: $route.params.potluckId },
        query: { action: 'signup', course: courseNumber, suggest: currentMeal.idMeal }}">Bring this Dish!</router-link>
</template>

<script>
import MealDBService from '@/services/MealDBService.js'

export default {
    data() {
        return {
            currentMeal: {},
            courseNumber: 0
        }
    },
    props: {
        currentMealId: Number,
        course: Number
    },
    created() {
        MealDBService.getRecipeById(this.currentMealId)
        .then(response => {
            this.currentMeal = response.data.meals[0];
        })
        .catch(error => {
            console.log(error.data)
        });
        this.courseNumber = this.course;
    }
}
</script>

<style scoped>
.mealTitle{
    font-size: 20px;
    font-weight: bold;
}
.button {
  padding: 10px 20px;
  margin: 10px;
  background-color: rgb(124, 169, 130);
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
</style>