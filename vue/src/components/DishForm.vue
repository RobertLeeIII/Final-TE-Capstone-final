<template>
    <div class="main">
        <div class="card">
            <form @submit.prevent="submitRecipe">
                <div class="card-body">
                    <label for="dishName">Dish Name</label>
                    <input id="dishName" type="text" v-model="newDish.name">

                    <textarea v-model="newDish.recipe" placeholder="Enter directions"></textarea>

                    <div class="toggle-section">
                        <button @click.prevent="toggleSpecialDiets" type="button">{{ showSpecialDiets ? 'Hide Special Diets'
                            : 'Special Diets' }}</button>
                        <div v-if="showSpecialDiets" class="checkbox-group">
                            <label v-for="diet in specialDiets" :key="diet">
                                <input type="checkbox" :value="diet" v-model="newDish.diets" />
                                {{ diet }}
                            </label>
                        </div>

                        <button @click.prevent="toggleAllergens" type="button">{{ showAllergens ? 'Hide Allergens' :
                            'Allergens' }}</button>
                        <div v-if="showAllergens" class="checkbox-group">
                            <label v-for="allergen in allergens" :key="allergen">
                                <input type="checkbox" :value="allergen" v-model="newDish.allergens" />
                                {{ allergen }}
                            </label>
                        </div>
                    </div>

                    <button type="submit">Submit Recipe</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            newDish: {},
        }
    },
}
</script>

<style></style>