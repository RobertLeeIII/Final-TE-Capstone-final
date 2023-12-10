import axios from 'axios';

axios.defaults.baseURL = 'https://www.themealdb.com/api/json/v1/1';




export default{

    getRandomRecipe(){
        return axios.get('/random.php'
        // , {
        //     headers: {
        //         'apikey': '1',
        //     },
        // }
        );
    }
}