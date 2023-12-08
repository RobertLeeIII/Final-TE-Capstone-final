import axios from 'axios';

export default{

    showPotlucks(userId){
        return axios.get("/" + userId);
    },
    getPotluckById(userId, potluckId) {
        return axios.get(`/${userId}/potlucks/` + potluckId)
    },
    addPotluck(userId, newPotluck) {
        return axios.post(`${userId}/potlucks/`, newPotluck);
    },
    updatePotluck(userId, updatedPotluck)
    {
        return axios.put(`${userId}/potlucks/${updatedPotluck.potluckId}`, updatedPotluck)
    },


}