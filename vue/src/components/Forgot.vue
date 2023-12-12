<template>
  <div>
    <form class="box" @submit.prevent="handleSubmit">
      <h3>Forgot Password?</h3>
      <br />
      <div class="field">
        <label>Email</label>
        <div class="control">
          <input
            type="email"
            class="form-control"
            v-model="email"
            placeholder="Email"
            required
          />
        </div>
        <p class="help is-danger" v-if="validationError">
          {{ validationError }}
        </p>
      </div>

      <div v-if="securityQuestion !== null">
        <div class="field">
          <label>Security Question</label>
          <p>{{ securityQuestion }}</p>
        </div>

        <div class="field">
          <label>Security Answer</label>
          <div class="control">
            <input
              type="text"
              class="form-control"
              v-model="securityAnswer"
              placeholder="Security Answer"
              required
            />
          </div>
        </div>

        <button class="button is-primary" :disabled="loading">Submit</button>
      </div>
    </form>
    <div class="forimage">
      <img class="img-size" src="/public/wall.jpg" />
    </div>
  </div>
</template>

<script>
// Import axios if needed
import axios from "axios";

export default {
  name: "Forgot",
  data() {
    return {
      email: "",
      securityAnswer: "",
      securityQuestion: null, // Initialize security question to null
      loading: false,
      validationError: null,
    };
  },
  methods: {
    async handleSubmit() {
      try {
        // Basic validation
        if (!this.email || !this.securityAnswer) {
          this.validationError = "Please enter both email and security answer.";
          return;
        } else {
          this.validationError = null; // Clear validation error if present
        }

        this.loading = true;

        // Make an API request to verify the user based on email and security answer
        const response = await axios.post("/reset", {
          email: this.email,
          securityAnswer: this.securityAnswer,
        });

        // Assuming your backend returns a success message
        // Redirect the user to the reset route with a token or handle accordingly
        this.$router.push(`/reset/${this.email}`);
      } catch (error) {
        console.error(error.response.data);
        // Display an error message to the user or handle errors appropriately
        this.validationError = "Invalid email or security answer.";
      } finally {
        this.loading = false; // Reset loading state
      }
    },

    async retrieveSecurityQuestion() {
      try {
        // Make an API request to retrieve the security question based on the user's email
        const securityQuestionResponse = await axios.post(
          "/forgot",
          {
            email: this.email,
          }
        );

        // Retrieve Security Question from Database/Backend
        this.securityQuestion = securityQuestionResponse.data.securityQuestion;
      } catch (error) {
        console.error(error.response.data);
        // Handle error appropriately, e.g., show an error message to the user
      }
    },
  },
  mounted() {
    // Call a method here to retrieve the security question initially
    this.retrieveSecurityQuestion();
  },
};
</script>
