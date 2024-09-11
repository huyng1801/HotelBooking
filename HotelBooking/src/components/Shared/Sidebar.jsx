import React from 'react';
import { ListGroup } from 'react-bootstrap';

const Sidebar = ({ roomTypes, selectedRoomTypeId, onSelectRoomType }) => {
    return (
        <div className="sidebar d-flex flex-column justify-content-center align-items-center">
            <ListGroup>
                {roomTypes.map((roomType) => (
                    <ListGroup.Item
                        key={roomType.roomTypeId}
                        active={roomType.roomTypeId === selectedRoomTypeId}
                        onClick={() => onSelectRoomType(roomType.roomTypeId)}
                        style={{ cursor: 'pointer' }}
                    >
                        {roomType.roomTypeName}
                    </ListGroup.Item>
                ))}
            </ListGroup>
        </div>
    );
};

export default Sidebar;
