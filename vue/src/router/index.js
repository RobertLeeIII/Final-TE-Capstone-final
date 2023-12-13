import { createRouter as createRouter, createWebHistory } from 'vue-router'
import { useStore } from 'vuex'

// Import components
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/LoginView.vue';
import LogoutView from '../views/LogoutView.vue';
import RegisterView from '../views/RegisterView.vue';
import UserDetailView from '../views/UserDetailView.vue';
import PotluckListView from '../views/PotluckListView.vue';
import PotluckDetailsView from '../views/PotluckDetailsView.vue';
import CreatePotluckView from '../views/CreatePotluckView.vue';
import PotluckUpdateView from '../views/PotluckUpdateView.vue';
import ForgotEmailView from '../views/ForgotEmailView.vue';
import ResetPasswordView from '../views/ResetView.vue';
import GuestListView from '../views/GuestListView.vue';
import InviteToPotluckForm from '../views/InviteToPotluckView.vue';
import ApiTestView from '../views/ApiTestView.vue';
import DishSignupView from '@/views/DishSignupView.vue';
import DishListView from '@/views/DishListView.vue';
import DishDetailsView from '@/views/DishDetailsView.vue';

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */
const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: {
      requiresAuth: false //TODO: CHANGE THIS BACK AFTER EVERYTHNG WORKS!
    }
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/logout",
    name: "logout",
    component: LogoutView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/register",
    name: "register",
    component: RegisterView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/users/:userId",
    name: "user-view",
    component: UserDetailView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/users/:userId/potlucks",
    name: "potluck-list",
    component: PotluckListView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/potlucks",
    name: "potluck-create",
    component: CreatePotluckView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/potlucks/:potluckId",
    name: "potluck-details",
    component: PotluckDetailsView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/potlucks/:potluckId/guest-list",
    name: "guest-list",
    component: GuestListView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/invite",
    name: "potluck-invite",
    component: InviteToPotluckForm,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/potlucks/:potluckId/update",
    name: "potluck-update",
    component: PotluckUpdateView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/PasswordReset/forgot",
    name: "forgot",
    component: ForgotEmailView
  },
  {
    path: "/PasswordReset/reset/:email",
    name: "reset",
    component: ResetPasswordView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/test",
    name: "api-test",
    component: ApiTestView,
  },
  {
    path: "/potlucks/:potluckId/menu",
    name: "dish-signup",
    component: DishSignupView,
  },
  {
    path: "/users/:userId/dishes",
    name: "dish-list",
    component: DishListView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/dishes/:dishId",
    name: "dish-details",
    component: DishDetailsView,
    meta: {
      requiresAuth: false
    }
  },

];

// Create the router
const router = createRouter({
  history: createWebHistory(),
  routes: routes
});

router.beforeEach((to) => {

  // Get the Vuex store
  const store = useStore();

  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    return { name: "login" };
  }
  // Otherwise, do nothing and they'll go to their next destination
});

export default router;
