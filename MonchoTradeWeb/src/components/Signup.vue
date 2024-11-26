<script setup>
import { ref } from 'vue';
import axios from 'axios';
import { appsettings } from '../../settings/appsettings';
import { useRouter } from 'vue-router';
const name = ref('');
const lastName = ref('');
const secondLastName = ref('');
const birthdate = ref('');
const email = ref('');
const country = ref('');
const phoneNumber = ref('');
const password = ref('');
const confirmPassword = ref('');

const router = useRouter();

const handleSubmit = async () => {
    try {
        const response = await axios.post(`${appsettings.apiUrl}${appsettings.sessionRoute}/signup`, {
            name: name.value,
            lastName: lastName.value,
            secondLastName: secondLastName.value,
            birthdate: birthdate.value,
            email: email.value,
            country: country.value,
            phoneNumber: phoneNumber.value,
            passwordHash: password.value,
        }, appsettings.axiosConfig);

        console.log('Signup successful:', response.data);
        router.push('/'); 
    } catch (error) {
        console.error('Signup failed:', error);
    }
};



</script>

<template>
    <form  @submit.prevent="handleSubmit">
        <label for="name">Name</label><br>
        <input id="name" type="text"  v-model="name" placeholder="Jose" required><br>

        <label for="lastName">Last Name</label><br>
        <input id="lastName" type="text"  v-model="lastName" placeholder="Carlos" required><br>

        <label for="secondLastName">Second Last Name</label><br>
        <input id="secondLastName" type="text" v-model="secondLastName" placeholder="Pérez" required><br>

        <label for="birthdate">Birthdate</label><br>
        <input id="birthdate" type="date"   v-model="birthdate" required><br>

        <label for="email">Email</label><br>
        <input id="email" type="email"  v-model="email" placeholder="m@example.com" required><br>

        <label for="country">Country</label><br>
        <input id="country" type="text"  v-model="country" placeholder="Country" required><br>

        <label for="phone">Phone Number</label><br>
        <input id="phone" type="tel"   v-model="phoneNumber" placeholder="(123) 456-7890" required><br>

        <label for="password">Password</label><br>
        <input id="password" type="password"   v-model="password" required><br>

        <label for="confirmPassword">Confirm Password</label><br>
        <input id="confirmPassword" type="password" v-model="confirmPassword" required><br>

        <button type="submit">Submit</button>
    </form>
</template>

<style>
/* Add styles here if needed */
form {
    max-width: 400px;
    margin: auto;
}

label {
    display: block;
    margin: 8px 0 4px;
}

input {
    width: 100%;
    padding: 8px;
    margin-bottom: 12px;
    border: 1px solid #ccc;
    border-radius: 4px;
}

button {
    padding: 10px 15px;
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

button:hover {
    background-color: #45a049;
}
</style>