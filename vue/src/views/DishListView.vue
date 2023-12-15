<template>
    <main class="main-container">
      <div class="container">
        <dish-form v-if="this.$route.query.action === 'create'"></dish-form>
        <dish-display v-else :myDishes="dishes"></dish-display>
      </div>
    </main>
  </template>
  
  <script>
  import DishDisplay from '@/components/DishDisplay.vue';
  import DishService from '@/services/DishService.js';
  import DishForm from '../components/DishForm.vue';
  
  export default {
    data() {
      return {
        dishes: [],
      };
    },
    components: {
      DishDisplay,
      DishForm,
    },
    methods: {
      getUserDishes() {
        DishService.getDishesByUserId(this.$store.state.user.userId)
          .then((response) => {
            this.dishes = response.data;
          })
          .catch((error) => {
            // Handle error
          });
      },
    },
    created() {
      this.getUserDishes();
    },
  };
  </script>
  
  <style scoped>
  .main-container {
    background-image: url('/public/tablecloth1.jpg'); /* Replace with your actual image path */
    background-size: cover;
    background-position: center;
    min-height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    margin: 0;
  }
  
  </style>
  