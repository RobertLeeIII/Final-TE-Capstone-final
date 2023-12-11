<template>
  <div class = 'main'>
<div class="box">
  <p>Step 1: Create a potluck</p>
</div>
    <form v-on:submit.prevent="saveNewPotluck" class="box">
      <label for="potluck-name">Name</label>
      <input class="input is-rounded" type="text" name="Name" id="potluck-name" v-model="newPotluck.name" />

      <label for="summary">Summary</label>
      <input class="input is-rounded" type="text" name="Summary" id="summary" v-model="newPotluck.summary" />

      <label for="location">Location</label>
      <input class="input is-rounded" type="text" name="location" id="location" v-model="newPotluck.location" />

      <label for="time">Date</label>
      <input class="input is-rounded" type="datetime-local" name="time" id="time" v-model="newPotluck.time" />

      <label for="apps">Appetizers</label>
      <input class="input is-rounded" type="number" name="apps" id="apps" v-model="newPotluck.courseRequest.apps">
      <label for="sides">Sides</label>
      <input class="input is-rounded" type="number" name="sides" id="sides" v-model="newPotluck.courseRequest.sides">
      <label for="mains">Mains</label>
      <input class="input is-rounded" type="number" name="mains" id="mains" v-model="newPotluck.courseRequest.mains">
      <label for="desserts">Desserts</label>
      <input class="input is-rounded" type="number" name="desserts" id="desserts"
        v-model="newPotluck.courseRequest.desserts">


      <!-- <label for="courses">Courses</label> 
        <div>
            <label for="courses">Courses</label>
            <div>
                <select id="course" class="input is-rounded" v-model="newPotluck.course">
                    <option>--Select a Course--</option>
                    <option>Appetizers</option>
                    <option>Sides</option>
                    <option>Mains</option>
                    <option>Desserts</option>
                </select>

                <div id="courseCounterContainer">
                    <input type="number" id="courseCounter" v-model="newPotluck.count" class="input is-rounded"
                        placeholder="0" v-on:click="showButton = true"
                        v-show="newPotluck.course !== '--Select a Course--'" />
                </div>
                <button type="submit" v-if="moreThanZero">+</button>
            </div>
        </div>
 -->
      <label for="potluck-theme">Theme</label>
      <select class="input is-rounded" id="potluck-theme" v-model="newPotluck.theme">
        <option>None</option>
        <option>🍀Spring</option>
        <option>🌞Summer</option>
        <option>🎃Fall</option>
        <option>❄️Winter</option>
      </select>
      <button class='button is-warning' type="submit">Create Your Potluck!</button>
    </form>

  </div>
</template>

<script>
import PotluckService from "@/services/PotluckService.js";

export default {
  data() {
    return {
      newPotluck: {
        hostId: this.$store.state.user.userId,
        name: '',
        summary: '',
        location: '',
        time: '',
        courseRequest: {
          apps: 0,
          sides: 0,
          mains: 0,
          desserts: 0,
        },
        theme: 'None',
        status: 'active'
      },
      showButton: false,
    }
  },
  props: {
    updatePotluck: Object,
  },
  methods: {
    validateNewPotluck() {
      let message = '';
      if (this.newPotluck.name.length === 0) {
        message += 'The new potluck needs a name.';
      }
      if (this.newPotluck.summary.length === 0) {
        message += 'The new potluck needs a summary.';
      }
      if (this.newPotluck.location.length === 0) {
        message += 'The new potluck needs a location';
      }
      if (this.newPotluck.date.length === 0) {
        message += 'The new potluck needs a date.';
      }
      if (this.newPotluck.courseRequest.apps === 0 &&
        this.newPotluck.courseRequest.sides === 0 &&
        this.newPotluck.courseRequest.mains === 0 &&
        this.newPotluck.courseRequest.desserts === 0) {
        message += 'The new potluck needs at least one course.';
      }
      return true;
    },
    saveNewPotluck() {
      PotluckService
        .addPotluck(this.$store.state.user.userId, this.newPotluck)
        .then(response => {
          this.resetPotluckForm;
          // If the controller does not handle the Post-Redirect-Get, uncomment this code below
          this.$router.push({ name: 'guest-list', params: { potluckId: response.data.potluckId }, query: {action: 'invite'} });
        })
        .catch(error => {
          if (error.response && error.response.status === 404) {
            this.handleErrorResponse(error)
          } else if (error.request) {
            console.log(error.response.data);
            console.log("OTHER PROBLEM");
          } else {
            console.log("ANOTHER PROBLEM");
          }
        })
    },

    resetPotluckForm() {
      this.newPotluck = {
        name: "",
        summary: "",
        location: "",
        time: "",
        course: {
          apps: 0,
          sides: 0,
          mains: 0,
          desserts: 0,
        },
        theme: "None",
      };
    },
    handleErrorResponse(error) {
      if (error.response) {
        if (error.response.status == 404) {
          console.log("404 PROBLEM");
        }
      }
    },
    // assignUpdatePotluck() {
    //   this.newPotluck = JSON.parse(JSON.stringify(this.updatePotluck));
    // },
    retrieveSpecificPotluck() {
      PotluckService.getPotluck(this.$route.params.potluckId)
        .then(response => {
          this.newPotluck = response.data;
        })
        .catch(error => {
          this.handleErrorResponse(error);
        })
    },
  },
  created() {
    // if (this.updatePotluck) {
    //   this.assignUpdatePotluck;
    // }
    this.retrieveSpecificPotluck;
  }
};
</script>

<style scoped>
.media{
    display: flex;
    justify-content: center;
}
.box {
  margin-left: 20px;
  width: 450px;
  
}
.main{
  background-image: url(/7637317.jpg);
  background-color:  rgb(255, 193, 146);
  min-height: 95vh; /* Set minimum height of layout to full viewport height */

}
</style>
