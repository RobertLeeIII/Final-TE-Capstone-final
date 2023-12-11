<!-- <template>
  <div>
    <label for="futureDateTime">Select a date and time for your potluck:</label>
    <input type="datetime-local" id="futureDateTime" v-model="selectedDateTime" @input="validateDateTime" :step="timeStep" />
  </div>
</template>

<script>
export default {
  data() {
    return {
      selectedDateTime: '',
      timeStep: 900,
    };
  },
  methods: {
    validateDateTime() {
      const currentDateTime = new Date().toISOString().slice(0, -8); 

      if (this.selectedDateTime < currentDateTime) {
        this.selectedDateTime = '';
        alert('Oooh, are you a time traveler?! If so, why are you wasting your talents on potlucks?? If not, please choose a date and time that has not occurred yet.');
      }
    }
  }
};
</script> -->