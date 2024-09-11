import axiosInstance from './axiosConfig';

const HOTEL_API_URL = '/Hotel';

export const getAllHotels = async () => {
    return await axiosInstance.get(HOTEL_API_URL);
};

export const getHotelById = async (id) => {
    return await axiosInstance.get(`${HOTEL_API_URL}/${id}`);
};

export const createHotel = async (hotelData, imageFiles) => {
    const formData = new FormData();
    Object.keys(hotelData).forEach(key => {
        formData.append(key, hotelData[key]);
    });
    if (imageFiles) {
        imageFiles.forEach(file => formData.append('files', file));
    }
    return await axiosInstance.post(HOTEL_API_URL, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const updateHotel = async (id, hotelData, imageFiles) => {
    const formData = new FormData();
    Object.keys(hotelData).forEach(key => {
        formData.append(key, hotelData[key]);
    });
    if (imageFiles) {
        imageFiles.forEach(file => formData.append('files', file));
    }
    return await axiosInstance.put(`${HOTEL_API_URL}/${id}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const deleteHotel = async (id) => {
    return await axiosInstance.delete(`${HOTEL_API_URL}/${id}`);
};
