<template>
    <form action="submit" v-on:submit.prevent="sendInvitations">
        <div v-for="index in invites" :key="index">
            <label :for=index>Invite</label>
            <input :ref=index type="email" v-on:keyup="increaseCounter(index)" :id=index autofocus>
        </div>
        <button id="invites" type="submit">
            <span>Send Invitations</span>
        </button>
        <h2 v-for="guest in guests" :key="guest">{{ guest }}</h2>
    </form>
</template>

<script>

import UserService from '@/services/UserService.js'
export default {
    data() {
        return {
            invites: 1,
            guests: [],
        }
    },
    methods: {
        increaseCounter(index) {
            if (event.target.value.trim()) {
                if (index == this.invites) {
                    if (index == this.guests.length + 1) {
                        this.invites += 1;
                        this.guests.push(event.target.value.trim());
                    }
                }
                else {
                    if (index == this.invites - 1) {
                        this.guests[index - 1] = event.target.value.trim();
                    }
                }
            }
            else {
                if (index <= this.guests.length) {
                    this.invites -= 1;
                    this.guests.pop();
                }
                // else {
                //     this.guests[index - 1] = event.target.value.trim();
                // }
            }

        },
        sendInvitations() {
            if (this.guests.length > 0) {
                UserService.inviteGuestsByEmails(this.$route.params.potluckId, this.guests)
            }
        }

    },

}
</script>

<style>
#invites {
    margin-top: 10px;
    margin-left: 67px;
    width: 130px;
    height: 30px;
    vertical-align: middle;
}

#invites>span {
    margin-top: auto;
    margin-bottom: auto;
}
</style>