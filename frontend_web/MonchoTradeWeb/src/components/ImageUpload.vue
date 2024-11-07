<script setup>
import { ref } from 'vue'

const props = defineProps({
  visible: {
    type: Boolean,
    required: true
  },
  currentImage: {
    type: String,
    default: null
  }
})

const emit = defineEmits(['close', 'update'])

// Local state
const imagePreview = ref(props.currentImage)
const selectedFile = ref(null)
const isLoading = ref(false)
const errorMessage = ref('')

const handleFileSelect = (event) => {
  const file = event.target.files[0]
  if (file) {
    // Validate file type
    if (!file.type.startsWith('image/')) {
      errorMessage.value = 'Por favor selecciona un archivo de imagen válido'
      return
    }
    
    // Validate file size (e.g., 5MB max)
    const maxSize = 5 * 1024 * 1024 // 5MB in bytes
    if (file.size > maxSize) {
      errorMessage.value = 'La imagen no debe superar los 5MB'
      return
    }

    selectedFile.value = file
    imagePreview.value = URL.createObjectURL(file)
    errorMessage.value = ''
  }
}

const handleSubmit = async () => {
  if (!selectedFile.value) {
    errorMessage.value = 'Por favor selecciona una imagen'
    return
  }

  isLoading.value = true
  errorMessage.value = ''

  try {
    // Create FormData object for file upload
    const formData = new FormData()
    formData.append('image', selectedFile.value)

    emit('update', formData)
  } catch (error) {
    errorMessage.value = 'Error al subir la imagen. Por favor intenta de nuevo.'
    console.error('Error uploading image:', error)
  } finally {
    isLoading.value = false
  }
}

const closeModal = () => {
  // Clean up the object URL to prevent memory leaks
  if (imagePreview.value && imagePreview.value.startsWith('blob:')) {
    URL.revokeObjectURL(imagePreview.value)
  }
  imagePreview.value = null
  selectedFile.value = null
  errorMessage.value = ''
  emit('close')
}
</script>

<template>
  <div v-if="visible" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white rounded-lg shadow-xl w-full max-w-md mx-4">
      <!-- Header -->
      <div class="flex justify-between items-center p-6 border-b border-gray-200">
        <h3 class="text-lg font-medium text-gray-900">
          Actualizar Imagen
        </h3>
        <button 
          @click="closeModal"
          class="text-gray-400 hover:text-gray-500 focus:outline-none"
        >
          <span class="sr-only">Cerrar</span>
          <i class="fas fa-times"></i>
        </button>
      </div>

      <!-- Body -->
      <div class="p-6 space-y-6">
        <!-- Current Image Preview -->
        <div v-if="imagePreview" class="flex justify-center">
          <div class="relative group">
            <img 
              :src="imagePreview"
              alt="Preview"
              class="max-h-64 rounded-lg object-contain"
            />
            <div class="absolute inset-0 bg-black bg-opacity-40 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center rounded-lg">
              <button 
                @click="imagePreview = null; selectedFile = null"
                class="text-white hover:text-gray-200"
                title="Eliminar imagen"
              >
                <i class="fas fa-trash"></i>
              </button>
            </div>
          </div>
        </div>

        <!-- File Input -->
        <div class="space-y-2">
          <label class="block text-sm font-medium text-gray-700">
            Seleccionar Nueva Imagen
          </label>
          <div class="flex justify-center px-6 pt-5 pb-6 border-2 border-gray-300 border-dashed rounded-md">
            <div class="space-y-1 text-center">
              <i class="fas fa-cloud-upload-alt text-gray-400 text-3xl mb-3"></i>
              <div class="flex text-sm text-gray-600">
                <label class="relative cursor-pointer bg-white rounded-md font-medium text-black hover:text-gray-700">
                  <span>Subir archivo</span>
                  <input 
                    type="file"
                    @change="handleFileSelect"
                    accept="image/*"
                    class="sr-only"
                  >
                </label>
                <p class="pl-1">o arrastra y suelta</p>
              </div>
              <p class="text-xs text-gray-500">
                PNG, JPG, GIF hasta 5MB
              </p>
            </div>
          </div>
        </div>

        <!-- Error Message -->
        <div v-if="errorMessage" class="text-red-500 text-sm">
          {{ errorMessage }}
        </div>
      </div>

      <!-- Footer -->
      <div class="flex justify-end gap-3 px-6 py-4 bg-gray-50 rounded-b-lg">
        <button 
          @click="closeModal"
          class="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50 focus:outline-none"
        >
          Cancelar
        </button>
        <button 
          @click="handleSubmit"
          :disabled="isLoading || !selectedFile"
          class="px-4 py-2 text-sm font-medium text-white bg-black rounded-md shadow-sm hover:bg-gray-800 focus:outline-none disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <span v-if="isLoading">
            <i class="fas fa-spinner fa-spin mr-2"></i>
            Subiendo...
          </span>
          <span v-else>
            Guardar Imagen
          </span>
        </button>
      </div>
    </div>
  </div>
</template>