<template>
    <router-link :to="{
        name: 'dish-details',
        params: { dishId: propDish.dishId },
    }">
        <div class="card">
            <div class="card-image">
                <figure class="image is-4by3">
                    <img :src="changingImage" alt="Dish Image" />
                </figure>
            </div>
            <div class="card-content">
                <div class="media">
                    <div class="media-left">
                        <figure class="image is-48x48">
                            <img src="/public/logo.png" alt="Logo" />
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
    </router-link>
</template>

<script>
export default {
    data() {
        return {

        }
    },
    props: {
        propDish: Object
    }
}
</script>

<style></style>