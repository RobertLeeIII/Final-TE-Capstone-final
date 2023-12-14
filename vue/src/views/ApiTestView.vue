<template>
  <api-test></api-test>
  <dish-suggestion></dish-suggestion>
</template>
<script>
import ApiTest from '../components/ApiTest.vue'
import DishSuggestion from '../components/DishSuggestion.vue';
import SuggestionInfo from '../components/SuggestionInfo.vue';
export default{
    components: {
        ApiTest,
        DishSuggestion,

    }
}
</script>
<style>
</style>