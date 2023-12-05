<template>
    <div class="potluck">
      <h2>{{ potluck.name }}</h2>
      <p>Date: {{ potluck.date }}</p>
      <p>Theme: {{ potluck.theme }}</p>
      <h3>Guests:</h3>
      <ul>
        <!-- <li v-for="Guests in potluck.Guests" :key="Guest.id"> -->
          <!-- {{ Guest.email }}  -->
        <!-- </li> -->
      </ul>
    </div>
  </template>
  
  <script>
  import PotluckService from '@/services/PotluckService.js'
  export default {
    data(){
      return{
        guests: [],
      }
    },
    props: {
      potluck: {
        type: Object,
        required: true
      }
    },
    methods: {
      retrieveGuests() { 
        PotluckService.showPotlucks(this.$route.params.userId)
        .then(response => {
          response.data.array.forEach(element => {
            this.guests.push(element);            
          });
        })
        .catch(
          "oh no!"
        )

      }
    },
    created() {
      this.retrieveGuests()
    }
  };
  </script>
  
  <style scoped>
  .potluck {
  }
  </style>
  