import axios from 'axios';

<template>
  <section class="hero">
    <div class="hero-body">
      <p class="title">
        Step 2: Invite Friends
      </p>
      <p class="subtitle"></p>
    </div>
  </section>
  <div id="form-container">
    <form @submit.prevent="sendInvites">
      <div v-for="(email, index) in emailList" :key="index">
        <label>Email {{ index + 1 }}</label>
        <input type="email" v-on:keyup="autoAddField(index)" v-model="emailList[index]" autofocus> >
      </div>
    </form>
    <h2 v-for="email in emailList" :key="email">{{ email }}</h2>
  </div>
  <div>
    <button type="button" @click="addEmailField">Add an Email</button>
    <button type="submit" :disabled="formHasEmails" @click="sendInvites">Send Invites</button>
  </div>
</template>
  
<script>
import UserService from '../services/UserService';
export default {

  data() {
    return {
      emailList: [''],
    };
  },
  computed: {
    formHasEmails() {
      if(this.emailList[0]){
        return false;
      }
      return true
    }
  },
  methods: {
    addEmailField() {
      this.emailList.push(''); // Add a new email to the list
    },
    autoAddField(index) {
      if (event.target.value.trim()) {
        if (index == this.emailList.length - 1) {
          this.emailList.push('');
        }
        else {
          if (index <= this.emailList.length) {
            this.emailList[index] = event.target.value.trim();
          }
        }
      }
      else {
        if (index < this.emailList.length && index > 0 && index !== this.emailList.length-1) {
          this.emailList.pop();
        }
      }
    },
    sendInvites() {   
      this.emailList.pop() 
      UserService.inviteGuestsByEmails(this.$route.params.potluckId, this.emailList)
      .then(response => {
        this.emailList = response.data;
        this.$router.push({name: 'potluck-details', params: {potluckId: this.$route.params.potluckId}})
      })
      .catch(error => {

      })
    },
  },
};
</script>

