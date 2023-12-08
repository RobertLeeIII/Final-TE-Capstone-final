<template>
    <body>
        <div :class="changingTheme">
            <ul>
            <li><i class="fa-regular fa-snowflake"></i>{{ Potluck.name }}</li>
            <li><i class="fa-regular fa-snowflake"></i>{{ Potluck.location }}</li>
            <li><i class="fa-regular fa-snowflake"></i>{{ Potluck.time }}</li>
            <li><i class="fa-regular fa-snowflake"></i>{{ Potluck.theme }}</li>
            <li><i class="fa-regular fa-snowflake"></i>{{ Potluck.summary }}</li>
            <li><i class="fa-regular fa-snowflake"></i>Appetizers: {{ Potluck.courseRequest.apps }}</li>
            <li><i class="fa-regular fa-snowflake"></i>Main Dishes: {{ Potluck.courseRequest.mains }}</li>
            <li><i class="fa-regular fa-snowflake"></i>Sides: {{ Potluck.courseRequest.sides }}</li>
            <li><i class="fa-regular fa-snowflake"></i>Desserts: {{ Potluck.courseRequest.desserts }}</li>
        </ul>
            <router-link v-if="isHost" :to="{ name: invitationList }">Invite People</router-link>
            <router-link v-if="isHost" :to="{
                name: 'potluck-update', params: { potluckId: Potluck.potluckId },
                query: { action: 'update' }
            }">Update This Potluck</router-link>
        </div>
    </body>
</template>

<script>
export default {
    data() {
        return {
            //currentPotluck: {}
            themeSpring: false,
            themeSummer: false,
            themeFall: false,
            themeWinter: false,
        }
    },
    computed: {
        isHost() {
            return this.Potluck.hostId == this.$store.state.user.userId;
        },
        changingTheme() {
            if (this.Potluck.theme === 'Spring') {
                this.themeSpring === true;
            }
            else if (this.Potluck.theme === 'Summer') {
                this.themeSummer === true;
            }
            else if (this.Potluck.theme === 'Fall') {
                this.themeFall === true;
            }
            else if (this.Potluck.theme === 'Winter') {
                return {winter: true}
                //this.themeWinter === true;
            }
            return true;
        }
    },
    props: {
        Potluck: Object
    },
}
</script>

<style scoped>
.potluck-detail h2 {
    padding: 5px;

}

.spring {
    color: red;
}

.summer {
    color: limegreen;
}

.fall {
    color: blue;
}

.winter {
    color: cadetblue;
    background-color: purple;
}</style>