import React from 'react';
import { Modal, Button, ListGroup } from 'react-bootstrap';

const BookingDetailModal = ({ show, onHide, booking }) => {
    if (!booking) return null;

    return (
        <Modal show={show} onHide={onHide}>
            <Modal.Header closeButton>
                <Modal.Title>Chi tiết đặt phòng</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <p><strong>Tên khách hàng:</strong> {booking.customerName}</p>
                <p><strong>Số điện thoại:</strong> {booking.phoneNumber}</p>
                <p><strong>Email:</strong> {booking.email}</p>
                <p><strong>Ngày nhận phòng:</strong> {new Date(booking.checkInDate).toLocaleString()}</p>
                <p><strong>Ngày trả phòng:</strong> {new Date(booking.checkOutDate).toLocaleString()}</p>
                <p><strong>Tổng tiền:</strong> {booking.totalAmount.toLocaleString()} VND</p>
                <p><strong>Trạng thái:</strong> {booking.status}</p>
                <p><strong>Số người lớn:</strong> {booking.numberOfAdults}</p>
                <p><strong>Số trẻ em:</strong> {booking.numberOfChildren}</p>
                <p><strong>Ghi chú:</strong> {booking.note}</p>
                <h5>Chi tiết phòng</h5>
                <ListGroup>
                    {booking.bookingDetails.map((detail) => (
                        <ListGroup.Item key={detail.bookingDetailId}>
                            <p><strong>Tên phòng:</strong> {detail.roomName}</p>
                            <p><strong>Giá:</strong> {detail.unitPrice.toLocaleString()} VND</p>
                            <p><strong>Số lượng phòng:</strong> {detail.roomCount}</p>
                            <img src={detail.imageUrl} alt={detail.roomName} style={{ width: '100px', height: '100px', objectFit: 'cover' }} />
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

export default BookingDetailModal;
