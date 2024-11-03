<script setup>
import { ref } from "vue";
import axios from "axios";
import { appsettings } from "../../settings/appsettings";
import { useRouter } from "vue-router";
const name = ref("");
const lastName = ref("");
const secondLastName = ref("");
const birthdate = ref("");
const email = ref("");
const country = ref("");
const phoneNumber = ref("");
const password = ref("");
const confirmPassword = ref("");

const router = useRouter();

const axiosConfig = {
  headers: {
    "Content-Type": "application/json",
  },
};

const handleSubmit = async () => {
  try {
    const response = await axios.post(
      `${appsettings.apiUrl}${appsettings.sessionRoute}/signup`,
      {
        name: name.value,
        lastName: lastName.value,
        secondLastName: secondLastName.value,
        birthdate: birthdate.value,
        email: email.value,
        country: country.value,
        phoneNumber: phoneNumber.value,
        passwordHash: password.value,
      },
      axiosConfig
    );

    console.log("Signup successful:", response.data);
    router.push("/");
  } catch (error) {
    console.error("Signup failed:", error);
  }
};
</script>

<template>
  <div class="flex justify-center m-4">
    <img
      src="@/assets/monchotradelogo.png"
      alt="Image logo"
      class="w-64 h-64"
    />
  </div>
  <form @submit.prevent="handleSubmit">
    <h1 class="font-bold text-center text-2xl">Sign up</h1>
    <label for="name">Name</label>
    <input id="name" type="text" v-model="name" placeholder="Kevin" required />

    <label for="lastName">Last Name</label>
    <input
      id="lastName"
      type="text"
      v-model="lastName"
      placeholder="Payan"
      required
    />

    <label for="email">Email</label>
    <input
      id="email"
      type="email"
      v-model="email"
      placeholder="m@example.com"
      required
    />

    <!--
    <label for="country">Country</label>
    <input
      id="country"
      type="text"
      v-model="country"
      placeholder="Country"
      required
    />
    
    <label for="phone">Phone Number</label>
    <input
      id="phone"
      type="tel"
      v-model="phoneNumber"
      placeholder="(123) 456-7890"
      required
    />
    -->

    <label for="password">Password</label>
    <input id="password" type="password" v-model="password" required />

    <label for="confirmPassword">Confirm Password</label>
    <input
      id="confirmPassword"
      type="password"
      v-model="confirmPassword"
      required
    />

    <button type="submit" class="mt-4 mb-4">Continue</button>
    <p class="pt-4 pb-4">
      Already have an account?
      <router-link to="/" class="underline ml-1">Log in</router-link>
    </p>
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
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #45a049;
}
</style>
