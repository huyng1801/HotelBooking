import React, { useState, useEffect, useCallback } from 'react';
import { Modal, Button, Form, ListGroup, Alert } from 'react-bootstrap';
import { getRoomAmenities, addRoomAmenity, deleteRoomAmenity } from '../../../api/RoomAmenityService';
import { getAllAmenities } from '../../../api/AmenityService';  // Import getAllAmenities from AmenityService

const RoomAmenityModal = ({ show, onHide, room, fetchRooms }) => {
    const [amenities, setAmenities] = useState([]);
    const [availableAmenities, setAvailableAmenities] = useState([]);
    const [selectedAmenity, setSelectedAmenity] = useState('');
    const [error, setError] = useState('');

    const fetchRoomAmenities = useCallback(async () => {
        if (room) {
            const response = await getRoomAmenities(room.roomId);
            setAmenities(response.data);
        }
    }, [room]);

    const fetchAvailableAmenities = useCallback(async () => {
        const response = await getAllAmenities();
        setAvailableAmenities(response.data);
    }, []);

    useEffect(() => {
        if (room) {
            fetchRoomAmenities();
            fetchAvailableAmenities();
        }
    }, [room, fetchRoomAmenities, fetchAvailableAmenities]);

    const handleAddAmenity = async () => {
        if (selectedAmenity) {
            const exists = amenities.some(amenity => amenity.amenityId === selectedAmenity);
            if (exists) {
                setError('Tiện ích này đã tồn tại trong danh sách.');
            } else {
                await addRoomAmenity(room.roomId, selectedAmenity);
                fetchRoomAmenities();
                fetchRooms();
                setSelectedAmenity('');
                setError('');
            }
        }
    };

    const handleDeleteAmenity = async (roomAmenityId) => {
        await deleteRoomAmenity(roomAmenityId);
        fetchRoomAmenities();
        fetchRooms(); // Refresh the rooms list in the parent component
    };

    return (
        <Modal show={show} onHide={onHide}>
            <Modal.Header closeButton>
                <Modal.Title>Tiện ích cho phòng {room?.roomName}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {error && <Alert variant="danger">{error}</Alert>}
                <Form>
                    <Form.Group>
                        <Form.Label>Chọn tiện ích</Form.Label>
                        <Form.Control
                            as="select"
                            value={selectedAmenity}
                            onChange={(e) => setSelectedAmenity(e.target.value)}
                        >
                            <option value="">Chọn tiện ích</option>
                            {availableAmenities.map((amenity) => (
                                <option key={amenity.amenityId} value={amenity.amenityId}>
                                    {amenity.amenityName}
                                </option>
                            ))}
                        </Form.Control>
                    </Form.Group>
                    <Button variant="primary" onClick={handleAddAmenity} className="mt-3">
                        Thêm tiện ích
                    </Button>
                </Form>
                <h5 className="mt-4">Danh sách tiện ích</h5>
                <ListGroup>
                    {amenities.map((amenity) => (
                        <ListGroup.Item key={amenity.roomAmenityId}>
                            {amenity.amenityName}
                            <Button
                                variant="danger"
                                className="float-right"
                                onClick={() => handleDeleteAmenity(amenity.roomAmenityId)}
                            >
                                x
                            </Button>
                        </ListGroup.Item>
                    ))}
                </ListGroup>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onHide}>
                    Đóng
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default RoomAmenityModal;
