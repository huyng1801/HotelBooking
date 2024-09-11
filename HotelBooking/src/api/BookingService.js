import axiosInstance from './axiosConfig';

const BOOKING_API_URL = '/Booking';

export const getAllBookings = async () => {
    return await axiosInstance.get(BOOKING_API_URL);
};

export const getBookingById = async (id) => {
    return await axiosInstance.get(`${BOOKING_API_URL}/${id}`);
};

export const updateBookingStatus = async (id, status) => {
    console.log(status);
    const statusAsNumber = parseInt(status, 10);
    const formData = new FormData();
    formData.append('status', statusAsNumber);

    return await axiosInstance.put(`${BOOKING_API_URL}/${id}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const deleteBooking = async (id) => {
    return await axiosInstance.delete(`${BOOKING_API_URL}/${id}`);
};
