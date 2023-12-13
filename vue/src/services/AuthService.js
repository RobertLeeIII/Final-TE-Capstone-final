import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },

  getRecoveryByEmail(forgotObject) {
    return axios.post('/PasswordReset/forgot', forgotObject)
  },
  getSecurityQuestionByEmail(email) {
    return axios.get(`/PasswordReset/forgot/${email}`)
  },
  recoverPassword(updateUserData) {
    return axios.put(`/PasswordReset/reset/${updateUserData.Email}`, updateUserData);
  }
}
