<template>
  <div>
    <section class="layout">
      <div class="header">
        <div class="header-content">
          <div class="header-text">
            <h1 class="header-title">Pot·luck</h1>
            <h2 class="header-phonetic">    <form v-on:submit.prevent="register">
      <h1>Register User</h1>
      <div role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="form-input-group">
        <input class="input is-rounded" type="text" id="username" placeholder="Username" v-model="user.username" required
          autofocus />
      </div>
      <div class="form-input-group control has-icons-left has-icons-right">
        <input class="input is-rounded" type="email" placeholder="Email" v-model="user.email" required>
        <span class="icon is-left">
          <i class="fas fa-envelope"></i>
        </span>
      </div>
      <div class="form-input-group control has-icons-left">
        <input class="input is-rounded" type="password" id="password" placeholder="Password" v-model="user.password"
          required />
        <span class="icon is-left">
          <i class="fas fa-lock"></i>
        </span>
      </div>
      <div class="form-input-group control has-icons-left">
        <input class="input is-rounded" type="password" id="confirmPassword" placeholder="Confirm Password"
          v-model="user.confirmPassword" required />
        <span class="icon is-left">
          <i class="fas fa-lock"></i>
        </span>
      </div>
      <div class="form-input-group">
        <label for="dietRes">Dietary Restrictions?</label>
        <input class="checkbox" type="checkbox" id="dietRes" v-model="user.dietRes" />
      </div>
      <br>
      <button class="button is-warning" type="submit">Create Account</button>
    </form></h2>
    </div>
    </div>
    </div>
  </section>
  <div id="register" class="text-center">
  </div>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  data() {
    return {
      user: {
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        role: 'user',
        dietRes: false,
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style scoped>
.form-input-group {
  margin-bottom: 1rem;
}

label {
  margin-right: 0.5rem;
}
.layout {
  display: grid;
  grid-template-rows: auto 1fr auto; /* Adjust as needed */
  gap: 8px;
  background-color: rgb(124, 169, 130); /* Salmon-colored background */
  min-height: 95vh; /* Set minimum height of layout to full viewport height */

  padding-bottom: 20px; /* Increase bottom padding */
}

.centerFooter {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-top: 20px; /* Move the footer down */
  color: white; /* Change text color */
}

.header {
  text-align: left;
  margin-bottom: 20px;
  background-color: rgb(241, 247, 237); /* Set background color for the header */
}

.header-content {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  border: 4px solid rgb(124, 169, 130)   ;
  padding: 20px; /* Add padding as needed */
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

.main{
  color: white; /* Change text color */

}</style>

