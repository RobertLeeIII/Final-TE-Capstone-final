import axios from 'axios';

<template>
  <section class="hero">
    <div class="hero-body">
      <p class="title">
        Invite Friends
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
    async sendInvites() {
      try {
        const response = await this.$axios.post('/api/email/send', {
          emails: this.emailList,
          subject: 'You are Invited! ', // OR WHATEVER WE DECIDE
          body: 'This is your invitation text.  I do not know what we are putting here.  I assume we need to pull in the potluck details, as well as provide a link that takes them to the account creation page.  Can we make it so that the link takes them to the page with the email filled out already?  Can we make it so that once the create and login to the account, they already have the potluck in their list?  ', // OR WHATEVER
        });

        console.log('Response:', response.data);
      } catch (error) {
        console.error('Error sending invites:', error);
      }
    },
  },
};
</script>

