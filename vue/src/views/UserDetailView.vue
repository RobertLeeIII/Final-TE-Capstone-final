<template>
    <div>
        <div class="card-container">
            <potluck class="potluck" v-for="potluck in potlucks" :Potluck="potluck" :key="potluck.potluckId"></potluck>
        </div>
        <p><br></p>
        <button class="button is-primary">
            <router-link :to="{ name: 'potluck-create' }"> HOST A POTLUCK </router-link>
        </button>
    </div>
</template>

<script>
import PotluckService from '@/services/PotluckService.js';
import Potluck from '@/components/Potluck.vue'

export default {
    data() {
        return {
            potlucks: [],
        };
    },
    components: {
        Potluck
    },
    methods: {
        getUserPotlucks(userId) {
            PotluckService.showPotlucks(userId)
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
        formatDate(dateTimeString) {
            const options = { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
            const date = new Date(dateTimeString);
            return date.toLocaleDateString('en-US', options);
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
        this.getUserPotlucks(this.$route.params.userId);
    },
};
</script>

<style>
</style>
