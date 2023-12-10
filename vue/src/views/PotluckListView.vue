<template>
    <potluck-form v-if="creating"></potluck-form>
    <potluck-form v-else-if="updating"></potluck-form>
    <potlucks-display v-else :myPotlucks="potlucks"></potlucks-display>
</template>

<script>
import PotluckService from '@/services/PotluckService.js';
import PotlucksDisplay from '@/components/PotlucksDisplay.vue';
import PotluckForm from '@/components/PotluckForm.vue';

export default {
    data() {
        return {
            potlucks: [],
        }
    },
    components: {
        PotlucksDisplay,
        PotluckForm
    },
    computed: {
        creating() {
            let action = this.$route.query.action;
            return action == 'create'
        },
        updating() {
            let action = this.$route.query.action;
            return action == 'update';
        },
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
    handleErrorResponse(error) {
      if (error.response) {
        if (error.response.status == 404) {
          console.log("404 PROBLEM");
        }
      }
    },
    created() {
        this.getUserPotlucks(this.$store.state.user.userId);
    },
}
</script>

<style></style>