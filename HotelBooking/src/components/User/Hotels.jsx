import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button, Card, Pagination, Alert } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { getAllHotels, searchHotels } from '../../api/HomeService';
import '../../assets/css/Hotels.css'; 

const Hotels = () => {
    const [hotels, setHotels] = useState([]);
    const [city, setCity] = useState('');
    const [checkInDate, setCheckInDate] = useState('');
    const [checkOutDate, setCheckOutDate] = useState('');
    const [numberOfPersons, setNumberOfPersons] = useState(2);
    const [numberOfRooms, setNumberOfRooms] = useState(1);
    const [currentPage, setCurrentPage] = useState(1);
    const [errorMessage, setErrorMessage] = useState('');
    const hotelsPerPage = 5;

    const fetchHotels = async () => {
        try {
            const response = await getAllHotels();
            setHotels(response);
        } catch (error) {
            console.error('Error fetching hotels:', error);
        }
    };

    const handleSearch = async () => {
        if (new Date(checkInDate) >= new Date(checkOutDate)) {
            setErrorMessage('Ngày nhận phòng phải trước ngày trả phòng.');
            return;
        }
        setErrorMessage('');
        try {
            const response = await searchHotels(city, checkInDate, checkOutDate, numberOfPersons, numberOfRooms);
            setHotels(response);
        } catch (error) {
            console.error('Error searching hotels:', error);
        }
    };

    const indexOfLastHotel = currentPage * hotelsPerPage;
    const indexOfFirstHotel = indexOfLastHotel - hotelsPerPage;
    const currentHotels = hotels.slice(indexOfFirstHotel, indexOfLastHotel);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    return (
        <Container>
            <Row className="my-3">
                <Col>
                    <Form>
                        <Row className="align-items-end">
                            <Col md={4}>
                                <Form.Group controlId="formGridCity">
                                    <Form.Label>Thành phố</Form.Label>
                                    <Form.Control 
                                        type="text" 
                                        placeholder="Nhập thành phố" 
                                        value={city} 
                                        onChange={(e) => setCity(e.target.value)} 
                                    />
                                </Form.Group>
                            </Col>
                            <Col md={2}>
                                <Form.Group controlId="formGridCheckIn">
                                    <Form.Label>Ngày nhận phòng</Form.Label>
                                    <Form.Control 
                                        type="date" 
                                        value={checkInDate} 
                                        onChange={(e) => setCheckInDate(e.target.value)} 
                                    />
                                </Form.Group>
                            </Col>
                            <Col md={2}>
                                <Form.Group controlId="formGridCheckOut">
                                    <Form.Label>Ngày trả phòng</Form.Label>
                                    <Form.Control 
                                        type="date" 
                                        value={checkOutDate} 
                                        onChange={(e) => setCheckOutDate(e.target.value)} 
                                    />
                                </Form.Group>
                            </Col>
                            <Col md={1}>
                                <Form.Group controlId="formGridPersons">
                                    <Form.Label>Người</Form.Label>
                                    <Form.Control 
                                        type="number" 
                                        placeholder="Người" 
                                        value={numberOfPersons} 
                                        min={1}
                                        onChange={(e) => setNumberOfPersons(e.target.value)} 
                                    />
                                </Form.Group>
                            </Col>
                            <Col md={1}>
                                <Form.Group controlId="formGridRooms">
                                    <Form.Label>Phòng</Form.Label>
                                    <Form.Control 
                                        type="number" 
                                        placeholder="Phòng" 
                                        value={numberOfRooms} 
                                        min={1}
                                        onChange={(e) => setNumberOfRooms(e.target.value)} 
                                    />
                                </Form.Group>
                            </Col>
                            <Col md={2}>
                                <Button variant="primary" onClick={handleSearch}>
                                    Tìm kiếm
                                </Button>
                            </Col>
                        </Row>
                    </Form>
                    {errorMessage && (
                        <Alert variant="danger" className="mt-3">
                            {errorMessage}
                        </Alert>
                    )}
                </Col>
            </Row>
            {hotels.length > 0 ? (
                <>
                    <h2 className="mt-3">Các kết quả phù hợp</h2>
                    <Row>
                        {currentHotels.map((hotel) => (
                            <Col key={hotel.hotelId} md={4}>
                                <Card className="mb-4 card-custom">
                                    <Card.Img variant="top" src={hotel.images[0]?.imageUrl} alt={`Image of ${hotel.hotelName}`} />
                                    <Card.Body>
                                        <Card.Title>{hotel.hotelName}</Card.Title>
                                        <Card.Text>
                                            <strong>Địa chỉ:</strong> {hotel.address}
                                        </Card.Text>
                                        <Card.Text>
                                            <strong>Thành phố:</strong> {hotel.city}
                                        </Card.Text>
                                        <Link to={`/hotel/${hotel.hotelId}`}>
                                            <Button variant="primary">Xem chi tiết</Button>
                                        </Link>
                                    </Card.Body>
                                </Card>
                            </Col>
                        ))}
                    </Row>
                    <Pagination>
                        {Array.from({ length: Math.ceil(hotels.length / hotelsPerPage) }, (_, index) => (
                            <Pagination.Item key={index + 1} active={index + 1 === currentPage} onClick={() => paginate(index + 1)}>
                                {index + 1}
                            </Pagination.Item>
                        ))}
                    </Pagination>
                </>
            ) : (
                <h2 className="mt-3">Không có kết quả phù hợp</h2>
            )}
        </Container>
    );
};

export default Hotels;
