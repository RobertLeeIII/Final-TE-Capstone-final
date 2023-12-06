<template>
    <section class="hero">
    <div class="hero-body">
      <p class="title">
        Create Potluck
      </p>
      <p class="subtitle">
      </p>
    </div>
  </section>
    <form v-on:submit.prevent="submitForm" class="potluck-form">
        <label for="potluck-name">Name</label>
        <input class="input is-rounded" type="text" name="Name" id="potluck-name" v-model="newPotluck.name">

        <label for="summary">Summary</label>
        <input class="input is-rounded" type="text" name="Summary" id="summary" v-model="newPotluck.summary">

        <label for="location">Location</label>
        <input class="input is-rounded" type="text" name="location" id="location" v-model="newPotluck.location">

        <label for="time">Date</label>
        <input class="input is-rounded" type="datetime-local" name="time" id="time" v-model="newPotluck.date">

        <label for="potluck-theme">Theme</label>
        <select class="input is-rounded" id="potluck-theme" v-model="newPotluck.theme">
            <option>None</option>
            <option>Spring</option>
            <option>Summer</option>
            <option>Fall</option>
            <option>Winter</option>
        </select>
        <button type="submit">Submit</button>

    </form>
    <br>
    <div>    <img src="/7637317.jpg" style="width:100%;" alt="">
</div>
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

<style>
.hero{
    background-color:rgb(255, 193, 146);
}
</style>
