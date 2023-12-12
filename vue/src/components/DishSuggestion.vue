<template>
    <p>Here's where you sign up to bring a dish!</p>
    <h1>Thank you for volunteering to bring a {course Name} to my potluck</h1>
    <router-link
        :to="{ name: 'dish-signup', params: { potluckId: this.$route.params.potluckId }, query: { action: 'signup', course: currentCourse } }">
        <button>Bring your own dish</button>
    </router-link>
    <button @click="getNewDish">Click for a new dish!</button>
    <div v-for='meal in mealSuggestions.sort(Math.random)' :key=meal.idMeal>
        <img :src="meal.strMealThumb" alt="">
    </div>
</template>
<script>
import MealDBService from '@/services/MealDBService.js'

export default {
    data() {
        return {
            mealSuggestions: [],
            currentCourse: 0

        }
    },
    props: ['course'],
    methods: {
        getNewDish() {
            this.mealSuggestions = [];

            MealDBService.getRecipeByCourseId(this.currentCourse)
                .then(response => {
                    this.mealSuggestions = (response.data.meals.sort(Math.random));
                    // this.mealSuggestions.sort(Math.random)
                }).catch()
        }
    },
    created() {
        this.currentCourse = this.course;
    }

}
</script>
<style scoped>
div img {
    width: 300px;
    height: auto;
}

;
</style>