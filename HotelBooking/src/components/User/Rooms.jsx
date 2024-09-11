import React, { useEffect, useState } from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import Sidebar from '../Shared/Sidebar';
import { getAllRooms, getAllRoomTypes } from '../../api/HomeService';

const Rooms = () => {
    const [rooms, setRooms] = useState([]);
    const [roomTypes, setRoomTypes] = useState([]);
    const [groupedRooms, setGroupedRooms] = useState({});
    const [selectedRoomTypeId, setSelectedRoomTypeId] = useState(null);

    useEffect(() => {
        const fetchRooms = async () => {
            try {
                const roomsData = await getAllRooms();
                setRooms(roomsData);
                groupRoomsByType(roomsData);
            } catch (error) {
                console.error('Error fetching rooms:', error);
            }
        };

        const fetchRoomTypes = async () => {
            try {
                const roomTypesData = await getAllRoomTypes();
                setRoomTypes(roomTypesData);
                if (roomTypesData.length > 0) {
                    setSelectedRoomTypeId(roomTypesData[0].roomTypeId);
                }
            } catch (error) {
                console.error('Error fetching room types:', error);
            }
        };

        fetchRooms();
        fetchRoomTypes();
    }, []);

    const groupRoomsByType = (rooms) => {
        const grouped = rooms.reduce((acc, room) => {
            if (!acc[room.roomTypeId]) {
                acc[room.roomTypeId] = [];
            }
            acc[room.roomTypeId].push(room);
            return acc;
        }, {});
        setGroupedRooms(grouped);
    };

    const handleSelectRoomType = (roomTypeId) => {
        setSelectedRoomTypeId(roomTypeId);
    };

    return (
        <Container>
            <Row className="mt-5">
                <Col md={3}>
                    <Sidebar
                        roomTypes={roomTypes}
                        selectedRoomTypeId={selectedRoomTypeId}
                        onSelectRoomType={handleSelectRoomType}
                    />
                </Col>
                <Col md={9}>
                    {selectedRoomTypeId && groupedRooms[selectedRoomTypeId] ? (
                        <>
                            <h3>{roomTypes.find((type) => type.roomTypeId === selectedRoomTypeId)?.roomTypeName}</h3>
                            <Row>
                                {groupedRooms[selectedRoomTypeId].map((room) => (
                                    <RoomCard key={room.roomId} room={room} />
                                ))}
                            </Row>
                        </>
                    ) : (
                        <p>No rooms found.</p>
                    )}
                </Col>
            </Row>
        </Container>
    );
};

export default Rooms;
