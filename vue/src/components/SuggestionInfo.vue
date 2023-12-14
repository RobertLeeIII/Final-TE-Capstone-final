<template>
<a :href="currentMeal.strMealThumb"></a>
<router-link :to="
        {name: 'dish-signup',
        params: { potluckId: $route.params.potluckId },
        query: { action: 'signup', course: courseNumber, suggest: currentMeal.idMeal }}"><button>Bring this Dish!</button></router-link>
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

<style></style>