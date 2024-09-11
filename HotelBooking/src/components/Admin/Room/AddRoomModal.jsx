import React, { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { createRoom } from '../../../api/RoomService';

const AddRoomModal = ({ show, onHide, fetchRooms, hotelId }) => {
    const [roomName, setRoomName] = useState('');
    const [price, setPrice] = useState('');
    const [roomCount, setRoomCount] = useState('');
    const [area, setArea] = useState('');
    const [numberPerson, setNumberPerson] = useState('');
    const [view, setView] = useState('');
    const [eatBreakfast, setEatBreakfast] = useState(false);
    const [bedDescription, setBedDescription] = useState('');
    const [imageFiles, setImageFiles] = useState([]);
    const [imagePreviews, setImagePreviews] = useState([]);

    useEffect(() => {
        if (show) {
            setRoomName('');
            setPrice(0);
            setRoomCount(1);
            setArea(0);
            setNumberPerson(2);
            setView('');
            setEatBreakfast(false);
            setBedDescription('');
            setImageFiles([]);
            setImagePreviews([]);
        }
    }, [show]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const roomData = { roomName, price, roomCount, area, numberPerson, view, eatBreakfast, bedDescription, hotelId };
        await createRoom(roomData, imageFiles);
        fetchRooms();
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
        const newImageFiles = imageFiles.filter((_, i) => i !== index);
        const newImagePreviews = imagePreviews.filter((_, i) => i !== index);
        setImageFiles(newImageFiles);
        setImagePreviews(newImagePreviews);

        // Revoke the object URL to free up memory
        URL.revokeObjectURL(imagePreviews[index]);
    };

    return (
        <Modal show={show} onHide={onHide} dialogClassName="modal-xl">
            <Modal.Header closeButton>
                <Modal.Title>Thêm phòng</Modal.Title>
            </Modal.Header>
            <Form onSubmit={handleSubmit}>
                <Modal.Body>
                    <Form.Group className="mb-3">
                        <Form.Label>Tên phòng</Form.Label>
                        <Form.Control
                            type="text"
                            value={roomName}
                            onChange={(e) => setRoomName(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Giá</Form.Label>
                        <Form.Control
                            type="number"
                            value={price}
                            onChange={(e) => setPrice(e.target.value)}
                            min={0}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Số lượng phòng</Form.Label>
                        <Form.Control
                            type="number"
                            value={roomCount}
                            onChange={(e) => setRoomCount(e.target.value)}
                            min={0}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Diện tích</Form.Label>
                        <Form.Control
                            type="number"
                            value={area}
                            onChange={(e) => setArea(e.target.value)}
                            min={0}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Số người</Form.Label>
                        <Form.Control
                            type="number"
                            value={numberPerson}
                            onChange={(e) => setNumberPerson(e.target.value)}
                            min={1}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Hướng phòng</Form.Label>
                        <Form.Control
                            type="text"
                            value={view}
                            onChange={(e) => setView(e.target.value)}
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Check
                            type="switch"
                            label="Ăn sáng"
                            checked={eatBreakfast}
                            onChange={(e) => setEatBreakfast(e.target.checked)}
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Mô tả giường</Form.Label>
                        <Form.Control
                            type="text"
                            value={bedDescription}
                            onChange={(e) => setBedDescription(e.target.value)}
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
                        Lưu
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    );
};

export default AddRoomModal;
