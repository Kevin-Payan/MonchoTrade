<!-- App.vue -->
<template>
    <div class="min-h-screen bg-gray-50 p-6">
      <header class="mb-8">
        <nav class="flex justify-between items-center">
          <h1 class="text-2xl font-bold">Barter Exchange</h1>
          <div class="space-x-4">
            <router-link to="/profile" class="text-gray-600 hover:text-gray-900">My Profile</router-link>
            <router-link to="/browse" class="text-gray-600 hover:text-gray-900">Browse Items</router-link>
          </div>
        </nav>
      </header>
  
      <main>
        <SearchBar @search="handleSearch" />
        <FeaturedItems :items="featuredItems" />
      </main>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import SearchBar from './components/SearchBar.vue'
  import FeaturedItems from './components/FeaturedItems.vue'
  
  const featuredItems = ref([
    {
      id: 1,
      title: 'Vintage Camera',
      imageUrl: '/path/to/camera.jpg',
      offeredBy: 'Alice',
      description: 'Classic film camera in excellent condition'
    },
    {
      id: 2,
      title: 'Mountain Bike',
      imageUrl: '/path/to/bike.jpg',
      offeredBy: 'Bob',
      description: 'Well-maintained mountain bike, perfect for trails'
    }
    // Add more items as needed
  ])
  
  const handleSearch = (query) => {
    // Implement search logic
    console.log('Searching for:', query)
  }
  </script>
  
  
 