import axios from 'axios';

export default{

    getPotlucks(userId){
        return axios.get("/users/" + userId);
    },
    getPotluck(potluckId) {
        return axios.get(`/potlucks/` + potluckId)
    },
    addPotluck(userId, newPotluck) {
        return axios.post(`/potlucks/`, newPotluck);
    },


}