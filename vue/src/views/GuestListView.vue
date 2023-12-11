<template>
    <invite-to-potluck-form v-if="this.$route.query.action == 'invite'"></invite-to-potluck-form>
    <uninvite-from-potluck v-else-if="this.$route.query.action == 'uninvite'" :guests="potluckGuests" :potluck="currentPotluck"></uninvite-from-potluck>
    <div v-else>
    <div v-for="guest in potluckGuests" :key="guest.username" :class="guest.userId == currentPotluck.hostId ? 'isHost':''" >{{ guest.username.substring(0,1).toUpperCase() + guest.username.substring(1) }}</div>
    <!-- <router-link :to="{name: 'guest-list', params: {potluckId: Potluck.potluckId}, 
                      query: {action: 'uninvite'}}">Uninvite Someone?</router-link> -->
        <router-link :to="{name: 'guest-list', params: {potluckId: this.$route.params.potluckId}, query: {action: 'uninvite'}}" ><button>Uninvite People</button></router-link>
    </div>
</template>

<script>
import UserService from '../services/UserService.js';
import PotluckService from '@/services/PotluckService';
import UninviteFromPotluck from '@/components/UninviteFromPotluck.vue'
import InviteToPotluckForm from '../components/InviteToPotluckForm.vue';

export default {
    data() {
        return {
            potluckGuests: [],
            currentPotluck: {}

        }
    },
    components: {
        UninviteFromPotluck,
        InviteToPotluckForm

    },
    props: {
        
    },
    computed: {
        
    },
    methods: {
        getPotluckGuests() {
            UserService.getGuestsByPotluckId(this.$route.params.potluckId)
                .then(response => {
                    this.potluckGuests = response.data;
                })
                .catch(error => {
                    if (error.response && error.response.status === 404) {
                        this.handleErrorResponse(error);
                    } else if (error.request) {
                        console.log("OTHER PROBLEM");
                    } else {
                        console.log("ANOTHER PROBLEM");
                    }
                });
        },
        getPotluckById() {
            PotluckService.getPotluck(this.$route.params.potluckId)
            .then(response => {
                this.currentPotluck = response.data;
            })
            .catch(error => {
                    if (error.response && error.response.status === 404) {
                        this.handleErrorResponse(error);
                    } else if (error.request) {
                        console.log("OTHER PROBLEM");
                    } else {
                        console.log("ANOTHER PROBLEM");
                    }
                });
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
        this.getPotluckGuests();
        this.getPotluckById()
    },
}
</script>

<style>
.isHost{
    color: red;
}</style>