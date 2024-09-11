import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';
import { registerUser } from '../../api/HomeService';
import CryptoJS from 'crypto-js';

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [fullName, setFullName] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [email, setEmail] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [country, setCountry] = useState('');
    const [role, setRole] = useState(2); // Default to role 2 for customer registration
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const hashedPassword = CryptoJS.MD5(password).toString();
        const userData = {
            username,
            hashPassword: hashedPassword,
            fullName,
            phoneNumber,
            email,
            birthDate,
            country,
            role
        };
        try {
            const response = await registerUser(userData);
            if (response.success) {
                navigate('/login_customer');
            } else {
                setError(response.message);
            }
        } catch (error) {
            setError('Đăng ký thất bại');
        }
    };

    return (
        <Container className="my-5">
            <h2>Đăng ký</h2>
            <Form onSubmit={handleSubmit}>
                <Form.Group controlId="username">
                    <Form.Label>Tên đăng nhập</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Nhập tên đăng nhập"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="password">
                    <Form.Label>Mật khẩu</Form.Label>
                    <Form.Control
                        type="password"
                        placeholder="Nhập mật khẩu"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="fullName">
                    <Form.Label>Họ và tên</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Nhập họ và tên"
                        value={fullName}
                        onChange={(e) => setFullName(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="phoneNumber">
                    <Form.Label>Số điện thoại</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Nhập số điện thoại"
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control
                        type="email"
                        placeholder="Nhập email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="birthDate">
                    <Form.Label>Ngày sinh</Form.Label>
                    <Form.Control
                        type="date"
                        value={birthDate}
                        onChange={(e) => setBirthDate(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="country">
                    <Form.Label>Quốc gia</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Nhập quốc gia"
                        value={country}
                        onChange={(e) => setCountry(e.target.value)}
                        required
                    />
                </Form.Group>

                {error && <p className="text-danger">{error}</p>}

                <Button variant="primary" type="submit" className="mt-3">
                    Đăng ký
                </Button>
            </Form>
        </Container>
    );
};

export default Register;
