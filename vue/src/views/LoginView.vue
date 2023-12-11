<template>
  <div class="header">
    <div class="header-content">
      <div class="header-text">
        <h1 class="header-title">Pot·luck</h1>
        <h2 class="header-phonetic">Login</h2>
        <form class="login-form" @submit.prevent="login">
          <div v-if="invalidCredentials" class="error-message">
            Invalid username and password!
          </div>
          <div v-if="$route.query.registration" class="success-message">
            Thank you for registering, please sign in.
          </div>
          <div class="form-input-group control has-icons-left">
            <input class="input is-rounded" type="text" id="username" placeholder="Username" v-model="user.username" required autofocus />
            <span class="icon is-left">
              <i class="fas fa-envelope"></i>
            </span>
          </div>
          <div class="form-input-group control has-icons-left">
            <input class="input is-rounded" type="password" id="password" placeholder="Password" v-model="user.password" required />
            <span class="icon is-left">
              <i class="fas fa-lock"></i>
            </span>
          </div>
          <button class="button is-warning" type="submit">Sign in</button>
          <p class="forgot-password">
            <router-link to="forgot">Forgot Password?</router-link>
          </p>
          <p class="create-account">
            <router-link to="register">Need an account? Sign up.</router-link>
          </p>
        </form>
      </div>
    </div>
    <p class="centerFooter">
      <strong>PotluckPlanner:</strong> Written by TE capstone team 2
    </p>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push(`/users/${response.data.user.userId}`);
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style scoped>
.header {
  text-align: left;
  padding: 20px;
  background-color: rgb(241, 247, 237);
  border: 4px solid rgb(124, 169, 130);
  margin-bottom: 20px;
}

.header-title {
  font-size: 36px;
  margin-bottom: 10px;
  color: #333;
}

.header-phonetic {
  font-size: 18px;
  margin-bottom: 20px;
  color: #666;
}
.login-form {
  display: flex;
  flex-direction: column;
}

.form-input-group {
  margin-bottom: 0.5rem; /* Adjusted margin */
  display: flex;
  align-items: center;
}

.input-group {
  display: flex;
  align-items: center;
}

.input {
  width: 250px; /* Adjusted width */
} 
.button {
  width: 120px; /* Adjusted width */
  margin-top: 0.5rem; /* Adjusted margin */
  align-self: flex-start; /* Align to the start (left) */
}

.forgot-password,
.create-account {
  margin-top: 10px;
  font-size: 14px;
}

.error-message {
  color: red;
  margin-bottom: 10px;
}

.success-message {
  color: green;
  margin-bottom: 10px;
}
</style>
