import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },
  /*updateUser(userId){
    return axios.put('/reset/', userId)
  }*/
  //This is for the password reset
}
