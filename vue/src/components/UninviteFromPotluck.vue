<template>
    <div>Uninvite People Here!</div>
    <div v-for="guest in guests" :key="guest.userId">
        <input v-model="selectedGuests" :id="guest.userId" 
        :value="guest.userId" type="checkbox"> {{ guest.username.substring(0,1).toUpperCase() + guest.username.substring(1) }}
        
    </div>
    <button >Delete Selected Users</button>
    <router-link v-if="this.$store.state.user.userId == this.$route.params.userId" style="color: blue;" :to="{name: 'guest-list'}">Invite more people!</router-link>
</template>

<script>
export default{
    data() {
        return {
            selectedGuests: []
        }
    },
    props: {
        guests: Array
    },
    methods: {

    }
}
</script>

<style>
</style>