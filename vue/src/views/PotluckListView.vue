<template>
    <div class="container">
      <div class="content">
        <potluck-form v-if="creating || updating"></potluck-form>
        <potlucks-display v-else :myPotlucks="potlucks"></potlucks-display>
      </div>
    </div>
  </template>
  
  <script>
  import PotluckService from '@/services/PotluckService.js';
  import PotlucksDisplay from '@/components/PotlucksDisplay.vue';
  import PotluckForm from '@/components/PotluckForm.vue';
  
  export default {
    data() {
      return {
        potlucks: [],
      }
    },
    components: {
      PotlucksDisplay,
      PotluckForm
    },
    computed: {
      creating() {
        return this.$route.query.action === 'create';
      },
      updating() {
        return this.$route.query.action === 'update';
      },
    },
    methods: {
      async getUserPotlucks(userId) {
        try {
          const response = await PotluckService.getPotlucks(userId);
          this.potlucks = response.data;
        } catch (error) {
          this.handleErrorResponse(error);
        }
      },
      handleErrorResponse(error) {
        if (error.response && error.response.status === 404) {
          console.log("404 PROBLEM");
        } else if (error.request) {
          console.log("OTHER PROBLEM");
        } else {
          console.log("ANOTHER PROBLEM");
        }
      },
    },
    created() {
      this.getUserPotlucks(this.$store.state.user.userId);
    },
  }
  </script>
  
  <style scoped>
  .container {
    display: flex;
    justify-content: center;
    
  }
  
  .content {
    width: 100%;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    background-color: #f9f9f9;
  }
  </style>
  