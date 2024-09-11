import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';
import { useAuth } from '../Auth/AuthProvider';
import axios from 'axios';

const LoginCustomer = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7280/api/Auth/login', { username, password });
            const userData = {
                token: response.data.token,
                refreshToken: response.data.refreshToken,
                role: response.data.role,
                userName: response.data.username,
                userId: response.data.userId
            };
        
            login(userData);
            navigate('/');
        } catch (err) {
            setError('Thông tin đăng nhập không chính xác');
        }
    };

    return (
        <Container className="my-5">
            <h2>Đăng nhập</h2>
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

                {error && <p className="text-danger">{error}</p>}

                <Button variant="primary" type="submit" className="mt-3">
                    Đăng nhập
                </Button>
            </Form>
        </Container>
    );
};

export default LoginCustomer;
