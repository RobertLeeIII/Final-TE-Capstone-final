<template>
  <router-link
    :to="{
      name: 'potluck-details',
      params: { potluckId: propPotluck.potluckId },
    }"
  >
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
            <figure class="image is-48x48">
              <img src="/public/potluck-planner-logo.png" alt="Logo" />
            </figure>
          </div>
          <div class="media-content">
            <p class="title is-4">{{ propPotluck.name }}</p>
            <p class="subtitle is-6">{{ $store.state.user.username }}</p>
          </div>
        </div>
        <div class="content">
          <p>{{ propPotluck.summary }}</p>
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
export default {
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
      return "/imagePotluck-transformed.jpg";
    },
  },
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
  overflow: auto;
  border: 2px solid rgb(124, 169, 130); 
}
.content{
  background-color: rgb(239, 239, 239);
  padding: 20px;
}
</style>
