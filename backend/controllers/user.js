const User = require("../models/user");

const createUser = async (req, res) => {
  try {
    const product = await User.create(req.body);
    res.status(200).json(product);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

module.exports = { createUser };
