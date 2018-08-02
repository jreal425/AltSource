<template>
  <v-snackbar v-model="snackbar" :color="color" :bottom="y === 'bottom'" :left="x === 'left'" :multi-line="mode === 'multi-line'" :right="x === 'right'" :timeout="timeout" :top="y === 'top'" :vertical="mode === 'vertical'">
    <div class="text">
      <slot></slot>
      <v-btn flat color="white" @click.native="snackbar = false" class="close-button">
        <v-icon>close</v-icon>
      </v-btn>
    </div>
  </v-snackbar>
</template>

<script>
export default {
  props: {
    y: {
      type: String,
      required: false,
      default: 'bottom' // top, bottom
    },
    x: {
      type: String,
      required: false,
      default: null // left, right
    },
    timeout: {
      type: Number,
      required: false,
      default: 5000
    },
    mode: {
      type: String,
      required: false,
      default: '' // multi-line, vertical
    },
    color: {
      type: String,
      required: false,
      default: 'primary'
    },
    open: Boolean
  },
  data () {
    return {
      snackbar: false
    }
  },
  watch: {
    open (val) {
      if (val === true) {
        this.snackbar = true
      } else {
        this.snackbar = false
      }
    },
    snackbar (val) {
      if (val === false) {
        this.$emit('close')
      }
    }
  }
}
</script>