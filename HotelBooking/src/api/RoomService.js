import axiosInstance from './axiosConfig';

const ROOM_API_URL = '/Room';

export const getRoomById = async (id) => {
    return await axiosInstance.get(`${ROOM_API_URL}/${id}`);
};
export const getRoomsByHotelId = async (hotelId) => { // Add this function
    return await axiosInstance.get(`/Hotel/${hotelId}/rooms`);
};
export const createRoom = async (roomData, imageFiles) => {
    const formData = new FormData();
    Object.keys(roomData).forEach(key => {
        formData.append(key, roomData[key]);
    });
    if (imageFiles) {
        imageFiles.forEach(file => formData.append('files', file));
    }
    return await axiosInstance.post(ROOM_API_URL, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const updateRoom = async (id, roomData, imageFiles) => {
    const formData = new FormData();
    Object.keys(roomData).forEach(key => {
        formData.append(key, roomData[key]);
    });
    if (imageFiles) {
        imageFiles.forEach(file => formData.append('files', file));
    }
    return await axiosInstance.put(`${ROOM_API_URL}/${id}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const getAllRooms = async () => {
    return await axiosInstance.get(ROOM_API_URL);
};

export const deleteRoom = async (id) => {
    return await axiosInstance.delete(`${ROOM_API_URL}/${id}`);
};
