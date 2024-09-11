import React, { useEffect, useState, useCallback } from 'react';
import { Button, Table, Modal, Pagination } from 'react-bootstrap';
import { getRoomsByHotelId, deleteRoom } from '../../../api/RoomService';
import { useParams, useNavigate } from 'react-router-dom';
import AddRoomModal from './AddRoomModal';
import EditRoomModal from './EditRoomModal';
import RoomAmenityModal from './RoomAmenityModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash, faBed, faPlus, faArrowLeft, faList } from '@fortawesome/free-solid-svg-icons';

const RoomList = () => {
    const { hotelId } = useParams();
    const navigate = useNavigate();
    const [rooms, setRooms] = useState([]);
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showAmenityModal, setShowAmenityModal] = useState(false);
    const [currentRoom, setCurrentRoom] = useState(null);
    const [roomToDelete, setRoomToDelete] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const roomsPerPage = 5;

    const fetchRooms = useCallback(async () => {
        const response = await getRoomsByHotelId(hotelId);
        setRooms(response.data);
    }, [hotelId]);

    useEffect(() => {
        fetchRooms();
    }, [fetchRooms]);

    const handleDelete = async () => {
        if (roomToDelete) {
            await deleteRoom(roomToDelete.roomId);
            fetchRooms();
            setRoomToDelete(null);
        }
    };

    const handleShowDeleteModal = (room) => {
        setRoomToDelete(room);
    };

    const handleShowEditModal = (room) => {
        setCurrentRoom(room);
        setShowEditModal(true);
    };

    const handleShowAmenityModal = (room) => {
        setCurrentRoom(room);
        setShowAmenityModal(true);
    };

    const indexOfLastRoom = currentPage * roomsPerPage;
    const indexOfFirstRoom = indexOfLastRoom - roomsPerPage;
    const currentRooms = rooms.slice(indexOfFirstRoom, indexOfLastRoom);

    const totalPages = Math.ceil(rooms.length / roomsPerPage);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    const formatCurrency = (amount) => {
        return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
    };

    return (
        <div className="container-fluid">
            <h2 className="text-center text-uppercase">Danh sách phòng</h2>
            <Button className="mb-3" onClick={() => setShowAddModal(true)}>
                <FontAwesomeIcon icon={faPlus} /> Thêm mới
            </Button>
            <Button className="mb-3 ms-2" variant="secondary" onClick={() => navigate('/admin/hotel')}>
                <FontAwesomeIcon icon={faArrowLeft} /> Quay lại
            </Button>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Ảnh</th>
                        <th>Tên phòng</th>
                        <th>Giá</th>
                        <th>Số lượng phòng</th>
                        <th>Diện tích</th>
                        <th>Số người</th>
                        <th>Ăn sáng</th>
                        <th>Tiện ích</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {currentRooms.map((room, index) => (
                        <tr key={room.roomId}>
                            <td>{indexOfFirstRoom + index + 1}</td>
                            <td>
                                {room.imageUrls.length > 0 && (
                                    <img
                                        src={room.imageUrls[0].imageUrl}
                                        alt={room.roomName}
                                        style={{ width: '50px', height: '50px', objectFit: 'cover' }}
                                    />
                                )}
                            </td>
                            <td>{room.roomName}</td>
                            <td className="text-end">{formatCurrency(room.price)}</td>
                            <td>{room.roomCount}</td>
                            <td>{room.area}</td>
                            <td>{room.numberPerson}</td>
                            <td>{room.eatBreakfast ? 'Có' : 'Không'}</td>
                            <td>{room.roomAmenities.map(amenity => amenity.amenityName).join(', ')}</td>
                            <td>
                                <Button variant="warning" onClick={() => handleShowEditModal(room)}>
                                    <FontAwesomeIcon icon={faEdit} />
                                </Button>{' '}
                                <Button variant="danger" onClick={() => handleShowDeleteModal(room)}>
                                    <FontAwesomeIcon icon={faTrash} />
                                </Button>{' '}
                                <Button variant="info" onClick={() => handleShowAmenityModal(room)}>
                                    <FontAwesomeIcon icon={faList} />
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
            <AddRoomModal show={showAddModal} onHide={() => setShowAddModal(false)} fetchRooms={fetchRooms} hotelId={hotelId} />
            <EditRoomModal show={showEditModal} onHide={() => setShowEditModal(false)} fetchRooms={fetchRooms} room={currentRoom} hotelId={hotelId} />
            <RoomAmenityModal show={showAmenityModal} onHide={() => setShowAmenityModal(false)} room={currentRoom} fetchRooms={fetchRooms} />
            <Modal show={!!roomToDelete} onHide={() => setRoomToDelete(null)}>
                <Modal.Header closeButton>
                    <Modal.Title>Xác nhận xóa</Modal.Title>
                </Modal.Header>
                <Modal.Body>Bạn chắc chắn muốn xóa phòng "{roomToDelete?.roomName}"?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setRoomToDelete(null)}>Hủy</Button>
                    <Button variant="danger" onClick={handleDelete}>Đồng ý</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default RoomList;
