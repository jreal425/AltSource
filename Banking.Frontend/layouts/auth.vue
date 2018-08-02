<template>
  <v-app>
    <v-content>
      <nuxt />
    </v-content>
  </v-app>
</template>

<script>
export default {
  data () {
    return {
      clipped: false,
      drawer: true,
      fixed: false,
      items: [
        { icon: 'apps', title: 'Welcome', to: '/' },
        { icon: 'bubble_chart', title: 'Login', to: '/Auth/Login' },
        { icon: 'bubble_chart', title: 'Register', to: '/Auth/Register' }
      ],
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Specials'
    }
  },
  computed: {
  }
}
</script>
