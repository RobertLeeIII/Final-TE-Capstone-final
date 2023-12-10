<template>
    <invite-to-potluck-form v-if="this.$route.query.action == 'invite'"></invite-to-potluck-form>
    <uninvite-from-potluck v-else-if="this.$route.query.action == 'uninvite'" :potluckGuests="guests"></uninvite-from-potluck>

    <router-link :to="{name: 'guest-list', params: {potluckId: Potluck.potluckId}, 
                      query: {action: 'uninvite'}}">Uninvite Someone?</router-link>
</template>

<script>
import UserService from '../services/UserService.js';
import UninviteFromPotluck from '@/components/UninviteFromPotluck.vue'
import InviteToPotluckForm from '../components/InviteToPotluckForm.vue';

export default {
    data() {
        return {
            potluckGuests: []

        }
    },
    components: {
        UninviteFromPotluck,
        InviteToPotluckForm

    },
    props: {
        Potluck: Object,
        
    },
    methods: {
        getPotluckGuests(potluckId) {
            this.UserService.getGuestsByPotluckId(potluckId)
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
        handleErrorResponse(error) {
            if (error.response) {
                if (error.response.status == 404) {
                    console.log("404 PROBLEM");
                }
            }
        },
    },
    created() {

    },
}
</script>

<style></style>