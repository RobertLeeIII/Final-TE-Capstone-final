<template>
    <div class="container">

    </div>
</template>

<script>
import DishService from '@/services/DishService.js';

export default {
    data() {
        return {
            currentDish: {},
        }
    },
    methods: {
        getSpecificDish() {
            DishService.getDish(this.$route.params.dishId)
            .then(response => {
                this.currentDish = response.data;
            })
            .catch(error => {

            })
        }
    }
}
</script>

<style></style>