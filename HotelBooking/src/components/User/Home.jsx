import React, { useEffect, useState } from 'react';
import { getAllHotels } from '../../api/HomeService';
import { Link } from 'react-router-dom'; 
import { Container, Row, Col, Image, Carousel, Card, Button } from 'react-bootstrap';
import bannerImage1 from '../../assets/images/banner1.png';
import bannerImage2 from '../../assets/images/banner2.png';
import '../../assets/css/Hotels.css'; 
const Home = () => {
    const [hotels, setHotels] = useState([]);

    useEffect(() => {
        const fetchRoomsAndHotels = async () => {
            try {
                const hotelsData = await getAllHotels();
                setHotels(hotelsData);
            } catch (error) {
                console.error('Lỗi khi lấy dữ liệu phòng và khách sạn:', error);
            }
        };

        fetchRoomsAndHotels();
    }, []);

    return (
        <Container>
            <Carousel className="my-4">
                <Carousel.Item>
                    <Image src={bannerImage1} fluid rounded />
                </Carousel.Item>
                <Carousel.Item>
                    <Image src={bannerImage2} fluid rounded />
                </Carousel.Item>
            </Carousel>
            <h1 className="text-center my-4">Chào mừng đến với Hotel Reserve</h1>
            <p className="text-center">Sự thoải mái của bạn là ưu tiên hàng đầu của chúng tôi. Đặt phòng ngay bây giờ!</p>
            <div className="my-4 p-4 bg-light rounded">
                <Row>
                    <Col md={6}>
                    </Col>
                    <Col md={6} className="d-flex align-items-center">
                        <div>
                            <h2>Về chúng tôi</h2>
                            <p>
                                Tại Hotel Reserve, chúng tôi cung cấp các chỗ ở hàng đầu với tiện nghi và dịch vụ xuất sắc. 
                                Dù bạn đi công tác hay du lịch, chúng tôi đảm bảo một kỳ nghỉ thoải mái và đáng nhớ.
                            </p>
                            <p>
                                Tận hưởng các phòng sang trọng của chúng tôi, các tiện ích đẳng cấp thế giới và đội ngũ nhân viên thân thiện
                                luôn sẵn sàng làm cho kỳ nghỉ của bạn trở nên không thể quên. Đặt phòng của bạn ngay hôm nay và 
                                trải nghiệm dịch vụ tốt nhất!
                            </p>
                        </div>
                    </Col>
                </Row>
            </div>
            <h2 className="my-4">Các khách sạn</h2>
            <Row>
                {hotels.map((hotel) => (
                    <Col md={4} key={hotel.hotelId}>
                         <Card className="mb-4 card-custom">
                            <Card.Img variant="top" src={hotel.images[0]?.imageUrl} alt={`Image of ${hotel.hotelName}`} />
                            <Card.Body>
                                <Card.Title>{hotel.hotelName}</Card.Title>
                                <Card.Text>
                                    <strong>Address:</strong> {hotel.address}
                                </Card.Text>
                                <Card.Text>
                                    <strong>City:</strong> {hotel.city}
                                </Card.Text>
                                <Link to={`/hotel/${hotel.hotelId}`}>
                                    <Button variant="primary">Xem chi tiết</Button>
                                </Link>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
            
        </Container>
    );
};

export default Home;
