<template>
  <div>
    <form class="box" @submit.prevent="handleSubmit">
      <h3>Reset Password</h3>
      <br />
      <div class="field">
        <label>Password</label>
        <div class="control">
          <input
            type="password"
            v-model="password"
            placeholder="********"
            required
          />
        </div>
      </div>
      <div class="field">
        <label>Password Confirm</label>
        <div class="control">
          <input
            type="password"
            v-model="password_confirm"
            placeholder="********"
            required
          />
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
import axios from "axios";
import authService from "@/services/AuthService.js";

export default {
  name: "Reset",
  data() {
    return {
      password: "",
      password_confirm: "",
      email: this.$route.params.email,
    };
  },
  methods: {
    handleSubmit() {
      try {
        if (!this.password || !this.password_confirm) {
          console.error("Please enter both password and confirmation.");
          return;
        }
        if (this.password !== this.password_confirm) {
          console.error("Password and confirmation do not match.");
          return;
        }

        const updateUserData = {
          Email: this.email,
          Password: this.password,
          ConfirmPassword: this.password_confirm,
        };

        // Make an API request to reset the password
        authService.recoverPassword(updateUserData)
        .then(response => {console.log(response.data);})
        // Assuming your backend returns a success message
        // This takes the user to the login page, how cool?
        this.$router.push("/login");
      } catch (error) {
        console.error(error.response.data);
        this.validationError = "Invalid email or security answer.";
      } finally {
        this.loading = false;
      }
    },
  },
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
