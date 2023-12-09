<template>
    <div v-for="guest in guests" :key="guest.userId">
        {{ guest.email }} 
    </div>
</template>

<script>
export default{
    data() {
        return {
        }
    },
    props: {
        guests: Array
    }
}
</script>

<style>
</style>