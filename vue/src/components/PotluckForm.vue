<template>
<<<<<<< HEAD
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

        <div>
            <label for="courses">Courses</label>
            <div>
                <select id="course" class="input is-rounded" v-model="newPotluck.course">
                    <option>--Select a Course--</option>
                    <option>Appetizers</option>
                    <option>Sides</option>
                    <option>Mains</option>
                    <option>Desserts</option>
                </select>

                <div id="courseCounterContainer">
                    <input type="number" id="courseCounter" v-model="newPotluck.count" class="input is-rounded"
                        placeholder="0" v-on:click="showButton = true"
                        v-show="newPotluck.course !== '--Select a Course--'" />
                </div>
                <button type="submit" v-if="moreThanZero">+</button>
            </div>
        </div>

        <label for="potluck-theme">Theme</label>
        <select class="input is-rounded" id="potluck-theme" v-model="newPotluck.theme">
            <option>None</option>
            <option>Spring</option>
            <option>Summer</option>
            <option>Fall</option>
            <option>Winter</option>
        </select>
        <button type="submit">Submit</button>
=======
    <form>
        <label for="potluck-name">Name</label>
        <input type="text" name="Name" id="potluck-name">

        <label for="summary">Summary</label>
        <input type="text" name="Summary" id="summary">

        <label for="location">Location</label>
        <input type="text" name="location" id="location">

        <label for="time">When</label>
        <input type="datetime-local" name="time" id="time">

        <label for="potluck-theme">Theme</label>
        <input type="text" name="Theme" id="potluck-theme">

>>>>>>> c05bc21a5352ce46c1ddee5f4127a64c46e1fbde

    </form>
    <br>
    <div> <img src="/7637317.jpg" style="width:100%;" alt="">
    </div>
</template>

<script>
<<<<<<< HEAD
export default {
    data() {
        return {

            newPotluck: {
                name: '',
                summary: '',
                location: '',
                date: '',
                course: '--Select a Course--',
                theme: 'None',
                count: 0
            },
            showButton: false,
        }
    },
    computed: {
        moreThanZero() {
            return this.newPotluck.count > 0;
        }
    }

}
</script>

<style>
.hero {
    background-color: rgb(255, 193, 146);
}
</style>
=======
</script>

<style>
</style>
>>>>>>> c05bc21a5352ce46c1ddee5f4127a64c46e1fbde
