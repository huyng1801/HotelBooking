import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form } from 'react-bootstrap';
import { getAllBookings, updateBookingStatus, deleteBooking, getBookingById } from '../../../api/BookingService';
import BookingDetailModal from './BookingDetailModal';

const BookingList = () => {
    const [bookings, setBookings] = useState([]);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [showStatusModal, setShowStatusModal] = useState(false);
    const [showDetailModal, setShowDetailModal] = useState(false);
    const [bookingToDelete, setBookingToDelete] = useState(null);
    const [bookingToUpdate, setBookingToUpdate] = useState(null);
    const [bookingToView, setBookingToView] = useState(null);
    const [newStatus, setNewStatus] = useState('');

    useEffect(() => {
        fetchBookings();
    }, []);

    const fetchBookings = async () => {
        try {
            const response = await getAllBookings();
            setBookings(response.data);
        } catch (error) {
            console.error('Failed to fetch bookings:', error);
        }
    };

    const handleDelete = async () => {
        if (bookingToDelete) {
            try {
                await deleteBooking(bookingToDelete.bookingId);
                fetchBookings();
                setShowDeleteModal(false);
            } catch (error) {
                console.error('Failed to delete booking:', error);
            }
        }
    };

    const handleShowDeleteModal = (booking) => {
        setBookingToDelete(booking);
        setShowDeleteModal(true);
    };

    const handleShowStatusModal = (booking) => {
        setBookingToUpdate(booking);
        setNewStatus(booking.status);
        setShowStatusModal(true);
    };

    const handleUpdateStatus = async () => {
        if (bookingToUpdate) {
            try {
                await updateBookingStatus(bookingToUpdate.bookingId, newStatus);
                fetchBookings();
                setShowStatusModal(false);
            } catch (error) {
                console.error('Failed to update booking status:', error);
            }
        }
    };

    const handleShowDetailModal = async (bookingId) => {
        try {
            const response = await getBookingById(bookingId);
            setBookingToView(response.data);
            setShowDetailModal(true);
        } catch (error) {
            console.error('Failed to fetch booking details:', error);
        }
    };

    const getStatusText = (status) => {
        switch (status) {
            case 0:
                return 'Đang chờ';
            case 1:
                return 'Đã xác nhận';
            case 2:
                return 'Đã hủy';
            default:
                return 'Không rõ';
        }
    };

    return (
        <div className="container">
            <h2 className="text-center">Danh sách đặt phòng</h2>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Ngày nhận phòng</th>
                        <th>Ngày trả phòng</th>
                        <th>Tổng tiền</th>
                        <th>Trạng thái</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {bookings.map((booking, index) => (
                        <tr key={booking.bookingId}>
                            <td>{index + 1}</td>
                            <td>{new Date(booking.checkInDate).toLocaleDateString()}</td>
                            <td>{new Date(booking.checkOutDate).toLocaleDateString()}</td>
                            <td>{booking.totalAmount.toLocaleString()} VND</td>
                            <td>{getStatusText(booking.status)}</td>
                            <td>
                                <Button
                                    variant="info"
                                    onClick={() => handleShowDetailModal(booking.bookingId)}
                                >
                                    Chi tiết
                                </Button>{' '}
                                <Button
                                    variant="warning"
                                    onClick={() => handleShowStatusModal(booking)}
                                >
                                    Thay đổi trạng thái
                                </Button>{' '}
                                <Button
                                    variant="danger"
                                    onClick={() => handleShowDeleteModal(booking)}
                                >
                                    Xóa
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Xác nhận xóa</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Bạn chắc chắn muốn xóa đặt phòng của "{bookingToDelete?.customerName}"?
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

            <Modal show={showStatusModal} onHide={() => setShowStatusModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Thay đổi trạng thái đặt phòng</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group>
                        <Form.Label>Trạng thái mới</Form.Label>
                        <Form.Control
                            as="select"
                            value={newStatus}
                            onChange={(e) => setNewStatus(e.target.value)}
                        >
                            <option value="0">Đang chờ</option>
                            <option value="1">Đã xác nhận</option>
                            <option value="2">Đã hủy</option>
                        </Form.Control>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowStatusModal(false)}>
                        Hủy
                    </Button>
                    <Button variant="primary" onClick={handleUpdateStatus}>
                        Lưu
                    </Button>
                </Modal.Footer>
            </Modal>

            <BookingDetailModal
                show={showDetailModal}
                onHide={() => setShowDetailModal(false)}
                booking={bookingToView}
            />
        </div>
    );
};

export default BookingList;
