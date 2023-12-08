<template @submit.prevent="handleSubmit">
    <form class="box">
    <h3>Forgot Password?</h3>
    <br>
    <div class="field">
        <label>Email</label>
        <div class='control'>
        <input type="email" class="form-control" v-model="email" placeholder="Email">
        </div>
    </div>

    <button class="button is-primary">Submit</button>
    </form>
    <div class="forimage">
    <img class='img-size' src="/public\wall.jpg">
    </div>
</template>

<script>
import axios from 'axios';
export default{
    name:'Forgot',
    data(){
        return{
          email: ''  
        }
    },
    methods: {
        async handleSubmit(){
            const response = await axios.post('forgot', {
                    email: this.email
            });
            /* console.log(response); */
        }
    }
}
</script>
<style>
.img-size{
    height: 650px; 
}
.forimage{
    display: flex;
    justify-content: center;
}
</style>