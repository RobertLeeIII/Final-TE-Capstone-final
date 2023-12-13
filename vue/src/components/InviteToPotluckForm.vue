<template>
  <div class="card-container">
    <section class="invite-section card">
    <div class="hero">
      <div class="hero-body">
        <h1 class="title">Step 2: Invite Friends</h1>
      </div>
    </div>
    <div class="form-container">
      <form @submit.prevent="sendInvites" class="email-form">
        <div v-for="(email, index) in emailList" :key="index" class="email-field">
          <label>Email {{ index + 1 }}</label>
          <input
            type="email"
            v-on:keyup="autoAddField(index)"
            v-model="emailList[index]"
            autofocus
            class="email-input"
          />
        </div>
      </form>
      
    </div>
    <div class="button-container">
      <button type="button" @click="addEmailField" class="add-email-button">Add Email</button>
      <button type="submit" :disabled="formHasEmails" @click="sendInvites" class="send-invites-button">
        Send Invites
      </button>
      <button type="button"  @click="backToPotluck" class="invite-nobody-button">
        Back to Potluck
      </button>
      </div>
    </section>
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
      return this.emailList.length <= 1; // Adjust the condition as necessary
    },
  },
  methods: {
    backToPotluck(){
      this.$router.push({ name: 'potluck-details', params: { potluckId: this.$route.params.potluckId } });
    },
    addEmailField() {
      this.emailList.push(''); // Add a new email to the list
    },
    autoAddField(index) {
      const inputValue = event.target.value.trim();
      if (inputValue && index === this.emailList.length - 1) {
        this.emailList.push('');
      } else if (!inputValue && index < this.emailList.length - 1 && index > 0) {
        this.emailList.pop();
      } else {
        this.emailList[index] = inputValue;
      }
    },
    sendInvites() {
      this.emailList.pop();
      UserService.inviteGuestsByEmails(this.$route.params.potluckId, this.emailList)
        .then(response => {
          this.emailList = response.data;
        })
        .catch(error => {
          // Handle errors if necessary
        })
        .finally(() => {
          this.$router.push({ name: 'potluck-details', params: { potluckId: this.$route.params.potluckId } });

        });
    },
  },
};
</script>

<style scoped>
.card-container {
  display: flex-wrap;
  justify-content: center;
  align-items: center;
  height: 95vh;
  padding: 0 20px;
  background-image: url(/7637317.jpg);
  background-size: 1080px;
}

.invite-section {
  width: 100%;
  max-width: 500px;
  background-color: #ffffff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.1);
  margin: 20px;
  box-sizing: border-box;
  max-height: 900px; 
  overflow-y: auto; 
}

.hero {
  background-color: #f5f5f5;
  width: 100%;
  padding: 20px;
}

.title {
  font-size: 24px;
  margin: 0;
}

.form-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;

}

.email-input {
  padding: 8px;
  border-radius: 4px;
  border: 1px solid #ccc;
}

.email-list {
  width: 100%;
  text-align: center;
}

.no-emails-text {
  margin: 10px 0;
  color: #888;
}

.emails-text {
  margin: 5px 0;
}

.email-ul {
  list-style-type: none;
  padding: 0;
}

.email-li {
  margin-bottom: 5px;
}

.add-email-button,
.send-invites-button, .invite-nobody-button{
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.add-email-button:hover {
  background-color: #007bff;
  color: #fff;
}

.send-invites-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
  color: #666;
}
</style>
