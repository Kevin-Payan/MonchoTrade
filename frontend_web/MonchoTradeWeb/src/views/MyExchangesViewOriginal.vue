<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Header -->
    <header class="bg-white shadow">
      <div class="container mx-auto px-4 py-6">
        <div class="flex flex-col md:flex-row md:items-center justify-between gap-4">
          
          
          <h1 class="text-2xl font-bold text-gray-900">Mis Intercambios</h1>
          
          <!-- Profile Dropdown -->
        <div class="absolute top-4 right-4">
          <ProfileDropdown />
        </div>

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
              <option value="Pending">Pendientes</option>
              <option value="Accepted">En Proceso</option>
              <option value="Completed">Completados</option>
              <option value="Rejected">Rechazados</option>
            </select>



          </div>
        </div>
      </div>
    </header>

    <!-- Contenido Principal -->
    <main class="container mx-auto px-4 py-8">
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
                :class="getTabCount(tab.value) > 0 ? 'bg-blue-100 text-blue-800' : 'bg-gray-100 text-gray-800'"
              >
                {{ getTabCount(tab.value) }}
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
                <th class="table-header">Productos</th>
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
                </td>
                <td class="table-cell">
                  <div class="flex items-center">
                    <div class="ml-3">
                      <div class="font-medium">
                        {{ isInitiator(exchange) ? exchange.receiverUserName : exchange.initiatorUserName }}
                      </div>
                    </div>
                  </div>
                </td>
                <td class="table-cell">
                  <div class="font-medium">{{ isInitiator(exchange) ? exchange.initiatorProductName : exchange.receiverProductName }}</div>
                  <div class="text-sm text-gray-500">por {{ isInitiator(exchange) ? exchange.receiverProductName : exchange.initiatorProductName }}</div>
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
                  <div>{{ formatDate(exchange.createdAt) }}</div>
                  <div class="text-sm text-gray-500">Actualizado: {{ formatDate(exchange.updatedAt) }}</div>
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
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
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
          <div class="space-y-4">
            <div>
              <h4 class="font-medium">Productos</h4>
              <p>{{ selectedExchange.initiatorProductName }} por {{ selectedExchange.receiverProductName }}</p>
            </div>
            <div>
              <h4 class="font-medium">Usuarios</h4>
              <p>Iniciador: {{ selectedExchange.initiatorUserName }}</p>
              <p>Receptor: {{ selectedExchange.receiverUserName }}</p>
            </div>
            <div>
              <h4 class="font-medium">Estado</h4>
              <p>{{ statusLabels[selectedExchange.status] }}</p>
            </div>
            <div v-if="selectedExchange.rejectionReason">
              <h4 class="font-medium">Razón de Rechazo</h4>
              <p>{{ selectedExchange.rejectionReason }}</p>
            </div>
            <div v-if="selectedExchange.notes">
              <h4 class="font-medium">Notas</h4>
              <p>{{ selectedExchange.notes }}</p>
            </div>
            <div>
              <h4 class="font-medium">Fechas</h4>
              <p>Creado: {{ formatDate(selectedExchange.createdAt) }}</p>
              <p>Actualizado: {{ formatDate(selectedExchange.updatedAt) }}</p>
            </div>

            
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import { appsettings } from '../../settings/appsettings';
import ProfileDropdown from '@/components/ProfileDropdown.vue';

// Estado
const searchQuery = ref('')
const statusFilter = ref('')
const currentTab = ref('all')
const selectedExchange = ref(null)
const exchanges = ref([])

// Datos estáticos
const tabs = [
  { label: 'Todos', value: 'all' },
  { label: 'Pendientes', value: 'Pending' },
  { label: 'En Proceso', value: 'Accepted' },
  { label: 'Completados', value: 'Completed' },
  { label: 'Rechazados', value: 'Rejected' }
]

const statusLabels = {
  'Pending': 'Pendiente',
  'Accepted': 'En Proceso',
  'Completed': 'Completado',
  'Rejected': 'Rechazado'
}

const statusClasses = {
  'Pending': 'bg-yellow-100 text-yellow-800',
  'Accepted': 'bg-blue-100 text-blue-800',
  'Completed': 'bg-green-100 text-green-800',
  'Rejected': 'bg-red-100 text-red-800'
}

// Computed
const filteredExchanges = computed(() => {
  let filtered = [...exchanges.value]
  console.log(exchanges.value)
  if (searchQuery.value) {
    const query = searchQuery.value.toLowerCase()
    filtered = filtered.filter(e => 
      e.id.toString().includes(query) ||
      e.initiatorUserName.toLowerCase().includes(query) ||
      e.receiverUserName.toLowerCase().includes(query)
    )
  }

  if (statusFilter.value) {
    filtered = filtered.filter(e => e.status === statusFilter.value)
  }

  if (currentTab.value !== 'all') {
    filtered = filtered.filter(e => e.status === currentTab.value)
  }

  return filtered
})

// Methods
const formatDate = (date) => {
  return new Date(date).toLocaleDateString()
}

const viewExchange = (exchange) => {
  selectedExchange.value = exchange
}

const getTabCount = (status) => {
  if (status === 'all') {
    return exchanges.value.length
  }
  return exchanges.value.filter(e => e.status === status).length
}

const isInitiator = (exchange) => {
  const userId = localStorage.getItem('userId')
  return exchange.initiatorUserId.toString() === userId
}

// Fetch data
const fetchExchanges = async () => {
  try {
    const userId = localStorage.getItem('userId')
    const response = await axios.get(`${appsettings.apiUrl}/exchanges/user/${userId}`)
    exchanges.value = response.data
  } catch (error) {
    console.error('Error fetching exchanges:', error)
  }
}

// Lifecycle
onMounted(() => {
  fetchExchanges()
})
</script>

<style scoped>
.table-header {
  @apply px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider;
}

.table-cell {
  @apply px-6 py-4 whitespace-nowrap text-sm;
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