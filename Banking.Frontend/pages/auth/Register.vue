<template>
  <v-container>
    <v-layout row wrap justify-center>
      <v-flex xs12 sm6>
        <v-card>
          <v-toolbar color="primary">
            <v-toolbar-title title class="white--text">
              Register
            </v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-container fluid>
            <v-form ref="form" v-model="isValid" lazy-validation>
              <v-layout row wrap>
                <v-flex xs6 pr-2>
                  <v-text-field v-model="model.firstName" :rules="[rules.required]" label="First Name"></v-text-field>
                </v-flex>
                <v-flex xs6 pl-2>
                  <v-text-field v-model="model.lastName" :rules="[rules.required]" label="Last Name"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field v-model="model.email" :rules="[rules.required, rules.email]" label="E-mail"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field v-model="model.password" :rules="[rules.required]" label="Password" type="password"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field v-model="confirmPassword" :rules="[rules.confirmPassword]" label="Confirm Password" type="password"></v-text-field>
                </v-flex>
                <v-flex xs12>
                </v-flex>
                <v-flex xs12 pa-0 ma-0>
                  <v-card-text v-if="error" class="pa-0 error--text">{{error}}</v-card-text>
                  <router-link to="/auth/login" class="pa-0">Already have an account? Login here.</router-link>
                </v-flex>
              </v-layout>
            </v-form>
          </v-container>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn flat @click="register">Submit</v-btn>
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
      model: {
        email: '',
        password: '',
        firstName: '',
        lastName: ''
      },
      confirmPassword: '',
      error: null,
      isValid: true,
      rules: {
        required: value => !!value || 'Required.',
        email: value => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
          return pattern.test(value) || 'Invalid e-mail.'
        },
        confirmPassword: value => {
          const match = value === this.model.password
          return match || 'Password does not match.'
        }
      }
    }
  },
  methods: {
    register () {
      if (this.$refs.form.validate()) {
        axios.post('api/auth/register', this.model).then(res => {
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