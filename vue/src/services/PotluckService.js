import axios from 'axios';

export default{

    showPotlucks(userId){
        return axios.get("/users/" + userId);
    },
    getPotluckById(potluckId) {
        return axios.get(`/potlucks/` + potluckId)
    },
    addPotluck(userId, newPotluck) {
        return axios.post(`/potlucks/`, newPotluck);
    },


}