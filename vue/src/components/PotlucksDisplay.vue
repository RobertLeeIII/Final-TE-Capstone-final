<template>
    <div class="main">

        <div class="card-container">
            <div class="card">
                <router-link :to="{ name: 'potluck-list', query: {action: 'create'}}">
                    <div class="card-image">
                        <figure class="image is-centered">
                            <img src="/public/imagePotluck-transformed.jpg" alt="Potluck Image">
                        </figure>
                    </div>
                    <div class="card-content">

                        <div class="media">
                            <div class="media-center">
                                <p>Create a PotLuck</p>
                            </div>

                        </div>
                    </div>
                </router-link>

            </div>
            <potluck class="potluck" v-for="potluck in myPotlucks" :propPotluck="potluck" :key="potluck.potluckId"></potluck>

        </div>
    </div>
    <div>

    </div>
</template>

<script>
import Potluck from '@/components/Potluck.vue'

export default {
    data() {
        return {
            potlucks: []
        }
    },
    components: {
        Potluck
    },
    props: {
        myPotlucks: Array
    },
    // methods: {
    //     populatePotluckArray() {
    //         this.potlucks = this.myPotlucks;
    //     }
    // },
    // created() {
    //     this.populatePotluckArray();
    // }
    

}
</script>

<style scoped>
.main{  
    border-top: 4px solid rgb(124, 169, 130);
    background-color: rgb(241, 247, 237); /* Set background color for the header */
  min-height: 95vh; /* Set minimum height of layout to full viewport height */
}
.centerFooter {
    display: flex;
    justify-content: center;
    gap: 16px;
    margin-top: 20px;
    color: #74B7C8;
}

.card-container {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    margin-top: 20px;
}
.media{
    display: flex;
    justify-content: center;
}
.card {
    width: 300px;
}

.image.is-centered {
    display: flex;
    justify-content: center;
}
</style>