<script setup>
import { ref, reactive } from 'vue'
import { appsettings } from '../../settings/appsettings';
import axios from 'axios';

const props = defineProps({
  visible: {
    type: Boolean,
    required: true
  }
})

const emit = defineEmits(['close', 'product-created'])

// Form state
const productForm = reactive({
  title: '',
  description: '',
  quantity: 0,
  category: '',
  isActive: true,
})

// Images state
const selectedImages = ref([])

// Image upload handler
const handleImageUpload = (event) => {
  const files = Array.from(event.target.files)
  
  files.forEach(file => {
    if (file.type.startsWith('image/')) {
      selectedImages.value.push({
        file: file,
        preview: URL.createObjectURL(file)
      })
    }
  })
}

const removeImage = (index) => {
  if (selectedImages.value[index].preview.startsWith('blob:')) {
    URL.revokeObjectURL(selectedImages.value[index].preview)
  }
  selectedImages.value.splice(index, 1)
}

const resetForm = () => {
  productForm.title = ''
  productForm.description = ''
  productForm.quantity = 0
  productForm.category = ''
  productForm.isActive = true
  selectedImages.value = []
}

const closeModal = () => {
  resetForm()
  emit('close')
}

const saveProduct = async () => {
  try {
    const formData = new FormData()
    
    // Append product data
    const storedId = parseInt(localStorage.getItem("userId"), 10);

    formData.append('UserId', storedId)
    formData.append('Title', productForm.title)
    formData.append('Description', productForm.description)
    formData.append('Quantity', productForm.quantity)
    formData.append('Category', productForm.category)
    formData.append('IsActive', productForm.isActive)
    
    // Append images
    if (selectedImages.value.length > 0) {
      formData.append('ImageFile', selectedImages.value[0].file)
    }


     // Make API call
     const response = await axios.post(
      `${appsettings.apiUrl}${appsettings.productRoute}`, 
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }
    );

  // Optional: Check specific status code
  if (response.status !== 201 && response.status !== 200) {
      throw new Error(`Unexpected status code: ${response.status}`)
    }
    
    emit('product-created')
    alert('Producto creado exitosamente')
    closeModal()
    
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error('Axios error:', error.response?.data)
      alert(`Error al crear el producto: ${error.response?.data?.message || error.message}`)
    } else {
      console.error('Error saving product:', error)
      alert('Error al crear el producto')
    }
  }
}
</script>

<template>
  <div v-if="visible" class="modal-backdrop">
    <div class="modal-content">
      <div class="modal-header">
        <h3 class="text-lg font-medium">Agregar Nuevo Producto</h3>
        <button @click="closeModal" class="text-gray-500 hover:text-gray-700">
          <i class="fas fa-times"></i>
        </button>
      </div>

      <!-- Add the form content -->
      <form @submit.prevent="saveProduct" class="p-6 space-y-4">
        <!-- Nombre -->
        <div class="form-group">
          <label class="label">Nombre del Producto *</label>
          <input 
            v-model="productForm.title"
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
            {{ productForm.description.length }}/500 caracteres
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
            <option value="Clothing">Clothing</option>
            <option value="Accessories">Accessories</option>
            <option value="Electronics">Electronics</option>
            <option value="Kitchen">Kitchen</option>
            <option value="Home Decor">Home Decor</option>
            <option value="Footwear">Footwear</option>
            <option value="Office Supplies">Office Supplies</option>
            <option value="Fitness">Fitness</option>
            <option value="Other">Other</option>
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
            class="input"
          >
          <!-- Preview de imágenes -->
          <div v-if="selectedImages.length" class="mt-2 flex gap-2 flex-wrap">
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
            @click="closeModal" 
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
</template>

<style scoped>
.modal-backdrop {
  @apply fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50;
}

.modal-content {
  @apply bg-white rounded-lg shadow-xl w-full max-w-2xl mx-4 max-h-[90vh] overflow-y-auto;
}

.modal-header {
  @apply flex justify-between items-center p-6 border-b border-gray-200;
}

.form-group {
  @apply flex flex-col gap-2;
}

.label {
  @apply text-sm font-medium text-gray-700;
}

.input {
  @apply w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent;
}
</style>
