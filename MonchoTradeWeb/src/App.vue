<script setup>
import { RouterView } from 'vue-router'
import { onMounted, onBeforeUnmount } from 'vue'
import authService from '@/services/authService'

onMounted(() => {
  authService.startTokenRefresh()
})

onBeforeUnmount(() => {
  authService.stopTokenRefresh()
})
</script>

<template>
  <RouterView />
</template>

<style scoped></style>
