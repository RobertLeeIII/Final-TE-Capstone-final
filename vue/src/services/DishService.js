import axios from 'axios'

export default {
    postNewDish(newDish, userId, potluckId){
        return axios.post(`/potlucks/${potluckId}/menu`, userId, newDish); // TODO: Can't have two parameters in addition to the path
    },

    updateDish(updatedDish, dishId){
        return axios.put(`/dishes/${dishId}/update`, updatedDish) //TODO: use a query here instead of a verb as a path
    }
}