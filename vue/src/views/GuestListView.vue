<template>
    <invite-users-form :potluckGuests="guests"></invite-users-form>
</template>

<script>
import InviteUsersForm from '../components/InviteUsersForm.vue';
import UserService from '../services/UserService.js';

export default {
    data() {
        return {
            potluckGuests: []

        }
    },
    components: {
        InviteUsersForm
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