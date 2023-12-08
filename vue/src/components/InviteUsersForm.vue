<template>
    <form action="submit">
        <div v-for="index in invites" :key="index">
            <label for="email">Invite</label>
            <input type="email" v-on:blur="increaseCounter(invites)" id="email" autofocus>
        </div>
    </form>
</template>

<script>
export default {
    data() {
        return {
            invites: 1,
            guests: []
        }
    },
    methods: {
        increaseCounter(key) {
            if (event.target.value !== '') {
                if (parseInt(key) == this.invites) {
                    this.invites += 1;
                    this.guests.push()
                }
            }
            else {
                if (this.invites > 1) {
                    this.invites -= 1;
                }
            }
        }
    }
}
</script>

<style></style>