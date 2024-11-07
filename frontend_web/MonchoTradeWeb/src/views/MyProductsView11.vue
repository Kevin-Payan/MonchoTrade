<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Header remains the same -->
    <header class="bg-white shadow">
      <div class="container mx-auto px-4 py-6">
        <div class="flex justify-between items-center">
          <h1 class="text-2xl font-bold text-gray-900">Mis Productos para Intercambio</h1>
        </div>
        
        <!-- Add Product Button and Modal -->
        <div>
          <button 
            @click="openAddProductModal"
            class="px-4 py-2 bg-black text-white rounded-md"
          >
            Add Product
          </button>
          
          <ProductModal 
            ref="productModalRef"
            @product-created="handleProductCreated"
          />
        </div>
      </div>
    </header>

    <!-- Replace the table with a grid of cards that open the modal -->
    <main class="container mx-auto px-4 py-8">
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div v-for="product in products" 
             :key="product.id" 
             @click="openProductDetails(product)"
             class="bg-white rounded-lg shadow hover:shadow-lg transition-shadow duration-200 cursor-pointer"
        >
          <div class="p-4">
            <div class="flex items-center">
              <img 
                :src="product.imageUrl || '/api/placeholder/100/100'"
                :alt="product.name"
                class="w-16 h-16 rounded-lg object-cover"
              >
              <div class="ml-4">
                <h3 class="font-medium text-gray-900">{{ product.name }}</h3>
                <p class="text-sm text-gray-500 truncate">{{ product.description || 'Sin descripción' }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>

    <!-- Product Details Modal -->
    <ProductDetailsModal
      :visible="showProductDetails"
      :product="selectedProduct"
      @close="closeProductDetails"
      @edit="handleEdit"
      @changeImage="handleChangeImage"
      @delete="handleDelete"
      @toggleStatus="handleToggleStatus"
    />


    <!-- Modal para Actualizar Imagen -->
    <ImageUpload
  v-if="showUpdateModal"
  :visible="showUpdateModal"
  :current-image="selectedProduct?.imageUrl"
  @close="showUpdateModal = false"
  @update="handleImageSubmit"
/>


  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import { appsettings } from '../../settings/appsettings'
import ProductModal from '@/components/ProductModal.vue'
import ProductDetailsModal from '@/components/ProductDetailsModal.vue'
import ImageUpload from '@/components/ImageUpload.vue'  // Add this import

//Funciones de ProductModal
const productModalRef = ref(null)

const openAddProductModal = () => {
  productModalRef.value.openAddModal()
}

const handleProductCreated = () => {
  // You'll need to implement fetchProducts()
  fetchProducts() 
}

//Ref para product modal 
const showProductDetails = ref(false)
const selectedProduct = ref(null)

// Estado
const products = ref([])
const showUpdateModal = ref(false)

// Formulario para nuevo producto
const productForm = ref({
  name: '',
  quantity: 0,
  imageUrl: null,
  isActive: true
})


//Metodo para modal imagenes 
const handleImageSubmit = async (formData) => {
  try {
    const response = await axios.post(
      `${appsettings.apiUrl}/products/${selectedProduct.value.id}/image`,
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }
    )
    
    // Update the product's image URL
    selectedProduct.value.imageUrl = response.data.imageUrl
    
    // Refresh products list
    await fetchProducts()
    
    // Close modal
    closeUpdateModal()
  } catch (error) {
    console.error('Error uploading image:', error)
    // You might want to show an error notification here
  }
}


// New methods for product details modal
const openProductDetails = (product) => {
  selectedProduct.value = product
  showProductDetails.value = true
}

const closeProductDetails = () => {
  showProductDetails.value = false
  selectedProduct.value = null
}

const handleEdit = (product) => {
  openUpdateModal(product)
  closeProductDetails()
}

const handleChangeImage = (product) => {
  selectedProduct.value = product
  showUpdateModal.value = true
}

const handleDelete = async (product) => {
  if (confirm('¿Estás seguro de que deseas eliminar este producto?')) {
    try {
      await axios.delete(`${appsettings.apiUrl}/products/${product.id}`)
      await fetchProducts()
      closeProductDetails()
    } catch (error) {
      console.error('Error deleting product:', error)
    }
  }
}

const handleToggleStatus = async (product) => {
  try {
    await axios.patch(`${appsettings.apiUrl}/products/${product.id}`, {
      isActive: !product.isActive
    })
    product.isActive = !product.isActive
  } catch (error) {
    console.error('Error toggling product status:', error)
  }
}



// Métodos
const fetchProducts = async () => {
  try {
    // Asumiendo que tienes el ID del usuario en localStorage o similar
    const userId = localStorage.getItem('userId')
    const response = await axios.get(`${appsettings.apiUrl}/user/products/${userId}`)
    products.value = response.data
    console.log(products.value)
  } catch (error) {
    console.error('Error fetching products:', error)
  }
}

const openUpdateModal = (product) => {
  selectedProduct.value = product
  showUpdateModal.value = true
}

const closeUpdateModal = () => {
  showUpdateModal.value = false
  selectedProduct.value = null
}


// Cargar productos al montar el componente
onMounted(() => {
  fetchProducts()
})


</script>

<style scoped>
.table-header {
  @apply px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider;
}

.table-cell {
  @apply px-6 py-4 whitespace-nowrap text-sm text-gray-900;
}

.modal-backdrop {
  @apply fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50;
}

.modal-content {
  @apply bg-white rounded-lg shadow-xl w-full max-w-2xl mx-4;
}

.modal-header {
  @apply flex justify-between items-center p-6 border-b border-gray-200;
}

.form-group {
  @apply flex flex-col;
}

.label {
  @apply text-sm font-medium text-gray-700 mb-1;
}

.input {
  @apply border border-gray-300 rounded-md px-3 py-2 focus:outline-none 
         focus:ring-2 focus:ring-gray-500 focus:border-transparent;
}
</style>