<template>
    <div>
      <form class="box" @submit.prevent="handleSubmit">
        <h3>Forgot Password?</h3>
        <br />
        <div class="field">
          <label>Email</label>
          <div class="control">
            <input type="email" class="form-control" v-model="email" placeholder="Email" />
          </div>
        </div>
  
        <button class="button is-primary">Submit</button>
      </form>
      <div class="forimage">
        <img class="img-size" src="/public\wall.jpg" />
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: 'Forgot',
    data() {
      return {
        email: ''
      };
    },
    methods: {
      async handleSubmit() {
        try {
          // Make an API request to send the reset link to the email
          const response = await axios.post('/controller/forgot', {
            email: this.email
          });
  
          console.log(response.data);
          // Assuming your backend returns a success message
  
          // Redirect the user to the reset route with a token
          this.$router.push(`/reset/${response.data.token}`);
        } catch (error) {
          console.error(error.response.data);
          // Handle errors appropriately
        }
      }
    }
  };
  </script>
  
  <style>
  .img-size {
    height: 650px;
  }
  .forimage {
    display: flex;
    justify-content: center;
  }
  </style>