<template>
  <div id="capstone-app">
    <div id="nav">
      <nav class="navbar" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
          <a class="navbar-item">
            <img src="/public/logo.png">
          </a>

          <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
          </a>
        </div>

        <div id="navbarBasicExample" class="navbar-menu">
          <div class="navbar-start">
            <a class="navbar-item">
              <router-link class='routerlink' :to="{ name: 'home' }">Welcome</router-link>
            </a>

            <a class="navbar-item">
              <router-link class='routerlink'
                :to="{ name: 'user-view', params: { userId: this.$store.state.user.userId } }"
                v-if="this.$store.state.token != ''">Home</router-link>
            </a>

            <a class="navbar-item">
              <router-link class='routerlink' :to="{ name: 'logout' }"
                v-if="this.$store.state.token != ''">Logout</router-link>
            </a>
            <div class="navbar-item has-dropdown is-hoverable">
              <a class="navbar-link">
                More
              </a>

              <div class="navbar-dropdown">
                <a class="navbar-item">
                  placeholder
                </a>
                <a class="navbar-item">
                  placeholder
                </a>
                <a class="navbar-item">
                  placeholder
                </a>
                <hr class="navbar-divider">
                <a class="navbar-item">
                  About us
                </a>
              </div>
            </div>
          </div>

          <br>

          <div class="navbar-end">
            <div class="navbar-item">
              <div class="buttons">
                <a class="button is-secondary is-light">
                  <p>
                    <router-link v-bind:to="{ name: 'login' }">Login</router-link>
                  </p>
                </a>
                <a class="button is-light">
                  <p>
                    <router-link v-bind:to="{ name: 'register' }">Sign up</router-link>
                  </p>
                </a>
              </div>
            </div>
          </div>
        </div>
      </nav>

    </div>
    <router-view />
  </div>
  <footer>
    <div class="centerFooter">
      <p>
        <strong>Potluck Planner:</strong> Written by
        <br>
        <a href="https://www.linkedin.com/in/mattcaltomare">Matt Altomare</a> |
        <a href="https://www.linkedin.com/in/kelseybrownwright">Kelsey Brownwright</a> |
        <a href="https://www.linkedin.com/in/leerobertiii/">Rob Lee</a> |
        <a href="https://www.linkedin.com/in/ted-nelson-developer/">Ted Nelson</a> |
        <a href="https://www.linkedin.com/in/kylewwacker/">Kyle Wacker</a>
      </p>
    </div>
  </footer>
</template>
<style>
.navbar {
  background-color: rgb(36, 62, 52);
}
.navbar-item{
  background-color: rgb(36, 62, 52);
}

a.router-link-active {
  color: rgb(241, 247, 237)
}

.routerlink {
  color: rgb(241, 247, 237);
  /* Set background color for the header */
}
.centerFooter {
  padding: 10px;
}
</style>