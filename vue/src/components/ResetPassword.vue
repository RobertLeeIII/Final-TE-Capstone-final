<template>
  <form class='box' @submit.prevent="handleSubmit">
  <h3>Reset Password</h3>
<br>
<div class="field">
  <label>Password</label>
  <div class="control">
  <input type="password" v-model="password" placeholder="********" />
  </div>
</div>
<div class="field">
  <label>Password Confirm</label>
  <div class="control">
  <input type="password" v-model="password_confirm" placeholder="********"/>
</div>
</div>

<button class="button is-primary">Submit</button>
</form>
<div class="whatever"><img class='confused' src="/public\confusedstickfigure.jpg"></div>

</template>

<script>
import axios from 'axios';
export default{
  name: 'Reset',
  data(){
    return{
      password: '',
      password_confirm: ''

    }
  },
  methods:{
    async handleSubmit(){
      const response = await axios.post('reset',{
        password: this.password,
        password_confirm: this.password_confirm,
        token: this.$route.params.token
      });
      this.$router.push('/Login');
    }
  }
}
</script>
<style scoped>
.box{
  margin-top: 20px;
  margin-left: 20px;
  width: 450px;
}
.whatever{
  display: flex;
  justify-content: center;
}
.confused{
  height: 590px; 
}


</style>