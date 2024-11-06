<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Header -->
    <header class="bg-white shadow">
      <div class="container mx-auto px-4 py-6">
        <div class="flex justify-between items-center">
          <h1 class="text-2xl font-bold text-gray-900">Mis Productos para Intercambio</h1>
          <button 
            @click="showAddModal = true"
            class="bg-black text-white px-4 py-2 rounded-md hover:bg-gray-800 
                   transition-colors duration-200"
          >
            Agregar Producto
          </button>
        </div>
      </div>
    </header>

<!-- Lista de Productos -->
<main class="container mx-auto px-4 py-8">
  <div class="bg-white rounded-lg shadow overflow-hidden">
    <table class="min-w-full">
      <thead class="bg-gray-50">
        <tr>
          <th class="table-header">Producto</th>
          <th class="table-header">Descripción</th>
          <th class="table-header">Categoría</th>
          <th class="table-header">Cantidad</th>
          <th class="table-header">Fecha Creación</th>
          <th class="table-header">Última Actualización</th>
          <th class="table-header">Estado</th>
          <th class="table-header">Acciones</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-gray-200">
        <tr v-for="product in products" :key="product.id" class="hover:bg-gray-50">
          <!-- Nombre e Imagen -->
          <td class="px-6 py-4">
            <div class="flex items-center">
              <img 
                :src="product.imageUrl || '/api/placeholder/100/100'"
                :alt="product.name"
                class="w-16 h-16 rounded-lg object-cover"
              >
              <div class="ml-4 font-medium text-gray-900">
                {{ product.name }}
              </div>
            </div>
          </td>

          <!-- Descripción -->
          <td class="table-cell">
            <div class="max-w-xs overflow-hidden">
              <p class="text-sm text-gray-500 truncate" :title="product.description">
                {{ product.description || 'Sin descripción' }}
              </p>
            </div>
          </td>

          <!-- Categoría -->
          <td class="table-cell">
            <span class="inline-flex px-2 text-xs font-semibold rounded-full bg-gray-100 text-gray-800">
              {{ product.category }}
            </span>
          </td>

          <!-- Cantidad -->
          <td class="table-cell">
            {{ product.quantity }} unidades
          </td>

          <!-- Fecha de Creación -->
          <td class="table-cell">
            <div class="text-sm text-gray-500">
              {{ new Date(product.createdAt).toLocaleDateString() }}
              <br>
              {{ new Date(product.createdAt).toLocaleTimeString() }}
            </div>
          </td>

          <!-- Última Actualización -->
          <td class="table-cell">
            <div class="text-sm text-gray-500">
              {{ product.updatedAt ? new Date(product.updatedAt).toLocaleDateString() : 'No actualizado' }}
              {{ product.updatedAt ? new Date(product.updatedAt).toLocaleTimeString() : '' }}
            </div>
          </td>

          <!-- Estado -->
          <td class="table-cell">
            <div class="flex items-center">
              <label class="relative inline-flex items-center cursor-pointer">
                <input 
                  type="checkbox"
                  class="sr-only peer"
                  :checked="product.isActive"
                  @change="toggleProductStatus(product)"
                >
                <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full 
                          peer-checked:after:translate-x-full peer-checked:after:border-white 
                          after:content-[''] after:absolute after:top-[2px] after:left-[2px] 
                          after:bg-white after:border-gray-300 after:border after:rounded-full 
                          after:h-5 after:w-5 after:transition-all peer-checked:bg-black">
                </div>
              </label>
              <span class="ml-2">{{ product.isActive ? 'Activo' : 'Inactivo' }}</span>
            </div>
          </td>

          <!-- Acciones -->
          <td class="table-cell">
            <div class="flex gap-2">
              <button 
                @click="openUpdateModal(product)"
                class="p-2 hover:bg-gray-100 rounded-md transition-colors duration-200"
                title="Editar producto"
              >
                <i class="fas fa-edit"></i>
              </button>
              <button 
                @click="openUpdateModal(product)"
                class="p-2 hover:bg-gray-100 rounded-md transition-colors duration-200"
                title="Cambiar imagen"
              >
                <i class="fas fa-image"></i>
              </button>
              <button 
                @click="deleteProduct(product)"
                class="p-2 hover:bg-gray-100 rounded-md transition-colors duration-200 text-red-600"
                title="Eliminar producto"
              >
                <i class="fas fa-trash"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</main>

    <!-- Modal para Actualizar Imagen -->
    <div v-if="showUpdateModal" class="modal-backdrop">
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
              @click="closeUpdateModal" 
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

   <!-- Modal para Agregar Producto -->
<div v-if="showAddModal" class="modal-backdrop">
  <div class="modal-content">
    <div class="modal-header">
      <h3 class="text-lg font-medium">Agregar Nuevo Producto</h3>
      <button @click="closeAddModal" class="text-gray-500 hover:text-gray-700">
        <i class="fas fa-times"></i>
      </button>
    </div>
    <form @submit.prevent="saveProduct" class="p-6 space-y-4">
      <!-- Nombre -->
      <div class="form-group">
        <label class="label">Nombre del Producto *</label>
        <input 
          v-model="productForm.name"
          type="text" 
          class="input"
          required
          maxlength="100"
          placeholder="Máximo 100 caracteres"
        >
      </div>

      <!-- Descripción -->
      <div class="form-group">
        <label class="label">Descripción</label>
        <textarea 
          v-model="productForm.description"
          class="input min-h-[100px]"
          maxlength="500"
          placeholder="Máximo 500 caracteres"
        ></textarea>
        <span class="text-xs text-gray-500">
          {{ productForm.description }}/500 caracteres
        </span>
      </div>

      <!-- Cantidad -->
      <div class="form-group">
        <label class="label">Cantidad *</label>
        <input 
          v-model.number="productForm.quantity"
          type="number" 
          min="0"
          class="input"
          required
        >
      </div>

      <!-- Categoría -->
      <div class="form-group">
        <label class="label">Categoría *</label>
        <select 
          v-model="productForm.category"
          class="input"
          required
        >
          <option value="">Selecciona una categoría</option>
          <option value="Electrónica">Electrónica</option>
          <option value="Ropa">Ropa</option>
          <option value="Hogar">Hogar</option>
          <option value="Deportes">Deportes</option>
          <option value="Libros">Libros</option>
          <option value="Otros">Otros</option>
        </select>
      </div>

      <!-- Estado Activo -->
      <div class="form-group">
        <label class="flex items-center space-x-2">
          <input 
            type="checkbox"
            v-model="productForm.isActive"
            class="form-checkbox h-4 w-4 text-black rounded border-gray-300"
          >
          <span class="text-sm text-gray-700">Producto Activo</span>
        </label>
      </div>

      <!-- Imágenes -->
      <div class="form-group">
        <label class="label">Imágenes</label>
        <input 
          type="file"
          @change="handleImageUpload"
          accept="image/*"
          multiple
          class="input"
        >
        <!-- Preview de imágenes -->
        <div v-if="selectedImages" class="mt-2 flex gap-2 flex-wrap">
          <div 
            v-for="(image, index) in selectedImages" 
            :key="index"
            class="relative w-20 h-20"
          >
            <img 
              :src="image.preview"
              class="w-full h-full object-cover rounded-lg"
              alt="Preview"
            >
            <button 
              @click="removeImage(index)"
              class="absolute -top-2 -right-2 bg-red-500 text-white rounded-full w-5 h-5 flex items-center justify-center text-xs"
              type="button"
            >
              ×
            </button>
          </div>
        </div>
      </div>

      <div class="flex justify-end gap-4">
        <button 
          type="button" 
          @click="closeAddModal" 
          class="px-4 py-2 bg-gray-200 text-gray-700 rounded-md hover:bg-gray-300"
        >
          Cancelar
        </button>
        <button 
          type="submit" 
          class="px-4 py-2 bg-black text-white rounded-md hover:bg-gray-800"
        >
          Crear Producto
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
import { appsettings } from '../../settings/appsettings';
// Estado
const products = ref([])
const showUpdateModal = ref(false)
const showAddModal = ref(false)
const selectedProduct = ref(null)

// Formulario para nuevo producto
const productForm = ref({
  name: '',
  quantity: 0,
  imageUrl: null,
  isActive: true
})
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

const handleImageUpload = (event) => {
  const file = event.target.files[0]
  if (file) {
    // Aquí normalmente subirías la imagen a tu servidor
    // y obtendrías la URL para guardarla
    const imageUrl = URL.createObjectURL(file)
    if (selectedProduct.value) {
      selectedProduct.value.imageUrl = imageUrl
    } else {
      productForm.value.imageUrl = imageUrl
    }
  }
}

const toggleProductStatus = async (product) => {
  try {
    await axios.patch(`/api/products/${product.id}`, {
      isActive: !product.isActive
    })
    product.isActive = !product.isActive
  } catch (error) {
    console.error('Error toggling product status:', error)
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

const closeAddModal = () => {
  showAddModal.value = false
  productForm.value = {
  name: '',
  description: '',
  quantity: 0,
  category: '',
  isActive: true,
  imageUrl: null,
  }
}

const updateProductImage = async () => {
  try {
    await axios.patch(`/api/products/${selectedProduct.value.id}`, {
      imageUrl: selectedProduct.value.imageUrl
    })
    await fetchProducts()
    closeUpdateModal()
  } catch (error) {
    console.error('Error updating product image:', error)
  }
}

const saveProduct = async () => {
  try {
    const userId = localStorage.getItem('userId')
    await axios.post('/api/products', {
      ...productForm.value,
      userId
    })
    await fetchProducts()
    closeAddModal()
  } catch (error) {
    console.error('Error saving product:', error)
  }
}

const deleteProduct = async (product) => {
  if (confirm('¿Estás seguro de que deseas eliminar este producto?')) {
    try {
      await axios.delete(`/api/products/${product.id}`)
      await fetchProducts()
    } catch (error) {
      console.error('Error deleting product:', error)
    }
  }
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