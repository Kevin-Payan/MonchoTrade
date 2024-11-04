<!-- MyExchanges.vue -->
<template>
    <div class="min-h-screen bg-gray-50">
      <!-- Header -->
      <header class="bg-white shadow">
        <div class="container mx-auto px-4 py-6">
          <div class="flex flex-col md:flex-row md:items-center justify-between gap-4">
            <h1 class="text-2xl font-bold text-gray-900">Mis Intercambios</h1>
            
            <!-- Filtros y Búsqueda -->
            <div class="flex flex-wrap gap-4 items-center">
              <div class="relative">
                <input 
                  v-model="searchQuery"
                  type="text"
                  placeholder="Buscar por ID o usuario..."
                  class="input pl-10"
                >
                <i class="fas fa-search absolute left-3 top-1/2 -translate-y-1/2 text-gray-400"></i>
              </div>
  
              <select v-model="statusFilter" class="input">
                <option value="">Todos los estados</option>
                <option value="pending">Pendientes</option>
                <option value="inProgress">En Proceso</option>
                <option value="completed">Completados</option>
                <option value="cancelled">Cancelados</option>
              </select>
  
              <select v-model="timeFilter" class="input">
                <option value="all">Todo el tiempo</option>
                <option value="today">Hoy</option>
                <option value="week">Esta semana</option>
                <option value="month">Este mes</option>
                <option value="year">Este año</option>
              </select>
            </div>
          </div>
        </div>
      </header>
  
      <!-- Contenido Principal -->
      <main class="container mx-auto px-4 py-8">
        <!-- Tarjetas de Estadísticas -->
        <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
          <div v-for="stat in stats" :key="stat.title" 
               class="bg-white rounded-lg shadow p-6">
            <div class="flex justify-between items-start">
              <div>
                <p class="text-sm text-gray-500">{{ stat.title }}</p>
                <p class="text-2xl font-bold mt-1">{{ stat.value }}</p>
              </div>
              <span :class="[
                'text-sm px-2 py-1 rounded-full',
                stat.trend > 0 ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
              ]">
                {{ stat.trend > 0 ? '+' : '' }}{{ stat.trend }}%
              </span>
            </div>
            <div class="mt-4">
              <div class="w-full bg-gray-200 rounded-full h-2">
                <div 
                  class="bg-blue-600 h-2 rounded-full" 
                  :style="{ width: `${stat.progress}%` }"
                ></div>
              </div>
            </div>
          </div>
        </div>
  
        <!-- Lista de Intercambios -->
        <div class="bg-white rounded-lg shadow">
          <!-- Tabs -->
          <div class="border-b border-gray-200">
            <nav class="flex -mb-px">
              <button 
                v-for="tab in tabs" 
                :key="tab.value"
                @click="currentTab = tab.value"
                :class="[
                  'px-6 py-4 text-sm font-medium whitespace-nowrap',
                  currentTab === tab.value
                    ? 'border-b-2 border-blue-500 text-blue-600'
                    : 'text-gray-500 hover:text-gray-700 hover:border-gray-300'
                ]"
              >
                {{ tab.label }}
                <span 
                  class="ml-2 px-2 py-0.5 text-xs rounded-full"
                  :class="tab.count > 0 ? 'bg-blue-100 text-blue-800' : 'bg-gray-100 text-gray-800'"
                >
                  {{ tab.count }}
                </span>
              </button>
            </nav>
          </div>
  
          <!-- Lista de Intercambios -->
          <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
              <thead class="bg-gray-50">
                <tr>
                  <th class="table-header">ID Intercambio</th>
                  <th class="table-header">Usuario</th>
                  <th class="table-header">Monto</th>
                  <th class="table-header">Estado</th>
                  <th class="table-header">Fecha</th>
                  <th class="table-header">Acciones</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-gray-200">
                <tr v-for="exchange in filteredExchanges" 
                    :key="exchange.id"
                    class="hover:bg-gray-50">
                  <td class="table-cell font-medium">
                    #{{ exchange.id }}
                    <span v-if="exchange.urgent" 
                          class="ml-2 text-xs text-red-600">
                      <i class="fas fa-exclamation-circle"></i> Urgente
                    </span>
                  </td>
                  <td class="table-cell">
                    <div class="flex items-center">
                      <img 
                        :src="exchange.userAvatar" 
                        class="w-8 h-8 rounded-full"
                        :alt="exchange.userName"
                      >
                      <div class="ml-3">
                        <div class="font-medium">{{ exchange.userName }}</div>
                        <div class="text-sm text-gray-500">{{ exchange.userEmail }}</div>
                      </div>
                    </div>
                  </td>
                  <td class="table-cell">
                    <div class="font-medium">${{ exchange.amount }}</div>
                    <div class="text-sm text-gray-500">{{ exchange.currency }}</div>
                  </td>
                  <td class="table-cell">
                    <span :class="[
                      'px-2 py-1 text-xs rounded-full',
                      statusClasses[exchange.status]
                    ]">
                      {{ statusLabels[exchange.status] }}
                    </span>
                  </td>
                  <td class="table-cell">
                    <div>{{ formatDate(exchange.date) }}</div>
                    <div class="text-sm text-gray-500">
                      {{ formatTime(exchange.date) }}
                    </div>
                  </td>
                  <td class="table-cell">
                    <div class="flex space-x-2">
                      <button 
                        @click="viewExchange(exchange)"
                        class="btn-icon text-blue-600"
                        title="Ver detalles"
                      >
                        <i class="fas fa-eye"></i>
                      </button>
                      <button 
                        v-if="exchange.status === 'pending'"
                        @click="processExchange(exchange)"
                        class="btn-icon text-green-600"
                        title="Procesar"
                      >
                        <i class="fas fa-check"></i>
                      </button>
                      <button 
                        v-if="['pending', 'inProgress'].includes(exchange.status)"
                        @click="cancelExchange(exchange)"
                        class="btn-icon text-red-600"
                        title="Cancelar"
                      >
                        <i class="fas fa-times"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
  
          <!-- Paginación -->
          <div class="bg-white px-4 py-3 border-t border-gray-200 sm:px-6">
            <div class="flex justify-between items-center">
              <div class="text-sm text-gray-700">
                Mostrando {{ startIndex + 1 }} a {{ endIndex }} de {{ totalExchanges }} intercambios
              </div>
              <div class="flex gap-2">
                <button 
                  @click="currentPage--"
                  :disabled="currentPage === 1"
                  class="btn-secondary"
                >
                  <i class="fas fa-chevron-left mr-2"></i>
                  Anterior
                </button>
                <button 
                  @click="currentPage++"
                  :disabled="endIndex >= totalExchanges"
                  class="btn-secondary"
                >
                  Siguiente
                  <i class="fas fa-chevron-right ml-2"></i>
                </button>
              </div>
            </div>
          </div>
        </div>
      </main>
  
      <!-- Modal de Detalles -->
      <div v-if="selectedExchange" class="modal-backdrop">
        <div class="modal-content">
          <div class="modal-header">
            <h3 class="text-lg font-medium">
              Detalles del Intercambio #{{ selectedExchange.id }}
            </h3>
            <button @click="selectedExchange = null" class="text-gray-500 hover:text-gray-700">
              <i class="fas fa-times"></i>
            </button>
          </div>
          <div class="p-6">
            <!-- Timeline -->
            <div class="flow-root">
              <ul class="-mb-8">
                <li v-for="(event, index) in selectedExchange.timeline" 
                    :key="index"
                    class="relative pb-8">
                  <div class="relative flex space-x-3">
                    <div>
                      <span class="h-8 w-8 rounded-full flex items-center justify-center ring-8 ring-white"
                            :class="timelineStyles[event.type]">
                        <i :class="timelineIcons[event.type]" class="text-white"></i>
                      </span>
                    </div>
                    <div class="flex min-w-0 flex-1 justify-between space-x-4 pt-1.5">
                      <div>
                        <p class="text-sm text-gray-500">{{ event.description }}</p>
                      </div>
                      <div class="text-right text-sm whitespace-nowrap text-gray-500">
                        <time :datetime="event.date">{{ formatDate(event.date) }}</time>
                      </div>
                    </div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed } from 'vue'
  
  // Estado
  const searchQuery = ref('')
  const statusFilter = ref('')
  const timeFilter = ref('all')
  const currentTab = ref('all')
  const currentPage = ref(1)
  const selectedExchange = ref(null)
  const itemsPerPage = 10
  
  // Datos de ejemplo
  const stats = [
    { 
      title: 'Intercambios Totales',
      value: '1,234',
      trend: 12,
      progress: 75
    },
    { 
      title: 'Monto Total',
      value: '$45,678',
      trend: 8,
      progress: 65
    },
    { 
      title: 'Tasa de Éxito',
      value: '94%',
      trend: -2,
      progress: 94
    },
    { 
      title: 'Tiempo Promedio',
      value: '1.5 días',
      trend: 5,
      progress: 82
    }
  ]
  
  const tabs = [
    { label: 'Todos', value: 'all', count: 156 },
    { label: 'Pendientes', value: 'pending', count: 12 },
    { label: 'En Proceso', value: 'inProgress', count: 24 },
    { label: 'Completados', value: 'completed', count: 108 },
    { label: 'Cancelados', value: 'cancelled', count: 12 }
  ]
  
  const statusLabels = {
    pending: 'Pendiente',
    inProgress: 'En Proceso',
    completed: 'Completado',
    cancelled: 'Cancelado'
  }
  
  const statusClasses = {
    pending: 'bg-yellow-100 text-yellow-800',
    inProgress: 'bg-blue-100 text-blue-800',
    completed: 'bg-green-100 text-green-800',
    cancelled: 'bg-red-100 text-red-800'
  }
  
  const timelineStyles = {
    created: 'bg-gray-500',
    updated: 'bg-blue-500',
    completed: 'bg-green-500',
    cancelled: 'bg-red-500'
  }
  
  const timelineIcons = {
    created: 'fas fa-plus',
    updated: 'fas fa-pencil-alt',
    completed: 'fas fa-check',
    cancelled: 'fas fa-times'
  }
  
  // Datos de ejemplo para intercambios
  const exchanges = ref([
    {
      id: '1234',
      userName: 'John Doe',
      userEmail: 'john@example.com',
      userAvatar: '/api/placeholder/32/32',
      amount: 1500,
      currency: 'USD',
      status: 'pending',
      date: new Date(),
      urgent: true,
      timeline: [
        {
          type: 'created',
          description: 'Intercambio creado',
          date: new Date(Date.now() - 86400000)
        },
        {
          type: 'updated',
          description: 'Documentación actualizada',
          date: new Date()
        }
      ]
    }
    // Agregar más intercambios aquí...
  ])
  
  // Computed
  const filteredExchanges = computed(() => {
    let filtered = [...exchanges.value]
    
    if (searchQuery.value) {
      const query = searchQuery.value.toLowerCase()
      filtered = filtered.filter(e => 
        e.id.toLowerCase().includes(query) ||
        e.userName.toLowerCase().includes(query)
      )
    }
  
    if (statusFilter.value) {
      filtered = filtered.filter(e => e.status === statusFilter.value)
    }
  
    if (currentTab.value !== 'all') {
      filtered = filtered.filter(e => e.status === currentTab.value)
    }
  
    // Filtrar por tiempo
    const now = new Date()
    const today = new Date(now.getFullYear(), now.getMonth(), now.getDate())
    
    switch (timeFilter.value) {
      case 'today':
        filtered = filtered.filter(e => new Date(e.date) >= today)
        break
      case 'week':
        const weekAgo = new Date(today.getTime() - 7 * 24 * 60 * 60 * 1000)
        filtered = filtered.filter(e => new Date(e.date) >= weekAgo)
        break
      case 'month':
        const monthAgo = new Date(today.getFullYear(), today.getMonth() - 1, today.getDate())
        filtered = filtered.filter(e => new Date(e.date) >= monthAgo)
      break
    case 'year':
      const yearAgo = new Date(today.getFullYear() - 1, today.getMonth(), today.getDate())
      filtered = filtered.filter(e => new Date(e.date) >= yearAgo)
      break
  }

  return filtered
})

const startIndex = computed(() => (currentPage.value - 1) * itemsPerPage)
const endIndex = computed(() => Math.min(startIndex.value + itemsPerPage, totalExchanges.value))
const totalExchanges = computed(() => filteredExchanges.value.length)

// Métodos
const formatDate = (date) => {
  return new Date(date).toLocaleDateString()
}

const formatTime = (date) => {
  return new Date(date).toLocaleTimeString()
}

const viewExchange = (exchange) => {
  selectedExchange.value = exchange
}

const processExchange = async (exchange) => {
  if (confirm('¿Deseas procesar este intercambio?')) {
    exchange.status = 'inProgress'
    exchange.timeline.push({
      type: 'updated',
      description: 'Intercambio iniciado',
      date: new Date()
    })
  }
}

const cancelExchange = async (exchange) => {
  if (confirm('¿Estás seguro de que deseas cancelar este intercambio?')) {
    exchange.status = 'cancelled'
    exchange.timeline.push({
      type: 'cancelled',
      description: 'Intercambio cancelado',
      date: new Date()
    })
  }
}
</script>

<style scoped>
.table-header {
  @apply px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider;
}

.table-cell {
  @apply px-6 py-4 whitespace-nowrap text-sm;
}

.btn-primary {
  @apply bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 
         transition-colors duration-200;
}

.btn-secondary {
  @apply bg-gray-200 text-gray-700 px-4 py-2 rounded-md hover:bg-gray-300 
         transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed;
}

.btn-icon {
  @apply p-2 hover:bg-gray-100 rounded-md transition-colors duration-200;
}

.input {
  @apply border border-gray-300 rounded-md px-3 py-2 focus:outline-none 
         focus:ring-2 focus:ring-blue-500 focus:border-transparent;
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
</style>

