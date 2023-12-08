<template>
    <div class="potluck-detail">
        <h2>{{ Potluck.name }}</h2>
        <h2>{{ Potluck.location }}</h2>
        <h2>{{ Potluck.time }}</h2>
        <h2>{{ Potluck.theme }}</h2>
        <h2>{{ Potluck.summary }}</h2>
        <h2>Appetizers: {{ Potluck.courseRequest.apps }}</h2>
        <h2>Main Dishes: {{ Potluck.courseRequest.mains }}</h2>
        <h2>Sides: {{ Potluck.courseRequest.sides }}</h2>
        <h2>Desserts: {{ Potluck.courseRequest.desserts }}</h2>
        <router-link :to="{name: invitationList}">Invite People</router-link>
    </div>
</template>

<script>
export default {
    data() {
        return {
            //currentPotluck: {}
        }
    },
    props: {
        Potluck: Object
    },
}
</script>

<style scoped></style>