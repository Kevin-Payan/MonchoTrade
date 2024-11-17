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
      <div class="bg-white rounded-lg shadow">
        <!-- Vista Tabs -->
        <div class="border-b border-gray-200">
          <nav class="flex -mb-px">
            <button 
              v-for="tab in viewTabs" 
              :key="tab.value"
              @click="currentView = tab.value"
              :class="[
                'px-6 py-4 text-sm font-medium whitespace-nowrap',
                currentView === tab.value
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

    <!-- Modal de Detalles para Propuestas -->
    <div v-if="selectedExchange && isInitiator(selectedExchange)" class="modal-backdrop">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="text-lg font-medium">
            Detalles de la Propuesta #{{ selectedExchange.id }}
          </h3>
          <button @click="selectedExchange = null" class="text-gray-500 hover:text-gray-700">
            <i class="fas fa-times"></i>
          </button>
        </div>
        <div class="p-6">
          <div class="space-y-4">
            <!-- Detalles del intercambio -->
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
              <div v-if="selectedExchange.notes">
                <h4 class="font-medium">Notas</h4>
                <p>{{ selectedExchange.notes }}</p>
              </div>
            </div>
            
            <div v-if="selectedExchange.status === 'Accepted'">
              <h4 class="font-medium">Contact Information</h4>
              <div v-if="contactInfo">
                <p><strong>Email:</strong> {{ contactInfo.email }}</p>
                <p><strong>Phone:</strong> {{ contactInfo.phoneNumber }}</p>
              </div>
              <div v-else class="text-gray-500">
                Loading contact information...
              </div>
            </div>

            <!-- Botones de acción -->
            <div class="flex justify-end space-x-3 mt-6">
              <button 
                v-if="selectedExchange.status !== 'Completed' && selectedExchange.status !== 'Rejected'"
                @click="updateExchangeStatus('Completed')"
                class="btn bg-green-500 text-white hover:bg-green-600"
              >
                Marcar como Completado
              </button>
              <button 
                v-if="selectedExchange.status === 'Pending'"
                @click="updateExchangeStatus('Rejected')"
                class="btn bg-red-500 text-white hover:bg-red-600"
              >
                Cancelar Propuesta
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal de Detalles para Solicitudes -->
    <div v-if="selectedExchange && !isInitiator(selectedExchange)" class="modal-backdrop">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="text-lg font-medium">
            Detalles de la Solicitud #{{ selectedExchange.id }}
          </h3>
          <button @click="selectedExchange = null" class="text-gray-500 hover:text-gray-700">
            <i class="fas fa-times"></i>
          </button>
        </div>
        <div class="p-6">
          <div class="space-y-4">
            <!-- Detalles del intercambio -->
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
              <div v-if="selectedExchange.notes">
                <h4 class="font-medium">Notas</h4>
                <p>{{ selectedExchange.notes }}</p>
              </div>
            </div>
            
            <div v-if="selectedExchange.status === 'Accepted'">
              <h4 class="font-medium">Contact Information</h4>
              <div v-if="contactInfo">
                <p><strong>Email:</strong> {{ contactInfo.email }}</p>
                <p><strong>Phone:</strong> {{ contactInfo.phoneNumber }}</p>
              </div>
              <div v-else class="text-gray-500">
                Loading contact information...
              </div>
            </div>
            <!-- Botones de acción -->
            <div class="flex justify-end space-x-3 mt-6">
              <button 
                v-if="selectedExchange.status === 'Pending'"
                @click="updateExchangeStatus('Accepted')"
                class="btn bg-blue-500 text-white hover:bg-blue-600"
              >
                Aceptar
              </button>
              <button 
                v-if="selectedExchange.status !== 'Completed' && selectedExchange.status !== 'Rejected'"
                @click="updateExchangeStatus('Completed')"
                class="btn bg-green-500 text-white hover:bg-green-600"
              >
                Marcar como Completado
              </button>
              <button 
                v-if="selectedExchange.status === 'Pending'"
                @click="updateExchangeStatus('Rejected')"
                class="btn bg-red-500 text-white hover:bg-red-600"
              >
                Rechazar
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch} from 'vue'
import axios from 'axios'
import { appsettings } from '../../settings/appsettings';
import ProfileDropdown from '@/components/ProfileDropdown.vue';

// Estado
const searchQuery = ref('')
const statusFilter = ref('')
const currentView = ref('all')
const selectedExchange = ref(null)
const exchanges = ref([])
const contactInfo = ref(null)

// Datos estáticos
const viewTabs = [
  { label: 'Todos los Intercambios', value: 'all' },
  { label: 'Mis Propuestas', value: 'proposals' },
  { label: 'Peticiones', value: 'requests' }
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
  
  // Filtrar por vista actual
  if (currentView.value === 'proposals') {
    filtered = filtered.filter(e => isInitiator(e))
  } else if (currentView.value === 'requests') {
    filtered = filtered.filter(e => !isInitiator(e))
  }

  // Aplicar filtros adicionales
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

  return filtered
})

// Methods
const formatDate = (date) => {
  return new Date(date).toLocaleDateString()
}

const viewExchange = (exchange) => {
  selectedExchange.value = exchange
  // Reset contact info when opening a new exchange
  contactInfo.value = null
  // Fetch contact info if status is Accepted
  if (exchange.status === 'Accepted') {
    fetchContactInfo(exchange)
  }
}

watch(
  () => selectedExchange.value?.status,
  (newStatus) => {
    if (newStatus === 'Accepted' && selectedExchange.value) {
      fetchContactInfo(selectedExchange.value)
    } else {
      contactInfo.value = null
    }
  }
)


const getTabCount = (value) => {
  if (value === 'all') {
    return exchanges.value.length
  }
  if (value === 'proposals') {
    return exchanges.value.filter(e => isInitiator(e)).length
  }
  if (value === 'requests') {
    return exchanges.value.filter(e => !isInitiator(e)).length
  }
  return 0
}

const isInitiator = (exchange) => {
  const userId = localStorage.getItem('userId')
  return exchange.initiatorUserId.toString() === userId
}

// Function to get the other user's ID
const getOtherUserId = (exchange) => {
  const currentUserId = localStorage.getItem('userId')
  return exchange.initiatorUserId.toString() === currentUserId 
    ? exchange.receiverUserId 
    : exchange.initiatorUserId

}

// Function to fetch contact information
const fetchContactInfo = async (exchange) => {
  if (!exchange || exchange.status !== 'Accepted') return

  try {
    const otherUserId = getOtherUserId(exchange)
    const response = await axios.get(
      `${appsettings.apiUrl}/user/contactinfo/${otherUserId}`,
      appsettings.axiosConfig
    )
    contactInfo.value = {
      email: response.data.email,
      phoneNumber: response.data.phoneNumber
    }
  } catch (error) {
    console.error('Error fetching contact information:', error)
    contactInfo.value = null
  }
}




const updateExchangeStatus = async (newStatus) => {
  try {
    const patchDoc = [
      {
        op: 'replace',
        path: '/status',
        value: newStatus
      }
    ]

    await axios.patch(
      `${appsettings.apiUrl}/exchanges/${selectedExchange.value.id}`,
      patchDoc,
      {
        headers: {
          'Content-Type': 'application/json-patch+json'
        }
      }
    )

    // Actualizar el estado local
    selectedExchange.value.status = newStatus
    const exchangeIndex = exchanges.value.findIndex(e => e.id === selectedExchange.value.id)
    if (exchangeIndex !== -1) {
      exchanges.value[exchangeIndex].status = newStatus
    }

    // Opcional: cerrar el modal después de la actualización
    // selectedExchange.value = null
  } catch (error) {
    console.error('Error updating exchange status:', error)
  }
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

.btn {
  @apply px-4 py-2 rounded-md font-medium transition-colors duration-200
         disabled:opacity-50 disabled:cursor-not-allowed;
}
</style>