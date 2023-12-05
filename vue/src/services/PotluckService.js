import axios from 'axios';

export default{

    showPotlucks(userId){
        return axios.get("/users/" + userId);
    },


}