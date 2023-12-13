<template>
    <br>
    <div class="title">Uninvite People Here!</div>
    <div v-for="guest in guests" :key="guest.userId">
        <div v-if="!checkIsHost(guest.userId)">
            <input v-model="selectedGuests" :id="guest.userId" :value="guest.userId" type="checkbox"> {{
                guest.username.substring(0, 1).toUpperCase() + guest.username.substring(1) }}
        </div>
    </div>
    <button class="button is-danger" v-on:click.prevent="removeGuest">Delete Selected Users</button>
    <router-link v-if="this.$store.state.user.userId == this.$route.params.userId" style="color: blue;"
    :to="{ name: 'guest-list' }">Invite more people!</router-link>
</template>
    
<script>
import UserService from '../services/UserService.js';

export default {
    data() {
        return {
            selectedGuests: []
        }
    },
    props: {
        guests: Array,
        potluck: Object
    },
    methods: {
        checkIsHost(userId) {
            if (userId == this.potluck.hostId) {
                return true
            }
            return false
        },
        removeGuest() {
            UserService.uninviteGuests(this.$route.params.potluckId, this.selectedGuests)
            .then(response => {
                this.selectedGuests = response.data;
            })
            .catch(error => {
                this.handleErrorResponse(error);
            })
        },
        handleErrorResponse(error) {
            if (error.response) {
                if (error.response.status == 404) {
                    console.log("404 PROBLEM")
                } else if (error.request) {
                    console.log("OTHER PROBLEM")
                } else {
                    console.log("ANOTHER PROBLEM")
                }
            }
        },
    }
}
</script>

<style></style>