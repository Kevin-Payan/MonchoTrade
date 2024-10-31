<script setup>
// Here you can set up any necessary logic for the form
</script>

<template>
    <form  @submit.prevent="handleSubmit">
        <label for="name">Name</label><br>
        <input id="name" type="text" placeholder="Jose" required><br>

        <label for="lastName">Last Name</label><br>
        <input id="lastName" type="text" placeholder="Carlos" required><br>

        <label for="secondLastName">Second Last Name</label><br>
        <input id="secondLastName" type="text" placeholder="Pérez" required><br>

        <label for="email">Email</label><br>
        <input id="email" type="email" placeholder="m@example.com" required><br>

        <label for="country">Country</label><br>
        <input id="country" type="text" placeholder="Country" required><br>

        <label for="phone">Phone Number</label><br>
        <input id="phone" type="tel" placeholder="(123) 456-7890" required><br>

        <label for="password">Password</label><br>
        <input id="password" type="password" required><br>

        <label for="confirmPassword">Confirm Password</label><br>
        <input id="confirmPassword" type="password" required><br>

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
