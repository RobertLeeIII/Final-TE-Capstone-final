<!-- <template>
    <div>
            <label for="courses">Courses</label>
            <div>
                <select id="course" class="input is-rounded" v-model="newPotluck.course">
                    <option>--Select a Course--</option>
                    <option>Appetizers</option>
                    <option>Sides</option>
                    <option>Mains</option>
                    <option>Desserts</option>
                </select>

                <div id="courseCounterContainer">
                    <input type="number" id="courseCounter" v-model="newPotluck.count" class="input is-rounded"
                        placeholder="0" v-on:click="showButton = true"
                        v-show="newPotluck.course !== '--Select a Course--'" />
                </div>
                <button type="submit" v-if="moreThanZero">+</button>
            </div>
        </div>
</template> -->