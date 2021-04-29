<template>
  <div id="app">
    <TheNavbar/>
    <TheHeader/>
    <TheContent/>
  </div>
</template>

<script>
import TheNavbar from  './components/layouts/TheNavbar.vue'
import TheHeader from  './components/layouts/TheHeader.vue'
import TheContent from  './components/layouts/TheContent.vue'

export default {
  name: 'App',
  components: {
    TheNavbar,
    TheHeader,
    TheContent
  }
}
</script>

<style>
@import './style/main.css';
</style>
