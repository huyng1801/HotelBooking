import React, { useState, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { updateHotel } from '../../../api/HotelService';

const EditHotelModal = ({ show, onHide, fetchHotels, hotel }) => {
    const [hotelName, setHotelName] = useState('');
    const [city, setCity] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [address, setAddress] = useState('');
    const [accommodationPolicy, setAccommodationPolicy] = useState('');
    const [description, setDescription] = useState('');
    const [star, setStar] = useState(0);
    const [isActive, setIsActive] = useState(true);
    const [imageFiles, setImageFiles] = useState([]);
    const [imagePreviews, setImagePreviews] = useState([]);
    const [removedOldImages, setRemovedOldImages] = useState([]);

    useEffect(() => {
        if (hotel) {
            setHotelName(hotel.hotelName);
            setCity(hotel.city);
            setPhoneNumber(hotel.phoneNumber);
            setAddress(hotel.address);
            setAccommodationPolicy(hotel.accommodationPolicy);
            setDescription(hotel.description);
            setStar(hotel.starRating);
            setIsActive(hotel.isActive);
            setImagePreviews(hotel.images.map(img => img.imageUrl));
        }
    }, [hotel]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const hotelData = { hotelName, city, phoneNumber, address, accommodationPolicy, description, star, isActive, removedOldImages };
        await updateHotel(hotel.hotelId, hotelData, imageFiles);
        fetchHotels();
        onHide();
    };

    const handleImageChange = (e) => {
        const files = Array.from(e.target.files);
        setImageFiles(prevFiles => [...prevFiles, ...files]);

        // Create image previews
        const previews = files.map(file => URL.createObjectURL(file));
        setImagePreviews(prevPreviews => [...prevPreviews, ...previews]);
    };

    const handleRemoveImage = (index) => {
        if (index < imagePreviews.length - imageFiles.length) {
            // Remove old image
            setRemovedOldImages(prev => [...prev, hotel.images[index].hotelImageId]);
        } else {
            // Remove new image
            const newImageIndex = index - (imagePreviews.length - imageFiles.length);
            const newImageFiles = imageFiles.filter((_, i) => i !== newImageIndex);
            const newImagePreviews = imagePreviews.filter((_, i) => i !== index);
            setImageFiles(newImageFiles);
            setImagePreviews(newImagePreviews);

            // Revoke the object URL to free up memory
            URL.revokeObjectURL(imagePreviews[index]);
        }
    };

    return (
        <Modal show={show} onHide={onHide} dialogClassName="modal-xl" centered>
            <Modal.Header closeButton>
                <Modal.Title>Sửa khách sạn</Modal.Title>
            </Modal.Header>
            <Form onSubmit={handleSubmit}>
                <Modal.Body>
                    <Form.Group className="mb-3">
                        <Form.Label>Tên khách sạn</Form.Label>
                        <Form.Control
                            type="text"
                            value={hotelName}
                            onChange={(e) => setHotelName(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Thành phố</Form.Label>
                        <Form.Control
                            type="text"
                            value={city}
                            onChange={(e) => setCity(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Số điện thoại</Form.Label>
                        <Form.Control
                            type="text"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Địa chỉ</Form.Label>
                        <Form.Control
                            type="text"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-5">
                        <Form.Label>Chính sách chỗ ở</Form.Label>
                        <ReactQuill
                            value={accommodationPolicy}
                            onChange={setAccommodationPolicy}
                            required
                            style={{ height: '150px' }}
                        />
                    </Form.Group>
                    <Form.Group className="mb-5">
                        <Form.Label>Mô tả</Form.Label>
                        <ReactQuill
                            value={description}
                            onChange={setDescription}
                            required
                            style={{ height: '150px' }} 
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Đánh giá sao</Form.Label>
                        <Form.Control
                            type="number"
                            value={star}
                            onChange={(e) => setStar(e.target.value)}
                            min={1}
                            max={5}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Check
                            type="switch"
                            label="Hoạt động"
                            checked={isActive}
                            onChange={(e) => setIsActive(e.target.checked)}
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Ảnh</Form.Label>
                        <Form.Control
                            type="file"
                            multiple
                            onChange={handleImageChange}
                        />
                        <div className="mt-3">
                            {imagePreviews.length > 0 && (
                                <div>
                                    <h6>Danh sách ảnh đã chọn:</h6>
                                    <div className="row">
                                        {imagePreviews.map((preview, index) => (
                                            <div key={index} className="col-4 mb-3">
                                                <div className="position-relative">
                                                    <img 
                                                        src={preview} 
                                                        alt={`preview-${index}`} 
                                                        className="img-thumbnail"
                                                        style={{ width: '100%', height: '200px', objectFit: 'cover' }} 
                                                    />
                                                    <Button
                                                        variant="danger"
                                                        size="sm"
                                                        className="position-absolute top-0 end-0"
                                                        onClick={() => handleRemoveImage(index)}
                                                    >
                                                        Xóa
                                                    </Button>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            )}
                        </div>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={onHide}>
                        Hủy
                    </Button>
                    <Button type="submit" variant="primary">
                        Lưu thay đổi
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    );
};

export default EditHotelModal;
