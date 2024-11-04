<script setup>
import Navbar from '@/components/Navbar.vue';
import { ref, computed, onMounted } from 'vue'
import SearchBar from '@/components/SearchBar.vue'
import FeaturedItems from '@/components/FeaturedItems.vue'
import { appsettings } from "../../settings/appsettings";
import axios from 'axios';

const axiosConfig = {
  headers: {
    "Content-Type": "application/json",
  },
};

// Pagination state
const currentPage = ref(1);
const itemsPerPage = ref(6);
const totalItems = ref(0);
const featuredItems = ref([]); 

// Computed properties for pagination
const totalPages = computed(() => Math.ceil(totalItems.value / itemsPerPage.value));
const startIndex = computed(() => (currentPage.value - 1) * itemsPerPage.value + 1);
const endIndex = computed(() => Math.min(startIndex.value + itemsPerPage.value - 1, totalItems.value));

const fetchProducts = async (page = 1) => {
  try {
    const response = await axios.get(
      `${appsettings.apiUrl}${appsettings.productRoute}`,
      {
        ...axiosConfig,
        params: {
          pageNumber: page,
          pageSize: itemsPerPage.value
        }
      }
    );

    console.log('API Response:', response.data); // Debug log

    featuredItems.value = response.data.items.map(product => ({
      id: product.id,
      title: product.title,
      imageUrl: product.imageUrl 
        ? `${appsettings.apiUrl}/uploads/products/${product.imageUrl}`
        : `${appsettings.apiUrl}/uploads/products/default.jpg`,
      offeredBy: product.offeredBy || 'Unknown',
      description: product.description || '',
      category: product.category
    }));

    totalItems.value = response.data.totalItems;
    
    console.log('Mapped Items:', featuredItems.value); // Debug log
  } catch (error) {
    console.error('Error fetching products:', error);
  } finally {
  }
};
// Handle page changes
const handlePageChange = async (newPage) => {
  if (newPage >= 1 && newPage <= totalPages.value) {
    currentPage.value = newPage;
    await fetchProducts(newPage);
    // Scroll to top of products section
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
};

onMounted(() => {
  fetchProducts(1);
});

const handleSearch = (query) => {
  // Implement search logic
  console.log('Searching for:', query);
  // Reset pagination when searching
  currentPage.value = 1;
  // You might want to add the search query to your fetchProducts call
};
</script>

<template>
  <main>
    <div>
      <Navbar />
      <SearchBar @search="handleSearch" />
      <FeaturedItems 
        :items="featuredItems" 
        :current-page="currentPage"
        :total-pages="totalPages"
        :total-items="totalItems"
        :items-per-page="itemsPerPage"
        :start-index="startIndex"
        :end-index="endIndex"
        @page-change="handlePageChange"
      />

      <!-- Pagination Controls -->
      <div class="mt-8 flex items-center justify-between max-w-7xl mx-auto px-4">
        <div class="text-sm text-gray-700">
          Showing {{ startIndex }} - {{ endIndex }} of {{ totalItems }} items
        </div>
        
        <div class="flex gap-2">
          <button
            @click="handlePageChange(currentPage - 1)"
            :disabled="currentPage === 1"
            class="px-4 py-2 border rounded-md hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            Previous
          </button>
          
          <div class="flex gap-1">
            <button
              v-for="page in totalPages"
              :key="page"
              @click="handlePageChange(page)"
              class="px-4 py-2 border rounded-md hover:bg-gray-50"
              :class="{'bg-blue-50 border-blue-500': currentPage === page}"
            >
              {{ page }}
            </button>
          </div>

          <button
            @click="handlePageChange(currentPage + 1)"
            :disabled="currentPage === totalPages"
            class="px-4 py-2 border rounded-md hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            Next
          </button>
        </div>
      </div>
    </div>
  </main>
</template>