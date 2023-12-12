import axios from 'axios'

export default {
    postNewDish(newDish, potluckId){
        return axios.post(`/potlucks/${potluckId}/menu`, newDish);
    },

    updateDish(updatedDish, dishId){
        return axios.put(`/dishes/${dishId}`, updatedDish) //TODO: use a query here instead of a verb as a path
    },
    getDishesByUserId(userId) {
        return axios.get(`/users/${userId}/dishes`);
    },
    getDish(dishId) {
        return axios.get(`/dishes/${dishId}`);
    },
    getDishesByPotluck(potluckId) {
        return axios.get(`potlucks/${potluckId}/dishes`);
    },
}