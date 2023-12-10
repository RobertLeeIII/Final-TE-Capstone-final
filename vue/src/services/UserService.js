import axios from 'axios'

export default {

    getGuestsByPotluckId(potluckId){
        return axios.get(`/potlucks/${potluckId}/guestList`)
    },
    inviteGuestsByEmails(potluckId, guestList){
        return axios.post(`/potlucks/${potluckId}/guestList`, guestList)
    }
}