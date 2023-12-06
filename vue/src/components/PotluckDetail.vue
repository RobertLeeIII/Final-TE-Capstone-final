<template>
    <div class="potluck-detail">
        <h2>{{ Potluck.name }}</h2>
        <h2>{{ Potluck.location }}</h2>
        <h2>{{ Potluck.time }}</h2>
        <h2>{{ Potluck.theme }}</h2>
        <h2>{{ Potluck.summary }}</h2>
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