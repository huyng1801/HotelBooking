import axiosInstance from './axiosConfig';

const AMENITY_API_URL = '/Amenity';

export const getAllAmenities = async () => {
    return await axiosInstance.get(AMENITY_API_URL);
};

export const getAmenityById = async (id) => {
    return await axiosInstance.get(`${AMENITY_API_URL}/${id}`);
};

export const createAmenity = async (amenityData, imageFile) => {
    const formData = new FormData();
    Object.keys(amenityData).forEach(key => {
        formData.append(key, amenityData[key]);
    });
    if (imageFile) {
        formData.append('imageFile', imageFile);
    }
    return await axiosInstance.post(AMENITY_API_URL, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const updateAmenity = async (id, amenityData, imageFile) => {
    const formData = new FormData();
    amenityData["amenityId"] = id;
    Object.keys(amenityData).forEach(key => {
        formData.append(key, amenityData[key]);
    });
    if (imageFile) {
        formData.append('imageFile', imageFile);
    }
    return await axiosInstance.put(`${AMENITY_API_URL}/${id}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};

export const deleteAmenity = async (id) => {
    return await axiosInstance.delete(`${AMENITY_API_URL}/${id}`);
};
