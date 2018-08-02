export const state = () => ({
  user: null
})

export const mutations = {
  user (state, user) {
    state.user = user
  }
}

export const getters = {
  user (state) {
    return state.user
  }
}
