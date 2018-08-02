<template>
  <v-container fluid>
    <v-layout row wrap justify-center>
      <v-flex xs12 sm6 md4 lg3>
        <v-card>
          <v-toolbar color="primary">
            <v-toolbar-title title class="white--text">
              Login
            </v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-container fluid>
            <v-form ref="form" v-model="isValid" lazy-validation>
              <v-layout row wrap>
                <v-flex xs12>
                  <v-text-field v-model="model.email" :rules="[rules.required, rules.email]" label="E-mail"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field v-model="model.password" :rules="[rules.required]" label="Password" type="password"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-card-text v-if="error" class="pa-0 error--text">{{error}}</v-card-text>
                </v-flex>
                <v-flex xs12 pa-0 ma-0>
                  <router-link to="/auth/register" class="pa-0">Don't have an account? Register here.</router-link>
                </v-flex>
              </v-layout>
            </v-form>
          </v-container>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn flat @click="login">Submit</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from '@/plugins/axios'

export default {
  layout: 'auth',
  data () {
    return {
      isValid: true,
      model: {
        email: '',
        password: ''
      },
      rules: {
        required: value => !!value || 'Required.',
        email: value => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
          return pattern.test(value) || 'Invalid e-mail.'
        }
      },
      error: null
    }
  },
  methods: {
    login () {
      if (this.$refs.form.validate()) {
        axios.post('api/auth/login', this.model).then(res => {
          this.$store.commit('account/user', res.data.user)
          this.$router.push('/account')
        }).catch((e) => {
          this.error = e.response.data
        })
      }
    }
  }
}
</script>