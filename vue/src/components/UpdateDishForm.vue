<template>
    <form v-on:submit.prevent="updateDish">
        <label for="recipe">Recipe</label>
        <input type="text" v-model="recipe">

        <button type="submit"></button>
    </form>
</template>

<script>
import DishService from '@/services/DishService.js'

export default {
    data() {
        return {
            Dish: {
                dishId: 0,  // THIS IS NOT DONE, WORK TO MAKE THIS GOOD
                recipe: "",
                potluckId: 0,

            }
        }
    },
    methods: {
        updateDish() {
            DishService.updateDish();
            this.$router.push //WHERE???
        }
    }
}
</script>

<style>
</style>