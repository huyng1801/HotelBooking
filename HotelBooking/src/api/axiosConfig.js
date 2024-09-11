import axios from 'axios';
import { getToken, refreshToken } from './tokenService';

const API_URL = 'https://localhost:7280/api';

const axiosInstance = axios.create({
    baseURL: API_URL,
});

axiosInstance.interceptors.request.use(
    async (config) => {
        const token = getToken();
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

axiosInstance.interceptors.response.use(
    (response) => response,
    async (error) => {
        const { config, response: { status } } = error;
        const originalRequest = config;

        if (status === 401) {
            try {
                const newToken = await refreshToken();
                if (newToken) {
                    originalRequest.headers['Authorization'] = `Bearer ${newToken}`;
                    return axiosInstance(originalRequest);
                }
            } catch (refreshError) {
                return Promise.reject(refreshError);
            }
        }
        return Promise.reject(error);
    }
);

export default axiosInstance;
