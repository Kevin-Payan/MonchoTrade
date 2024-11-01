<script setup>
import { ref } from 'vue';
import axios from 'axios';
import { appsettings } from '../../settings/appsettings';
import { useRouter } from 'vue-router';
const email = ref('');
const password = ref('');
const errorMessage = ref('');
const router = useRouter(); // Instantiate router

const axiosConfig = {
    headers: {
        'Content-Type': 'application/json',
    }
};

    const handleSubmit = async () => {
        errorMessage.value = '';
        try {
            const response = await axios.post(
                `${appsettings.apiUrl}${appsettings.sessionRoute}/login`,
                {
                    email: email.value,
                    password: password.value,
                },
                axiosConfig
            );
            console.log('Login successful:', response.data);
            router.push('/'); // Adjust this route as needed
        } catch (error) {
            if (error.response) {
                errorMessage.value = error.response.data || 'Login failed. Please try again.';
            } else {
                errorMessage.value = 'No response from server.';
            }
        }
    };
</script>


<template>
    <form @submit.prevent="handleSubmit">
        <label for="email">Email</label><br>
        <input id="email" type="email"  v-model="email" placeholder="m@example.com" required><br>
        
        <label for="password">Password</label><br>
        <input id="password" type="password"  v-model="password" placeholder="Enter your password" required><br>

        <button type="submit">Login</button>
    </form>
</template>

<style>
/* Add styles here */
form {
    max-width: 400px;
    margin: 50px auto; /* Center the form vertically */
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    background-color: #fff;
}

label {
    display: block;
    margin: 10px 0 5px;
    font-weight: bold;
}

input {
    width: 100%;
    padding: 10px;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    border-radius: 4px;
    transition: border-color 0.3s;
}

input:focus {
    border-color: #4CAF50; /* Change border color on focus */
    outline: none; /* Remove default outline */
}

button {
    padding: 10px;
    width: 100%;
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 16px;
    transition: background-color 0.3s;
}

button:hover {
    background-color: #45a049; /* Darker shade on hover */
}
</style>
