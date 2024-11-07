<!-- src/components/ProductDetailsModal.vue -->
<template>
    <div v-if="visible" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div class="bg-white w-11/12 max-w-3xl p-6 rounded-lg shadow-lg">
        <div class="flex justify-between items-center">
          <h3 class="text-lg font-semibold">{{ product.name || 'Producto' }}</h3>
          <button @click="closeModal" class="text-gray-600 hover:text-gray-900">✕</button>
        </div>
        
        <div class="flex flex-col md:flex-row mt-4">
          <!-- Imagen del producto -->
          <div class="md:w-1/3 flex justify-center items-center">
            <img 
              :src="product.imageUrl || '/api/placeholder/100/100'"
              :alt="product.name"
              class="w-32 h-32 rounded-lg object-cover"
            />
          </div>
  
          <!-- Detalles del producto -->
          <div class="md:w-2/3 mt-4 md:mt-0 md:ml-6">
            <p><strong>Descripción:</strong> {{ product.description || 'Sin descripción' }}</p>
            <p><strong>Categoría:</strong> {{ product.category }}</p>
            <p><strong>Cantidad:</strong> {{ product.quantity }} unidades</p>
            <p><strong>Fecha de Creación:</strong> {{ formatDate(product.createdAt) }}</p>
            <p><strong>Última Actualización:</strong> {{ formatDate(product.updatedAt) || 'No actualizado' }}</p>
            
            <div class="flex items-center mt-4">
              <label class="relative inline-flex items-center cursor-pointer">
                <input 
                  type="checkbox"
                  class="sr-only peer"
                  :checked="product.isActive"
                  @change="toggleProductStatus"
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
          </div>
        </div>
  
        <!-- Acciones -->
        <div class="flex justify-end gap-4 mt-6">
          <button @click="editProduct" class="px-4 py-2 bg-blue-600 text-white rounded-lg">Editar</button>
          <button @click="changeImage" class="px-4 py-2 bg-gray-600 text-white rounded-lg">Cambiar Imagen</button>
          <button @click="deleteProduct" class="px-4 py-2 bg-red-600 text-white rounded-lg">Eliminar</button>
        </div>
      </div>
    </div>
  </template>
  
  <!-- src/components/ProductDetailsModal.vue -->
<script setup>
const props = defineProps({
  visible: Boolean,
  product: {
    type: Object,
    default: () => ({
      name: '',
      description: '',
      category: '',
      quantity: 0,
      createdAt: '',
      updatedAt: '',
      imageUrl: '',
      isActive: true,
    }),
  },
})

const emit = defineEmits(['close', 'edit', 'changeImage', 'delete', 'toggleStatus'])

const closeModal = () => {
  emit('close')
}

const editProduct = () => {
  emit('edit', props.product)
}

const changeImage = () => {
  emit('changeImage', props.product)
}

const deleteProduct = () => {
  emit('delete', props.product)
}

const toggleProductStatus = () => {
  emit('toggleStatus', props.product)
}

const formatDate = (date) => {
  return date ? new Date(date).toLocaleDateString() : ''
}
</script>
  
  <style scoped>
  /* Añade los estilos necesarios */
  </style>
  