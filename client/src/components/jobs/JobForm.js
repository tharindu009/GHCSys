import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import jobService from '../../services/jobService';
import customerService from '../../services/customerService';
import vehicleService from '../../services/vehicleService';
import userService from '../../services/userService';
import serviceService from '../../services/serviceService'; // For billable services
import inventoryService from '../../services/inventoryService';
import '../../assets/css/JobForm.css'; // Create this for styling

const JobForm = ({ jobData, isEditMode = false }) => {
  const [formData, setFormData] = useState({
    customer: '',
    vehicle: '',
    description: '',
    jobDate: new Date().toISOString().split('T')[0], // Default to today
    assignedMechanic: '',
    services: [], // Array of service IDs
    parts: [], // Array of { item: partId, quantity: number }
    status: 'Pending',
    estimatedCost: 0,
  });

  const [customers, setCustomers] = useState([]);
  const [vehicles, setVehicles] = useState([]); // Vehicles for selected customer
  const [mechanics, setMechanics] = useState([]);
  const [availableServices, setAvailableServices] = useState([]);
  const [availableParts, setAvailableParts] = useState([]);
  
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [formError, setFormError] = useState({}); // For field-specific errors

  const navigate = useNavigate();

  // Populate dropdowns
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const [custRes, mechRes, servRes, partRes] = await Promise.all([
          customerService.getAllCustomers(),
          userService.getAllUsers({ role: 'mechanic' }), // Assuming backend filters by role
          serviceService.getAllServices(),
          inventoryService.getAllInventoryItems({ limit: 1000 }), // Get all parts for selection
        ]);
        setCustomers(custRes.customers || custRes || []); // Adjust based on actual API response structure
        setMechanics(mechRes.users || mechRes || []);
        setAvailableServices(servRes.services || servRes || []);
        setAvailableParts(partRes.items || partRes || []);
      } catch (err) {
        setError('Failed to load necessary data for the form: ' + err.message);
        console.error("Data loading error:", err);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  // Populate vehicles when customer changes
  useEffect(() => {
    if (formData.customer) {
      const fetchVehicles = async () => {
        try {
          setLoading(true);
          const data = await vehicleService.getVehiclesByCustomerId(formData.customer);
          setVehicles(data.vehicles || data || []); // Adjust based on actual API response
          // If editing, and the original vehicle belongs to the selected customer, keep it.
          // Otherwise, reset vehicle if the new customer doesn't own the currently selected vehicle.
          if (jobData && jobData.vehicle && jobData.vehicle.customer !== formData.customer) {
            setFormData(prev => ({ ...prev, vehicle: '' }));
          }
        } catch (err) {
          setError('Failed to load vehicles for customer: ' + err.message);
          setVehicles([]);
        } finally {
          setLoading(false);
        }
      };
      fetchVehicles();
    } else {
      setVehicles([]); // Clear vehicles if no customer is selected
    }
  }, [formData.customer, jobData]);

  // Initialize form if in edit mode and jobData is provided
  useEffect(() => {
    if (isEditMode && jobData) {
      setFormData({
        customer: jobData.customer?._id || jobData.customer || '',
        vehicle: jobData.vehicle?._id || jobData.vehicle || '',
        description: jobData.description || '',
        jobDate: jobData.jobDate ? new Date(jobData.jobDate).toISOString().split('T')[0] : new Date().toISOString().split('T')[0],
        assignedMechanic: jobData.assignedMechanic?._id || jobData.assignedMechanic || '',
        services: jobData.services?.map(s => typeof s === 'object' ? s._id : s) || [],
        parts: jobData.parts?.map(p => ({
          item: p.item?._id || p.item,
          quantity: p.quantity || 1,
        })) || [],
        status: jobData.status || 'Pending',
        estimatedCost: jobData.estimatedCost || 0,
      });
    }
  }, [isEditMode, jobData]);


  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    setFormError(prev => ({...prev, [name]: ''})); // Clear error on change
  };

  const handleServiceChange = (serviceId) => {
    setFormData(prev => ({
      ...prev,
      services: prev.services.includes(serviceId)
        ? prev.services.filter(id => id !== serviceId)
        : [...prev.services, serviceId],
    }));
  };

  const handlePartChange = (partId, quantity) => {
    const numQuantity = parseInt(quantity, 10);
    setFormData(prev => {
      const existingPartIndex = prev.parts.findIndex(p => p.item === partId);
      let newParts = [...prev.parts];
      if (existingPartIndex > -1) {
        if (numQuantity > 0) {
          newParts[existingPartIndex] = { ...newParts[existingPartIndex], quantity: numQuantity };
        } else {
          newParts.splice(existingPartIndex, 1); // Remove if quantity is 0 or less
        }
      } else if (numQuantity > 0) {
        newParts.push({ item: partId, quantity: numQuantity });
      }
      return { ...prev, parts: newParts };
    });
  };
  
  const validateForm = () => {
    const errors = {};
    if (!formData.customer) errors.customer = "Customer is required.";
    if (!formData.vehicle) errors.vehicle = "Vehicle is required.";
    if (!formData.description.trim()) errors.description = "Description is required.";
    if (!formData.jobDate) errors.jobDate = "Job date is required.";
    if (!formData.status) errors.status = "Status is required.";
    // assignedMechanic can be optional
    setFormError(errors);
    return Object.keys(errors).length === 0;
  };


  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) {
      setError("Please fill all required fields correctly.");
      return;
    }
    setLoading(true);
    setError('');
    try {
      const payload = { ...formData };
      // Ensure parts are just item ID and quantity
      payload.parts = formData.parts.map(p => ({ item: p.item, quantity: p.quantity }));

      if (isEditMode) {
        await jobService.updateJob(jobData._id, payload);
        navigate(`/jobs/${jobData._id}`); // Navigate to job view page
      } else {
        const newJob = await jobService.createJob(payload);
        navigate(`/jobs/${newJob.job._id}`); // Navigate to the new job's view page
      }
    } catch (err) {
      setError(err.message || `Failed to ${isEditMode ? 'update' : 'create'} job.`);
      if (err.errors) { // Handle validation errors from backend
        setFormError(err.errors);
      }
    } finally {
      setLoading(false);
    }
  };
  
  if (loading && !customers.length) return <p>Loading form data...</p>; // Show loading only on initial data fetch

  return (
    <form onSubmit={handleSubmit} className="job-form">
      {error && <p className="error-message">{error}</p>}
      
      <div className="form-group">
        <label htmlFor="customer">Customer:</label>
        <select id="customer" name="customer" value={formData.customer} onChange={handleChange} required>
          <option value="">Select Customer</option>
          {customers.map(c => <option key={c._id} value={c._id}>{c.name}</option>)}
        </select>
        {formError.customer && <span className="form-error">{formError.customer}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="vehicle">Vehicle:</label>
        <select id="vehicle" name="vehicle" value={formData.vehicle} onChange={handleChange} required disabled={!formData.customer || vehicles.length === 0}>
          <option value="">Select Vehicle</option>
          {vehicles.map(v => <option key={v._id} value={v._id}>{v.make} {v.model} ({v.licensePlate || v.vin})</option>)}
        </select>
        {formError.vehicle && <span className="form-error">{formError.vehicle}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="description">Description:</label>
        <textarea id="description" name="description" value={formData.description} onChange={handleChange} required />
        {formError.description && <span className="form-error">{formError.description}</span>}
      </div>
      
      <div className="form-group">
        <label htmlFor="jobDate">Job Date:</label>
        <input type="date" id="jobDate" name="jobDate" value={formData.jobDate} onChange={handleChange} required />
        {formError.jobDate && <span className="form-error">{formError.jobDate}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="assignedMechanic">Assign Mechanic:</label>
        <select id="assignedMechanic" name="assignedMechanic" value={formData.assignedMechanic} onChange={handleChange}>
          <option value="">Select Mechanic (Optional)</option>
          {mechanics.map(m => <option key={m._id} value={m._id}>{m.username}</option>)}
        </select>
      </div>

      <div className="form-group">
        <label>Services:</label>
        <div className="checkbox-group">
            {availableServices.map(service => (
            <div key={service._id} className="checkbox-item">
                <input
                type="checkbox"
                id={`service-${service._id}`}
                checked={formData.services.includes(service._id)}
                onChange={() => handleServiceChange(service._id)}
                />
                <label htmlFor={`service-${service._id}`}>{service.name} (${service.price})</label>
            </div>
            ))}
        </div>
      </div>

      <div className="form-group">
        <label>Parts:</label>
        {availableParts.map(part => (
          <div key={part._id} className="part-selection-item">
            <span>{part.name} (SKU: {part.sku}, Stock: {part.quantity}, Price: ${part.price})</span>
            <input
              type="number"
              min="0"
              placeholder="Qty"
              value={formData.parts.find(p => p.item === part._id)?.quantity || 0}
              onChange={(e) => handlePartChange(part._id, e.target.value)}
              style={{width: '60px', marginLeft: '10px'}}
            />
          </div>
        ))}
      </div>
      
      <div className="form-group">
        <label htmlFor="status">Status:</label>
        <select id="status" name="status" value={formData.status} onChange={handleChange} required>
          <option value="Pending">Pending</option>
          <option value="Scheduled">Scheduled</option>
          <option value="In Progress">In Progress</option>
          <option value="Awaiting Parts">Awaiting Parts</option>
          <option value="Completed">Completed</option>
          <option value="Cancelled">Cancelled</option>
          {/* Invoiced status might be set automatically */}
        </select>
        {formError.status && <span className="form-error">{formError.status}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="estimatedCost">Estimated Cost ($):</label>
        <input type="number" id="estimatedCost" name="estimatedCost" value={formData.estimatedCost} onChange={handleChange} min="0" step="0.01" />
      </div>

      <button type="submit" className="btn btn-primary" disabled={loading}>
        {loading ? 'Saving...' : (isEditMode ? 'Update Job' : 'Create Job')}
      </button>
    </form>
  );
};

export default JobForm;
