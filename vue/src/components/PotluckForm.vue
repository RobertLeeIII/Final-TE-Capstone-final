<template>
  <div class="main">
    <div class="container">
      <div class="box">
        <p>Step 1: Create a potluck</p>
      </div>
      <form @submit.prevent="saveNewPotluck" class="box">
        <div>
          <label for="potluck-name">Name</label>
          <input
            class="input is-rounded"
            type="text"
            name="Name"
            id="potluck-name"
            v-model="newPotluck.name"
          />
          <div v-if="validationErrors.name" class="error-message">
            {{ validationErrors.name }}
          </div>
        </div>

        <div>
          <label for="summary">Summary</label>
          <input
            class="input is-rounded"
            type="text"
            name="Summary"
            id="summary"
            v-model="newPotluck.summary"
          />
          <div v-if="validationErrors.summary" class="error-message">
            {{ validationErrors.summary }}
          </div>
        </div>

        <div>
          <label for="location">Location</label>
          <input
            class="input is-rounded"
            type="text"
            name="location"
            id="location"
            v-model="newPotluck.location"
          />
          <div v-if="validationErrors.location" class="error-message">
            {{ validationErrors.location }}
          </div>
        </div>

        <div>
          <label for="time">Date</label>
          <input
            class="input is-rounded"
            type="datetime-local"
            name="time"
            id="time"
            v-model="newPotluck.time"
          />
          <div v-if="validationErrors.time" class="error-message">
            {{ validationErrors.time }}
          </div>
          <div v-if="validationErrors.past" class="error-message">
            {{ validationErrors.past }}
          </div>
        </div>

        <div>
          <label for="apps">Appetizers</label>
          <input
            class="input is-rounded"
            type="number"
            name="apps"
            id="apps"
            v-model="newPotluck.courseRequest.apps"
          />

        </div>
        <div>
          <label for="sides">Sides</label>
          <input
            class="input is-rounded"
            type="number"
            name="sides"
            id="sides"
            v-model="newPotluck.courseRequest.sides"
          />

        </div>
        <div>
          <label for="apps">Mains</label>
          <input
            class="input is-rounded"
            type="number"
            name="mains"
            id="mains"
            v-model="newPotluck.courseRequest.mains"
          />

        </div>
        <div>
          <label for="desserts">Desserts</label>
          <input
            class="input is-rounded"
            type="number"
            name="desserts"
            id="desserts"
            v-model="newPotluck.courseRequest.Desserts"
          />
          <div v-if="validationErrors.courses" class="error-message">
            {{ validationErrors.courses }}
          </div>
        </div>



        <div>
          <label for="potluck-theme">Theme</label>
          <select
            class="input is-rounded"
            id="potluck-theme"
            v-model="newPotluck.theme"
          >
            <option>None</option>
            <option>🍀Spring</option>
            <option>🌞Summer</option>
            <option>🎃Fall</option>
            <option>❄️Winter</option>
          </select>
          <div v-if="validationErrors.theme" class="error-message">
            {{ validationErrors.theme }}
          </div>
        </div>



        <button class="button is-info" type="submit">
          Create Your Potluck!
        </button>
      </form>
    </div>
  </div>
</template>

<script>
import PotluckService from "../services/PotluckService.js";

export default {
  data() {
    return {
      newPotluck: {
        hostId: this.$store.state.user.userId,
        name: "",
        summary: "",
        location: "",
        time: "",
        courseRequest: {
          apps: 0,
          sides: 0,
          mains: 0,
          desserts: 0,
        },
        theme: "None",
        status: "active",
      },
      validationErrors: {},
      validationError: null,
    };
  },
  methods: {
    validateNewPotluck() {
      this.validationErrors = {};
      let hasErrors = false;
      let today = new Date();
      let selectedDate = new Date(this.newPotluck.time);

      if (this.newPotluck.name.length === 0) {
        this.validationErrors.name = "The new potluck needs a name.";
        hasErrors = true;
      }
      if (this.newPotluck.summary.length === 0) {
        this.validationErrors.summary = "The new potluck needs a summary.";
        hasErrors = true;
      }
      if (this.newPotluck.location.length === 0) {
        this.validationErrors.location = "The new potluck needs a location";
        hasErrors = true;
      }
      if (this.newPotluck.time.length === 0) {
        this.validationErrors.time = "The new potluck needs a date.";
        hasErrors = true;
      }
      if (selectedDate < today){
        this.validationErrors.past = "The new potluck must be after today.";
        hasErrors = true;
      }
      if (
        this.newPotluck.courseRequest.apps === 0 &&
        this.newPotluck.courseRequest.sides === 0 &&
        this.newPotluck.courseRequest.mains === 0 &&
        this.newPotluck.courseRequest.desserts === 0
      ) {
        this.validationErrors.courses =
          "The new potluck needs at least one course.";
        hasErrors = true;
      }

      this.validationError = hasErrors ? "Please fix the errors." : null;

      return !hasErrors;
    },
    saveNewPotluck() {
      if (!this.validateNewPotluck()) {
        console.error("Validation failed:", this.validationErrors);
        return;
      }

      PotluckService.addPotluck(this.$store.state.user.userId, this.newPotluck)
        .then((response) => {
          this.resetPotluckForm();
          this.$router.push({
            name: "guest-list",
            params: { potluckId: response.data.potluckId },
            query: { action: "invite" },
          });
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            this.handleErrorResponse(error);
          } else if (error.request) {
            console.log(error.response.data);
            console.log("OTHER PROBLEM");
          } else {
            console.log("ANOTHER PROBLEM");
          }
        });
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
    retrieveSpecificPotluck() {
      PotluckService.getPotluck(this.$route.params.potluckId)
        .then((response) => {
          this.newPotluck = response.data;
        })
        .catch((error) => {
          this.handleErrorResponse(error);
        });
    },
  },
  created() {
    this.retrieveSpecificPotluck();
  },
};
</script>

<style scoped>
.main {
  background-image: url(/7637317.jpg);
  background-color: rgb(255, 193, 146);
  min-height: 95vh;
}

.container {
  padding-top: 20px;
}

.box {
  margin-left: 20px;
  width: 450px;
}

.error-message {
  color: red;
  margin-top: 5px;
  font-size: 12px;
}
</style>
