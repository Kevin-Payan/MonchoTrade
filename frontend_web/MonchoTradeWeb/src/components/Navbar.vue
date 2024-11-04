<template>
    <nav class="navbar">
        <div class="navbar-container">
            <div class="logo">MonchoTrade</div>
            
            <ul :class="{ 'show': isMobileMenuOpen }" class="nav-links">
                <li><a href="#home">Home</a></li>
                <li><a href="#about">About</a></li>
                <li><a href="#services">Services</a></li>
                <li><a href="#contact">Contact</a></li>
            </ul>

            <!-- Profile Section -->
            <div class="relative">
                <button 
                    @click="toggleProfileMenu"
                    @blur="handleBlur"
                    class="w-12 h-12 rounded-full bg-gray-200 overflow-hidden focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                    <img 
                        :src=profileImage
                        alt="User Profile" 
                        class="w-full h-full object-cover"
                    >
                </button>
                
              <!-- Dropdown Menu -->
<div 
    v-show="isProfileMenuOpen"
    class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg z-50 py-1"
>
    <router-link 
        to="/my-profile" 
        class="block px-4 py-2 text-gray-800 hover:bg-gray-100 text-sm"
    >
        My Profile 
    </router-link>
    <router-link 
        to="/my-products" 
        class="block px-4 py-2 text-gray-800 hover:bg-gray-100 text-sm"
    >
        My Products
    </router-link>
    <router-link 
        to="/my-exchanges" 
        class="block px-4 py-2 text-gray-800 hover:bg-gray-100 text-sm"
    >
        My Exchanges
    </router-link>
    <router-link 
        to="/login" 
        class="block px-4 py-2 text-gray-800 hover:bg-gray-100 text-sm"
    >
        Logout
    </router-link>
</div>
            </div>

            <button 
                class="menu-toggle" 
                @click="toggleMobileMenu"
                aria-label="Toggle navigation menu"
            >
                <span class="bar"></span>
                <span class="bar"></span>
                <span class="bar"></span>
            </button>
        </div>
    </nav>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { appsettings } from "../../settings/appsettings";

const isProfileMenuOpen = ref(false);
const isMobileMenuOpen = ref(false);
const profileImage = ref('');

const toggleProfileMenu = () => {
    isProfileMenuOpen.value = !isProfileMenuOpen.value;
};

const toggleMobileMenu = () => {
    isMobileMenuOpen.value = !isMobileMenuOpen.value;
};

const axiosConfig = {
  headers: {
    "Content-Type": "application/json",
  },
};

const getProfileImage = async () => {

    const storedId = parseInt(localStorage.getItem("userId"), 10);

    try{
        const response = await axios.get( `${appsettings.apiUrl}${appsettings.profileImageRoute}/user/${storedId}`,axiosConfig);
        profileImage.value = appsettings.apiUrl + response.data;
        console.log(profileImage.value)
    }catch(error){
        console.error("Error fetching profile image:", error); 
    }
};

onMounted(() => {
    getProfileImage(); 
});


// Handle blur event with slight delay to allow for click events on menu items
const handleBlur = (event) => {
    setTimeout(() => {
        if (!event.relatedTarget?.closest('.dropdown-menu')) {
            isProfileMenuOpen.value = false;
        }
    }, 100);
};
</script>

<style scoped>
.navbar {
    background-color: #333;
    color: white;
    padding: 0.75rem 1.25rem;
    position: relative;
}

.navbar-container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    max-width: 1200px;
    margin: 0 auto;
}

.logo {
    font-size: 1.5rem;
    font-weight: bold;
}

.nav-links {
    display: flex;
    list-style: none;
    padding: 0;
    margin: 0;
    gap: 2rem;
}

.nav-links a {
    color: white;
    text-decoration: none;
    font-weight: 500;
    transition: color 0.2s;
}

.nav-links a:hover {
    color: #e2e2e2;
}

.menu-toggle {
    display: none;
    flex-direction: column;
    background: none;
    border: none;
    padding: 0;
    margin-left: 1rem;
    cursor: pointer;
}

.menu-toggle .bar {
    height: 2px;
    width: 24px;
    background-color: white;
    margin: 3px 0;
    transition: 0.3s;
}

@media (max-width: 768px) {
    .nav-links {
        display: none;
        flex-direction: column;
        position: absolute;
        top: 100%;
        left: 0;
        right: 0;
        background-color: #333;
        padding: 1rem;
        gap: 1rem;
    }

    .nav-links.show {
        display: flex;
    }

    .menu-toggle {
        display: flex;
    }
}
</style>