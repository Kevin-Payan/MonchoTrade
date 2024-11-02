<script setup>
import Navbar from '@/components/Navbar.vue';
import { ref } from 'vue'
  import SearchBar from '@/components/SearchBar.vue'
  import FeaturedItems from '@/components/FeaturedItems.vue'

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

<template>
  <main>
   
    <div>
    <Navbar />
    <SearchBar @search="handleSearch" />
    <FeaturedItems :items="featuredItems" />
  </div>
  </main>

</template>
