const express = require('express');
const router = express.Router();
const jobController = require('../controllers/job.controller');
const { verifyToken, isAdmin, isServiceAdvisor, isMechanic } = require('../middleware/authJwt');

// Create a new job
router.post(
  '/',
  [verifyToken, (req, res, next) => { // Custom middleware to check for admin or service_advisor
    if (req.userRole === 'admin' || req.userRole === 'service_advisor') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin or Service Advisor Role!' });
    }
  }],
  jobController.createJob
);

// Get all jobs
router.get(
  '/',
  [verifyToken, (req, res, next) => { // Custom middleware to check for admin, service_advisor or mechanic
    if (req.userRole === 'admin' || req.userRole === 'service_advisor' || req.userRole === 'mechanic') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin, Service Advisor, or Mechanic Role!' });
    }
  }],
  jobController.getAllJobs
);

// Get a single job by ID
router.get(
  '/:id',
  [verifyToken, (req, res, next) => {
    if (req.userRole === 'admin' || req.userRole === 'service_advisor' || req.userRole === 'mechanic') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin, Service Advisor, or Mechanic Role!' });
    }
  }],
  jobController.getJobById
);

// Update an existing job
router.put(
  '/:id',
  [verifyToken, (req, res, next) => {
    if (req.userRole === 'admin' || req.userRole === 'service_advisor') {
      next();
    } else {
      res.status(403).json({ message: 'Require Admin or Service Advisor Role!' });
    }
  }],
  jobController.updateJob
);

// Delete a job (soft delete)
router.delete(
  '/:id',
  [verifyToken, isAdmin], // Only Admin can delete
  jobController.deleteJob
);

module.exports = router;
