<!-- components/SearchBar.vue -->
<template>
    <div class="mx-auto mb-8">
      <h2 class="text-xl font-normal mb-4 text-center">Find Items to Exchange</h2>      
      <form @submit.prevent="handleSearch" class="space-y-2">
        <!-- Input Container -->
        <div class="relative">
          <input
            v-model="searchQuery"
            type="text"
            placeholder="Search for items..."
            class="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:border-gray-400"
          >
        </div>
  
        <!-- Search Button -->
        <button
          type="submit"
          class="w-full bg-black text-white py-3 px-6 rounded-md hover:bg-gray-800 transition-colors duration-200"
        >
          Search
        </button>
      </form>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  
  const emit = defineEmits(['search'])
  const searchQuery = ref('')
  
  const handleSearch = () => {
    emit('search', searchQuery.value)
  }
  </script>