<script setup>
import Navbar from '@/components/Navbar.vue';
import { ref, onMounted } from 'vue'
import SearchBar from '@/components/SearchBar.vue'
import FeaturedItems from '@/components/FeaturedItems.vue'
import { appsettings } from "../../settings/appsettings";
import axios from 'axios';




  const featuredItems = ref([]); 

const fetchProducts = async (page = 1) => {
  try {
    const response = await axios.get(
      `${appsettings.apiUrl}${appsettings.productRoute}`,
      {
        ...appsettings.axiosConfig,
        params: {
          pageNumber: page,
          pageSize: itemsPerPage.value
        }
      }
    );

      console.log(featuredItems.value.imageUrl); 
    } catch (error) {
      console.log("Error fetching products: ", error); 
    }
  };
  onMounted(() => {
  fetchProducts();
  });


  
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
