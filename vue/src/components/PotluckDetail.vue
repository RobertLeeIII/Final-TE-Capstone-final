<template>
    <body>
    <div :class="changingTheme" v-if="Potluck.courseRequest">
        <ul class="container">
                <li><i :class="changingIcon"></i> {{ Potluck.name }}</li>
                <li><i :class="changingIcon"></i> {{ Potluck.location }}</li>
                <li><i :class="changingIcon"></i> {{ formatDate(Potluck.time) }}</li>
                <li><i :class="changingIcon"></i> Theme: {{ Potluck.theme }}</li>
                <li><i :class="changingIcon"></i> About: {{ Potluck.summary }}</li>
                <li><i :class="changingIcon"></i> Appetizers: {{ Potluck.courseRequest.apps }}</li>
                <li><i :class="changingIcon"></i> Main Dishes: {{ Potluck.courseRequest.mains }}</li>
                <li><i :class="changingIcon"></i> Sides: {{ Potluck.courseRequest.sides }}</li>
                <li><i :class="changingIcon"></i> Desserts: {{ Potluck.courseRequest.desserts }}</li>
            </ul>
        <router-link v-if="isHost" :to="{name: 'guest-list', params: {potluckId: Potluck.potluckId}, 
                                        query: {action: 'invite'}}">Invite People</router-link>
        <router-link v-if="isHost" :to="{name: 'potluck-update', params: {potluckId: Potluck.potluckId}}">Update This Potluck</router-link>
        <router-link :to="{name: 'guest-list', params: {potluckId: Potluck.potluckId} }">Who's coming?</router-link>
    </div>
</body>
</template>

<script>
export default {
    data() {
        return {
            
        }
    },
    computed: {
        isHost() {
            return this.Potluck.hostId == this.$store.state.user.userId;
        },
        changingTheme() {
            if (this.Potluck.theme === 'Spring') {
                return { spring: true }
            }
            else if (this.Potluck.theme === 'Summer') {
                return { summer: true }
            }
            else if (this.Potluck.theme === 'Fall') {
                return { fall: true }
            }
            else if (this.Potluck.theme === 'Winter') {
                return { winter: true }
            }
            return true;
        },
        changingIcon() {
            if (this.Potluck.theme === 'Spring') {
                return "fa-solid fa-cloud-sun-rain"
            }
            else if (this.Potluck.theme === 'Summer') {
                return "fa-solid fa-sun"
            }
            else if (this.Potluck.theme === 'Fall') {
                return "fa-solid fa-leaf"
            }
            else if (this.Potluck.theme === 'Winter') {
                return "fa-regular fa-snowflake"
            }
            return true;
        }
    },
    props: {
        Potluck: Object
    },
    methods: {
        formatDate(dateTimeString) {
      const options = { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
      const date = new Date(dateTimeString);
      return date.toLocaleDateString('en-US', options);
    }
    }
}
</script>

<style scoped>
body {
  margin: 0;
  font-family: 'Roboto', sans-serif; /* You can replace 'Roboto' with your preferred font */
  background-color: #f4f4f4;
}

.container {
  list-style: none;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  max-width: 600px;
  margin: 50px auto;
}

.container li {
  margin: 10px 0;
  display: flex;
  align-items: center;
}

.container li i {
  margin-right: 10px;
}

.changingIcon {
  /* Add styles for changing icons */
}

.changingTheme {
  /* Add styles for changing theme */
}

.router-link {
  text-decoration: none;
  color: #3498db;
  margin-top: 20px;
  display: inline-block;
  padding: 10px 15px;
  background-color: #3498db;
  color: #fff;
  border-radius: 5px;
  transition: background-color 0.3s ease;
}

.router-link:hover {
  background-color: #297fb8;
}
.spring {
    background-color: rgb(166, 255, 166);
}

.summer {
    background-color: rgb(255, 255, 154);
}

.fall {
    background-color: rgb(241, 200, 138);
}

.winter {
    color: cadetblue;
    background-color: rgb(189, 239, 251);

}
</style>