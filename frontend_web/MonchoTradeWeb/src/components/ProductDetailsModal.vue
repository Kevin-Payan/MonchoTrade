<template>
  <div v-if="visible && product" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
    <div class="bg-white w-11/12 max-w-3xl p-6 rounded-lg shadow-lg">
      <!-- Error Alert -->
      <div v-if="error" class="mb-4 bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative">
        {{ error }}
      </div>

      <div class="flex justify-between items-center">
        <h3 class="text-lg font-semibold">{{ product?.title || 'Producto' }}</h3>
        <button @click="closeModal" class="text-gray-600 hover:text-gray-900">✕</button>
      </div>

      <div class="flex flex-col md:flex-row mt-4">
        <!-- Product Image -->
        <div class="md:w-1/3 flex justify-center items-center">
          <img
            :src="product?.imageUrl || '/api/placeholder/100/100'"
            :alt="product?.title || 'Producto'"
            class="w-32 h-32 rounded-lg object-cover"
          />
        </div>

        <!-- Product Details -->
        <div class="md:w-2/3 mt-4 md:mt-0 md:ml-6">
          <p><strong>Descripción:</strong> {{ product?.description || 'Sin descripción' }}</p>
          <p><strong>Categoría:</strong> {{ product?.category || 'Sin categoría' }}</p>
          <p><strong>Cantidad:</strong> {{ product?.quantity || 0 }} unidades</p>
          <p><strong>Fecha de Creación:</strong> {{ formatDate(product?.createdAt) }}</p>
          <p><strong>Última Actualización:</strong> {{ formatDate(product?.updatedAt) || 'No actualizado' }}</p>

          <div class="flex items-center mt-4">
            <label class="relative inline-flex items-center cursor-pointer">
              <input
                type="checkbox"
                class="sr-only peer"
                :checked="product?.isActive"
                @change="toggleProductStatus"
              >
              <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full
                         peer-checked:after:translate-x-full peer-checked:after:border-white
                         after:content-[''] after:absolute after:top-[2px] after:left-[2px]
                         after:bg-white after:border-gray-300 after:border after:rounded-full
                         after:h-5 after:w-5 after:transition-all peer-checked:bg-black">
              </div>
            </label>
            <span class="ml-2">{{ product?.isActive ? 'Activo' : 'Inactivo' }}</span>
          </div>
        </div>
      </div>

      <!-- Actions -->
      <div class="flex justify-end gap-4 mt-6">
        <button 
          @click="editProduct" 
          class="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700"
          :disabled="loading"
        >
          Editar
        </button>
        <button 
          @click="changeImage" 
          class="px-4 py-2 bg-gray-600 text-white rounded-lg hover:bg-gray-700"
          :disabled="loading"
        >
          Cambiar Imagen
        </button>
        <button 
          @click="confirmDelete" 
          class="px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700"
          :disabled="loading"
        >
          Eliminar
        </button>
      </div>

      <!-- Delete Confirmation Modal -->
      <div v-if="showDeleteConfirm" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
        <div class="bg-white p-6 rounded-lg shadow-lg max-w-md w-full mx-4">
          <h4 class="text-lg font-semibold mb-4">Confirmar Eliminación</h4>
          <p class="mb-6">¿Estás seguro de que deseas eliminar el producto "{{ product?.title }}"? Esta acción no se puede deshacer.</p>
          
          <div class="flex justify-end gap-4">
            <button 
              @click="cancelDelete"
              class="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300"
              :disabled="loading"
            >
              Cancelar
            </button>
            <button 
              @click="deleteProduct"
              class="px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700"
              :disabled="loading"
            >
              <span v-if="loading" class="animate-spin mr-2">↻</span>
              Confirmar Eliminación
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { appsettings } from '../../settings/appsettings'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  product: {
    type: Object,
    required: true
  }
})

const loading = ref(false)
const error = ref(null)
const showDeleteConfirm = ref(false)

const emit = defineEmits(['close', 'edit', 'changeImage', 'product-deleted', 'toggleStatus'])

const closeModal = () => {
  showDeleteConfirm.value = false
  error.value = null
  emit('close')
}

const editProduct = () => {
  if (props.product) {
    emit('edit', props.product)
  }
}

const changeImage = () => {
  if (props.product) {
    emit('changeImage', props.product)
  }
}

const confirmDelete = () => {
  showDeleteConfirm.value = true
}

const cancelDelete = () => {
  showDeleteConfirm.value = false
}

const deleteProduct = async () => {
  if (!props.product?.id) return
  
  loading.value = true
  error.value = null
  
  try {
    await axios.delete(`${appsettings.apiUrl}${appsettings.productRoute}/${props.product.id}`, appsettings.axiosConfig)
    emit('product-deleted')
    closeModal()
  } catch (err) {
    error.value = err.response?.data?.error || 'Error al eliminar el producto'
    console.error('Error deleting product:', err)
  } finally {
    loading.value = false
    showDeleteConfirm.value = false
  }
}

const toggleProductStatus = () => {
  if (props.product) {
    emit('toggleStatus', props.product)
  }
}

const formatDate = (date) => {
  return date ? new Date(date).toLocaleDateString() : ''
}
</script>

<style scoped>
/* Your existing styles here */
</style>