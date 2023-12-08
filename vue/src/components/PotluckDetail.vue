<template>
    <div class="potluck-detail" v-if="Potluck.courseRequest"> <!--Wait until the Potluck is fully loaded-->
        <h2>{{ Potluck.name }}</h2>
        <h2>{{ Potluck.location }}</h2>
        <h2>{{ Potluck.time }}</h2>
        <h2>{{ Potluck.theme }}</h2>
        <h2>{{ Potluck.summary }}</h2>
        <h2>Appetizers: {{ Potluck.courseRequest.apps }}</h2>
        <h2>Main Dishes: {{ Potluck.courseRequest.mains }}</h2>
        <h2>Sides: {{ Potluck.courseRequest.sides }}</h2>
        <h2>Desserts: {{ Potluck.courseRequest.desserts }}</h2>
        <router-link v-if="isHost" :to="{name: invitationList}">Invite People</router-link>
        <router-link v-if="isHost" :to="{name: 'potluck-update', params: {potluckId: Potluck.potluckId}, 
                                        query: {action: 'update'}}">Update This Potluck</router-link>
    </div>
</template>

<script>
export default {
    data() {
        return {
            //currentPotluck: {}
        }
    },
    computed: {
        isHost() {
            return this.Potluck.hostId == this.$store.state.user.userId;
        }
    },
    props: {
        Potluck: Object
    },
}
</script>

<style scoped></style>