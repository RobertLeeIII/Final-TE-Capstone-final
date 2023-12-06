<template>
    Not sure what goes here.
    <router-link :to="{ name: 'potluck-list' }">This takes you to the list of potlucks!</router-link>
    <potluck class="potluck" v-for="potluck in potlucks" :Potluck="potluck" :key="potluck.hostId"></potluck>
</template>

<script>
import Potluck from '../components/Potluck.vue';
import PotluckService from '@/services/PotluckService.js'

export default {
    data() {
        return {
            potlucks: [],
        }
    },
    components: {
        Potluck
    },
    methods: {
        getUserPotlucks() {
            PotluckService.showPotlucks(this.$route.params.userId)
                .then(response => {
                    this.potlucks = response.data;
                })
                .catch(error => {

                    if (error.status === 404) {
                        this.handleErrorResponse(error);
                    }
                }
                )

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
        this.getUserPotlucks(this.$route.params.userId)
    }

}
</script>

<style>
.potluck{
    margin: 20px;
}
</style>