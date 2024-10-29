const express = require("express");
const router = express.Router();
const productController = require("../controllers/product");

router.get("/", productController.getAllProducts);

router.get("/:id", productController.getProductById);

router.post("/", productController.postProduct);

module.exports = router;
