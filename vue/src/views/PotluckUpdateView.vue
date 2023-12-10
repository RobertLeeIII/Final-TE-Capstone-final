<template>
    <potluck-update-form></potluck-update-form>
</template>

<script>
import PotluckUpdateForm from '../components/PotluckUpdateForm.vue';
import PotluckService from '../services/PotluckService';

export default {
    data() {
        return {
            currentPotluck: {},
        }
    },
    components: {
        PotluckUpdateForm
    },
    methods: {
        retrieveSpecificPotluck(potluckId) {
            PotluckService.getPotluck(potluckId)
                .then(response => {
                    this.currentPotluck = response.data;
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
    },
    created() {
        this.retrieveSpecificPotluck(this.$route.params.potluckId);
    }
}
</script>

<style></style>