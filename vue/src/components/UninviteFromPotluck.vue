<template>
    <div>HELLO!</div>
    <div v-for="guest in guests" :key="guest.userId">
        {{ guest.email }} 
    </div>
    <router-link v-if="this.$store.state.user.userId == this.$route.params.userId" style="color: blue;" :to="{name: 'guest-list'}">Invite more people!</router-link>
</template>

<script>
export default{
    data() {
        return {
        }
    },
    props: {
        guests: Array
    }
}
</script>

<style>
</style>