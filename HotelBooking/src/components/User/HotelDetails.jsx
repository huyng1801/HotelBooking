import React, { useState, useEffect, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Button, Carousel } from 'react-bootstrap';
import { getHotelById, getRoomsByHotelId } from '../../api/HomeService';
import '../../assets/css/HotelDetails.css'; // Ensure you create this CSS file for custom styles

const HotelDetails = () => {
    const { hotelId } = useParams();
    const [hotel, setHotel] = useState(null);
    const [rooms, setRooms] = useState([]);
    const navigate = useNavigate();

    const fetchHotelDetails = useCallback(async () => {
        try {
            const response = await getHotelById(hotelId);
            setHotel(response);
        } catch (error) {
            console.error('Lỗi khi lấy thông tin khách sạn:', error);
        }
    }, [hotelId]);

    const fetchHotelRooms = useCallback(async () => {
        try {
            const response = await getRoomsByHotelId(hotelId);
            setRooms(response);
        } catch (error) {
            console.error('Lỗi khi lấy thông tin phòng:', error);
        }
    }, [hotelId]);

    useEffect(() => {
        fetchHotelDetails();
        fetchHotelRooms();
    }, [fetchHotelDetails, fetchHotelRooms]);

    const handleBookNow = (room) => {
        const savedCart = JSON.parse(localStorage.getItem('cart')) || [];
        const existingRoomIndex = savedCart.findIndex((item) => item.roomId === room.roomId);

        if (existingRoomIndex > -1) {
            savedCart[existingRoomIndex].quantity += 1;
        } else {
            savedCart.push({ ...room, quantity: 1 });
        }

        localStorage.setItem('cart', JSON.stringify(savedCart));
        navigate('/cart');
    };

    if (!hotel) {
        return <div>Đang tải...</div>;
    }

    return (
        <Container>
            <Row className="my-3">
                <Col>
                    <h2>{hotel.hotelName}</h2>
                    <p><strong>Địa chỉ:</strong> {hotel.address}</p>
                    <p><strong>Thành phố:</strong> {hotel.city}</p>
                </Col>
            </Row>
            <Row className="my-3">
                <Col>
                    <Card className="mb-4">
                        {hotel.images && hotel.images.length > 0 && (
                            <Carousel>
                                {hotel.images.map((image, index) => (
                                    <Carousel.Item key={index}>
                                        <img
                                            className="d-block w-100"
                                            src={image.imageUrl}
                                            alt={`Hình ảnh ${index + 1} của ${hotel.hotelName}`}
                                        />
                                    </Carousel.Item>
                                ))}
                            </Carousel>
                        )}
                    </Card>
                </Col>
            </Row>
            <Row className="my-3">
                <Col>
                    <h2>Chọn phòng của bạn</h2>
                    {rooms.map((room) => (
                        <Card key={room.roomId} className="mb-4 room-card">
                            <Row noGutters>
                                <Col md={4}>
                                    {room.imageUrls && room.imageUrls.length > 0 && (
                                        <Carousel>
                                            {room.imageUrls.map((image, index) => (
                                                <Carousel.Item key={index}>
                                                    <img
                                                        className="d-block w-100"
                                                        src={image.imageUrl}
                                                        alt={`Hình ảnh ${index + 1} của ${room.roomName}`}
                                                    />
                                                </Carousel.Item>
                                            ))}
                                        </Carousel>
                                    )}
                                </Col>
                                <Col md={8}>
                                    <Card.Body>
                                        <Card.Title>{room.roomName}</Card.Title>
                                        <Card.Text>
                                            <strong>Diện tích:</strong> {room.area} m²<br />
                                            <strong>Tầm nhìn:</strong> {room.view}<br />
                                            <strong>Không hút thuốc:</strong> {room.nonSmoking ? 'Có' : 'Không'}<br />
                                            <strong>Giá:</strong> {room.price.toLocaleString()} VND<br />
                                            <strong>Số người:</strong> {room.numberPerson}<br />
                                            <strong>Mô tả giường:</strong> {room.bedDescription}<br />
                                            <strong>Bao gồm bữa sáng:</strong> {room.eatBreakfast ? 'Có' : 'Không'}<br />
                                            <strong>Tiện nghi:</strong> {room.roomAmenities && room.roomAmenities.map(amenity => (
                                                <span key={amenity.amenityId}>{amenity.amenityName}, </span>
                                            ))}
                                        </Card.Text>
                                        <Button variant="primary" onClick={() => handleBookNow(room)}>Đặt ngay</Button>
                                    </Card.Body>
                                </Col>
                            </Row>
                        </Card>
                    ))}
                </Col>
            </Row>
            <Row className="my-3">
                <Col>
                    <Card className="mb-4">
                        <Card.Header><strong>Mô tả khách sạn</strong></Card.Header>
                        <Card.Body>
                            <Card.Text dangerouslySetInnerHTML={{ __html: hotel.description }} />
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row className="my-3">
                <Col>
                    <Card className="mb-4">
                        <Card.Header><strong>Chính sách chỗ ở</strong></Card.Header>
                        <Card.Body>
                            <Card.Text dangerouslySetInnerHTML={{ __html: hotel.accommodationPolicy }} />
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
};

export default HotelDetails;
