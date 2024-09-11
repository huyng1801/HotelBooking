import React, { useState } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { createAmenity } from '../../../api/AmenityService';

const AddAmenityModal = ({ show, onHide, fetchAmenities }) => {
    const [amenityName, setAmenityName] = useState('');
    const [imageFile, setImageFile] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await createAmenity({ amenityName });
        fetchAmenities();
        onHide();
    };

    return (
        <Modal show={show} onHide={onHide}>
            <Modal.Header closeButton>
                <Modal.Title>Thêm tiện nghi</Modal.Title>
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
                        Lưu
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    );
};

export default AddAmenityModal;
