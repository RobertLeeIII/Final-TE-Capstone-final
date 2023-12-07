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
              Used in reference to a situation in which one must take a chance that whatever is available will prove to be
              good or acceptable.
            </p>
            <p class="header-description">
              <br>
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
        <h2>Your Potluck Events:</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis pretium dolor sit amet est gravida, vitae
          efficitur
          ex mattis. Sed at ante ex. Vivamus ut ligula eu ex eleifend malesuada. Donec auctor sit amet neque venenatis
          rutrum. Suspendisse consectetur dictum aliquam. Sed rutrum ullamcorper leo, ac venenatis tellus auctor in. Duis
          finibus lobortis metus nec rhoncus. Ut metus quam, aliquam sit amet elit in, hendrerit facilisis nunc.
          Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Mauris tempor libero non
          lacus fringilla, eu lobortis sapien viverra.</p>


        <p>Nullam congue volutpat risus vel interdum. Vivamus non velit iaculis, feugiat tellus nec, gravida erat.
          Aliquam id feugiat elit, in sagittis velit. Maecenas eget nibh eget sapien tincidunt euismod. Vivamus felis
          eros,
          vestibulum vitae fringilla nec, tincidunt eu enim. Morbi nec elementum orci. Cras lobortis, felis elementum
          molestie convallis, lorem arcu cursus sapien, sed eleifend urna ligula sit amet nulla. Nullam blandit eleifend
          libero. Curabitur convallis mattis tempor. Ut sed ante metus. Proin malesuada, felis non sollicitudin tempus,
          orci lacus tristique nisi, imperdiet aliquam nisl libero at ante. Nullam at justo lobortis, imperdiet diam in,
          maximus erat.</p>
      </div>
      <div class="centerFooter">
        <p><strong>potluckPlanner:</strong> Written by TE capstone team 2</p>
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

.layout {
  display: grid;
  grid-template-rows: auto 1fr auto; /* Adjust as needed */
  gap: 8px;
  background-color: rgb(124, 169, 130); /* Salmon-colored background */
  min-height: 95vh; /* Set minimum height of layout to full viewport height */

  padding-bottom: 20px; /* Increase bottom padding */
}

.centerFooter {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-top: 20px; /* Move the footer down */
  color: white; /* Change text color */
}

.header {
  text-align: left;
  margin-bottom: 20px;
  background-color: rgb(241, 247, 237); /* Set background color for the header */
}

.header-content {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  border: 4px solid rgb(124, 169, 130)   ;
  padding: 20px; /* Add padding as needed */
}

.header-text {
  text-align: left;

  margin-right: 50px;
  margin-left: 50px;
}

.header-title {
  font-size: 36px;
  margin-bottom: 5px;
}

.header-phonetic {
  font-size: 18px;
  margin-left: 50px;

  color: #888;
}

.header-definition {
  font-size: 24px;
  margin-top: 10px;
  margin-bottom: 5px;
  margin-left: 50px;

}

.header-description {
  font-size: 16px;

  margin-left: 50px;
  line-height: 1.5;
}

.header-image {
  flex-shrink: 0;
}

.header-img {
  width: 380px;
  height: auto;
  border-radius: 4%;
}

.main{
  color: white; /* Change text color */

}</style>
