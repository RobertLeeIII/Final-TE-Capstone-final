<template>
    <div class="container">
        <dish-display :dishes="dishes"></dish-display>
    </div>
</template>

<script>
import DishDisplay from '@/components/DishDisplay.vue';
import DishService from '@/services/DishService.js';
import CreateDishForm from '../components/CreateDishForm.vue';

export default {
    data() {
        return {
            dishes: [],
        }
    },
    components: {
        DishDisplay
    },
    methods: {
        getUserDishes() {
            DishService.getDishesByUserId(this.$store.state.user.userId)
            .then(response => {
                this.dishes = response.data;
            })
            .catch(error => {

            })
            
        }
    },
    created() {
        this.getUserDishes();
    }
}
</script>

<style></style>