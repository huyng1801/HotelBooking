import React, { useEffect, useState } from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import { Bar } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { getStatistics } from '../../api/StatisticsService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHotel, faBed, faClipboardList, faMoneyBillWave, faCalendarAlt, faUser } from '@fortawesome/free-solid-svg-icons';

// Register components
ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const AdminDashboard = () => {
    const [stats, setStats] = useState({
        totalHotels: 0,
        totalRooms: 0,
        totalUsers: 0,
        totalBookings: 0,
        dailyRevenue: 0,
        monthlyRevenue: 0,
        yearlyRevenue: []
    });

    useEffect(() => {
        const fetchStatistics = async () => {
            try {
                const data = await getStatistics();
                setStats(data);
            } catch (error) {
                console.error('Lỗi khi lấy dữ liệu thống kê:', error);
            }
        };

        fetchStatistics();
    }, []);

    const formatCurrency = (amount) => {
        return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
    };



    return (
        <div className="container my-3">
            <h1 className="text-center text-uppercase">Thống kê</h1>
            <Row className="mt-4">
                <Col md={3}>
                    <Card className="shadow-sm bg-primary text-white  p-3">
                        <Card.Body>
                            <Card.Title className="d-flex justify-content-between align-items-center">
                                Khách sạn
                                <FontAwesomeIcon icon={faHotel} size="2x" />
                            </Card.Title>
                            <Card.Text className="h2">{stats.totalHotels}</Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3}>
                    <Card className="shadow-sm bg-success text-white p-3">
                        <Card.Body>
                            <Card.Title className="d-flex justify-content-between align-items-center">
                                Phòng
                                <FontAwesomeIcon icon={faBed} size="2x" />
                            </Card.Title>
                            <Card.Text className="h2">{stats.totalRooms}</Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3}>
                    <Card className="shadow-sm bg-warning text-white p-3">
                        <Card.Body>
                            <Card.Title className="d-flex justify-content-between align-items-center">
                                Người dùng
                                <FontAwesomeIcon icon={faUser} size="2x" />
                            </Card.Title>
                            <Card.Text className="h2">{stats.totalUsers}</Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3}>
                    <Card className="shadow-sm bg-danger text-white p-3">
                        <Card.Body>
                            <Card.Title className="d-flex justify-content-between align-items-center">
                                Đặt phòng
                                <FontAwesomeIcon icon={faClipboardList} size="2x" />
                            </Card.Title>
                            <Card.Text className="h2">{stats.totalBookings}</Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row className="mt-4">
                <Col md={6}>
                    <Card className="shadow-sm bg-info text-white p-3">
                        <Card.Body>
                            <Card.Title className="d-flex justify-content-between align-items-center">
                                Doanh thu hàng ngày
                                <FontAwesomeIcon icon={faMoneyBillWave} size="2x" />
                            </Card.Title>
                            <Card.Text className="h2">{formatCurrency(stats.dailyRevenue)}</Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={6}>
                    <Card className="shadow-sm bg-secondary text-white p-3">
                        <Card.Body>
                            <Card.Title className="d-flex justify-content-between align-items-center">
                                Doanh thu hàng tháng
                                <FontAwesomeIcon icon={faCalendarAlt} size="2x" />
                            </Card.Title>
                            <Card.Text className="h2">{formatCurrency(stats.monthlyRevenue)}</Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
   
        </div>
    );
};

export default AdminDashboard;
