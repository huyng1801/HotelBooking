import axiosInstance from './axiosConfig';

export const registerUser = async (userData) => {
    try {
        const response = await axiosInstance.post('/home/register', userData);
        return response.data;
    } catch (error) {
        console.error('Error registering user:', error);
        throw error;
    }
};

export const getAllHotels = async () => {
    try {
        const response = await axiosInstance.get('/home/hotels');
        return response.data;
    } catch (error) {
        console.error('Error fetching hotels:', error);
        throw error;
    }
};

export const getHotelById = async (hotelId) => {
    try {
        const response = await axiosInstance.get(`/home/${hotelId}/hotels`);
        console.log(response);
        return response.data;
    } catch (error) {
        console.error(`Error fetching hotel with ID ${hotelId}:`, error);
        throw error;
    }
};

export const getRoomsByHotelId = async (hotelId) => {
    try {
        const response = await axiosInstance.get(`/home/${hotelId}/rooms`);
        return response.data;
    } catch (error) {
        console.error('Error fetching rooms by hotel ID:', error);
        throw error;
    }
};
export const getAllRooms = async () => {
    try {
        const response = await axiosInstance.get('/home/rooms');
        return response.data;
    } catch (error) {
        console.error('Error fetching rooms:', error);
        throw error;
    }
};


export const getRoomById = async (roomId) => {
    try {
        const response = await axiosInstance.get(`/home/rooms/${roomId}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching room with ID ${roomId}:`, error);
        throw error;
    }
};
export const getRoomAmenitiesByRoomId = async (roomId) => {
    try {
        const response = await axiosInstance.get(`/home/rooms/${roomId}/amenities`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching amenities for room with ID ${roomId}:`, error);
        throw error;
    }
};
export const createBooking = async (bookingData) => {
    try {

        const response = await axiosInstance.post('/home/bookings', bookingData);
        console.log(response);
        return response.data;
    } catch (error) {
        console.error('Error creating booking:', error);
        throw error;
    }
};
export const getRelatedRooms = async (roomId) => {
    try {
        const response = await axiosInstance.get(`/home/rooms/${roomId}/related`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching related rooms for room with ID ${roomId}:`, error);
        throw error;
    }
};


export const searchHotels = async (city, checkInDate, checkOutDate, numberOfPersons, numberOfRooms) => {
    try {
        const response = await axiosInstance.get('/home/search', {
            params: {
                city,
                checkInDate,
                checkOutDate,
                numberOfPersons,
                numberOfRooms
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error searching hotels:', error);
        throw error;
    }
};