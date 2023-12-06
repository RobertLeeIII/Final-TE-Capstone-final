import axios from 'axios'

export default {
    postNewDish(newDish, userId, potluckId){
        return axios.post(`/${userId}/potlucks/${potluckId}`, newDish);
    },

    updateDish(updatedDish, userId, dishId){
        return axios.put(`/${userId}/dishes/${dishId}/update`, updatedDish)
    }
}