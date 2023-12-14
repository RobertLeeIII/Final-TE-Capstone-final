<template>
    <div class="top">
      <div v-for="guest in guests" :key="guest.userId">
        <div v-if="!checkIsHost(guest.userId)" class="checkbox-container">
          <input
            v-model="selectedGuests"
            :id="guest.userId"
            :value="guest.userId"
            type="checkbox"
            class="styled-checkbox"
          />
          <label :for="guest.userId" class="checkbox-label">
            {{ guest.username.substring(0, 1).toUpperCase() + guest.username.substring(1) }}
          </label>
        </div>
      </div>
      <button class="button is-danger" @click.prevent="removeGuest">Delete Selected Users</button>
      <router-link
        v-if="this.$store.state.user.userId == this.$route.params.userId"
        style="color: blue;"
        :to="{ name: 'guest-list' }"
      >
        Invite more people!
      </router-link>
    </div>
  </template>
  
  <script>
  import UserService from '../services/UserService.js';
  
  export default {
    data() {
      return {
        selectedGuests: [],
      };
    },
    props: {
      guests: Array,
      potluck: Object,
    },
    methods: {
      checkIsHost(userId) {
        return userId == this.potluck.hostId;
      },
      removeGuest() {
        UserService.uninviteGuests(this.$route.params.potluckId, this.selectedGuests)
          .then(response => {
            this.selectedGuests = [];
          })
          .catch(error => {
            this.handleErrorResponse(error);
          })
          .finally(
            this.$router.push({ name: 'potluck-details', params: { potluckId: this.$route.params.potluckId } })
          );
      },
      handleErrorResponse(error) {
        if (error.response) {
          if (error.response.status == 404) {
            console.log('404 PROBLEM');
          } else if (error.request) {
            console.log('OTHER PROBLEM');
          } else {
            console.log('ANOTHER PROBLEM');
          }
        }
      },
    },
  };
  </script>
  
  <style>
  .checkbox-container {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
    padding: 10px;
  }
  
  .styled-checkbox {
    appearance: none;
    width: 20px;
    height: 20px;
    border: 2px solid #3498db;
    border-radius: 4px;
    cursor: pointer;
    margin-right: 10px;
  }
  
  .styled-checkbox:checked {
    background-color: #3498db;
    border: 2px solid #3498db;
  }
  
  .checkbox-label {
    font-size: 20px;
    font: bold;
    cursor: pointer;
  }
  .top{
    padding-top: 10px;
  }
  </style>
  