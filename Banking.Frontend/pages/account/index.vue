<template>
  <v-container fluid>
    <v-layout row wrap justify-center>
      <v-flex xs12 sm8>
        <v-card>
          <v-toolbar color="primary">
            <v-toolbar-title title class="white--text">
              Account Balance: {{currentBalance | currency}}
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <transaction-dialog :balance="currentBalance" @update="updateHistory($event)"></transaction-dialog>
          </v-toolbar>
          <v-data-table :headers="headers" :items="history" class="elevation-1">
            <template slot="items" slot-scope="props">
              <td>{{ props.item.dateFormatted }}</td>
              <td>{{ (props.item.type == 1) ? 'Deposit' : 'Withdraw' }}</td>
              <td>{{ props.item.amount | currency}}</td>
              <td>{{ props.item.balance | currency }}</td>
            </template>
          </v-data-table>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from '~/plugins/axios'
import { mapGetters } from 'vuex'
import TransactionDialog from '@/components/account/TransactionDialog'

export default {
  components: {
    'transaction-dialog': TransactionDialog
  },
  computed: {
    ...mapGetters({
      user: 'account/user'
    }),
    currentBalance () {
      return (this.history[0]) ? this.history[0].balance : 0
    }
  },
  data () {
    return {
      headers: [
        { text: 'Date', sortable: false, value: 'date' },
        { text: 'Type', sortable: false, value: 'type' },
        { text: 'Amount', sortable: false, value: 'amount' },
        { text: 'Balance', sortable: false, value: 'balance' }
      ],
      history: []
    }
  },
  mounted () {
    this.getHistory()
  },
  methods: {
    getHistory () {
      axios.get(`api/account?email=${this.user.email}`).then(res => {
        this.history = res.data
      }).catch((e) => {
        console.log(e)
      })
    },
    updateHistory (tranasaction) {
      this.history.unshift(tranasaction)
    }
  }
}
</script>