<template>
    <div class="main">
        <div class="box">
            <p>Update a potluck</p>
        </div>
        <form v-on:submit.prevent="saveUpdatedPotluck" class="box">
            <label for="potluck-name">Name</label>
            <input class="input is-rounded" type="text" name="Name" id="potluck-name" v-model="updatedPotluck.name" />

            <label for="summary">Summary</label>
            <input class="input is-rounded" type="text" name="Summary" id="summary" v-model="updatedPotluck.summary" />

            <label for="location">Location</label>
            <input class="input is-rounded" type="text" name="location" id="location" v-model="updatedPotluck.location" />

            <label for="time">Date</label>
            <input class="input is-rounded" type="datetime-local" name="time" id="time" v-model="updatedPotluck.time" />

            <label for="apps">Appetizers</label>
            <input class="input is-rounded" type="number" name="apps" id="apps" v-model="updatedPotluck.courseRequest.apps">
            <label for="sides">Sides</label>
            <input class="input is-rounded" type="number" name="sides" id="sides"
                v-model="updatedPotluck.courseRequest.sides">
            <label for="mains">Mains</label>
            <input class="input is-rounded" type="number" name="mains" id="mains"
                v-model="updatedPotluck.courseRequest.mains">
            <label for="desserts">Desserts</label>
            <input class="input is-rounded" type="number" name="desserts" id="desserts"
                v-model="updatedPotluck.courseRequest.desserts">


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
            <select class="input is-rounded" id="potluck-theme" v-model="updatedPotluck.theme">
                <option>None</option>
                <option>🍀Spring</option>
                <option>🌞Summer</option>
                <option>🎃Fall</option>
                <option>❄️Winter</option>
            </select>
            <button type="submit">Update This Potluck</button>
        </form>
    </div>
</template>
  
<script>
import PotluckService from "../services/PotluckService.js";

export default {
    data() {
        return {
            updatedPotluck: {
                name: '',
                summary: '',
                location: '',
                time: '',
                isRecurring: false,
                repeatInterval: 0,
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
    computed: {
        updating() {
            return this.$route.params.action == 'update';
        }
    },
    props: {
        updatePotluck: Object,
    },
    methods: {
        validateNewPotluck() {
            let message = '';
            if (this.updatedPotluck.name.length === 0) {
                message += 'The new potluck needs a name.';
            }
            if (this.updatedPotluck.summary.length === 0) {
                message += 'The new potluck needs a summary.';
            }
            if (this.updatedPotluck.location.length === 0) {
                message += 'The new potluck needs a location';
            }
            if (this.updatedPotluck.date.length === 0) {
                message += 'The new potluck needs a date.';
            }
            if (this.updatedPotluck.courseRequest.apps === 0 &&
                this.updatedPotluck.courseRequest.sides === 0 &&
                this.updatedPotluck.courseRequest.mains === 0 &&
                this.updatedPotluck.courseRequest.desserts === 0) {
                message += 'The new potluck needs at least one course.';
            }
            return true;
        },
        saveUpdatedPotluck() {
            // console.log(this.updatedPotluck.theme.substring(2));
            //this.updatedPotluck.theme = this.updatedPotluck.theme.substring(2);
            console.log(this.$store.state.user.userId, parseInt(this.$route.params.potluckId))
            PotluckService.updatePotluck(this.$store.state.user.userId, parseInt(this.$route.params.potluckId), this.updatedPotluck)
                .then(response => {
                    this.$router.push({ name: 'potluck-details', params: { potluckId: response.data.potluckId } });
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
        retrieveSpecificPotluck(potluckId) {
            PotluckService.getPotluck(potluckId)
                .then(response => {
                    this.updatedPotluck = response.data;
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
        this.retrieveSpecificPotluck(this.$route.params.potluckId);
    }
};
</script>
  
<style scoped>
.media {
    display: flex;
    justify-content: center;
}

.box {
    margin-left: 20px;
    width: 450px;

}

.main {
    background-image: url(/7637317.jpg);
    background-color: rgb(255, 193, 146);
    min-height: 95vh;
    /* Set minimum height of layout to full viewport height */

}</style>
  