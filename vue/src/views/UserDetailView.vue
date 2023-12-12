<template>
  <div class="main">
    <div class="card-container">
      <div class="card">
        <router-link :to="{ name: 'potluck-list', params: { userId: this.$store.state.user.userId } }">
          <div class="card-image">
            <img src="/public/imagePotluck-transformed.jpg" alt="Potluck Image">
          </div>
          <div class="card-content">
            <div class="media">
              <div class="media-center">
                <p>View My Potlucks</p>
              </div>
            </div>
          </div>
        </router-link>
      </div>
      <div class="dish-card">
        <!--Router link goes here-->
          <div class="dish-image">
            <img src="/public/imagePotluck-transformed.jpg" alt="Dish Image">
          </div>
          <div class="dish-content">
            <div class="dish-media">
              <div class="dish-media-center">
                <p>View My Dishes</p>
              </div>
            </div>
          </div>
        <!--Router link closes here-->
      </div>
    </div>
  </div>
  <p><br></p>
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
    async getUserPotlucks(userId) {
      try {
        const response = await PotluckService.getPotlucks(userId);
        this.potlucks = response.data;
      } catch (error) {
        this.handleErrorResponse(error);
      }
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

<style scoped>
.main {
  display: flex;
  align-items: center;
  flex-direction: column;
  min-height: 88vh;
  background-color: rgb(241, 247, 237);
  border-top: 4px solid rgb(124, 169, 130);
}

.centerFooter {
  margin-top: 20px;
  color: #74B7C8;
}

.card-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  margin-top: 20px;
}

.card {
  width: 300px;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.card-image {
  overflow: hidden;
}

.card-image img {
  width: 100%;
  height: auto;
  display: block;
}

.card-content {
  padding: 20px;
  background-color: #fff;
}

.media {
  display: flex;
  justify-content: center;
}

.media-center p {
  margin: 0;
  font-size: 18px;
  color: #333;
  text-align: center;
}
.dish-card {
  width: 300px;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
.dish-image {
  overflow: hidden;
}
.dish-image img {
  width: 100%;
  height: auto;
  display: block;
}
.dish-content {
  padding: 20px;
  background-color: #fff;
}
.dish-media {
  display: flex;
  justify-content: center;
}
.dish-media-center p {
  margin: 0;
  font-size: 18px;
  color: #333;
  text-align: center;
}
</style>
