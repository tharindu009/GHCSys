import apiClient from './api';

const createInvoiceFromJob = async (jobId) => {
  try {
    const response = await apiClient.post('/invoices', { jobId });
    return response.data; // Expects { message: 'Invoice created successfully', invoice: newInvoice }
  } catch (error) {
    console.error('Error creating invoice from job:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to create invoice');
  }
};

const getAllInvoices = async (filters) => {
  try {
    const response = await apiClient.get('/invoices', { params: filters });
    return response.data; // Expects { invoices: [], totalPages, currentPage, totalInvoices }
  } catch (error) {
    console.error('Error fetching invoices:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch invoices');
  }
};

const getInvoiceById = async (invoiceId) => {
  try {
    const response = await apiClient.get(`/invoices/${invoiceId}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching invoice ${invoiceId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to fetch invoice details');
  }
};

const updateInvoiceStatus = async (invoiceId, paymentStatus) => {
  try {
    const response = await apiClient.put(`/invoices/${invoiceId}/status`, { paymentStatus });
    return response.data;
  } catch (error) {
    console.error(`Error updating invoice status for ${invoiceId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to update invoice status');
  }
};

const deleteInvoice = async (invoiceId) => {
  try {
    const response = await apiClient.delete(`/invoices/${invoiceId}`);
    return response.data; // Expects { message: 'Invoice deleted (soft delete) successfully.' }
  } catch (error) {
    console.error(`Error deleting invoice ${invoiceId}:`, error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : new Error('Failed to delete invoice');
  }
};

const invoiceService = {
  createInvoiceFromJob,
  getAllInvoices,
  getInvoiceById,
  updateInvoiceStatus,
  deleteInvoice,
};

export default invoiceService;
