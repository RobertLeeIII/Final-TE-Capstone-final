<template>
    <div class="main">

        <div class="card-container">
            <div class="card">
                <router-link :to="{ name: 'dish-list', params: { userId: this.$store.state.user.userId } }">
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
            <potluck class="dish" v-for="dish in dishes" :propDish="dish" :key="dish.dishId">
            </potluck>
            <div class="card-container" v-if="!hasPotlucks">
                <div class="card">
                    <div class="card-image">
                        <figure class="image is-centered">
                            <img src="/public/imagePotluck-transformed.jpg" alt="Potluck Image">
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

        }
    },
    props: ['dishes']
}
</script>

<style></style>