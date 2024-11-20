<template>
    <div>
        <button 
            @click="toggle"
            @blur="handleBlur"
            class="w-12 h-12 rounded-full bg-gray-200 overflow-hidden focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
            <img 
                :src="profileImage"
                alt="User Profile" 
                class="w-full h-full object-cover"
            >
        </button>

        <div v-show="isOpen" 
             class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg z-50 py-1">
            <router-link 
                v-for="(item, index) in filteredMenuItems" 
                :key="index"
                :to="item.route"
                class="block px-4 py-2 text-gray-800 hover:bg-gray-100 text-sm"
                @click="close"
            >
                {{ item.label }}
            </router-link>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import axios from 'axios';
import { appsettings } from "../../settings/appsettings";

const isOpen = ref(false);
const profileImage = ref('');
const route = useRoute(); // Usamos `useRoute` para acceder a la ruta actual

const menuItems = [
    { label: 'Home', route: '/home' },
    { label: 'My Profile', route: '/my-profile' },
    { label: 'My Products', route: '/my-products' },
    { label: 'My Exchanges', route: '/my-exchanges' },
    { label: 'Logout', route: '/' }
];

// Filtra los elementos del menú dinámicamente según la ruta actual
const filteredMenuItems = computed(() => {
    return menuItems.filter(item => item.route !== route.path);
});

const toggle = () => {
    isOpen.value = !isOpen.value;
};

const close = () => {
    isOpen.value = false;
};

const handleBlur = (event) => {
    setTimeout(() => {
        if (!event.relatedTarget?.closest('.dropdown-menu')) {
            isOpen.value = false;
        }
    }, 100);
};

const getProfileImage = async () => {
    const storedId = parseInt(localStorage.getItem("userId"), 10);
    try {
        const response = await axios.get(`${appsettings.apiUrl}${appsettings.profileImageRoute}/user/${storedId}`, {
            headers: { "Content-Type": "application/json" }
        });
        profileImage.value = appsettings.apiUrl + response.data;
    } catch(error) {
        console.error("Error fetching profile image:", error);
    }
};

onMounted(() => {
    getProfileImage();
});
</script>
