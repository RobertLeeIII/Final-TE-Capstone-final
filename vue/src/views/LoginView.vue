<template>
  <section class="hero is-info">
    <div class="hero-body">
      <p class="title">
        Potluck planner
      </p>
      <p class="subtitle">
        Sign in
      </p>
    </div>
  </section>
  <h1>Sign In</h1>

  <div id="login">
    <form v-on:submit.prevent="login">
      <div role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-input-group control has-icons-left">
        <input class="input is-rounded " type="text" id="username" placeholder="Username" v-model="user.username" required
          autofocus />
        <span class="icon is-left">
          <i class="fas fa-envelope"></i>
        </span>
      </div>
      <div class="form-input-group control has-icons-left">
        <input class="input is-rounded  " type="password" id="password" placeholder="Password"
          v-model="user.confirmPassword" required />
        <span class="icon is-left">
          <i class="fas fa-lock"></i>
        </span>
      </div>

      <button class="button is-primary" type="submit">Sign in</button>
      <p>
        <router-link v-bind:to="{ name: 'register' }">Need an account? Sign up.</router-link>
      </p>
    </form>
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
            this.$router.push("/");
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

label {
  margin-right: 0.5rem;
}
</style>