<template>
    <div class="main">

        <div class="card-container">
            <div class="card">
                <router-link :to="{ name: 'dish-list', params: { userId: this.$store.state.user.userId }, query: { action: 'create' } }">
                    <div class="card-image">
                        <figure class="image is-centered">
                            <img src="/public/imagePotluck-transformed.jpg" alt="Potluck Image">
                        </figure>
                    </div>
                    <div class="card-content">

                        <div class="media">
                            <div class="media-center">
                                <p>Create a Dish</p>
                            </div>

                        </div>
                    </div>
                </router-link>
            </div>
            <dish class="dish" v-for="dish in myDishes" :propDish="dish" :key="dish.dishId">
            </dish>
            <div class="card-container" v-if="!hasDishes">
                <div class="card">
                    <div class="card-image">
                        <figure class="image is-centered">
                            <img src="/public/imagePotluck-transformed.jpg" alt="Dish Image">
                        </figure>
                    </div>
                    <div class="card-content">

                        <div class="media">
                            <div class="media-center">
                                <p>You Have No Dishes :(</p>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Dish from '@/components/Dish.vue';

export default {
    data() {
        return {
            dishes: [],
            hasDishes: true
        }
    },
    components: {
        Dish
    },
    props: {
        myDishes: Array
    },
    computed: {

    },
    methods: {
        fillDishesArray() {
            this.dishes = this.myDishes; //JSON.parse(JSON.stringify(this.myDishes));
        }
    },
    created() {
        this.fillDishesArray();
    }
}
</script>

<style></style>