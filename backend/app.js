const express = require("express");
const connectDB = require("./config/db");

// Routers
const productRoutes = require("./routes/product");

const app = express();
const port = process.env.PORT || 3000; // Define the port variable

// Middleware for parsing JSON data
app.use(express.json());

// Connect to MongoDB
connectDB();

// Mount all routes
app.use("/api/products", productRoutes);

// Start the server
app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}/`);
});
