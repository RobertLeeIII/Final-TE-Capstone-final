<template>
    <potluck-form v-if="creating"></potluck-form>
    <potlucks-display v-else :myPotlucks="potlucks"></potlucks-display>
</template>

<script>
import PotluckService from '../services/PotluckService.js';
import PotlucksDisplay from '../components/PotlucksDisplay.vue';
import PotluckForm from '../components/PotluckForm.vue';

export default {
    data() {
        return {
            potlucks: [],
            create: false
        }
    },
    components: {
        PotlucksDisplay,
        PotluckForm
    },
    computed: {
        creating() {
            let action = this.$route.query.action;
            if (action == 'create') {
                return true;
            }
            return false;
        }
    },
    methods: {
        getUserPotlucks(userId) {
            PotluckService.getPotlucks(userId)
                .then(response => {
                    this.potlucks = response.data;
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
    },
    created() {
        this.getUserPotlucks(this.$store.state.user.userId);
    },
}
</script>

<style></style>