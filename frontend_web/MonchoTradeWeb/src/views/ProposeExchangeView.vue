<!-- ProposeExchange.vue -->
<template>
    <div class="min-h-screen bg-gray-50">
      <Navbar />
      
      <div class="container mx-auto px-4 py-8">
        <div class="max-w-4xl mx-auto">
          <!-- Header -->
          <div class="mb-8">
            <h1 class="text-2xl font-bold text-gray-900">Proponer Intercambio</h1>
            <p class="mt-2 text-gray-600">Propón un intercambio para el artículo seleccionado</p>
          </div>
  
          <div class="grid grid-cols-1 md:grid-cols-2 gap-8">
            <!-- Artículo Seleccionado -->
            <div class="bg-white p-6 rounded-lg shadow">
              <h2 class="text-lg font-semibold mb-4">Artículo Seleccionado</h2>
              <div class="aspect-video bg-gray-100 rounded-lg overflow-hidden mb-4">
                <img 
                  :src="selectedItem.imageUrl" 
                  :alt="selectedItem.title"
                  class="w-full h-full object-cover"
                >
              </div>
              <h3 class="font-medium">{{ selectedItem.title }}</h3>
              <p class="text-sm text-gray-600 mt-2">{{ selectedItem.description }}</p>
              <div class="mt-4 text-sm text-gray-600">
                Ofrecido por: <span class="font-medium">{{ selectedItem.offeredBy }}</span>
              </div>
            </div>
  
            <!-- Formulario de Propuesta -->
            <div class="bg-white p-6 rounded-lg shadow">
              <h2 class="text-lg font-semibold mb-4">Tu Propuesta</h2>
              
              <form @submit.prevent="submitProposal" class="space-y-6">
                <!-- Selección de Items -->
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">
                    Selecciona tus artículos para intercambiar
                  </label>
                  <div class="space-y-4">
                    <div v-for="item in myItems" :key="item.id" 
                         class="flex items-center p-3 border rounded-lg hover:bg-gray-50">
                      <input
                        type="checkbox"
                        :value="item.id"
                        v-model="selectedItemIds"
                        class="h-4 w-4 text-blue-600 rounded border-gray-300"
                      >
                      <div class="ml-3 flex items-center flex-1">
                        <img 
                          :src="item.imageUrl" 
                          :alt="item.title"
                          class="w-16 h-16 object-cover rounded"
                        >
                        <div class="ml-3">
                          <h4 class="font-medium">{{ item.title }}</h4>
                          <p class="text-sm text-gray-500">{{ item.description }}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
  
                <!-- Mensaje -->
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">
                    Mensaje para el propietario
                  </label>
                  <textarea
                    v-model="message"
                    rows="4"
                    class="w-full rounded-lg border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    placeholder="Escribe un mensaje amable explicando por qué estás interesado en el intercambio..."
                  ></textarea>
                </div>
  
                <!-- Condiciones -->
                <div class="space-y-2">
                  <label class="flex items-center">
                    <input
                      type="checkbox"
                      v-model="acceptTerms"
                      class="h-4 w-4 text-blue-600 rounded border-gray-300"
                    >
                    <span class="ml-2 text-sm text-gray-600">
                      Acepto las condiciones del intercambio
                    </span>
                  </label>
                  <label class="flex items-center">
                    <input
                      type="checkbox"
                      v-model="acceptCommunication"
                      class="h-4 w-4 text-blue-600 rounded border-gray-300"
                    >
                    <span class="ml-2 text-sm text-gray-600">
                      Acepto recibir comunicaciones sobre este intercambio
                    </span>
                  </label>
                </div>
  
                <!-- Botones -->
                <div class="flex gap-4">
                  <button
                    type="button"
                    @click="$router.back()"
                    class="flex-1 px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    :disabled="!isFormValid"
                    class="flex-1 px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:bg-gray-400"
                  >
                    Enviar Propuesta
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed, onMounted } from 'vue'
  import { useRoute, useRouter } from 'vue-router'
  import axios from 'axios'
  import { appsettings } from "../../settings/appsettings";
  import Navbar from '@/components/Navbar.vue'
  
  const route = useRoute()
  const router = useRouter()
  
  // Estado
  const selectedItem = ref({})
  const myItems = ref([])
  const selectedItemIds = ref([])
  const message = ref('')
  const acceptTerms = ref(false)
  const acceptCommunication = ref(false)
  
  // Computed
  const isFormValid = computed(() => {
    return selectedItemIds.value.length > 0 && 
           message.value.trim() !== '' && 
           acceptTerms.value && 
           acceptCommunication.value
  })

//route.params.id investigar

const ItemYouWant = parseInt(localStorage.getItem("proposedExchange"), 10); 
const YourId = parseInt(localStorage.getItem("userId"), 10);
  // Métodos
  const fetchSelectedItem = async () => {
    try {
      const response = await axios.get(
        `${appsettings.apiUrl}${appsettings.productRoute}/${ItemYouWant}`,
        {
          headers: {
            "Content-Type": "application/json",
          }
        }
      )
      selectedItem.value = {
        ...response.data,
        imageUrl: appsettings.apiUrl + "/uploads/products/" + (response.data.imageUrl || '/default.jpg')
      }
    } catch (error) {
      console.error('Error fetching selected item:', error)
      // Manejar el error apropiadamente
    }
  }
  
  const fetchMyItems = async () => {
    try {
      const response = await axios.get(
        `${appsettings.apiUrl}${appsettings.userRoute}/products/${YourId}`, // Ajusta la ruta según tu API
        {
          headers: {
            "Content-Type": "application/json",
            // Añade aquí headers de autorización si es necesario
          }
        }
      )
      myItems.value = response.data.map(item => ({
        ...item,
        imageUrl:  (item.imageUrl || 'https://localhost:7001/uploads/products/default.jpg')
      }))
    } catch (error) {
      console.error('Error fetching my items:', error)
      // Manejar el error apropiadamente
    }
  }
  
  const submitProposal = async () => {
    if (!isFormValid.value) return
  
    try {
      const proposal = {
        targetItemId: selectedItem.value.id,
        offeredItemIds: selectedItemIds.value,
        message: message.value,
        terms: acceptTerms.value,
        allowCommunication: acceptCommunication.value
      }
  
      await axios.post(
        `${appsettings.apiUrl}/exchanges/propose`, // Ajusta la ruta según tu API
        proposal,
        {
          headers: {
            "Content-Type": "application/json",
            // Añade aquí headers de autorización si es necesario
          }
        }
      )
  
      // Redirigir a una página de confirmación o a la lista de intercambios
      router.push('/exchanges')
    } catch (error) {
      console.error('Error submitting proposal:', error)
      // Manejar el error apropiadamente
    }
  }
  
  // Lifecycle
  onMounted(() => {
    fetchSelectedItem()
    fetchMyItems()
  })
  </script>