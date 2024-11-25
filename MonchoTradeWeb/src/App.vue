<script setup>
<<<<<<< Updated upstream
import { RouterView } from "vue-router";
=======
import { RouterView } from 'vue-router'
import { onMounted, onBeforeUnmount } from 'vue'
import authService from '@/services/authService'

onMounted(() => {
  authService.startTokenRefresh()
})

onBeforeUnmount(() => {
  authService.stopTokenRefresh()
})
>>>>>>> Stashed changes
</script>

<template>
  <RouterView />
</template>

<style scoped></style>
