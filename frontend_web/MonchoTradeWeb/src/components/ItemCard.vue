 <!-- components/ItemCard.vue -->
 <template>
  <div 
    class="bg-white rounded-lg border border-gray-200 overflow-hidden hover:shadow-lg transition-shadow duration-300"
    :class="{ 'animate-pulse': loading }"
  >
    <!-- Card Header -->
    <h3 class="text-xl font-semibold p-4">{{ title }}</h3>

    <!-- Image Container -->
    <div class="aspect-video relative bg-gray-100 overflow-hidden">
      <img
        v-if="imageUrl"
        :src="imageUrl"
        :alt="title"
        class="w-full h-full object-cover transition-opacity duration-300"
        :class="{ 'opacity-0': loading, 'opacity-100': !loading }"
        @load="handleImageLoad"
        @error="handleImageError"
      >
      <div
        v-if="!imageUrl || imageError"
        class="w-full h-full flex items-center justify-center text-gray-400"
      >
        <span class="flex items-center">
          <svg
            class="w-6 h-6 mr-2"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z"
            />
          </svg>
          No image available
        </span>
      </div>
    </div>

    <!-- Card Content -->
    <div class="p-4 space-y-4">
      <p v-if="description" class="text-gray-600 text-sm">
        {{ description }}
      </p>
      
      <div class="text-sm text-gray-600">
        Offered by: <span class="font-medium">{{ offeredBy }}</span>
      </div>

      <!-- Exchange Button -->
      <button
        @click="$emit('propose-exchange')"
        class="w-full bg-black text-white py-2 px-4 rounded-md flex items-center justify-center space-x-2 hover:bg-gray-800 transition-colors focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-black disabled:opacity-50 disabled:cursor-not-allowed"
        :disabled="loading"
      >
        <svg 
          class="w-4 h-4" 
          viewBox="0 0 24 24" 
          fill="none" 
          stroke="currentColor"
        >
          <path 
            stroke-linecap="round" 
            stroke-linejoin="round" 
            stroke-width="2" 
            d="M8 7h12m0 0l-4-4m4 4l-4 4m0 6H4m0 0l4 4m-4-4l4-4"
          />
        </svg>
        <span>Propose Exchange</span>
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const props = defineProps({
  title: {
    type: String,
    required: true
  },
  imageUrl: {
    type: String,
    default: ''
  },
  offeredBy: {
    type: String,
    required: true
  },
  description: {
    type: String,
    default: ''
  }
})

const emit = defineEmits(['propose-exchange'])

const loading = ref(true)
const imageError = ref(false)

const handleImageLoad = () => {
  loading.value = false
}

const handleImageError = () => {
  loading.value = false
  imageError.value = true
}
</script>