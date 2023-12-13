<template>
  <div id="capstone-app">
    <header>
      <nav class="navbar" role="navigation">
        <div class="navbar-container">
          <div class="navbar-brand">
            <a class="navbar-item" href="/">
              <img src="/public/logo.png" alt="Logo">
            </a>
            <button class="navbar-toggle" @click="toggleNavbar">
              <span class="toggle-icon"></span>
              <span class="toggle-icon"></span>
              <span class="toggle-icon"></span>
            </button>
          </div>
          <div class="navbar-links" :class="{ 'active': isNavbarActive }">
            <router-link class="nav-link" :to="{ name: 'home' }">Welcome</router-link>
            <router-link v-if="$store.state.token !== ''" class="nav-link" :to="{ name: 'user-view', params: { userId: $store.state.user.userId } }">Home</router-link>
            <router-link v-if="$store.state.token !== ''" class="nav-link" :to="{ name: 'logout' }">Logout</router-link>
            <div class="navbar-buttons">
              <router-link v-if="$store.state.token == ''" class="button is-secondary" :to="{ name: 'login' }">Login</router-link>
              <router-link v-if="$store.state.token == ''" class="button is-primary" :to="{ name: 'register' }">Sign up</router-link>
            </div>
          </div>
        </div>
      </nav>
    </header>

    <router-view />


  <footer>
    <div class="centerFooter">
      <p>
        <strong>Potluck Planner:</strong> Written by<br>
        <a href="https://www.linkedin.com/in/mattcaltomare">Matt Altomare</a> |
        <a href="https://www.linkedin.com/in/kelseybrownwright">Kelsey Brownwright</a> |
        <a href="https://www.linkedin.com/in/leerobertiii/">Rob Lee</a> |
        <a href="https://www.linkedin.com/in/ted-nelson-developer/">Ted Nelson</a> |
        <a href="https://www.linkedin.com/in/kylewwacker/">Kyle Wacker</a>
      </p>
    </div>
  </footer>
</div>
</template>

<script>
export default {
  data() {
    return {
      isNavbarActive: false,
      isDropdownActive: false
    };
  },
  methods: {
    toggleNavbar() {
      this.isNavbarActive = !this.isNavbarActive;
    },
    toggleDropdown() {
      this.isDropdownActive = !this.isDropdownActive;
    }
  }
};
</script>

<style>
/* Your existing styles can go here */

.navbar {
  background-color: rgb(36, 62, 52);
  color: rgb(241, 247, 237);
  padding: 1rem;
}

.navbar-brand {
  display: flex;
  align-items: center;
}

.navbar-brand img {
  height: 2.5rem;
  padding-left: 22px;
}

.navbar-toggle {
  display: none;
  background: none;
  border: none;
  cursor: pointer;
}

.toggle-icon {
  display: block;
  width: 20px;
  height: 2px;
  background-color: rgb(241, 247, 237);
  margin-bottom: 4px;
  transition: transform 0.3s ease;
}

.navbar-links {
  display: flex;
  align-items: center;
  justify-content: flex-end;
}

.navbar-links.active {
  display: block;
}

.nav-link {
  text-decoration: none;
  color: rgb(241, 247, 237);
  padding: 0.5rem 1rem;
  margin: 0 0.5rem;
  transition: color 0.3s ease;
}

.nav-link:hover {
  color: rgb(8, 16, 3);
}

.navbar-buttons {
  display: flex;
  align-items: center;
}

.button {
  margin-left: 0.5rem;
}
.centerFooter {
  padding: 10px;
}
</style>
