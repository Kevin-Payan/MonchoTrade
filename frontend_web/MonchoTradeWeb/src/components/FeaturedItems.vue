<!-- components/FeaturedItems.vue -->
<template>
  <div>
    <h2 class="text-xl font-semibold mb-6">Featured Items</h2>
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
      <ItemCard
        v-for="item in items"
        :key="item.id"
        :title="item.title"
        :image-url="item.imageUrl"
        :offered-by="item.offeredBy"
        :description="item.description"
        :category="item.category"
        @propose-exchange="$emit('propose-exchange', item.id)"
      />
    </div>
  </div>
</template>

<script setup>
import ItemCard from './ItemCard.vue';

const props = defineProps({
  items: {
    type: Array,
    required: true
  },
  currentPage: {
    type: Number,
    required: true
  },
  totalPages: {
    type: Number,
    required: true
  },
  totalItems: {
    type: Number,
    required: true
  },
  itemsPerPage: {
    type: Number,
    required: true
  },
  startIndex: {
    type: Number,
    required: true
  },
  endIndex: {
    type: Number,
    required: true
  }
});

const emit = defineEmits(['page-change', 'propose-exchange']);
</script>