import React, { useState, useEffect } from 'react';
import { Button, Table, Modal, Alert } from 'react-bootstrap';
import { getAllUsers, deleteUser, toggleIsActive } from '../../../api/UserService';
import AddUserModal from './AddUserModal';
import EditUserModal from './EditUserModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash, faPlus, faToggleOn, faToggleOff } from '@fortawesome/free-solid-svg-icons';

const UserList = () => {
    const [users, setUsers] = useState([]);
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [currentUser, setCurrentUser] = useState(null);
    const [userToDelete, setUserToDelete] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        const response = await getAllUsers();
        if (response.success) {
            setUsers(response.data);
        } else {
            setError(response.error);
        }
    };

    const handleDelete = async () => {
        try {
            if (userToDelete) {
                await deleteUser(userToDelete.userId);
                fetchUsers();
                setShowDeleteModal(false);
            }
        } catch (error) {
            setError('Xóa người dùng không thành công do có liên kết');
        }
    };

    const handleToggleIsActive = async (user) => {
        try {
            await toggleIsActive(user.userId);
            fetchUsers();
        } catch (error) {
            setError('Không thể thay đổi trạng thái hoạt động của người dùng.');
        }
    };

    const handleShowDeleteModal = (user) => {
        setUserToDelete(user);
        setShowDeleteModal(true);
    };

    const handleShowEditModal = (user) => {
        setCurrentUser(user);
        setShowEditModal(true);
    };

    const handleError = (errorMessage) => {
        setError(errorMessage);
        setTimeout(() => {
            setError('');
        }, 3000);
    };

    return (
        <div className="container">
            <h1 className="text-center text-uppercase">Danh sách người dùng</h1>
            {error && <Alert variant="danger">{error}</Alert>}
            <Button className="mb-3" onClick={() => setShowAddModal(true)}>
                <FontAwesomeIcon icon={faPlus} /> Thêm mới
            </Button>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Tên đăng nhập</th>
                        <th>Họ và tên</th>
                        <th>Email</th>
                        <th>Số điện thoại</th>
                        <th>Ngày sinh</th>
                        <th>Quốc gia</th>
                        <th>Vai trò</th>
                        <th>Trạng thái</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((user, index) => (
                        <tr key={user.userId}>
                            <td>{index + 1}</td>
                            <td>{user.username}</td>
                            <td>{user.fullName}</td>
                            <td>{user.email}</td>
                            <td>{user.phoneNumber}</td>
                            <td>{new Date(user.birthDate).toLocaleDateString()}</td>
                            <td>{user.country}</td>
                            <td>
                                {user.role === 0 ? 'Quản trị viên' 
                                : user.role === 1 ? 'Nhân viên' 
                                : user.role === 2 ? 'Khách hàng' 
                                : 'Không xác định'}
                            </td>
                            <td>
                                <Button variant="link" onClick={() => handleToggleIsActive(user)}>
                                    <FontAwesomeIcon icon={user.isActive ? faToggleOn : faToggleOff} />
                                </Button>
                            </td>
                            <td>
                                <Button
                                    variant="warning"
                                    onClick={() => handleShowEditModal(user)}
                                    disabled={user.role === 2}
                                >
                                    <FontAwesomeIcon icon={faEdit} />
                                </Button>{' '}
                                <Button
                                    variant="danger"
                                    onClick={() => handleShowDeleteModal(user)}
                                >
                                    <FontAwesomeIcon icon={faTrash} />
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <AddUserModal
                show={showAddModal}
                onHide={() => setShowAddModal(false)}
                fetchUsers={fetchUsers}
                onError={handleError}
            />

            <EditUserModal
                show={showEditModal}
                onHide={() => setShowEditModal(false)}
                fetchUsers={fetchUsers}
                user={currentUser}
                onError={handleError}
            />

            <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Xác nhận xóa</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Bạn chắc chắn muốn xóa người dùng "{userToDelete?.username}"?
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
        </div>
    );
};

export default UserList;
