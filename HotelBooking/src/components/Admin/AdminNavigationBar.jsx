import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../Auth/AuthProvider';
import { Navbar, Nav, NavDropdown, Container } from 'react-bootstrap';

const AdminNavigationBar = () => {
    const { auth, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <Navbar bg="light" expand="lg">
            <Container>
                <Navbar.Brand as={Link} to="/admin">Hotel Booking</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link as={Link} to="/admin">Trang chủ</Nav.Link>
                        {auth && auth.role === 0 && (
                            <Nav.Link as={Link} to="/admin/user">Người dùng</Nav.Link>
                        )}
                        <Nav.Link as={Link} to="/admin/hotel">Khách sạn</Nav.Link>
                        <Nav.Link as={Link} to="/admin/amenity">Tiện nghi</Nav.Link>
                        <Nav.Link as={Link} to="/admin/booking">Đặt phòng</Nav.Link>
                    </Nav>
                    {auth && (
                        <Nav>
                            <NavDropdown title={auth.username} id="basic-nav-dropdown">
                                <NavDropdown.Item onClick={handleLogout}>Đăng xuất</NavDropdown.Item>
                            </NavDropdown>
                        </Nav>
                    )}
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default AdminNavigationBar;
