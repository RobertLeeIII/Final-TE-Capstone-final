<template>
  <div>
    <form class="box" @submit.prevent="handleSubmit">
      <h3>Password Recovery</h3>
      <br />
      <div class="field">
        <label for="email">Email</label>
        <div class="control">
          <input
            type="email"
            id="email"
            class="form-control"
            v-model="email"
            placeholder="Email"
            autocomplete="email"
            required
          />
        </div>
        <p class="help is-danger" v-if="validationError">
          {{ validationError }}
        </p>
      </div>

      <div v-if="!securityQuestion">
        <button class="button is-primary" @click="retrieveSecurityQuestion">
          Get Security Question
        </button>
      </div>

      <div v-if="securityQuestion !== null">
        <div class="field">
          <label>Security Question: </label>
          {{ securityQuestion }}
        </div>

        <div class="field">
          <label for="securityAnswer">Security Answer</label>
          <div class="control">
            <input
              type="text"
              id="securityAnswer"
              class="form-control"
              v-model="securityAnswer"
              placeholder="Security Answer"
              autocomplete="off"
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
import authService from "@/services/AuthService.js";

export default {
  name: "PasswordRecovery",
  data() {
    return {
      email: "",
      securityAnswer: "",
      securityQuestion: null,
      loading: false,
      validationError: null,
    };
  },
  methods: {
    async handleSubmit() {
      try {
        if (!this.email || !this.securityAnswer) {
          this.validationError = "Please enter both email and security answer.";
          return;
        } else {
          this.validationError = null;
        }

        this.loading = true;

        const response = await authService.getRecoveryByEmail({
          email: this.email,
          answer: this.securityAnswer,
        });
        if(response.data)
        {
          this.$router.push(`/PasswordReset/reset/${this.email}`);
        }
      } catch (error) {
        console.error(error);
     
        this.validationError = "Invalid email or security answer.";
      } finally {
        this.loading = false; 
      }
    },

    async retrieveSecurityQuestion() {
      try {
        // Make an API request to retrieve the security question based on the user's email
        const securityQuestionResponse = await authService.getSecurityQuestionByEmail(
          this.email
        );
        console.log(securityQuestionResponse)
        // Retrieve Security Question from Database/Backend
        this.securityQuestion = securityQuestionResponse.data;
      } catch (error) {
        console.error(error);
        // Handle error appropriately, e.g., show an error message to the user
      }
    },
  },
};
</script>
