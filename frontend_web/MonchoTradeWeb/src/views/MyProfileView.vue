<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Previous template code remains unchanged until the handleImageUpload function -->
    <header class="bg-white shadow">
      <div class="container mx-auto px-4 py-6">
        <div class="flex justify-between items-center">
          <h1 class="text-2xl font-bold text-gray-900">My Profile</h1>
          <div class="absolute top-4 right-4">
            <ProfileDropdown />
          </div>
         
          <!-- 
          <div class="flex gap-4">
            <button class="btn-secondary">
              <i class="fas fa-cog mr-2"></i>
              Configuration
            </button>
            <button class="btn-danger">
              <i class="fas fa-sign-out-alt mr-2"></i>
              Logout
            </button>
          </div>
          -->

        </div>
      </div>
    </header>

    <!-- Main Content -->
    <main class="container mx-auto px-4 py-8">
      <div v-if="loading" class="flex justify-center items-center min-h-[400px]">
        <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>

      <div v-else-if="error" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative" role="alert">
        <strong class="font-bold">Error!</strong>
        <span class="block sm:inline"> {{ error }}</span>
      </div>

      <div v-else class="grid grid-cols-1 md:grid-cols-3 gap-8">
        <!-- Left Column - Personal Info -->
        <div class="col-span-1">
          <div class="bg-white rounded-lg shadow p-6">
            <!-- Profile Picture -->
            <div class="flex flex-col items-center">
              <div class="relative">
                <img 
                  :src="profilePic"
                  class="w-32 h-32 rounded-full object-cover"
                  alt="Profile picture"
                >
                <button 
                  @click="handleImageUpload"
                  class="absolute bottom-0 right-0 bg-primary text-white p-2 rounded-full"
                  :disabled="imageUploading"
                >
                  <i v-if="!imageUploading" class="fas fa-camera"></i>
                  <i v-else class="fas fa-spinner animate-spin"></i>
                </button>
              </div>
              <h2 class="mt-4 text-xl font-semibold">{{ userProfile.name }} {{ userProfile.lastName }}</h2>
              <p class="text-gray-600">{{ userProfile.secondLastName }}</p>
            </div>

            <!-- Rest of the template remains the same -->
            <!-- Contact Information -->
            <div class="mt-6 space-y-4">
              <div class="flex items-center">
                <i class="fas fa-envelope w-6 text-gray-400"></i>
                <span class="ml-3">{{ userProfile.email }}</span>
              </div>
              <div class="flex items-center">
                <i class="fas fa-phone w-6 text-gray-400"></i>
                <span class="ml-3">{{ userProfile.phoneNumber }}</span>
              </div>
              <div class="flex items-center">
                <i class="fas fa-map-marker-alt w-6 text-gray-400"></i>
                <span class="ml-3">{{ userProfile.country }}</span>
              </div>
            </div>
          </div>
        </div>

        <!-- Right Column - Profile Details -->
        <div class="col-span-1 md:col-span-2">
          <div class="bg-white rounded-lg shadow p-6">
            <h3 class="text-xl font-semibold mb-4">Profile Details</h3>
            
            <!-- Edit Form -->
            <form @submit.prevent="updateProfile" class="space-y-6">
              <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div class="form-group">
                  <label class="label">Name</label>
                  <input 
                    v-model="formData.name"
                    type="text" 
                    class="input"
                  >
                </div>

                <div class="form-group">
                  <label class="label">Last Name</label>
                  <input 
                    v-model="formData.lastName"
                    type="text" 
                    class="input"
                  >
                </div>

                <div class="form-group">
                  <label class="label">Second Last Name</label>
                  <input 
                    v-model="formData.secondLastName"
                    type="text" 
                    class="input"
                  >
                </div>

                <div class="form-group">
                  <label class="label">Phone Number</label>
                  <input 
                    v-model="formData.phoneNumber"
                    type="tel" 
                    class="input"
                  >
                </div>

                <div class="form-group md:col-span-2">
                  <label class="label">Country</label>
                  <input 
                    v-model="formData.country"
                    type="text" 
                    class="input"
                  >
                </div>
              </div>

              <div class="flex justify-end space-x-4">
                <button 
                  type="button"
                  class="btn-secondary"
                  @click="resetForm"
                  :disabled="loading"
                >
                  Cancel
                </button>
                <button 
                  type="submit"
                  class="btn-primary"
                  :disabled="loading"
                >
                  <span v-if="loading" class="animate-spin mr-2">↻</span>
                  Save Changes
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { appsettings } from "../../settings/appsettings"
import axios from 'axios'
import ProfileDropdown from '@/components/ProfileDropdown.vue'

const axiosConfig = {
  headers: {
    "Content-Type": "application/json",
  },
}

const axiosPatchConfig = {
  headers: {
    "Content-Type": 'application/json-patch+json',
  },
}

// State
const userProfile = ref({})
const formData = ref({})
const loading = ref(false)
const error = ref(null)
const imageUploading = ref(false)
const storedId = parseInt(localStorage.getItem("userId"), 10)
const profilePic = ref('')

// Get user profile on component mount
onMounted(async () => {
  await fetchUserProfile()
})

// Fetch user profile data
const fetchUserProfile = async () => {
  loading.value = true
  error.value = null
  
  try {
    const response = await axios.get(`${appsettings.apiUrl}/user/${storedId}`, axiosConfig)
    userProfile.value = response.data
    profilePic.value = `${appsettings.apiUrl}/uploads/profiles/${userProfile.value.profileImageUrl || 'default.jpg'}`
    
    // Initialize form data with current values
    formData.value = {
      name: response.data.name,
      lastName: response.data.lastName,
      secondLastName: response.data.secondLastName,
      phoneNumber: response.data.phoneNumber,
      country: response.data.country
    }
  } catch (err) {
    error.value = err.response?.data?.error || 'Error fetching profile'
    console.error('Error fetching profile:', err)
  } finally {
    loading.value = false
  }
}

const handleImageUpload = () => {
  const input = document.createElement('input')
  input.type = 'file'
  input.accept = 'image/*'
  
  input.onchange = async (event) => {
    const file = event.target.files[0]
    if (!file) return

    // Check file size (1MB = 1 * 1024 * 1024 bytes)
    if (file.size > 1 * 1024 * 1024) {
      error.value = 'File size should not exceed 1 MB'
      return
    }

    // Check file extension
    const allowedExtensions = [".jpg", ".jpeg", ".png", ".jfif"]
    const fileExtension = '.' + file.name.split('.').pop().toLowerCase()
    if (!allowedExtensions.includes(fileExtension)) {
      error.value = 'Only .jpg, .jpeg, .jfif and .png files are allowed'
      return
    }

    const formData = new FormData()
    formData.append('Id', storedId)
    formData.append('UserId', storedId) // Adding UserId as required by DTO
    formData.append('ProfileImageUrl', userProfile.value.profileImageUrl || '') // Adding current ProfileImageUrl
    formData.append('ImageFile', file)
    
    imageUploading.value = true
    error.value = null

    try {
      const response = await axios.put(
        `${appsettings.apiUrl}${appsettings.profileImageRoute}/${storedId}`,
        formData,
        {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        }
      )

      // Update profile picture URL
      if (response.data && response.data.url) {
        profilePic.value = `${appsettings.apiUrl}/uploads/profiles/${response.data.url}`
        userProfile.value.profileImageUrl = response.data.url
      }
    } catch (err) {
      error.value = err.response?.data || 'Error uploading image'
      console.error('Error uploading image:', err)
    } finally {
      imageUploading.value = false
    }
  }

  input.click()
}


const updateProfile = async () => {
  loading.value = true
  error.value = null
  
  // Generate patch operations for changed fields
  const patchDocument = Object.entries(formData.value).reduce((patches, [key, value]) => {
    if (value !== userProfile.value[key]) {
      patches.push({
        op: key in userProfile.value ? 'replace' : 'add',
        path: `/${key}`,
        value: value
      })
    }
    return patches
  }, [])

  if (patchDocument.length > 0) {
    try {
      const response = await axios({
        method: 'PATCH',
        url: `${appsettings.apiUrl}/user/${storedId}`,
        headers: {
          'Content-Type': 'application/json-patch+json'
        },
        data: patchDocument
      })
      
      userProfile.value = response.data
      error.value = null
    } catch (err) {
      error.value = err.response?.data?.error || 'Error updating profile'
    }
  }
  
  loading.value = false
}

const resetForm = () => {
  formData.value = { ...userProfile.value }
}
</script>

<style scoped>
.btn-primary {
  @apply bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 
         transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed;
}

.btn-secondary {
  @apply bg-gray-200 text-gray-700 px-4 py-2 rounded-md hover:bg-gray-300 
         transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed;
}

.btn-danger {
  @apply bg-red-600 text-white px-4 py-2 rounded-md hover:bg-red-700 
         transition-colors duration-200;
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