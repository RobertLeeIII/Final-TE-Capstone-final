<template>
    <div class="potluck-detail">
            <h2><i class="fa-regular fa-snowflake"></i>{{ Potluck.name }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>{{ Potluck.location }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>{{ Potluck.time }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>{{ Potluck.theme }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>{{ Potluck.summary }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>Appetizers: {{ Potluck.courseRequest.apps }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>Main Dishes: {{ Potluck.courseRequest.mains }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>Sides: {{ Potluck.courseRequest.sides }}</h2>
            <h2><i class="fa-regular fa-snowflake"></i>Desserts: {{ Potluck.courseRequest.desserts }}</h2>
        <router-link :to="{ name: invitationList }">Invite People</router-link>
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

<style scoped>
.potluck-detail h2{
    padding: 5px;

}
</style>