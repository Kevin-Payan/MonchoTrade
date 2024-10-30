const express = require("express");
const router = express.Router();
const userController = require("../controllers/user");

//create new user
//edit user

router.post("/", userController.createUser);

module.exports = router;
