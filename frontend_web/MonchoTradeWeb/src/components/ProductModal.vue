<template>
    <div v-if="showAddModal" class="modal-backdrop">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="text-lg font-medium">{{ editingProduct ? 'Editar Producto' : 'Agregar Nuevo Producto' }}</h3>
          <button @click="closeAddModal" class="text-gray-500 hover:text-gray-700">
            <i class="fas fa-times"></i>
          </button>
        </div>
        <form @submit.prevent="saveProduct" class="p-6 space-y-4">
          <div class="form-group">
            <label class="label">Nombre del Producto *</label>
            <input 
              v-model="productForm.name"
              type="text" 
              class="input"
              required
              maxlength="100"
              placeholder="Nombre del producto (máx. 100 caracteres)"
            >
          </div>
  
          <div class="form-group">
            <label class="label">Categoría *</label>
            <select 
              v-model="productForm.category" 
              class="input"
              required
            >
              <option value="">Selecciona una categoría</option>
              <option v-for="category in categories" :key="category" :value="category">
                {{ category }}
              </option>
            </select>
          </div>
  
          <div class="form-group">
            <label class="label">Descripción</label>
            <textarea 
              v-model="productForm.description"
              class="input min-h-[100px]"
              maxlength="500"
              placeholder="Describe tu producto (máx. 500 caracteres)"
            ></textarea>
            <span class="text-xs text-gray-500">
              {{ productForm.description.length }}/500 caracteres
            </span>
          </div>
  
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
  
          <div class="form-group">
            <label class="label">Imágenes</label>
            <input 
              type="file" 
              @change="handleImageUpload"
              accept="image/*"
              multiple
              class="input"
            >
            <div v-if="selectedImages.length > 0" class="mt-2 flex gap-2 flex-wrap">
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
              {{ editingProduct ? 'Guardar Cambios' : 'Crear Producto' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, watch } from 'vue'
  
  const props = defineProps({
    showAddModal: Boolean,
    editingProduct: Object
  })
  
  const emit = defineEmits(['close', 'save'])
  
  // Categorías disponibles
  const categories = [
    'Electrónica',
    'Ropa',
    'Hogar',
    'Deportes',
    'Libros',
    'Otros'
  ]
  
  // Estado del formulario
  const productForm = ref({
    name: '',
    category: '',
    description: '',
    quantity: 0,
    isActive: true,
  })
  
  const selectedImages = ref([])
  
  // Observar cambios en editingProduct para actualizar el formulario
  watch(() => props.editingProduct, (newProduct) => {
    if (newProduct) {
      productForm.value = {
        name: newProduct.name,
        category: newProduct.category,
        description: newProduct.description || '',
        quantity: newProduct.quantity,
        isActive: newProduct.isActive
      }
    } else {
      resetForm()
    }
  }, { immediate: true })
  
  const handleImageUpload = (event) => {
    const files = Array.from(event.target.files)
    files.forEach(file => {
      const reader = new FileReader()
      reader.onload = (e) => {
        selectedImages.value.push({
          file: file,
          preview: e.target.result
        })
      }
      reader.readAsDataURL(file)
    })
  }
  
  const removeImage = (index) => {
    selectedImages.value.splice(index, 1)
  }
  
  const resetForm = () => {
    productForm.value = {
      name: '',
      category: '',
      description: '',
      quantity: 0,
      isActive: true
    }
    selectedImages.value = []
  }
  
  const closeAddModal = () => {
    resetForm()
    emit('close')
  }
  
  const saveProduct = () => {
    const formData = new FormData()
    
    // Agregar datos del producto
    Object.keys(productForm.value).forEach(key => {
      formData.append(key, productForm.value[key])
    })
    
    // Agregar imágenes
    selectedImages.value.forEach((image, index) => {
      formData.append(`images[${index}]`, image.file)
    })
  
    emit('save', formData)
  }
  </script>
  
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