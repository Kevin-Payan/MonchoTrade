<!-- MyProducts.vue -->
<template>
    <div class="min-h-screen bg-gray-50">
      <!-- Header -->
      <header class="bg-white shadow">
        <div class="container mx-auto px-4 py-6">
          <div class="flex justify-between items-center">
            <h1 class="text-2xl font-bold text-gray-900">Mis Productos</h1>
            <button 
              @click="showAddModal = true"
              class="btn-primary"
            >
              <i class="fas fa-plus mr-2"></i>
              Agregar Producto
            </button>
          </div>
  
          <!-- Filtros y Búsqueda -->
          <div class="mt-6 flex flex-col md:flex-row gap-4 justify-between">
            <div class="flex gap-4 flex-1">
              <input 
                v-model="searchQuery"
                type="text"
                placeholder="Buscar productos..."
                class="input flex-1 max-w-md"
              >
              <select v-model="categoryFilter" class="input w-40">
                <option value="">Todas las categorías</option>
                <option v-for="cat in categories" :key="cat" :value="cat">
                  {{ cat }}
                </option>
              </select>
            </div>
            <div class="flex gap-4">
              <select v-model="sortBy" class="input w-40">
                <option value="name">Nombre</option>
                <option value="price">Precio</option>
                <option value="stock">Stock</option>
                <option value="date">Fecha</option>
              </select>
              <button 
                @click="sortDirection = sortDirection === 'asc' ? 'desc' : 'asc'"
                class="btn-secondary"
              >
                <i :class="sortDirection === 'asc' ? 'fas fa-sort-up' : 'fas fa-sort-down'"></i>
              </button>
            </div>
          </div>
        </div>
      </header>
  
      <!-- Contenido Principal -->
      <main class="container mx-auto px-4 py-8">
        <!-- Stats Cards -->
        <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
          <div v-for="stat in stats" :key="stat.title" 
               class="bg-white rounded-lg shadow p-6">
            <h3 class="text-gray-500 text-sm">{{ stat.title }}</h3>
            <div class="flex items-center mt-2">
              <span class="text-2xl font-bold">{{ stat.value }}</span>
              <span :class="[
                'ml-2 text-sm',
                stat.change >= 0 ? 'text-green-500' : 'text-red-500'
              ]">
                {{ stat.change }}%
              </span>
            </div>
          </div>
        </div>
  
        <!-- Tabla de Productos -->
        <div class="bg-white rounded-lg shadow overflow-hidden">
          <table class="min-w-full">
            <thead class="bg-gray-50">
              <tr>
                <th class="table-header">Producto</th>
                <th class="table-header">Categoría</th>
                <th class="table-header">Precio</th>
                <th class="table-header">Stock</th>
                <th class="table-header">Estado</th>
                <th class="table-header">Acciones</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-gray-200">
              <tr v-for="product in filteredProducts" :key="product.id" class="hover:bg-gray-50">
                <td class="px-6 py-4">
                  <div class="flex items-center">
                    <img 
                      :src="product.image" 
                      :alt="product.name"
                      class="w-12 h-12 rounded-lg object-cover"
                    >
                    <div class="ml-4">
                      <div class="font-medium text-gray-900">{{ product.name }}</div>
                      <div class="text-sm text-gray-500">ID: {{ product.id }}</div>
                    </div>
                  </div>
                </td>
                <td class="table-cell">{{ product.category }}</td>
                <td class="table-cell">${{ product.price.toFixed(2) }}</td>
                <td class="table-cell">
                  <span :class="[
                    'px-2 py-1 rounded-full text-xs',
                    product.stock > 10 ? 'bg-green-100 text-green-800' : 
                    'bg-red-100 text-red-800'
                  ]">
                    {{ product.stock }} unidades
                  </span>
                </td>
                <td class="table-cell">
                  <span :class="[
                    'px-2 py-1 rounded-full text-xs',
                    product.status === 'active' ? 'bg-green-100 text-green-800' : 
                    'bg-gray-100 text-gray-800'
                  ]">
                    {{ product.status === 'active' ? 'Activo' : 'Inactivo' }}
                  </span>
                </td>
                <td class="table-cell">
                  <div class="flex gap-2">
                    <button 
                      @click="editProduct(product)"
                      class="btn-icon"
                    >
                      <i class="fas fa-edit"></i>
                    </button>
                    <button 
                      @click="toggleProductStatus(product)"
                      class="btn-icon"
                    >
                      <i class="fas fa-power-off"></i>
                    </button>
                    <button 
                      @click="deleteProduct(product)"
                      class="btn-icon text-red-600"
                    >
                      <i class="fas fa-trash"></i>
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
  
          <!-- Paginación -->
          <div class="bg-white px-4 py-3 border-t border-gray-200 sm:px-6">
            <div class="flex justify-between items-center">
              <div class="text-sm text-gray-700">
                Mostrando {{ startIndex + 1 }} a {{ endIndex }} de {{ totalProducts }} productos
              </div>
              <div class="flex gap-2">
                <button 
                  @click="currentPage--"
                  :disabled="currentPage === 1"
                  class="btn-secondary"
                >
                  Anterior
                </button>
                <button 
                  @click="currentPage++"
                  :disabled="endIndex >= totalProducts"
                  class="btn-secondary"
                >
                  Siguiente
                </button>
              </div>
            </div>
          </div>
        </div>
      </main>
  
      <!-- Modal Agregar/Editar Producto -->
      <div v-if="showAddModal" class="modal-backdrop">
        <div class="modal-content">
          <div class="modal-header">
            <h3 class="text-lg font-medium">
              {{ editingProduct ? 'Editar Producto' : 'Agregar Nuevo Producto' }}
            </h3>
            <button @click="closeModal" class="text-gray-500 hover:text-gray-700">
              <i class="fas fa-times"></i>
            </button>
          </div>
          <form @submit.prevent="saveProduct" class="p-6 space-y-4">
            <div class="form-group">
              <label class="label">Nombre del Producto</label>
              <input 
                v-model="productForm.name"
                type="text" 
                class="input"
                required
              >
            </div>
  
            <div class="form-group">
              <label class="label">Categoría</label>
              <select v-model="productForm.category" class="input" required>
                <option v-for="cat in categories" :key="cat" :value="cat">
                  {{ cat }}
                </option>
              </select>
            </div>
  
            <div class="grid grid-cols-2 gap-4">
              <div class="form-group">
                <label class="label">Precio</label>
                <input 
                  v-model.number="productForm.price"
                  type="number" 
                  step="0.01"
                  class="input"
                  required
                >
              </div>
  
              <div class="form-group">
                <label class="label">Stock</label>
                <input 
                  v-model.number="productForm.stock"
                  type="number" 
                  class="input"
                  required
                >
              </div>
            </div>
  
            <div class="form-group">
              <label class="label">Descripción</label>
              <textarea 
                v-model="productForm.description"
                class="input h-32"
              ></textarea>
            </div>
  
            <div class="flex justify-end gap-4">
              <button type="button" @click="closeModal" class="btn-secondary">
                Cancelar
              </button>
              <button type="submit" class="btn-primary">
                {{ editingProduct ? 'Guardar Cambios' : 'Crear Producto' }}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed } from 'vue'
  
  // Estado
  const searchQuery = ref('')
  const categoryFilter = ref('')
  const sortBy = ref('name')
  const sortDirection = ref('asc')
  const currentPage = ref(1)
  const showAddModal = ref(false)
  const editingProduct = ref(null)
  const itemsPerPage = 10
  
  // Datos de ejemplo
  const categories = ['Electrónica', 'Ropa', 'Hogar', 'Deportes', 'Libros']
  
  const stats = [
    { title: 'Total Productos', value: '124', change: 12 },
    { title: 'Productos Activos', value: '98', change: 8 },
    { title: 'Bajo Stock', value: '15', change: -5 },
    { title: 'Ventas del Mes', value: '$12,234', change: 23 }
  ]
  
  const products = ref([
    {
      id: '1',
      name: 'Laptop Pro',
      category: 'Electrónica',
      price: 999.99,
      stock: 15,
      status: 'active',
      image: '/api/placeholder/100/100'
    },
    // Agregar más productos aquí...
  ])
  
  // Formulario
  const productForm = ref({
    name: '',
    category: '',
    price: 0,
    stock: 0,
    description: ''
  })
  
  // Computed
  const filteredProducts = computed(() => {
    let filtered = [...products.value]
    
    // Aplicar filtro de búsqueda
    if (searchQuery.value) {
      filtered = filtered.filter(p => 
        p.name.toLowerCase().includes(searchQuery.value.toLowerCase())
      )
    }
  
    // Aplicar filtro de categoría
    if (categoryFilter.value) {
      filtered = filtered.filter(p => p.category === categoryFilter.value)
    }
  
    // Aplicar ordenamiento
    filtered.sort((a, b) => {
      const modifier = sortDirection.value === 'asc' ? 1 : -1
      return a[sortBy.value] > b[sortBy.value] ? modifier : -modifier
    })
  
    return filtered
  })
  
  const startIndex = computed(() => (currentPage.value - 1) * itemsPerPage)
  const endIndex = computed(() => Math.min(startIndex.value + itemsPerPage, totalProducts.value))
  const totalProducts = computed(() => filteredProducts.value.length)
  
  // Métodos
  const editProduct = (product) => {
    editingProduct.value = product
    productForm.value = { ...product }
    showAddModal.value = true
  }
  
  const closeModal = () => {
    showAddModal.value = false
    editingProduct.value = null
    productForm.value = {
      name: '',
      category: '',
      price: 0,
      stock: 0,
      description: ''
    }
  }
  
  const saveProduct = () => {
    if (editingProduct.value) {
      // Actualizar producto existente
      const index = products.value.findIndex(p => p.id === editingProduct.value.id)
      products.value[index] = { ...editingProduct.value, ...productForm.value }
    } else {
      // Crear nuevo producto
      products.value.push({
        id: Date.now().toString(),
        ...productForm.value,
        status: 'active',
        image: '/api/placeholder/100/100'
      })
    }
    closeModal()
  }
  
  const toggleProductStatus = (product) => {
    product.status = product.status === 'active' ? 'inactive' : 'active'
  }
  
  const deleteProduct = (product) => {
    if (confirm('¿Estás seguro de que deseas eliminar este producto?')) {
      products.value = products.value.filter(p => p.id !== product.id)
    }
  }
  </script>
  
  <style scoped>
  .table-header {
    @apply px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider;
  }
  
  .table-cell {
    @apply px-6 py-4 whitespace-nowrap text-sm text-gray-900;
  }
  
  .btn-primary {
    @apply bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 
           transition-colors duration-200;
  }
  
  .btn-secondary {
    @apply bg-gray-200 text-gray-700 px-4 py-2 rounded-md hover:bg-gray-300 
           transition-colors duration-200;
  }
  
  .btn-icon {
    @apply p-2 hover:bg-gray-100 rounded-md transition-colors duration-200;
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
           focus:ring-2 focus:ring-blue-500 focus:border-transparent;
  }
  </style>