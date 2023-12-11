<template>
  <div>
    <form class="box" @submit.prevent="handleSubmit">
      <h3>Reset Password</h3>
      <br />
      <div class="field">
        <label>Password</label>
        <div class="control">
          <input type="password" v-model="password" placeholder="********" />
        </div>
      </div>
      <div class="field">
        <label>Password Confirm</label>
        <div class="control">
          <input type="password" v-model="password_confirm" placeholder="********" />
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

export default {
  name: 'Reset',
  data() {
    return {
      password: '',
      password_confirm: ''
    };
  },
  methods: {
    async handleSubmit() {
      try {
        // Make an API request to reset the password
        const response = await axios.post('/api/reset', {
          password: this.password,
          password_confirm: this.password_confirm,
          token: this.$route.params.token
        });

        console.log(response.data);
        // Assuming your backend returns a success message

        // Redirect the user to the login route
        this.$router.push('/Login');
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