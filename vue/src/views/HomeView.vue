<template>
  <div>
    <section class="hero is-info">
      <div class="hero-body">
        <p class="title">
          Welcome to the Potluck Planner
        </p>
        <p class="subtitle">
          Plan and organize your potluck events!
        </p>
      </div>
    </section>

    <div class="container">
      <h2>Your Potluck Events:</h2>
      <!-- Display potluck events or create new ones -->
      <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis pretium dolor sit amet est gravida, vitae efficitur
        ex mattis. Sed at ante ex. Vivamus ut ligula eu ex eleifend malesuada. Donec auctor sit amet neque venenatis
        rutrum. Suspendisse consectetur dictum aliquam. Sed rutrum ullamcorper leo, ac venenatis tellus auctor in. Duis
        finibus lobortis metus nec rhoncus. Ut metus quam, aliquam sit amet elit in, hendrerit facilisis nunc. Vestibulum
        ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Mauris tempor libero non lacus
        fringilla, eu lobortis sapien viverra.

        Aenean vehicula congue augue quis semper. Nullam hendrerit augue in odio egestas maximus. Praesent placerat
        facilisis ullamcorper. Donec massa ex, pulvinar sit amet accumsan id, porta ut augue. In ultrices orci in purus
        dignissim aliquam. Aenean vel pharetra sem, vitae scelerisque mauris. Sed lacinia, lectus placerat tempor dictum,
        ex mi mollis urna, non porta nisi nunc a mi. Praesent lobortis mauris eget faucibus mattis. Quisque vestibulum
        tristique dictum. Lorem ipsum dolor sit amet, consectetur adipiscing elit.

        Nullam congue volutpat risus vel interdum. Vivamus non velit iaculis, feugiat tellus nec, gravida erat. Aliquam id
        feugiat elit, in sagittis velit. Maecenas eget nibh eget sapien tincidunt euismod. Vivamus felis eros, vestibulum
        vitae fringilla nec, tincidunt eu enim. Morbi nec elementum orci. Cras lobortis, felis elementum molestie
        convallis, lorem arcu cursus sapien, sed eleifend urna ligula sit amet nulla. Nullam blandit eleifend libero.
        Curabitur convallis mattis tempor. Ut sed ante metus. Proin malesuada, felis non sollicitudin tempus, orci lacus
        tristique nisi, imperdiet aliquam nisl libero at ante. Nullam at justo lobortis, imperdiet diam in, maximus erat.

        Sed tristique ut elit et malesuada. Aliquam ornare tristique nunc, a venenatis magna mollis quis. Ut ornare
        interdum posuere. Suspendisse scelerisque odio sem, non pharetra justo efficitur id. Donec malesuada imperdiet
        elit, id laoreet erat pellentesque eu. Suspendisse potenti. Nulla non sapien tristique, faucibus nisl vitae,
        fringilla orci.

        Suspendisse auctor nulla sed lectus tempus ornare pulvinar non magna. Cras at blandit nisl. Morbi vel ipsum felis.
        Donec elementum velit in libero ultricies fringilla. Nullam fringilla velit non dolor porta, at consequat libero
        rutrum. Morbi blandit, leo laoreet accumsan interdum, sapien dolor venenatis tortor, eu iaculis eros est in mi.
        Sed malesuada congue ante, dapibus pretium augue fringilla sed. Proin vestibulum elit id lacus posuere, pharetra
        imperdiet enim gravida. Sed vulputate vulputate nulla et semper. Vivamus scelerisque ipsum ut tincidunt accumsan.
        Cras mollis rhoncus faucibus.</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'home',
  //data here
  data() {
    return {
      potluckEvents: [] //from the api i guess
    };
  },
  methods: {
    fetchPotluckEvents() {
      // axios get from 
    },
    createPotluckEvent() {
      // use the backend to save the potluck
    }
  },
  created() {

  }
};
</script>

<style scoped></style>
