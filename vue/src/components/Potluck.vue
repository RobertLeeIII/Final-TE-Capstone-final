<template>
  <router-link
    :to="{ name: 'potluck-details', params: { userId: $store.state.user.userId, potluckId: Potluck.potluckId } }">
    <div>
      <div class="card">
        <div class="card-image">
          <figure class="image is-4by3">
            <img src="/public/potluck.jpg" alt="Potluck Image">
          </figure>
        </div>
        <div class="card-content">
          <div class="media">
            <div class="media-left">
              <figure class="image is-48x48">
                <img src="/public/logo.png" alt="Logo">
              </figure>
            </div>
            <div class="media-content">
              <p class="title is-4">{{ Potluck.name }}</p>
              <p class="subtitle is-6">@{{ this.$store.state.user.userId }}</p>
            </div>
          </div>
          <div class="content">
            <p>{{ Potluck.summary }}</p>
            <a href="#">#{{ Potluck.location }}</a> <a href="#"></a>
            <br>
            <time :datetime="Potluck.time"> formatDate({{ Potluck.time }}) </time>
          </div>
        </div>
      </div>
    </div>
  </router-link>
</template>
  
<script>
export default {
  data() {
    return {
      //potlucks: [],
    }
  },
  props: {
    Potluck: Object,
  },
}
</script>
  
<style>
.card-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.card {
  width: 300px;
}
</style>
  