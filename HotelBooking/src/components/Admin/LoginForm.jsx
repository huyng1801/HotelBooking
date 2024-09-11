import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../Auth/AuthProvider';
import { Form, Button, Alert } from 'react-bootstrap';
import '../../assets/css/LoginForm.css'; 
import BackgroundImage from '../../assets/images/background.jpg';
import Logo from '../../assets/images/logo.png';
import { Link } from 'react-router-dom';

const LoginForm = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const { login } = useAuth();

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
            navigate('/admin');
        } catch (err) {
            setError('Thông tin đăng nhập không chính xác');
        }
    };

    return (
        <div className="sign-in__wrapper" style={{ backgroundImage: `url(${BackgroundImage})` }}>
            <div className="sign-in__backdrop"></div>
            <Form className="shadow p-4 bg-white rounded" onSubmit={handleSubmit}>
                <img className="mx-auto d-block mb-2" src={Logo} alt="logo" />
                <div className="h4 mb-2 text-center">Đăng nhập</div>
                {error && <Alert className="mb-2" variant="danger" onClose={() => setError('')} dismissible>{error}</Alert>}
                <Form.Group className="mb-3" controlId="username">
                    <Form.Label>Tên đăng nhập</Form.Label>
                    <Form.Control
                        type="text"
                        value={username}
                        placeholder="Tên đăng nhập"
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </Form.Group>
                <Form.Group className="mb-3" controlId="password">
                    <Form.Label>Mật khẩu</Form.Label>
                    <Form.Control
                        type="password"
                        value={password}
                        placeholder="Mật khẩu"
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </Form.Group>
                <Button className="w-100" variant="primary" type="submit">Đăng nhập</Button>
                <Link className="btn btn-secondary w-100 mt-3" to="/">Trở về</Link>
            </Form>
            <div className="w-100 mb-2 position-absolute bottom-0 start-50 translate-middle-x text-white text-center">
                Hotel Booking | &copy;2024
            </div>
        </div>
    );
};

export default LoginForm;
