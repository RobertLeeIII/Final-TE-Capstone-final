<template>
  <router-link :to="{ name: 'potluck-details', params: {userId: $store.state.user.userId, potluckId: Potluck.potluckId}}">
  <div class="potluck">
    <h2>{{ Potluck.name }}</h2>
    <p>When: {{ new Date(Potluck.time).toDateString() }}</p>
    <p>Where: {{ Potluck.location }}</p>
    <ul>
      <li v-for="guest in potlucks" :key="guest.id">
        {{ guest.email }}
      </li>
    </ul>
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
  
<style scoped></style>
  