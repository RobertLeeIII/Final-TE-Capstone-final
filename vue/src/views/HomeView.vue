<template>
  <div>
    <section class="layout">
      <div class="header">
        <div class="header-content">
          <div class="header-text">
            <h1 class="header-title">Pot·luck</h1>
            <h2 class="header-phonetic">/ˌpätˈlək/</h2>
            <h3 class="header-definition">noun</h3>
            <p class="header-description">
              A meal or party to which each of the guests contributes a dish.
            </p>
            <p class="header-description">
              Coordinate potluck dishes, supplies and party RSVPs with online sign up sheets for gatherings with family,
              friends and large groups.
            </p>
          </div>
          <div class="header-image">
            <img src="/public/potluckHomeViewImage.jpg" alt="Image description" class="header-img">
          </div>
        </div>
      </div>
      <div class="main">
        <h2>Mission Statement:</h2>
        <p>
          At Potluck Planner, our mission is to bring people together through the joy of shared meals. We believe in fostering a sense of community and connection by simplifying the process of organizing and participating in potluck events. Our goal is to provide a user-friendly
          platform that empowers individuals to plan, coordinate, and enjoy memorable gatherings effortlessly. With a commitment to inclusivity, collaboration, and delicious experiences, we strive to enhance the spirit of togetherness one potluck at a time. Join us in building a 
          world where every meal is an opportunity for shared joy and lasting connections.
        </p>
      </div>
      <div class="centerFooter">
        <p>
          <strong>potluckPlanner:</strong> Written by
          <br>
          <a href="https://www.linkedin.com/in/mattcaltomare">Matt Altomare</a> | 
          <a href="https://www.linkedin.com/in/kelseybrownwright">Kelsey Brownwright</a> |
          <a href="https://www.linkedin.com/in/leerobertiii/">Rob Lee</a> |
          <a href="https://www.linkedin.com/in/ted-nelson-developer/">Ted Nelson</a> |
          <a href="https://www.linkedin.com/in/kylewwacker/">Kyle Wacker</a>
        </p>
      </div>
    </section>
  </div>
</template>


<script>
export default {
  name: 'home',
  //data here
  data() {
    return {
      potluckEvents: [] //from the api i guess
    };
  },
  methods: {
    fetchPotluckEvents() {
      // axios get from 
    },
    createPotluckEvent() {
      // use the backend to save the potluck
    }
  },
  created() {

  }
};
</script>

<style scoped>
/* Updated styles for a more modern look */

.layout {
  display: grid;
  grid-template-rows: auto 1fr auto;
  gap: 20px;
  background-color: #f0f0f0;
  min-height: 95vh;
  padding: 20px;
}

.centerFooter {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-top: 20px;
  color: #888;
}

.header {
  text-align: left;
  margin-bottom: 20px;
  background-color: #fafafa;
  padding: 20px;
}

.header-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  border: 4px solid #ccc;
  border-radius: 8px;
  padding: 20px;
}

.header-title {
  font-size: 3rem;
  margin-bottom: 5px;
  color: #333;
}

.header-phonetic {
  font-size: 1.5rem;
  color: #666;
}

.header-definition {
  font-size: 2rem;
  margin-top: 10px;
  margin-bottom: 5px;
  color: #333;
}

.header-description {
  font-size: 1rem;
  line-height: 1.6;
  color: #333;
}

.header-image {
  flex-shrink: 0;
}

.header-img {
  width: 380px;
  height: auto;
  border-radius: 8px;
}

.main {
  color: #333;
}

/* Adjustments for smaller screens */
@media (max-width: 768px) {
  .header-content {
    flex-direction: column;
    align-items: flex-start;
  }
  .header-image {
    margin-top: 20px;
  }
  .header-img {
    width: 100%;
  }
}
</style>

