import axios from 'axios'

export default {

    getGuestsByPotluckId(potluckId){
        return axios.get(`/users/potlucks/${potluckId}/guestList`)
    },
}