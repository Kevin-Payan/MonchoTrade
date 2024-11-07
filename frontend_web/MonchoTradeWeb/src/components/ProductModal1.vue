<template>
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
</template>


<script setup>
import { ref, reactive } from 'vue'

// Modal visibility state
const showAddModal = ref(false)

// Form state
const productForm = reactive({
  name: '',
  description: '',
  quantity: 0,
  category: '',
  isActive: true,
})

// Images state
const selectedImages = ref([])

// Methods
const openAddModal = () => {
  showAddModal.value = true
}

const closeAddModal = () => {
  showAddModal.value = false
  resetForm()
}

const resetForm = () => {
  productForm.name = ''
  productForm.description = ''
  productForm.quantity = 0
  productForm.category = ''
  productForm.isActive = true
  selectedImages.value = []
}

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
  // Release the object URL to free memory
  URL.revokeObjectURL(selectedImages.value[index].preview)
  selectedImages.value.splice(index, 1)
}

const saveProduct = async () => {
  try {
    // Create FormData object to handle file uploads
    const formData = new FormData()
    
    // Append product data
    formData.append('name', productForm.name)
    formData.append('description', productForm.description)
    formData.append('quantity', productForm.quantity)
    formData.append('category', productForm.category)
    formData.append('isActive', productForm.isActive)
    
    // Append images
    selectedImages.value.forEach((image, index) => {
      formData.append(`images[${index}]`, image.file)
    })
    
    // Make API call
    const response = await fetch('/api/products', {
      method: 'POST',
      body: formData
    })
    
    if (!response.ok) {
      throw new Error('Error creating product')
    }
    
    // Emit event to refresh product list
    emit('product-created')
    
    // Show success message
    // You'll need to implement your notification system
    alert('Producto creado exitosamente')
    
    // Close modal and reset form
    closeAddModal()
    
  } catch (error) {
    console.error('Error saving product:', error)
    alert('Error al crear el producto')
  }
}

// Expose methods for parent component
defineExpose({
  openAddModal,
  closeAddModal
})
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 50;
}

.modal-content {
  background-color: white;
  border-radius: 0.5rem;
  width: 100%;
  max-width: 32rem;
  max-height: 90vh;
  overflow-y: auto;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 1.5rem;
  border-bottom: 1px solid #e5e7eb;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.label {
  font-size: 0.875rem;
  font-weight: 500;
  color: #374151;
}

.input {
  width: 100%;
  padding: 0.5rem;
  border: 1px solid #e5e7eb;
  border-radius: 0.375rem;
  font-size: 0.875rem;
}

.input:focus {
  outline: none;
  border-color: #374151;
  
}
</style>