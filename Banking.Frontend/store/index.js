export const state = () => ({
  sidebar: false,
  snackbar: {}
})

export const mutations = {
  toggleSidebar (state) {
    state.sidebar = !state.sidebar
  },
  toggleSnackbar (state, snackbar) {
    state.snackbar = snackbar
    state.snackbar.open = !!snackbar.text
  }
}

export const getters = {
  snackbar (state) {
    return state.snackbar
  }
}
