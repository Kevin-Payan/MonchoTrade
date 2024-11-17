<script setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { appsettings } from '../../settings/appsettings';
import axios from 'axios';

const props = defineProps({
  productToEdit: {
    type: Object,
    required: true
  },
  visible: {
    type: Boolean,
    required: true
  }
})

const categories = ref([]);
const isLoadingCategories = ref(false);
const loading = ref(false);
const error = ref(null);

const emit = defineEmits(['close', 'product-updated'])

// Form state
const productForm = reactive({
  title: '',
  description: '',
  quantity: 0,
  category: '',
  isActive: true,
})

// Watch for changes in productToEdit to populate form
watch(() => props.visible, (newVal) => {
  if (newVal && props.productToEdit) {
    productForm.title = props.productToEdit.title
    productForm.description = props.productToEdit.description
    productForm.quantity = props.productToEdit.quantity
    productForm.category = props.productToEdit.category
    productForm.isActive = props.productToEdit.isActive

    if (categories.value.length > 0) {
      setDefaultCategory()
    }
  }
})

watch(() => categories.value, () => {
  if (props.visible && props.productToEdit && categories.value.length > 0) {
    setDefaultCategory()
  }
})

const closeModal = () => {
  error.value = null
  emit('close')
}

const setDefaultCategory = () => {
  if (props.productToEdit.category) {
    productForm.category = props.productToEdit.category
  }
}

const fetchCategories = async () => {
  isLoadingCategories.value = true;
  try {
    const response = await axios.get(`${appsettings.apiUrl}${appsettings.categoriesRoute}`);
    categories.value = response.data;
    
    if (props.visible && props.productToEdit) {
      setDefaultCategory()
    }
  } catch (error) {
    console.error('Error fetching categories:', error);
  } finally {
    isLoadingCategories.value = false;
  }
};

const updateProduct = async () => {
  loading.value = true
  error.value = null
  
  try {
    const patchOperations = []
    
    if (productForm.title !== props.productToEdit.title) {
      patchOperations.push({
        op: 'replace',
        path: '/Title',
        value: productForm.title
      })
    }
    
    if (productForm.description !== props.productToEdit.description) {
      patchOperations.push({
        op: 'replace',
        path: '/Description',
        value: productForm.description
      })
    }
    
    if (productForm.quantity !== props.productToEdit.quantity) {
      patchOperations.push({
        op: 'replace',
        path: '/Quantity',
        value: productForm.quantity
      })
    }
    
    if (productForm.category !== props.productToEdit.category) {
      patchOperations.push({
        op: 'replace',
        path: '/Category',
        value: productForm.category
      })
    }
    
    if (productForm.isActive !== props.productToEdit.isActive) {
      patchOperations.push({
        op: 'replace',
        path: '/IsActive',
        value: productForm.isActive
      })
    }

    if (patchOperations.length > 0) {
      await axios.patch(
        `${appsettings.apiUrl}${appsettings.productRoute}/${props.productToEdit.id}`,
        patchOperations,
        {
          headers: {
            'Content-Type': 'application/json-patch+json'
          }
        }
      );
      
      emit('product-updated')
      closeModal()
    }
    
  } catch (err) {
    error.value = err.response?.data?.error || 'Error updating product'
    console.error('Error updating product:', err)
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchCategories();
});
</script>

<template>
  <div v-if="visible && productToEdit" class="modal-backdrop">
    <div class="modal-content">
      <div class="modal-header">
        <h3 class="text-lg font-medium">Editar Producto</h3>
        <button @click="closeModal" class="text-gray-500 hover:text-gray-700">
          <i class="fas fa-times"></i>
        </button>
      </div>

      <!-- Form content -->
      <form @submit.prevent="updateProduct" class="p-6 space-y-4">
        <!-- Error Alert -->
        <div v-if="error" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative">
          {{ error }}
        </div>

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
            :disabled="isLoadingCategories"
          >
            <option value="">
              {{ isLoadingCategories ? 'Cargando categorías...' : 'Selecciona una categoría' }}
            </option>
            <option
              v-for="category in categories"
              :key="category.id"
              :value="category.title || category.name" 
            >
              {{ category.title || category.name }}
            </option>
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

        <div class="flex justify-end gap-4">
          <button 
            type="button" 
            @click="closeModal" 
            class="px-4 py-2 bg-gray-200 text-gray-700 rounded-md hover:bg-gray-300"
            :disabled="loading"
          >
            Cancelar
          </button>
          <button 
            type="submit" 
            class="px-4 py-2 bg-black text-white rounded-md hover:bg-gray-800"
            :disabled="loading"
          >
            <span v-if="loading" class="animate-spin mr-2">↻</span>
            Guardar Cambios
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