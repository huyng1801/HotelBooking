import React, { useState, useEffect } from 'react';
import { Container, Table, Button, ButtonGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const Cart = () => {
    const [cart, setCart] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const savedCart = JSON.parse(localStorage.getItem('cart')) || [];
        setCart(savedCart);
    }, []);

    const updateCartQuantity = (roomId, quantity) => {
        const updatedCart = cart.map((item) =>
            item.roomId === roomId ? { ...item, quantity: item.quantity + quantity } : item
        ).filter((item) => item.quantity > 0);
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart));
    };

    const removeFromCart = (roomId) => {
        const updatedCart = cart.filter((item) => item.roomId !== roomId);
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart));
    };

    const totalPrice = cart.reduce((total, item) => total + item.price * item.quantity, 0);

    const handleCheckout = () => {
        navigate('/checkout');
    };

    return (
        <Container className="mt-5 mb-3">
            {cart.length > 0 ? (
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Phòng</th>
                            <th>Giá</th>
                            <th>Số lượng</th>
                            <th>Thành tiền</th>
                            <th>Thao tác</th>
                        </tr>
                    </thead>
                    <tbody>
                        {cart.map((item) => (
                            <tr key={item.roomId}>
                                <td>
                                    {item.imageUrls && item.imageUrls.length > 0 && (
                                        <img src={item.imageUrls[0].imageUrl} alt={item.roomName} style={{ maxWidth: '100px' }} />
                                    )}
                                    {item.roomName}
                                </td>
                                <td>{item.price.toLocaleString()} VND</td>
                                <td>
                                    <ButtonGroup aria-label="Quantity" className="me-2">
                                        <Button variant="outline-secondary" onClick={() => updateCartQuantity(item.roomId, -1)}>-</Button>
                                        <Button variant="outline-secondary" disabled>{item.quantity}</Button>
                                        <Button variant="outline-secondary" onClick={() => updateCartQuantity(item.roomId, 1)}>+</Button>
                                    </ButtonGroup>
                                </td>
                                <td>{(item.price * item.quantity).toLocaleString()} VND</td>
                                <td>
                                    <Button variant="danger" onClick={() => removeFromCart(item.roomId)}>X</Button>
                                </td>
                            </tr>
                        ))}
                        <tr>
                            <td colSpan="3" style={{ textAlign: 'right' }}><strong>Tổng tiền:</strong></td>
                            <td colSpan="2" className="fw-bold">{totalPrice.toLocaleString()} VND</td>
                        </tr>
                    </tbody>
                </Table>
            ) : (
                <p>Không có sản phẩm trong giỏ hàng của bạn.</p>
            )}
            {cart.length > 0 && <Button className="btn btn-primary" onClick={handleCheckout}>Thanh toán</Button>}
        </Container>
    );
};

export default Cart;
