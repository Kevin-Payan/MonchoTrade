<!-- components/FeaturedItems.vue -->
<template>
    <div>
      <h2 class="text-xl font-semibold mb-6">Featured Items</h2>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <ItemCard
          v-for="item in items"
          :key="item.id"
          v-bind="item"
          @propose-exchange="handleProposeExchange(item.id)"
        />
      </div>
    </div>
  </template>
  
  <script setup>
  import { defineProps } from 'vue'
  import ItemCard from './ItemCard.vue'
  
  const props = defineProps({
    items: {
      type: Array,
      required: true
    }
  })
  
  const handleProposeExchange = (itemId) => {
    // Implement exchange proposal logic
    console.log('Proposing exchange for item:', itemId)
  }
  </script>