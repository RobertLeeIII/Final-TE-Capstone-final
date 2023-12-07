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
              <router-link v-bind:to="{ name: 'home' }">Welcome</router-link>
            </a>
            
            <a class="navbar-item">
              <router-link v-bind:to="{ name: '' }" v-if="this.$store.state.token != ''">Home</router-link>
            </a>
            
            <a class="navbar-item">
              <router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">Logout</router-link>
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
                <a class="button is-warning">
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
</template>
