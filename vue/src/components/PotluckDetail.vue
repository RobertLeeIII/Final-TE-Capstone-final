<template>
<div class="potluck-detail">
    <h2>{{ Potluck.name }}</h2>
    <h2>{{ Potluck.location }}</h2>
    <h2>{{ Potluck.time }}</h2>
    <h2>{{ Potluck.theme }}</h2>
    <h2>{{ Potluck.summary }}</h2>
</div>
</template>

<script>
import PotluckService from '@/services/PotluckService.js'

export default {
    data() {
        return {
            potlucks: []
        }
    },
    props: {
        Potluck: Object
    },
    methods: {
        retrieveSpecificPotluck() {
            PotluckService.getPotluckById(this.$route.params.userId)
            .then(response => {
                this.potluck = response.data;
            })
            .catch(error => {
                this.handleErrorResponse(error);
            })
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
        this.retrieveSpecificPotluck()
    }
    }
}
</script>

<style scoped>
</style>