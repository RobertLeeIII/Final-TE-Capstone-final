<template>
    <invite-to-potluck-form></invite-to-potluck-form>
    <uninvite-from-potluck :potluckGuests="guests"></uninvite-from-potluck>

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

    },
    methods: {
        getPotluckGuests(potluckId) {
            this.UserService.getGuestsByPotluckId(potluckId)
                .then(response => {
                    this.guests = response.data;
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