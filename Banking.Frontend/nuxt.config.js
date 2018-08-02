module.exports = {
  head: {
    title: 'AltSource - Banking',
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: 'AltSource Code Challenge' }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },
      { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons' }
    ]
  },
  mode: 'spa',
  plugins: [
    '~/plugins/vuetify.js',
    { src: '~/plugins/vue-currency-filter.js', ssr: false }
  ],
  css: [
    '~/assets/style/app.styl'
  ],
  loading: { color: '#3B8070' },
  dev: (process.env.NODE_ENV !== 'production'),
  env: {
    baseUrl: process.env.BASE_URL || 'http://localhost:51518'
  },
  build: {
    vendor: [
      '~/plugins/vuetify.js',
      'axios'
    ],
    extractCSS: true,
    extend (config, ctx) {
      if (ctx.isDev && ctx.isClient) {
        config.module.rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /(node_modules)/
        })
      }
    }
  }
}
