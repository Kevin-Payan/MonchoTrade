<template>
  <div class="min-h-screen bg-gray-50">
    <header class="bg-white shadow">
      <div class="container mx-auto px-4 py-6">
        <div class="flex justify-between items-center">
          <h1 class="text-2xl font-bold text-gray-900">Mis Productos para Intercambio</h1>
        </div>
        
        <div>
          <button 
            @click="openAddProductCreateModal"
            class="px-4 py-2 bg-black text-white rounded-md"
          >
            Add Product
          </button>
          
          <ProductCreateModal 
            :visible="showProductCreateModal"
            @close="showProductCreateModal = false"
            @product-created="handleProductCreated"
          />
        </div>
      </div>
    </header>

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

    <ProductDetailsModal
      :visible="showProductDetails"
      :product="selectedProduct"
      @close="closeProductDetails"
      @edit="openEditProductModal(selectedProduct)"
      @changeImage="handleChangeImage"
      @delete="handleDelete"
      @toggleStatus="handleToggleStatus"
    />

    <ProductEditModal
      :visible="showProductEditModal"
      :product-to-edit="productToEdit"
      @close="showProductEditModal = false"
      @product-updated="handleProductUpdated"
    />

    <!-- Modal para Actualizar Imagen -->
    <div v-if="showUpdateImageModal" class="modal-backdrop">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="text-lg font-medium">Actualizar Imagen</h3>
          <button @click="closeUpdateModal" class="text-gray-500 hover:text-gray-700">
            <i class="fas fa-times"></i>
          </button>
        </div>
        <form @submit.prevent="updateProductImage" class="p-6 space-y-4">
          <div class="form-group">
            <label class="label">Nueva Imagen</label>
            <input 
              type="file" 
              @change="handleImageUpload"
              accept="image/*"
              class="input"
              required
            >
          </div>
          
          <div class="flex justify-end gap-4">
            <button 
              type="button" 
              @click="closeUpdateImageModal" 
              class="px-4 py-2 bg-gray-200 text-gray-700 rounded-md hover:bg-gray-300"
            >
              Cancelar
            </button>
            <button 
              type="submit" 
              class="px-4 py-2 bg-black text-white rounded-md hover:bg-gray-800"
            >
              Guardar
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import { appsettings } from '../../settings/appsettings'
import ProductCreateModal from '@/components/ProductCreateModal.vue'
import ProductEditModal from '@/components/ProductEditModal.vue'
import ProductDetailsModal from '@/components/ProductDetailsModal.vue'
//Create Modal
const showProductCreateModal = ref(false)

//Edit Modal
const showProductEditModal = ref(false)

//Details Modal
const showProductDetails = ref(false)

//Images Modal
const showUpdateImageModal = ref(false)

//Details modal
const selectedProduct = ref(null)

//Edit Modal
const productToEdit = ref(null)

//Fetch products
const products = ref([])

// Cargar productos al montar el componente



const fetchProducts = async () => {
  try {
    const userId = localStorage.getItem('userId')
    const response = await axios.get(`${appsettings.apiUrl}/user/products/${userId}`)
    products.value = response.data
  } catch (error) {
    console.error('Error fetching products:', error)
  }
}

const openAddProductCreateModal = () => {
  showProductCreateModal.value = true
}

const openProductDetails = (product) => {
  selectedProduct.value = product
  showProductDetails.value = true
}

const closeProductDetails = () => {
  showProductDetails.value = false
  selectedProduct.value = null
}

const openEditProductModal = (product) => {
  productToEdit.value = product
  console.log(productToEdit.value)
  showProductEditModal.value = true
}

const handleProductCreated = (product) => {

  console.log(product)

  /*
  try{
    await axios.post(`${appsettings.apiUrl}${appsettings.productRoute}`, product); 
  }catch(error){

  }*/
  fetchProducts()
}

const handleProductUpdated = () => {
  fetchProducts()
}

const handleChangeImage = (product) => {
  selectedProduct.value = product
  showUpdateImageModal.value = true
  closeProductDetails()
}

const handleDelete = async (product) => {
  if (confirm('¿Estás seguro de que deseas eliminar este producto?')) {
    try {
      await axios.delete(`${appsettings.apiUrl}/products/${product.id}`)
      fetchProducts()
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

//Imagen Modal

const handleImageUpload = (event) => {
  const file = event.target.files[0]
  if (file) {
    const imageUrl = URL.createObjectURL(file)
    if (selectedProduct.value) {
      selectedProduct.value.imageUrl = imageUrl
    } else {
      productForm.value.imageUrl = imageUrl
    }
  }
}

const closeUpdateImageModal = () => {
  showUpdateImageModal.value = false
  selectedProduct.value = null
}

const updateProductImage = async () => {
  try {
    await axios.patch(`/api/products/${selectedProduct.value.id}`, {
      imageUrl: selectedProduct.value.imageUrl
    })
    fetchProducts()
    closeUpdateImageModal()
  } catch (error) {
    console.error('Error updating product image:', error)
  }
}

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
  @apply border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:border-transparent;
}
</style>
