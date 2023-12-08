import axios from 'axios'

export default {
    postNewDish(newDish, userId, potluckId){
        return axios.post(`/potlucks/${potluckId}/menu`, userId, newDish);
    },

    updateDish(updatedDish, dishId){
        return axios.put(`/dishes/${dishId}/update`, updatedDish)
    }
}