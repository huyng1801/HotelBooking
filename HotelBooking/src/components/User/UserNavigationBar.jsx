import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Form, FormControl, Button, Nav, Navbar } from 'react-bootstrap';
import { useAuth } from '../Auth/AuthProvider';

const UserNavigationBar = () => {
    const [searchQuery, setSearchQuery] = useState('');
    const navigate = useNavigate();
    const { auth, logout } = useAuth();

    const handleLogout = () => {
        logout();
        navigate('/');
    };

    return (
        <header>
            <Navbar bg="light" expand="lg">
                <div className="container-fluid">
                    <Navbar.Brand as={Link} to="/">Hotel Booking</Navbar.Brand>
                    <Navbar.Toggle aria-controls="navbarNav" />
                    <Navbar.Collapse id="navbarNav">
                        <Nav className="me-auto">
                            <Nav.Link as={Link} to="/">Trang chủ</Nav.Link>
                            <Nav.Link as={Link} to="/hotels">Khách sạn</Nav.Link>
                            <Nav.Link as={Link} to="/cart">Giỏ hàng</Nav.Link>
                        </Nav>
                        <Nav className="ml-auto">
                            {auth ? (
                                <>
                                    <Nav.Link as={Link} to="/profile">{auth.fullName}</Nav.Link>
                                    <Nav.Link onClick={handleLogout}>Logout</Nav.Link>
                                </>
                            ) : (
                                <>
                                    <Nav.Link as={Link} to="/login_customer">Đăng nhập</Nav.Link>
                                    <Nav.Link as={Link} to="/register">Đăng ký</Nav.Link>
                                </>
                            )}
                        </Nav>
                    </Navbar.Collapse>
                </div>
            </Navbar>
        </header>
    );
};

export default UserNavigationBar;
