<template>
  <div>
    <form class="box" @submit.prevent="handleSubmit">
      <h3>Reset Password</h3>
      <br />
      <div class="field">
        <label>Password</label>
        <div class="control">
          <input type="password" v-model="password" placeholder="********" required />
        </div>
      </div>
      <div class="field">
        <label>Password Confirm</label>
        <div class="control">
          <input type="password" v-model="password_confirm" placeholder="********" required />
        </div>
      </div>

      <button class="button is-primary">Submit</button>
    </form>
    <div class="whatever">
      <img class="confused" src="/public\confusedstickfigure.jpg" />
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import authService from "@/services/AuthService.js";

export default {
  name: 'Reset',
  data() {
    return {
      password: '',
      password_confirm: '',
      email: ''
    };
  },
  methods: {
    handleSubmit() {
      try {
        // Basic client-side validation
        if (!this.password || !this.password_confirm) {
          console.error('Please enter both password and confirmation.');
          return;
        }

        if (this.password !== this.password_confirm) {
          console.error('Password and confirmation do not match.');
          return;
        }

        // Make an API request to reset the password
        const response = authService.recoverPassword(this.email, this.answer);

        console.log(response.data);
        // Assuming your backend returns a success message

        // Redirect the user to the login route
        this.$router.push('/login');
      } catch (error) {
        console.error(error.response.data);
        // Handle errors appropriately
      }
    }
  }
};
</script>

<style scoped>
.box {
  margin-top: 20px;
  margin-left: 20px;
  width: 450px;
}
.whatever {
  display: flex;
  justify-content: center;
}
.confused {
  height: 590px;
}
</style>
