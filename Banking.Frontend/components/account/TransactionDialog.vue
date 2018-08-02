<template>
  <v-dialog v-model="dialog" width="500">
    <v-btn slot="activator" icon>
      <v-icon color="white">edit</v-icon>
    </v-btn>

    <v-card>
      <v-toolbar color="primary">
        <v-toolbar-title title class="white--text">
          Make a Transaction
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn @click="resetDialog" icon>
          <v-icon color="white">close</v-icon>
        </v-btn>
      </v-toolbar>

      <v-card-text>Current Balance: {{balance | currency}}</v-card-text>
      <v-layout row wrap>
        <v-flex xs10 offset-xs1>
          <v-select v-model="transaction.type" :items="types" placeholder="Type" prepend-icon="compare_arrows" required></v-select>
        </v-flex>
        <v-flex xs10 offset-xs1>
          <v-text-field v-model="transaction.amount" type="number" min="1" :max="maxAmount" prepend-icon="attach_money"></v-text-field>
        </v-flex>
      </v-layout>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="success" flat @click="postTransaction" :disabled="!validAmount">
          I accept
        </v-btn>
        <v-btn color="error" flat @click="resetDialog">
          cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from '~/plugins/axios'
import { mapGetters } from 'vuex'

export default {
  props: {
    balance: Number
  },
  computed: {
    ...mapGetters({
      user: 'account/user'
    }),
    validAmount () {
      return (this.transaction.type === 1 && this.transaction.amount >= 1) || (this.transaction.type === 2 && this.transaction.amount <= this.balance && this.transaction.amount >= 1)
    },
    maxAmount () {
      return (this.transaction.type === 2) ? this.balance : null
    },
    selectedType () {
      return (this.transaction.type === 1) ? 'Deposit' : 'Withdraw'
    }
  },
  data () {
    return {
      dialog: false,
      total: this.balance,
      types: [
        {
          text: 'Deposit',
          value: 1
        },
        {
          text: 'Withdraw',
          value: 2
        }
      ],
      transaction: {
        type: 1,
        amount: 0
      },
      snackbar: {
        text: '',
        color: ''
      }
    }
  },
  methods: {
    postTransaction () {
      let request = {
        ...this.transaction,
        email: this.user.email
      }
      axios.post('api/account', request).then(res => {
        this.$emit('update', res.data)
        this.snackbar = {
          text: `${this.selectedType} was successful.`,
          color: 'success'
        }
      }).catch((e) => {
        this.snackbar = {
          text: `${this.selectedType} was unsuccessful.`,
          color: 'error'
        }
      }).then(() => {
        this.dialog = false
        this.$store.commit('toggleSnackbar', this.snackbar)
        this.resetDialog()
      })
    },
    resetDialog () {
      this.dialog = false
      this.transaction = {
        amount: 0,
        type: 1
      }
    }
  }
}
</script>