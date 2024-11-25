<script setup>
import Navbar from '@/components/Navbar.vue';
import { ref, onMounted } from 'vue'
import SearchBar from '@/components/SearchBar.vue'
import FeaturedItems from '@/components/FeaturedItems.vue'
import { appsettings } from "../../settings/appsettings";
import axios from 'axios';
<<<<<<< Updated upstream
  const axiosConfig = {
  headers: {
    "Content-Type": "application/json",
  },
  };
=======


>>>>>>> Stashed changes


  const featuredItems = ref([]); 

<<<<<<< Updated upstream
  const fetchProducts = async () => {
    try {
        const response = await axios.get(`${appsettings.apiUrl}${appsettings.productRoute}`,axiosConfig); // Replace with your actual API endpoint
        featuredItems.value = response.data.map(product => ({
        id: product.id,
        title: product.title,
        imageUrl: appsettings.apiUrl + "/uploads/products/" +  product.imageUrl || '/default.jpg', // Fallback if imageUrl is missing
        offeredBy: product.offeredBy,
        description: product.description,
        category: product.category
      }));
=======
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
>>>>>>> Stashed changes

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
