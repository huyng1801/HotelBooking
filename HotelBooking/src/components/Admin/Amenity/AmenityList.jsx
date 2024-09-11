import React, { useEffect, useState } from 'react';
import { Button, Table, Modal } from 'react-bootstrap';
import { getAllAmenities, deleteAmenity } from '../../../api/AmenityService';
import AddAmenityModal from './AddAmenityModal';
import EditAmenityModal from './EditAmenityModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash, faPlus } from '@fortawesome/free-solid-svg-icons';

const AmenityList = () => {
    const [amenities, setAmenities] = useState([]);
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [currentAmenity, setCurrentAmenity] = useState(null);
    const [amenityToDelete, setAmenityToDelete] = useState(null);

    useEffect(() => {
        fetchAmenities();
    }, []);

    const fetchAmenities = async () => {
        const response = await getAllAmenities();
        setAmenities(response.data);
    };

    const handleDelete = async () => {
        if (amenityToDelete) {
            await deleteAmenity(amenityToDelete.amenityId);
            fetchAmenities();
            setShowDeleteModal(false);
        }
    };

    const handleShowDeleteModal = (amenity) => {
        setAmenityToDelete(amenity);
        setShowDeleteModal(true);
    };

    const handleShowEditModal = (amenity) => {
        setCurrentAmenity(amenity);
        setShowEditModal(true);
    };

    return (
        <div className="container">
            <h2 className="text-center">Danh sách tiện nghi</h2>
            <Button className="mb-3" onClick={() => setShowAddModal(true)}>
                <FontAwesomeIcon icon={faPlus} /> Thêm mới
            </Button>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Tên tiện nghi</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {amenities.map((amenity, index) => (
                        <tr key={amenity.amenityId}>
                            <td>{index + 1}</td>
                            <td>{amenity.amenityName}</td>
                            <td>
                                <Button
                                    variant="warning"
                                    onClick={() => handleShowEditModal(amenity)}
                                >
                                    <FontAwesomeIcon icon={faEdit} />
                                </Button>{' '}
                                <Button
                                    variant="danger"
                                    onClick={() => handleShowDeleteModal(amenity)}
                                >
                                    <FontAwesomeIcon icon={faTrash} />
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <AddAmenityModal
                show={showAddModal}
                onHide={() => setShowAddModal(false)}
                fetchAmenities={fetchAmenities}
            />

            <EditAmenityModal
                show={showEditModal}
                onHide={() => setShowEditModal(false)}
                fetchAmenities={fetchAmenities}
                amenity={currentAmenity}
            />

            <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Xác nhận xóa</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Bạn chắc chắn muốn xóa tiện nghi "{amenityToDelete?.amenityName}"?
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowDeleteModal(false)}>
                        Hủy
                    </Button>
                    <Button variant="danger" onClick={handleDelete}>
                        Đồng ý
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default AmenityList;
