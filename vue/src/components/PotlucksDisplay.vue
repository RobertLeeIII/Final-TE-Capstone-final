<template>
  <div class="main">
    <div class="card-container">
      <div class="card">
        <router-link
          :to="{ name: 'potluck-list', query: { action: 'create' } }"
        >
          <div class="card-image">
            <figure class="image is-centered">
              <img src="/public/potluck-planner-logo.png" alt="Potluck Image" />
            </figure>
            <p></p>
          </div>
          <div class="card-content">
            <div class="media">
              <div class="media-center">
                <p class="create-potluck-text">
                  Create Your<br />Perfect Potluck
                </p>
              </div>
            </div>
          </div>
        </router-link>
      </div>
      <potluck
        class="potluck"
        v-for="potluck in myPotlucks"
        :propPotluck="potluck"
        :key="potluck.potluckId"
      >
      </potluck>
      <div class="card-container" v-if="!hasPotlucks">
        <div class="card">
          <div class="card-image">
            <figure class="image is-centered">
              <img
                src="/public/imagePotluck-transformed.jpg"
                alt="Potluck Image"
              />
            </figure>
          </div>
          <div class="card-content">
            <div class="media">
              <div class="media-center">
                <p>You Have No Potlucks :(</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Potluck from "@/components/Potluck.vue";

export default {
    data() {
        return {
            potlucks: [],
            // futurePotlucks: [],
            // pastPotlucks: []
        }
    },
    components: {
        Potluck
    },
    props: {
        myPotlucks: Array
    },
    computed: {
        hasPotlucks() {
            return this.myPotlucks.length > 0;
        },
        futurePotlucks() {
            return this.myPotlucks.filter((item) => {
                const today = new Date()
                const scheduled = new Date(item.time)
                return scheduled > today
            })
        },
        pastPotlucks() {
            return this.myPotlucks.filter((item) => {
                const today = new Date()
                const scheduled = new Date(item.time)
                return scheduled < today
            })
        }

    },
    methods: {
        populatePotluckArrays() {
            this.potlucks = JSON.parse(JSON.stringify(this.myPotlucks));
            // this.futurePotlucks = this.myPotlucks.filter((item) => {
            //     const today = new Date().getDate()
            //     const scheduled = new Date(item.time).getDate()
            //     if (scheduled > today) {
            //         return item;
            //     }
            // })
            // this.pastPotlucks = this.myPotlucks.filter((item) => {
            //     const today = new Date().getDate()
            //     const scheduled = new Date(item.time).getDate()
            //     return scheduled < today
            //     })
            
            }
    },
    created() {
        this.populatePotluckArrays();

    }


}
</script>

<style scoped>
.main {
  border: 4px solid rgb(124, 169, 130, 0.2);
  background-color: rgb(241, 247, 237);
  /* Set background color for the header */
  min-height: 95vh;
  /* Set minimum height of layout to full viewport height */
}

.centerFooter {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-top: 20px;
  color: #74b7c8;
}

.card-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  margin-top: 20px;
  justify-content: center;
  padding-bottom: 20px;
}

.potluck {
  max-width: 300px;
  height: 500px;
  word-wrap: break-word;
  overflow: auto;
}
.past{
    opacity: 0.5;
}

.media {
  display: flex;
  justify-content: center;
}

.card {
  width: 300px;
  height: 500px;
  overflow: auto;
}

.image.is-centered {
  margin-top: 50px;
  display: flex;
  justify-content: center;
  align-content: center;
}
.create-potluck-text {
  font-size: 30px;
  text-align: center;
}
</style>