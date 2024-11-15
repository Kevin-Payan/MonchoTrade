<template>
    <nav class="navbar">
        <div class="navbar-container">
            <div class="logo">MonchoTrade</div>
            <!-- Profile Section -->
            <div class="relative">
                
                <ProfileDropdown/>
            </div>
        </div>
    </nav>
</template>

<script setup>
import ProfileDropdown from './ProfileDropdown.vue';
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