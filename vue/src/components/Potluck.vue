<template>
  <div class="potluck">
    <h2>{{ Potluck.name }}</h2>
    <p>Date: {{ new Date(Potluck.time).toDateString() }}</p>
    <p>Location: {{ Potluck.location }}</p>
    <h3>Guests:</h3>
    <ul>
      <li v-for="guest in potlucks" :key="guest.id">
        {{ guest.email }}
      </li>
    </ul>
  </div>
</template>
  
<script>
import PotluckService from '@/services/PotluckService.js'
export default {
  data() {
    return {
      potlucks: [],
    }
  },
  props: {
    Potluck: Object,
  },
  methods: {
    retrieveGuests() {
      PotluckService.showPotlucks(this.$route.params.userId)
        .then(response => {
          this.guests = response.data;
        })
        .catch(error => {
          this.handleErrorResponse(error);
        }
        )

    },
    handleErrorResponse(error) {
      if (error.response) {
        if (error.response.status == 404) {
          console.log("404 PROBLEM")
        } else if (error.request) {
          console.log("OTHER PROBLEM")
        } else {
          console.log("ANOTHER PROBLEM")

        }
      }
    },
    created() {
      this.retrieveGuests()
    }
  }
}
</script>
  
<style scoped></style>
  