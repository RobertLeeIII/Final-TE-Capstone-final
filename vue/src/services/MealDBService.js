import axios from 'axios';

// axios.defaults.baseURL = 'https://www.themealdb.com/api/json/v1/1';


const http = axios.create({ baseURL: 'https://www.themealdb.com/api/json/v1/1' })

export default{

    getRandomRecipe(){
        return http.get('/random.php');
        // , {
        //     headers: {
        //         'apikey': '1',
        //     },
        // }
        // );
    }
}