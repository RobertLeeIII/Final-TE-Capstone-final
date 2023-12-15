<template>
  <router-link
    :to="{
      name: 'potluck-details',
      params: { potluckId: propPotluck.potluckId },
    }">
    <div class="card">
      <div class="card-image">
        <p></p>
        <figure class="image is-3by2">
          <img :src="changingImage" alt="Potluck Image" />
        </figure>
      </div>
      <div class="card-content">
        <div class="media">
          <div class="media-left">
           <!--  <figure class="image is-48x48">
              <img src="/public/potluck-planner-logo.png" alt="Logo" />
            </figure> Little logo may be too much?-->
          </div>
          <div class="media-content">
            <p class="title is-4">{{ propPotluck.name }}</p>
            <p class="subtitle is-6">{{ hostIdentify() }}</p>
          </div>
        </div>
        <div class="content">
          <a :href="`#${propPotluck.location}`">#{{ propPotluck.location }}</a>
          <br />
          <time :datetime="propPotluck.time">{{
            formatDate(propPotluck.time)
          }}</time>
        </div>
      </div>
    </div>
  </router-link>
</template>

<script>
import UserService from '../services/UserService';
export default {
  data() {
    return {
      currentPotluck: {}
    }
  },
  props: {
    propPotluck: Object,
  },
  methods: {
    formatDate(dateTimeString) {
      const options = {
        year: "numeric",
        month: "short",
        day: "numeric",
        hour: "numeric",
        minute: "numeric",
      };
      const date = new Date(dateTimeString);
      return date.toLocaleDateString("en-US", options);
    },
    getHost() {
      UserService.getHostUsernameByPotluckId(this.propPotluck.potluckId)
      .then(response => {
        this.currentPotluck.host = response.data;
      })
    },
    hostIdentify() {
      if(this.propPotluck.hostId === this.$store.state.user.userId){
        return 'You are Hosting';
      }
      else{
        return `Hosted By ${this.currentPotluck.host.substring(0,1).toUpperCase() + this.currentPotluck.host.substring(1) }`
      }
    }
  },
  computed: {
    changingImage() {
      if (this.propPotluck.theme.includes("Spring")) {
        return "/public/springFoods2.jpg";
      } else if (this.propPotluck.theme.includes("Summer")) {
        return "/public/SummerFoods_03.jpg";
      } else if (this.propPotluck.theme.includes("Fall")) {
        return "/public/fallfood.png";
      } else if (this.propPotluck.theme.includes("Winter")) {
        return "/public/winter2.jpg";
      }
      return "/public/default-potluck.jpg";
    },
    
  },
  created() {
    this.currentPotluck = this.propPotluck;
    this.getHost();
  }
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
  height: 500px;
  overflow-y: scroll; /* Add the ability to scroll */
  border: 2px solid rgb(124, 169, 130, 0.3); 
}
.card::-webkit-scrollbar {
    display: none;
}
.content{
  background-color: rgb(239, 239, 239,);
  padding: 20px;
}
</style>
