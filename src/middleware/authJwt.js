const jwt = require('jsonwebtoken');
require('dotenv').config();
const User = require('../models/User'); // Import User model for role checks

const JWT_SECRET = process.env.JWT_SECRET;

exports.verifyToken = (req, res, next) => {
  const authHeader = req.headers.authorization;

  if (!authHeader || !authHeader.startsWith('Bearer ')) {
    return res.status(403).json({ message: 'No token provided or token is not Bearer type.' });
  }

  const token = authHeader.substring(7, authHeader.length); // Extract token after "Bearer "

  jwt.verify(token, JWT_SECRET, (err, decoded) => {
    if (err) {
      if (err.name === 'TokenExpiredError') {
        return res.status(401).json({ message: 'Unauthorized! Token has expired.' });
      }
      return res.status(401).json({ message: 'Unauthorized! Invalid token.' });
    }
    req.userId = decoded.userId; // Attach userId to request
    req.userRole = decoded.role;   // Attach role to request
    next();
  });
};

exports.isAdmin = async (req, res, next) => {
  try {
    const user = await User.findById(req.userId);
    if (user && user.role === 'admin') {
      next();
      return;
    }
    res.status(403).json({ message: 'Require Admin Role!' });
  } catch (error) {
    console.error('isAdmin error:', error);
    res.status(500).json({ message: 'Server error during admin role check.' });
  }
};

exports.isMechanic = async (req, res, next) => {
  try {
    const user = await User.findById(req.userId);
    if (user && user.role === 'mechanic') {
      next();
      return;
    }
    res.status(403).json({ message: 'Require Mechanic Role!' });
  } catch (error) {
    console.error('isMechanic error:', error);
    res.status(500).json({ message: 'Server error during mechanic role check.' });
  }
};

exports.isServiceAdvisor = async (req, res, next) => {
  try {
    const user = await User.findById(req.userId);
    if (user && user.role === 'service_advisor') {
      next();
      return;
    }
    res.status(403).json({ message: 'Require Service Advisor Role!' });
  } catch (error) {
    console.error('isServiceAdvisor error:', error);
    res.status(500).json({ message: 'Server error during service advisor role check.' });
  }
};
