<template>
    <invite-to-potluck-form v-if="this.$route.query.action == 'invite'"></invite-to-potluck-form>
    <uninvite-from-potluck
      v-else-if="this.$route.query.action == 'uninvite'"
      :guests="potluckGuests"
      :potluck="currentPotluck"
    ></uninvite-from-potluck>
  
    <div v-else>
      <div>
        <div v-for="guest in potluckGuests" :key="guest.username">
          <div class="guest-card" :class="{ isHost: guest.userId === currentPotluck.hostId }">
            {{ guest.username.substring(0, 1).toUpperCase() + guest.username.substring(1) }}
          </div>
        </div>
  
        <router-link
          v-if="isHost"
          :to="{
            name: 'guest-list',
            params: { potluckId: this.$route.params.potluckId },
            query: { action: 'uninvite' },
          }"
        >
          <button class="button is-danger">Uninvite People</button>
        </router-link>
      </div>
  
      <div>
        <p></p>
        <button class="button is-info" type="button" @click="backToPotluck">
          Back to Potluck
        </button>
      </div>
    </div>
  </template>
  
  <script>
  import UserService from '../services/UserService.js';
  import PotluckService from '@/services/PotluckService';
  import UninviteFromPotluck from '@/components/UninviteFromPotluck.vue';
  import InviteToPotluckForm from '../components/InviteToPotluckForm.vue';
  
  export default {
    data() {
      return {
        potluckGuests: [],
        currentPotluck: {},
      };
    },
    components: {
      UninviteFromPotluck,
      InviteToPotluckForm,
    },
    props: {},
    computed: {
      isHost() {
        return this.currentPotluck.hostId == this.$store.state.user.userId;
      },
    },
    methods: {
      backToPotluck() {
        this.$router.push({
          name: 'potluck-details',
          params: { potluckId: this.$route.params.potluckId },
        });
      },
      getPotluckGuests() {
        UserService.getGuestsByPotluckId(this.$route.params.potluckId)
          .then(response => {
            this.potluckGuests = response.data;
          })
          .catch(error => {
            if (error.response && error.response.status === 404) {
              this.handleErrorResponse(error);
            } else if (error.request) {
              console.log('OTHER PROBLEM');
            } else {
              console.log('ANOTHER PROBLEM');
            }
          });
      },
      getPotluckById() {
        PotluckService.getPotluck(this.$route.params.potluckId)
          .then(response => {
            this.currentPotluck = response.data;
          })
          .catch(error => {
            if (error.response && error.response.status === 404) {
              this.handleErrorResponse(error);
            } else if (error.request) {
              console.log('OTHER PROBLEM');
            } else {
              console.log('ANOTHER PROBLEM');
            }
          });
      },
      handleErrorResponse(error) {
        if (error.response) {
          if (error.response.status == 404) {
            console.log('404 PROBLEM');
          }
        }
      },
    },
    created() {
      this.getPotluckGuests();
      this.getPotluckById();
    },
  };
  </script>
  
  <style>
  .guest-card {
    padding: 10px;
    margin: 5px;
    border: 1px solid #ddd;
    border-radius: 5px;
    background-color: #fff;
    font-size: 20px;
  }
  
  .isHost {
    color: red;
  }
  </style>
  