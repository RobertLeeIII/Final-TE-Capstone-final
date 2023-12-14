<template>
        <div class="card">
            <div class="card-image">
                <figure class="image">
                    <img :src="imageLink" alt="Dish Image" />
                </figure>
            </div>
            <div class="card-content">
                <div class="media">
                    <div class="media-left">
                        <figure class="image is-48x48">
                            <img src="/public/potluck-planner-logo.png" alt="Logo" />
                        </figure>
                    </div>
                    <div class="media-content">
                        <p class="title is-4">{{ propDish.name }}</p>
                        <p class="subtitle is-6">{{ propDish.creator }}</p>
                    </div>
                </div>
                <div class="content">
                    <p>{{ propDish.recipe }}</p>
                    <br />
                </div>
            </div>
        </div>
</template>

<script>
import MealDBService from "@/services/MealDBService.js"
export default {
    data() {
        return {
            imageLink: '/public/spoonDish.jpg'
        }
    },
    props: {
        propDish: Object
    },
    created() {
        MealDBService.getRecipeByName(this.propDish.name).then(response => {
            this.imageLink = response.data.meals[0].strMealThumb
        })
    }
}
</script>

<style></style>