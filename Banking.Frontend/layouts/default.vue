<template>
  <v-app>
    <!--Left Navigation-->
    <v-navigation-drawer :mini-variant.sync="miniVariant" :clipped="clipped" v-model="drawer" fixed app>
      <v-list>
        <v-list-tile router :to="item.to" :key="i" v-for="(item, i) in items" exact>
          <v-list-tile-action>
            <v-icon v-html="item.icon"></v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title v-text="item.title"></v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>

    <!--Toolbar-->
    <v-toolbar fixed app :clipped-left="clipped">
      <v-toolbar-side-icon @click="drawer = !drawer"></v-toolbar-side-icon>
      <v-btn icon @click.stop="miniVariant = !miniVariant">
        <v-icon v-html="miniVariant ? 'chevron_right' : 'chevron_left'"></v-icon>
      </v-btn>
      <v-toolbar-title v-text="title"></v-toolbar-title>

      <v-spacer></v-spacer>

      <v-toolbar-title v-if="user">{{user.firstName}} {{user.lastName}}</v-toolbar-title>
      <v-btn icon @click.stop="rightDrawer = !rightDrawer">
        <v-icon>menu</v-icon>
      </v-btn>
    </v-toolbar>

    <!--Content-->
    <v-content>
      <nuxt />
    </v-content>

    <!--Right Navigation-->
    <v-navigation-drawer temporary :right="right" v-model="rightDrawer" fixed>
      <v-list>
        <v-list-tile @click="logout">
          <v-list-tile-action>
            <v-icon light>input</v-icon>
          </v-list-tile-action>
          <v-list-tile-title>Logout</v-list-tile-title>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>

    <!--Snackbar-->
    <snackbar :open="snackbar.open" :color="snackbar.color" @close="resetSnackbar()">{{snackbar.text}}</snackbar>
  </v-app>
</template>

<script>
import { mapGetters } from 'vuex'
import Snackbar from '@/components/Snackbar'

export default {
  components: {
    'snackbar': Snackbar
  },
  data () {
    return {
      clipped: false,
      drawer: true,
      items: [
        { icon: 'account_balance', title: 'Account', to: 'account' }
      ],
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Banking'
    }
  },
  computed: {
    ...mapGetters({
      user: 'account/user',
      snackbar: 'snackbar'
    })
  },
  mounted () {
    this.isLoggedIn()
  },
  methods: {
    logout () {
      // Because we are using a cache, just removing
      // user info from store to act as 'logout'
      this.$store.commit('account/user', null)
      this.$router.push('auth/login')
    },
    isLoggedIn () {
      // No refresh in app since store gets wiped out.
      // Real application, would use vuex-presistedstate
      // or create auth middleware to check cookie
      // and persistence, but just doing this for demo purposes.
      if (!this.user) {
        this.$router.push('auth/login')
      }
    },
    resetSnackbar () {
      this.$store.commit('toggleSnackbar', {})
    }
  }
}
</script>
