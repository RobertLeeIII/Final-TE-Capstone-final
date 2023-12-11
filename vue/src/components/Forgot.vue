<template>
  <div>
    <form class="box" @submit.prevent="handleSubmit">
      <h3>Forgot Password?</h3>
      <br />
      <div class="field">
        <label>Email</label>
        <div class="control">
          <input type="email" class="form-control" v-model="email" placeholder="Email" required />
        </div>
        <p class="help is-danger" v-if="validationError">{{ validationError }}</p>
      </div>

      <div class="field">
        <label>Security Question</label>
        <p>{{ securityQuestion }}</p>
      </div>

      <div class="field">
        <label>Security Answer</label>
        <div class="control">
          <input type="text" class="form-control" v-model="securityAnswer" placeholder="Security Answer" required />
        </div>
      </div>

      <button class="button is-primary" :disabled="loading">Submit</button>
    </form>
    <div class="forimage">
      <img class="img-size" src="/public\wall.jpg" />
    </div>
  </div>
</template>

<script>
// Import axios if needed
import axios from 'axios';

export default {
  name: 'Forgot',
  data() {
    return {
      email: '',
      securityAnswer: '',
      securityQuestion: '',
      loading: false,
      validationError: null,
    };
  },
  methods: {
    async handleSubmit() {
      try {
        // Basic validation
        if (!this.email || !this.securityAnswer) {
          this.validationError = 'Please enter both email and security answer.';
          return;
        } else {
          this.validationError = null; // Clear validation error if present
        }

        this.loading = true;

        // Make an API request to retrieve the security question based on the user's email
        const securityQuestionResponse = await axios.post('/controller/getSecurityQuestion', {
          email: this.email,
        });

        // Retrieve Security Question from Database/Backend
        this.securityQuestion = securityQuestionResponse.data.question;

        // Make an API request to verify the user based on email and security answer
        // Replace the following lines with your actual verification logic
        const response = await axios.post('/controller/verify', {
          email: this.email,
          securityAnswer: this.securityAnswer,
        });

        console.log(response.data);

        // Assuming your backend returns a success message
        // Redirect the user to the reset route with a token or handle accordingly
        this.$router.push(`/reset/${response.data.token}`);
      } catch (error) {
        console.error(error.response.data);
        // Display an error message to the user or handle errors appropriately
        this.validationError = 'Invalid email or security answer.';
      } finally {
        this.loading = false; // Reset loading state
      }
    },
  },
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
