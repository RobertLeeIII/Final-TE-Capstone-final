<template>
    <section class="hero">
    <div class="hero-body">
      <p class="title">
        Create Potluck
      </p>
      <p class="subtitle">
      </p>
    </div>
  </section>
    <form>
        <label for="potluck-name">Title</label>
        <input class="input is-rounded" type="text" name="Name" id="potluck-name">

        <label for="summary">Summary</label>
        <input class= "input is-rounded" type="text" name="Summary" id="summary">

        <label for="location">Location</label>
        <input class="input is-rounded" type="text" name="location" id="location">

        <label for="time">When</label>
        <input class="input is-rounded" type="datetime-local" name="time" id="time">

        <label for="potluck-theme">Theme</label>
        <input class="input is-rounded" type="text" name="Theme" id="potluck-theme">


    </form>
    <br>
    <div>    <img src="/7637317.jpg" style="width:100%;" alt="">
</div>
</template>

<script>
</script>

<style>
.hero{
    background-color:rgb(255, 193, 146);
}
</style>