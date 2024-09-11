import React, { useEffect, useState } from 'react';
import { Button, Table, Modal, Pagination } from 'react-bootstrap';
import { getAllHotels, deleteHotel } from '../../../api/HotelService';
import { useNavigate } from 'react-router-dom';
import AddHotelModal from './AddHotelModal';
import EditHotelModal from './EditHotelModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash, faBed, faPlus } from '@fortawesome/free-solid-svg-icons';

const HotelList = () => {
    const [hotels, setHotels] = useState([]);
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [currentHotel, setCurrentHotel] = useState(null);
    const [hotelToDelete, setHotelToDelete] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const hotelsPerPage = 5;
    const navigate = useNavigate();

    useEffect(() => {
        fetchHotels();
    }, []);

    const fetchHotels = async () => {
        const response = await getAllHotels();
        setHotels(response.data);
    };

    const handleDelete = async () => {
        if (hotelToDelete) {
            await deleteHotel(hotelToDelete.hotelId);
            fetchHotels();
            setHotelToDelete(null);
        }
    };

    const handleShowDeleteModal = (hotel) => {
        setHotelToDelete(hotel);
    };

    const handleShowEditModal = (hotel) => {
        setCurrentHotel(hotel);
        setShowEditModal(true);
    };

    const handleShowRoomsPage = (hotelId) => {
        navigate(`/admin/hotel/${hotelId}/rooms`);
    };

    const indexOfLastHotel = currentPage * hotelsPerPage;
    const indexOfFirstHotel = indexOfLastHotel - hotelsPerPage;
    const currentHotels = hotels.slice(indexOfFirstHotel, indexOfLastHotel);

    const totalPages = Math.ceil(hotels.length / hotelsPerPage);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    return (
        <div className="container-fluid">
            <h2 className="text-center text-uppercase">Danh sách khách sạn</h2>
            <Button className="mb-3" onClick={() => setShowAddModal(true)}>
                <FontAwesomeIcon icon={faPlus} /> Thêm mới
            </Button>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Ảnh</th>
                        <th>Tên khách sạn</th>
                        <th>Thành phố</th>
                        <th>Địa chỉ</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {currentHotels.map((hotel, index) => (
                        <tr key={hotel.hotelId}>
                            <td>{indexOfFirstHotel + index + 1}</td>
                            <td>
                                {hotel.images.length > 0 && (
                                    <img
                                        src={hotel.images[0].imageUrl}
                                        alt={hotel.hotelName}
                                        style={{ width: '50px', height: '50px', objectFit: 'cover' }}
                                    />
                                )}
                            </td>
                            <td>{hotel.hotelName}</td>
                            <td>{hotel.city}</td>
                            <td>{hotel.address}</td>
                            <td>
                                <Button variant="warning" onClick={() => handleShowEditModal(hotel)}>
                                    <FontAwesomeIcon icon={faEdit} />
                                </Button>{' '}
                                <Button variant="danger" onClick={() => handleShowDeleteModal(hotel)}>
                                    <FontAwesomeIcon icon={faTrash} />
                                </Button>{' '}
                                <Button variant="info" onClick={() => handleShowRoomsPage(hotel.hotelId)}>
                                    <FontAwesomeIcon icon={faBed} />
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Pagination>
                {Array.from({ length: totalPages }, (_, index) => (
                    <Pagination.Item
                        key={index + 1}
                        active={index + 1 === currentPage}
                        onClick={() => paginate(index + 1)}
                    >
                        {index + 1}
                    </Pagination.Item>
                ))}
            </Pagination>

            <AddHotelModal show={showAddModal} onHide={() => setShowAddModal(false)} fetchHotels={fetchHotels} />
            <EditHotelModal show={showEditModal} onHide={() => setShowEditModal(false)} fetchHotels={fetchHotels} hotel={currentHotel} />

            <Modal show={!!hotelToDelete} onHide={() => setHotelToDelete(null)}>
                <Modal.Header closeButton>
                    <Modal.Title>Xác nhận xóa</Modal.Title>
                </Modal.Header>
                <Modal.Body>Bạn chắc chắn muốn xóa khách sạn "{hotelToDelete?.hotelName}"?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setHotelToDelete(null)}>Hủy</Button>
                    <Button variant="danger" onClick={handleDelete}>Đồng ý</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default HotelList;
