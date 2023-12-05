<template>
    <div class="potluck">
      <h2>{{ potluck.name }}</h2>
      <p>Date: {{ potluck.date }}</p>
      <p>Theme: {{ potluck.theme }}</p>
      <h3>Guests:</h3>
      <ul>
        <li v-for="Guests in potluck.Guests" :key="Guest.id">
          {{ Guest.email }} 
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Potluck',
    props: {
      potluck: {
        type: Object,
        required: true
      }
    }
  };
  </script>
  
  <style scoped>
  .potluck {
  }
  </style>
  