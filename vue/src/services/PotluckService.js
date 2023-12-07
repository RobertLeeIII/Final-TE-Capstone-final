import axios from 'axios';

export default{

    showPotlucks(userId){
        return axios.get("/" + userId);
    },
    getPotluckById(userId, potluckId) {
        return axios.get(`/${userId}/potlucks/` + potluckId)
    },
    addPotluck(userId, potluck) {
        return axios.post(`${userId}/potlucks/`, potluck);
    },


}