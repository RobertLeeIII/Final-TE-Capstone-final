import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },

  getRecoveryByEmail(email) {
    return axios.post('/PasswordReset/forgot', {
      params: {email},
    })
  },
  getSecurityQuestionByEmail(email){
    return axios.get(`/PasswordReset/forgot/${email}`)
  }
  /*updateUser(userId){
    return axios.put('/reset/', userId)
  }*/
  //This is for the password reset
}
