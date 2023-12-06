<template>
  <div>
    <potluck class="potluck" v-for="potluck in potlucks" :Potluck="potluck" :key="potluck.potluckId"></potluck>
    <div class="card-container">
      <div v-for="potluck in potlucks" :key="potluck.hostId" class="card">
        <div class="card-image">
          <figure class="image is-4by3">
            <img src="/public/potluck.jpg" alt="Potluck Image">
          </figure>
        </div>
        <div class="card-content">
          <div class="media">
            <div class="media-left">
              <figure class="image is-48x48">
                <img src="/public/logo.png" alt="Logo">
              </figure>
            </div>
            <div class="media-content">
              <p class="title is-4">{{ potluck.name }}</p>
              <p class="subtitle is-6">@{{ this.$store.state.user.userId }}</p>
            </div>
          </div>
          <div class="content">
            <P>{{ potluck.summary }}</P>
            <a href="#">#{{ potluck.location }}</a> <a href="#"></a>
            <br>
            <time :datetime="potluck.time">{{ formatDate(potluck.time) }}</time>
          </div>
        </div>
      </div>
    </div>
    <p><br></p>
    <button class="button is-primary">
      <router-link :to="{ name: 'potluck-create' }"> HOST A POTLUCK </router-link>
    </button>
  </div>
</template>

<script>
import PotluckService from '@/services/PotluckService.js';

export default {
  data() {
    return {
      potlucks: [],
    };
  },
  methods: {
    getUserPotlucks(userId) {
      PotluckService.showPotlucks(userId)
        .then(response => {
          this.potlucks = response.data;
        })
        .catch(error => {
          if (error.response && error.response.status === 404) {
            this.handleErrorResponse(error);
          } else if (error.request) {
            console.log("OTHER PROBLEM");
          } else {
            console.log("ANOTHER PROBLEM");
          }
        });
    },
    formatDate(dateTimeString) {
      const options = { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
      const date = new Date(dateTimeString);
      return date.toLocaleDateString('en-US', options);
    },
    handleErrorResponse(error) {
      if (error.response) {
        if (error.response.status == 404) {
          console.log("404 PROBLEM");
        }
      }
    },
  },
  created() {
    this.getUserPotlucks(this.$route.params.userId);
  },
};
</script>

<style>
.card-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.card {
  width: 300px;
}
</style>
