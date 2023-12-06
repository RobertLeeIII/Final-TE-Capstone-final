<template>
    <form v-on:submit.prevent="submitForm" class="potluck-form">
        <label for="potluck-name">Name</label>
        <input type="text" name="Name" id="potluck-name" v-model="newPotluck.name">

        <label for="summary">Summary</label>
        <input type="text" name="Summary" id="summary" v-model="newPotluck.summary">

        <label for="location">Location</label>
        <input type="text" name="location" id="location" v-model="newPotluck.location">

        <label for="time">Date</label>
        <input type="datetime-local" name="time" id="time" v-model="newPotluck.date">

        <label for="potluck-theme">Theme</label>
        <select id="potluck-theme" v-model="newPotluck.theme">
            <option>None</option>
            <option>Spring</option>
            <option>Summer</option>
            <option>Fall</option>
            <option>Winter</option>
        </select>
        <button type="submit">Submit</button>

    </form>
</template>

<script>
export default {
    data() {
        return {
        newPotluck: {
            name: '',
            summary: '',
            location: '',
            date: '',
            theme: 'None'
        }
    }
    }
    
}
</script>

<style></style>