<template>
  <div :class="changingTheme" v-if="Potluck.courseRequest">
    <div class="container">
      <section class="requested-items">
        <div :class="changingTheme" v-if="Potluck.courseRequest">
          <div class="container">
            <section class="requested-items">
              <dish-suggestion v-if="signUpForm" :course="currentCourse"></dish-suggestion>
              <div class="requested-items-details" v-if="!signUpForm">
                <h3 style="padding: 10px;">The host has requested:</h3>

                <!-- <router-link
                  :to="{ name: 'dish-signup', params: { potluckId: this.$route.params.potluckId }, query: { action: 'signup', course: 1 } }"> -->
                <div class="requested-item"  @click="dishSignup(1)" id="1">
                  <span>{{ Potluck.courseRequest.apps }} Appetizers</span>
                </div>
                <!-- </router-link> -->
                <!-- <router-link
                  :to="{ name: 'dish-signup', params: { potluckId: this.$route.params.potluckId }, query: { action: 'signup', course: 2 } }"> -->
                <div class="requested-item" @click="dishSignup(2)" id="2">
                  <span class="host-request">{{ Potluck.courseRequest.sides }} Sides</span>
                </div>
                <!-- </router-link> -->
                <!-- <router-link
                  :to="{ name: 'dish-signup', params: { potluckId: this.$route.params.potluckId }, query: { action: 'signup', course: 3 } }"> -->
                <div class="requested-item" @click="dishSignup(3)" id="3">
                  <span>{{ Potluck.courseRequest.mains }} Main Dishes</span>
                </div>
                <!-- </router-link> -->
                <!-- <router-link
                  :to="{ name: 'dish-signup', params: { potluckId: this.$route.params.potluckId }, query: { action: 'signup', course: 4 } }"> -->
                <div class="requested-item" @click="dishSignup(4)" id="4">
                  <span>{{ Potluck.courseRequest.desserts }} Desserts</span>
                </div>
                <!-- </router-link> -->
              </div>
            </section>
          <div>
            <img>
          </div>
            <ul class="list" v-if="!signUpForm">
              <li><i :class="changingIcon"></i> Potluck Name: {{ Potluck.name }}</li>
              <li><i :class="changingIcon"></i> Location:  {{ Potluck.location }}</li>
              <li><i :class="changingIcon"></i> {{ formatDate(Potluck.time) }}</li>
              <li><i :class="changingIcon"></i> Theme: {{ Potluck.theme == 'None' ? Potluck.theme : Potluck.theme.substring(2) }}</li>
              <li><i :class="changingIcon"></i> About: {{ Potluck.summary }}</li>
            </ul>

            <div class="links" v-if="!signUpForm">
              <ul>
                <li><router-link v-if="isHost"
                    :to="{ name: 'guest-list', params: { potluckId: Potluck.potluckId }, query: { action: 'invite' } }"
                    class="invitation-link">Invite People</router-link> </li>
                <li><router-link v-if="isHost" :to="{ name: 'potluck-update', params: { potluckId: Potluck.potluckId } }"
                    class="update-link">Update This Potluck</router-link> </li>
                <li><router-link :to="{ name: 'guest-list', params: { potluckId: Potluck.potluckId } }"
                    class="whos-coming-link">Who's coming?</router-link> </li>
                <li> <router-link :to="{ name: 'potluck-list', params: { userId: this.$store.state.user.userId } }"
                    class="my-potlucks-link">Back to My Potlucks</router-link></li>
              </ul>
            </div>
          </div>
        </div>
        </section>
    </div>
  </div>
</template>
<script>
import DishSuggestion from '@/components/DishSuggestion.vue'
import UserService from '@/services/UserService.js'
import DishService from '@/services/DishService.js'
export default {
  data() {
    return {
      invitedGuests: [],
      dishes: [],
      currentCourse: 0,
      signUpForm: false,
      currentPotluck: {}


    }
  },
  components: {
    DishSuggestion
  },
  computed: {
    isHost() {
      return this.Potluck.hostId == this.$store.state.user.userId;
    },
    changingTheme() {
      if (this.Potluck.theme.includes('Spring')) {
        return { spring: true }
      }
      else if (this.Potluck.theme.includes('Summer')) {
        return { summer: true }
      }
      else if (this.Potluck.theme.includes('Fall')) {
        return { fall: true }
      }
      else if (this.Potluck.theme.includes('Winter')) {
        return { winter: true }
      } 
      else if (this.Potluck.theme.includes('None')) {
        return { noneTheme: true }
      }
      return true;
    },
    changingIcon() {
      if (this.Potluck.theme.includes('Spring')) {
        return "fa-solid fa-cloud-sun-rain"
      }
      else if (this.Potluck.theme.includes('Summer')) {
        return "fa-solid fa-sun"
      }
      else if (this.Potluck.theme.includes('Fall')) {
        return "fa-solid fa-leaf"
      }
      else if (this.Potluck.theme.includes('Winter')) {
        return "fa-regular fa-snowflake"
      }
      return true;
    }
  },
  props: {
    Potluck: Object
  },
  methods: {
    formatDate(dateTimeString) {
      const options = { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
      const date = new Date(dateTimeString);
      return date.toLocaleDateString('en-US', options);
    },
    dishSignup(ID) {
      this.currentCourse = ID;
      this.signUpForm = !this.signUpForm;
    },
    getAttendingUsers() {
      UserService.getGuestsByPotluckId(this.$route.params.potluckId)
      .then(response => {
        this.invitedGuests = response.data;
      })
      .catch(error => {
        console.log(error)
      })
    },
    // getDishesForPotluck() {
    //   DishService.getDishesByPotluck(this.Potluck.potluckId)
    //   .then(response => {
    //     this.dishes = response.data;
    //   })
    //   .catch(error => {
    //     console.log(error)
    //   })
    //}
    // toggleDishSignup(courseName) {
    //     if (this.currentCourse === '') {
    //         this.currentCourse = courseName;
    //     }
    //     else {
    //         this.currentCourse = '';
    //     }
    //     this.dishSignup = !this.dishSignup;
    // }
  },
  created() {
    this.currentPotluck = this.Potluck;
    this.getAttendingUsers();
    //this.getDishesForPotluck();
  }
}
</script>
<style scoped>
body {
  margin: 0;
  font-family: 'Roboto', sans-serif;
  background-color: rgba(0, 0, 0, 0.1);
}

.container {
  display: flex;
  flex-direction: column;
  padding: 20px;
  background-color: rgba(255, 255, 255, 0.9);
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  max-width: 600px;
}

.container li {
  margin: 10px 0;
  display: flex-wrap;
  align-items: left;
  background-color: rgba(255, 255, 255, 0.9);
}

.container li i {
  margin-right: 10px;

}

.changingIcon {
  transition: color 0.3s ease;
}

.spring {
  transition: background-color 0.3s ease;
  background-image: url('/springFoods.jpeg');
}

.summer {
  transition: background-color 0.3s ease;
  background-image: url('/summer-food-collection.jpg');
}

.fall {
  transition: background-color 0.3s ease;
  background-image: url('/fallfood.png');
}

.winter {
  transition: background-color 0.3s ease;
  background-image: url('/winter2.jpg');
}
.noneTheme{
  background-color: rgba(98, 175, 95, 0.411);
}
.links {
  margin-top: 50px;
  margin-top: 50px;
}

.invitation-link,
.update-link,
.my-potlucks-link,
.whos-coming-link {
  text-decoration: none;
  color: #3498DB;
  margin-right: 20px;
  padding: 10px 15px;
  border-radius: 5px;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.invitation-link:hover,
.update-link:hover,
.my-potlucks-link:hover,
.whos-coming-link:hover {
  background-color: #297FB8;
  color: #fff;
}

.signup {
  width: 75vw;
  height: 40vh;
  border: 2px solid #ccc;
  border-radius: 10px;
  padding: 20px;
}

.list li {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
  font-size: 18px;
  color: #333;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: box-shadow 0.3s ease;
  width: 500px;
}

.list li i {
  margin-right: 10px;
  font-size: 22px;
}

.requested-items {
  margin-bottom: 20px;
  /* Add some space below this section */
}

.requested-items span {

  margin-right: 20px;
  cursor: pointer;
  transition: color 0.3s ease;
  display: inline-block;
  margin-bottom: 10px;
}

.image-container {
  margin-left: 800px;
  /* Add some space below the image */
  margin-bottom: -200px;
}

.right-image {
  width: 300px;
  height: auto;
}

.requested-item {
  text-align: left;
  padding: 10px;
  border-radius: 8px;
  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: box-shadow 0.3s ease, transform 0.3s ease;
}

.requested-item span {
  display: block;
  cursor: pointer;
  transition: color 0.3s ease;
}

      .requested-items span:last-child {
        margin-right: 0;
        /* Remove right margin for the last span */
        margin-right: 0;
        /* Remove right margin for the last span */
      }

.host-request {
  display: flex;

}

.requested-items {
  margin-top: 20px;
  font-size: 18px;
  color: #555;
  padding: 15px;
  border-radius: 8px;
  background-color: #F9F9F9;
  transition: background-color 0.3s ease;
  position: relative;
  overflow: hidden;
}

.requested-items h3 {
  margin-bottom: 10px;
  font-size: 20px;
  color: #333;
}

.requested-items-details {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.requested-item {
  flex: 1;
  text-align: center;
  padding: 10px;
  border-radius: 8px;
  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: box-shadow 0.3s ease, transform 0.3s ease;
}

.requested-item span {
  display: block;
  cursor: pointer;
  transition: color 0.3s ease;
}

.requested-item:hover {
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  transform: translateY(-2px);
}

.requested-item:hover span {
  color: #3498DB;
}</style>