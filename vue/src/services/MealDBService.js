import axios from 'axios';

// axios.defaults.baseURL = 'https://www.themealdb.com/api/json/v1/1';


const http = axios.create({ baseURL: 'https://www.themealdb.com/api/json/v1/1' })

export default{
    data(){
        return {
            courseSearch: {
                apps: [''],
                main: [''],
                side: [''],
                dessert: []
            }
        }
    },
    getRandomRecipe(){
        return http.get('/random.php');
    },
    getRecipeByCourseId(ID){
        let search = '';
        if(ID == 1){
            search = 'pasta'
        }else if (ID == 2){
            search = 'seafood'
        }else if (ID == 3){
            search = 'miscellaneous'
        }else if (ID == 4){
            search = 'dessert'
        }
        return http.get(`filter.php?c=${search}`)
    },
    getRecipeById(ID) {
        return http.get(`lookup.php?i=${ID}`)
    }
}