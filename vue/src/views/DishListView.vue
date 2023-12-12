<template>
    <div class="container">
        <dish-form v-if="this.$route.query.action === 'create'"></dish-form>
        <dish-display v-else :myDishes="dishes"></dish-display>
        
    </div>
</template>

<script>
import DishDisplay from '@/components/DishDisplay.vue';
import DishService from '@/services/DishService.js';
import DishForm from '../components/DishForm.vue';

export default {
    data() {
        return {
            dishes: [],
        }
    },
    components: {
        DishDisplay,
        DishForm
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