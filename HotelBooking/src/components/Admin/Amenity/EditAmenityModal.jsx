import React, { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { updateAmenity } from '../../../api/AmenityService';

const EditAmenityModal = ({ show, onHide, fetchAmenities, amenity }) => {
    const [amenityName, setAmenityName] = useState('');
    const [imageFile, setImageFile] = useState(null);

    useEffect(() => {
        if (amenity) {
            setAmenityName(amenity.amenityName);
        }
    }, [amenity]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await updateAmenity(amenity.amenityId, { amenityName });
        fetchAmenities();
        onHide();
    };

    return (
        <Modal show={show} onHide={onHide}>
            <Modal.Header closeButton>
                <Modal.Title>Sửa tiện nghi</Modal.Title>
            </Modal.Header>
            <Form onSubmit={handleSubmit}>
                <Modal.Body>
                    <Form.Group>
                        <Form.Label>Tên tiện nghi</Form.Label>
                        <Form.Control
                            type="text"
                            value={amenityName}
                            onChange={(e) => setAmenityName(e.target.value)}
                            required
                        />
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

export default EditAmenityModal;
