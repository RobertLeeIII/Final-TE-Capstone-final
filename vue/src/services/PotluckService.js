import axios from 'axios';

export default{

    showPotlucks(userId){
        return axios.get("/" + userId);
    },
    getPotluckById(userId, potluckId) {
        return axios.get(`/${userId}/potlucks/` + potluckId)
    }


}