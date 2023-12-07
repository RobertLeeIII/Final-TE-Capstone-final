<template>
  <div class="header">
    <div class="header-content">
      <div class="header-text">
        <h1 class="header-title">Pot·luck</h1>
        <h2 class="header-phonetic">Login</h2>
        <div id="login">
          <form v-on:submit.prevent="login">
            <div role="alert" v-if="invalidCredentials">
              Invalid username and password!
            </div>
            <div role="alert" v-if="this.$route.query.registration">
              Thank you for registering, please sign in.
            </div>
            <div class="form-input-group control has-icons-left">
              <input class="input is-rounded " type="text" id="username" placeholder="Username" v-model="user.username"
                required autofocus />
              <span class="icon is-left">
                <i class="fas fa-envelope"></i>
              </span>
            </div>
            <div class="form-input-group control has-icons-left">
              <input class="input is-rounded  " type="password" id="password" placeholder="Password"
                v-model="user.password" required />
              <span class="icon is-left">
                <i class="fas fa-lock"></i>
              </span>
            </div>

            <button class="button is-warning" type="submit">Sign in</button>
            <p>
              <br>
              <router-link v-bind:to="{ name: 'register' }" class="button is-warning">Need an account? Sign up.</router-link>
            </p>
          </form>
        </div>
      </div>
    </div>

      <button class="button is-primary" type="submit">Sign in</button>
      <p>
        <br>
    <router-link v-bind:to="{ name: 'register' }" class="button is-link" >Need an account? Sign up.</router-link>
  </p>
  <p class="forgot-password text-right">
    <router-link to="forgot">Forgot Password?</router-link>
  </p>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  components: {},
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
            this.$router.push(`/${response.data.user.userId}`);
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
.form-input-group {
  margin-bottom: 1rem;
}

.form-input-group {
  margin-bottom: 1rem;
}

label {
  margin-right: 0.5rem;
}

.layout {
  display: grid;
  grid-template-rows: auto 1fr auto;
  /* Adjust as needed */
  gap: 8px;
  background-color: rgb(124, 169, 130);
  /* Salmon-colored background */
  min-height: 95vh;
  /* Set minimum height of layout to full viewport height */

  padding-bottom: 20px;
  /* Increase bottom padding */
}

.centerFooter {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-top: 20px;
  /* Move the footer down */
  color: white;
  /* Change text color */
}

.header {
  text-align: left;
  margin-bottom: 20px;
  background-color: rgb(241, 247, 237);
  /* Set background color for the header */
}

.header-content {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  border: 4px solid rgb(124, 169, 130);
  padding: 20px;
  /* Add padding as needed */
}

.header-text {
  text-align: left;

  margin-right: 50px;
  margin-left: 50px;
}

.header-title {
  font-size: 36px;
  margin-bottom: 5px;
}

.header-phonetic {
  font-size: 18px;
  margin-left: 50px;

  color: #888;
}

.header-definition {
  font-size: 24px;
  margin-top: 10px;
  margin-bottom: 5px;
  margin-left: 50px;

}

.header-description {
  font-size: 16px;

  margin-left: 50px;
  line-height: 1.5;
}

.header-image {
  flex-shrink: 0;
}

.header-img {
  width: 380px;
  height: auto;
  border-radius: 4%;
}

.main {
  color: white;
  /* Change text color */

}</style>
