import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },

  getRecoveryByEmail(email) {
    return axios.get(`/forgot/${email}`)
  }
  /*updateUser(userId){
    return axios.put('/reset/', userId)
  }*/
  //This is for the password reset
}
