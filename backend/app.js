const express = require("express");
const connectDB = require("./config/db");

// Routers
const productRoutes = require("./routes/product");
const userRoutes = require("./routes/user");

// Initialize express app
const app = express();
const port = process.env.PORT || 3000;

// Middleware for parsing JSON data
app.use(express.json());

// Mount all routes
app.use("/api/products", productRoutes);
app.use("/api/users", userRoutes);

// Connect to MongoDB
connectDB();

// Start the server
app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}/`);
});
