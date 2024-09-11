import axiosInstance from './axiosConfig';

const USER_API_URL = '/User';

export const getAllUsers = async () => {
    try {
        const response = await axiosInstance.get(USER_API_URL);
        return { success: true, data: response.data };
    } catch (error) {
        return handleApiError(error);
    }
};

export const getUserById = async (id) => {
    try {
        const response = await axiosInstance.get(`${USER_API_URL}/${id}`);
        return { success: true, data: response.data };
    } catch (error) {
        return handleApiError(error);
    }
};

export const createUser = async (userData) => {
    try {
        const response = await axiosInstance.post(USER_API_URL, userData);
        return { success: true, data: response.data };
    } catch (error) {
        return handleApiError(error);
    }
};

export const updateUser = async (id, userData) => {
    try {
        const response = await axiosInstance.put(`${USER_API_URL}/${id}`, userData);
        return { success: true, data: response.data };
    } catch (error) {
        return handleApiError(error);
    }
};

export const deleteUser = async (id) => {
    try {
        const response = await axiosInstance.delete(`${USER_API_URL}/${id}`);
        return { success: true, data: response.data };
    } catch (error) {
        return handleApiError(error);
    }
};

export const toggleIsActive = async (id) => {
    try {
        const response = await axiosInstance.put(`${USER_API_URL}/toggleIsActive/${id}`);
        return { success: true, data: response.data };
    } catch (error) {
        return handleApiError(error);
    }
};

const handleApiError = (error) => {
    return {
        success: false,
        error: error.response?.data || 'An error occurred.'
    };
};
