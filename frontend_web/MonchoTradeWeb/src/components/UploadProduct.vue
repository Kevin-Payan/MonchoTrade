<template>
    <div class="upload-product">
      <h2>Upload Product</h2>
      <form @submit.prevent="handleSubmit">
        <div>
          <label for="title">Title</label>
          <input
            type="text"
            id="title"
            v-model="product.title"
            required
            maxlength="100"
          />
        </div>
  
        <div >
          <label for="description">Description</label>
          <textarea
            id="description"
            v-model="product.description"
            required
            maxlength="500"
          ></textarea>
        </div>
  
        <div>
          <label for="category">Category</label>
          <input
            type="text"
            id="category"
            v-model="product.category"
            required
          />
        </div>
  
        <div>
          <label for="quantity">Quantity</label>
          <input
            type="number"
            id="quantity"
            v-model="product.quantity"
            required
            min="0"
          />
        </div>
  
        <div>
          <label for="images">Images</label>
          <input
            type="file"
            id="images"
            multiple
            @change="handleImageUpload"
            accept="image/*"
          />
          <div v-if="images.length > 0">
            <h4>Uploaded Images:</h4>
            <ul>
              <li v-for="(image, index) in images" :key="index">
                {{ image.name }}
              </li>
            </ul>
          </div>
        </div>
  
        <button type="submit">Upload Product</button>
      </form>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import axios from 'axios';
  
  const product = ref({
    title: '',
    description: '',
    category: '',
    quantity: 0,
  });
  
  const images = ref([]);
  
  const handleImageUpload = (event) => {
    images.value = Array.from(event.target.files); // Store uploaded images
  };
  
  const handleSubmit = async () => {
    const formData = new FormData();
    formData.append('title', product.value.title);
    formData.append('description', product.value.description);
    formData.append('category', product.value.category);
    formData.append('quantity', product.value.quantity);
    
    // Append all images to FormData
    images.value.forEach((image) => {
      formData.append('images', image); // Adjust this if your backend expects a different key
    });
  
    try {
      const response = await axios.post('http://localhost:5121/api/products', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      console.log('Product uploaded successfully:', response.data);
      // Reset form after successful upload
      resetForm();
    } catch (error) {
      console.error('Error uploading product:', error);
    }
  };
  
  const resetForm = () => {
    product.value = {
      title: '',
      description: '',
      category: '',
      quantity: 0,
    };
    images.value = [];
  };
  </script>
  
  <style scoped>
  .upload-product {
    max-width: 600px;
    margin: auto;
  }
  
  .upload-product h2 {
    text-align: center;
  }
  
  .upload-product form div {
    margin-bottom: 15px;
  }
  
  .upload-product label {
    display: block;
    margin-bottom: 5px;
  }
  
  .upload-product input,
  .upload-product textarea {
    border: 2px solid black;
    width: 100%;
    padding: 10px;
    box-sizing: border-box;
  }
  </style>
  