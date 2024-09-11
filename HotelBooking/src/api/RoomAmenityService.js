import axiosInstance from './axiosConfig';

const ROOM_AMENITY_API_URL = '/RoomAmenity';

export const getRoomAmenities = async (roomId) => {
    return await axiosInstance.get(`${ROOM_AMENITY_API_URL}/ByRoom/${roomId}`);
};


export const addRoomAmenity = async (roomId, amenityId) => {
    return await axiosInstance.post(ROOM_AMENITY_API_URL, {
        roomId,
        amenityId
    });
};

export const deleteRoomAmenity = async (roomAmenityId) => {
    return await axiosInstance.delete(`${ROOM_AMENITY_API_URL}/${roomAmenityId}`);
};
