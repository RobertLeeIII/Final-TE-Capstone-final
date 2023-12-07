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

        <label for="apps">Appetizers</label>
        <input class="input is-rounded" type="number" name="apps" id="time" v-model="newPotluck.course.apps">
        <label for="sides">Sides</label>
        <input class="input is-rounded" type="number" name="sides" id="time" v-model="newPotluck.course.sides">
        <label for="mains">Mains</label>
        <input class="input is-rounded" type="number" name="mains" id="time" v-model="newPotluck.course.mains">
        <label for="desserts">Desserts</label>
        <input class="input is-rounded" type="number" name="desserts" id="time" v-model="newPotluck.course.desserts">


        <!-- <label for="courses">Courses</label>
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
 -->
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
    <div> <img src="/7637317.jpg" style="width:100%;" alt="">
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
<<<<<<< HEAD
                course: '--Select a Course--',
                theme: 'None',
                count: 0
=======
                course: {
                    apps: 0,
                    sides: 0,
                    mains: 0,
                    desserts: 0,
                },
                theme: 'None'
>>>>>>> 85c93a920651bd6011bf8f0786949b902238c18b
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
