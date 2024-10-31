<template>
    <nav class="navbar">
        <div class="navbar-container">
            <div class="logo">MonchoTrade</div>
            <ul class="nav-links">
                <li><a href="#home">Home</a></li>
                <li><a href="#about">About</a></li>
                <li><a href="#services">Services</a></li>
                <li><a href="#contact">Contact</a></li>
            </ul>
            <div class="menu-toggle" @click="toggleMenu">
                <span class="bar"></span>
                <span class="bar"></span>
                <span class="bar"></span>
            </div>
        </div>
    </nav>
</template>

<script setup>
import { ref } from 'vue';

const isMenuOpen = ref(false);

const toggleMenu = () => {
    isMenuOpen.value = !isMenuOpen.value;
};
</script>

<style>
.navbar {
    background-color: #333;
    color: white;
    padding: 10px 20px;
}

.navbar-container {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.logo {
    font-size: 24px;
    font-weight: bold;
}

.nav-links {
    display: flex;
    list-style: none;
    padding: 0;
    margin: 0;
}

.nav-links li {
    margin: 0 15px;
}

.nav-links a {
    color: white;
    text-decoration: none;
    font-weight: 500;
}

.nav-links a:hover {
    text-decoration: underline;
}

.menu-toggle {
    display: none;
    flex-direction: column;
    cursor: pointer;
}

.menu-toggle .bar {
    height: 3px;
    width: 25px;
    background-color: white;
    margin: 4px 0;
}

/* Responsive Styles */
@media (max-width: 768px) {
    .nav-links {
        display: none;
        flex-direction: column;
        position: absolute;
        top: 60px; /* Adjust according to your navbar height */
        left: 0;
        background-color: #333;
        width: 100%;
    }

    .nav-links.show {
        display: flex;
    }

    .menu-toggle {
        display: flex;
    }
}
</style>
