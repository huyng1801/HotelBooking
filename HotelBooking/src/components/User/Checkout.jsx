import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Form, Button } from 'react-bootstrap';
import { createBooking } from '../../api/HomeService';
import { useAuth } from '../Auth/AuthProvider';

const Checkout = () => {
    const [cart, setCart] = useState(JSON.parse(localStorage.getItem('cart')) || []);
    const [checkInDate, setCheckInDate] = useState('');
    const [checkOutDate, setCheckOutDate] = useState('');
    const [numberOfAdults, setNumberOfAdults] = useState(1);
    const [numberOfChildren, setNumberOfChildren] = useState(0);
    const [note, setNote] = useState('');
    const navigate = useNavigate();
    const { auth } = useAuth();

    const generateBookingId = () => {
        const now = new Date();
        const day = String(now.getDate()).padStart(2, '0');
        const month = String(now.getMonth() + 1).padStart(2, '0'); // Months are 0-based
        const year = String(now.getFullYear()).slice(-2);
        const hours = String(now.getHours()).padStart(2, '0');
        const minutes = String(now.getMinutes()).padStart(2, '0');
        const seconds = String(now.getSeconds()).padStart(2, '0');
        return `BK${day}${month}${year}${hours}${minutes}${seconds}`;
    };
    const handleSubmit = async (event) => {
        event.preventDefault();
        const bookingData = {
            bookingId: generateBookingId(),
            checkInDate,
            checkOutDate,
            totalAmount: cart.reduce((total, item) => total + item.price * item.quantity, 0),
            status: 0,
            numberOfAdults,
            numberOfChildren,
            note,
            paymentMethod: 'Credit Card', // Assuming payment method is predefined
            paymentStatus: false, // Assuming initial payment status
            userId: auth.userId, // Use userId from auth context
            bookingDetails: cart.map(item => ({
                roomId: item.roomId,
                unitPrice: item.price,
                roomCount: item.quantity
            }))
        };

        try {
            const response = await createBooking(bookingData);
            localStorage.removeItem('cart');
            setCart([]);
            setCart([]);
            localStorage.setItem('bookingId', response.bookingId); 
            window.location.href = response.paymentUrl;
        } catch (error) {
            console.error('Error creating booking:', error);
        }
    };

    return (
        <Container className="mt-5 mb-3">
            <h2>Xác nhận thông tin</h2>
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label>Ngày nhận phòng</Form.Label>
                    <Form.Control
                        type="datetime-local"
                        value={checkInDate}
                        onChange={(e) => setCheckInDate(e.target.value)}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Ngày trả phòng</Form.Label>
                    <Form.Control
                        type="datetime-local"
                        value={checkOutDate}
                        onChange={(e) => setCheckOutDate(e.target.value)}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Số người lớn</Form.Label>
                    <Form.Control
                        type="number"
                        value={numberOfAdults}
                        onChange={(e) => setNumberOfAdults(e.target.value)}
                        min={1}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Số trẻ em</Form.Label>
                    <Form.Control
                        type="number"
                        value={numberOfChildren}
                        onChange={(e) => setNumberOfChildren(e.target.value)}
                        min={0}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Ghi chú</Form.Label>
                    <Form.Control
                        as="textarea"
                        value={note}
                        onChange={(e) => setNote(e.target.value)}
                    />
                </Form.Group>
                <Button type="submit" variant="primary mt-3">Submit</Button>
            </Form>
        </Container>
    );
};

export default Checkout;
