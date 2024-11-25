<script setup>
import { EnvelopeIcon } from "@heroicons/vue/24/outline";
import { ref } from "vue";
import axios from "axios";
import { appsettings } from "../../settings/appsettings";
import { useRouter } from "vue-router";
const email = ref("");
const password = ref("");
const errorMessage = ref("");
const router = useRouter(); // Instantiate router


const handleSubmit = async () => {
  errorMessage.value = "";
  try {
    const response = await axios.post(
      `${appsettings.apiUrl}${appsettings.sessionRoute}/login`,
      {
        email: email.value,
        password: password.value,
      },
      appsettings.axiosConfig
    );
    console.log("Login successful:", response.data);

    // Assuming the response data contains an id property
    if (response.data.id) {
      localStorage.setItem("userId", response.data.id.toString());
    } else {
      console.error("No user ID returned from server");
    }

    router.push("/"); // Adjust this route as needed
  } catch (error) {
    if (error.response) {
      errorMessage.value =
        error.response.data || "Login failed. Please try again.";
    } else {
      errorMessage.value = "No response from server.";
    }
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
    <h1 class="font-bold text-center text-2xl">Log in</h1>
    <h2 class="text-center text-gray-500 text-sm">
      Enter your email and password to access your acount
    </h2>
    <label for="email">Email</label>
    <input
      id="email"
      type="email"
      v-model="email"
      placeholder="m@example.com"
      required
    /><br />

    <label for="password">Password</label>
    <input id="password" type="password" v-model="password" required /><br />
    <div class="flex justify-start mb-3">
      <label class="flex items-center">
        <input type="checkbox" class="w-4 h-4 m-1" />
        <p class="">Remember Me</p>
      </label>
    </div>
    <button type="submit">Log in</button>
    <h2 class="text-center text-gray-500 text-xs p-3">OR CONTINUE WITH</h2>
    <button class="google-button">
      <EnvelopeIcon class="size-6 m-2" />Google
    </button>
    <p class="text-sm flex justify-center p-2">
      Don't have an account?
      <router-link to="/signup" class="underline ml-1">Sign up</router-link>
    </p>
    <p class="text-sm flex justify-center underline p-2">
      Forgot your password?
    </p>
  </form>
</template>

<style>
/* Add styles here */
form {
  max-width: 400px;
  margin: 50px auto; /* Center the form vertically */
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 12px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  background-color: #fff;
}

label {
  display: block;
  margin: 10px 0 5px;
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
  border-color: #4caf50; /* Change border color on focus */
  outline: none; /* Remove default outline */
}

button {
  padding: 10px 0px;
  width: 100%;
  background-color: #1d1d1d;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s;
}

.google-button {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 15px;
  padding: 4px 0px;
  width: 100%;
  background-color: #ffffff;
  color: rgb(0, 0, 0);
  border: 1px solid;
  border-color: #cccccc;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s;
}

.google-button:hover {
  background-color: #e9e9e9e1;
  transition: background-color 0.3s;
}

button:hover {
  background-color: #333333;
}
</style>
