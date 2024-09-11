import React, { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { updateUser } from '../../../api/UserService';
import CryptoJS from 'crypto-js';

const EditUserModal = ({ show, onHide, fetchUsers, user, onError }) => {
    const [fullName, setFullName] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [email, setEmail] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [country, setCountry] = useState('');
    const [role, setRole] = useState(0);
    const [hashPassword, setHashPassword] = useState('');

    useEffect(() => {
        if (user) {
            setFullName(user.fullName);
            setPhoneNumber(user.phoneNumber);
            setEmail(user.email);
            setBirthDate(user.birthDate);
            setCountry(user.country);
            setRole(user.role);
        }
        if (show) {
            setHashPassword('');
        }
    }, [user, show]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        let userData = { fullName, phoneNumber, email, birthDate, country, role };
        
        if (hashPassword) {
            const hashedPassword = CryptoJS.MD5(hashPassword).toString();
            userData = { ...userData, hashPassword: hashedPassword };
        }

        try {
            const response = await updateUser(user.userId, userData);
            if (response.success) {
                fetchUsers();
                onHide();
            } else {
                onError(response.error);
            }
        } catch (error) {
            onError('Error updating user');
        }
    };

    return (
        <Modal show={show} onHide={onHide}>
            <Modal.Header closeButton>
                <Modal.Title>Sửa người dùng</Modal.Title>
            </Modal.Header>
            <Form onSubmit={handleSubmit}>
                <Modal.Body>
                    <Form.Group className="mb-3">
                        <Form.Label>Họ và tên</Form.Label>
                        <Form.Control
                            type="text"
                            value={fullName}
                            onChange={(e) => setFullName(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Số điện thoại</Form.Label>
                        <Form.Control
                            type="text"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Ngày sinh</Form.Label>
                        <Form.Control
                            type="date"
                            value={birthDate}
                            onChange={(e) => setBirthDate(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Quốc gia</Form.Label>
                        <Form.Control
                            type="text"
                            value={country}
                            onChange={(e) => setCountry(e.target.value)}
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Vai trò</Form.Label>
                        <Form.Control
                            as="select"
                            value={role}
                            onChange={(e) => setRole(e.target.value)}
                            required
                        >
                            <option value="0">Quản trị viên</option>
                            <option value="1">Nhân viên</option>
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Mật khẩu (để trống nếu không đổi)</Form.Label>
                        <Form.Control
                            type="password"
                            value={hashPassword}
                            onChange={(e) => setHashPassword(e.target.value)}
                        />
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={onHide}>
                        Đóng
                    </Button>
                    <Button type="submit" variant="primary">
                        Lưu thay đổi
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    );
};

export default EditUserModal;
