<template :class="this.potluck.theme">
    <potluck-detail :Potluck="currentPotluck"></potluck-detail>
</template>

<script>
import PotluckService from '@/services/PotluckService.js'
import PotluckDetail from '@/components/PotluckDetail.vue'
export default {
    data() {
        return {
            currentPotluck: {}
        }
    },
    components: {
        PotluckDetail,
    },
    methods: {
        retrieveSpecificPotluck() {
            PotluckService.getPotluckById(this.$route.params.potluckId)
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
        this.retrieveSpecificPotluck()
    }
}
</script>

<style></style>