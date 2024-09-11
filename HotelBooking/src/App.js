import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AdminDashboard from './components/Admin/AdminDashboard';
import Home from './components/User/Home';
import LoginForm from './components/Admin/LoginForm';
import AdminLayout from './components/Admin/AdminLayout';
import UserLayout from './components/User/UserLayout';
import { AuthProvider } from './components/Auth/AuthProvider';
import PrivateRoute from './components/Auth/PrivateRoute';
import RoomList from './components/Admin/Room/RoomList';
import UserList from './components/Admin/User/UserList';
import AmenityList from './components/Admin/Amenity/AmenityList';
import Cart from './components/User/Cart';
import Checkout from './components/User/Checkout';
import BookingList from './components/Admin/Booking/BookingList';
import HotelList from './components/Admin/Hotel/HotelList';
import Hotels from './components/User/Hotels';
import HotelDetails from './components/User/HotelDetails';
import LoginCustomer from './components/User/LoginCustomer';
import Register from './components/User/Register';

const App = () => {
    return (
        <AuthProvider>
            <Router>
                <Routes>
                    {/* User Routes */}
                    <Route path="/" element={<UserLayout><Home /></UserLayout>} />
                    <Route path="/hotels" element={<UserLayout><Hotels /></UserLayout>} />
                    <Route path="/cart" element={<UserLayout><Cart /></UserLayout>} />
                    <Route path="/checkout" element={<UserLayout><Checkout /></UserLayout>} />
                    <Route path="/hotel/:hotelId" element={<UserLayout><HotelDetails /></UserLayout>} />
                    <Route path="/login_customer" element={<UserLayout><LoginCustomer /></UserLayout>} />
                    <Route path="/register" element={<UserLayout><Register /></UserLayout>} />

                    {/* Auth Routes */}
                    <Route path="/login" element={<LoginForm />} />

                    {/* Admin Routes */}
                    <Route path="/admin" element={<PrivateRoute roles={[0, 1]}><AdminLayout><AdminDashboard /></AdminLayout></PrivateRoute>} />
                    <Route path="/admin/hotel" element={<PrivateRoute roles={[0, 1]}><AdminLayout><HotelList /></AdminLayout></PrivateRoute>} />
                    <Route path="/admin/user" element={<PrivateRoute roles={[0, 1]}><AdminLayout><UserList /></AdminLayout></PrivateRoute>} />
                    <Route path="/admin/hotel/:hotelId/rooms" element={<PrivateRoute roles={[0, 1]}><AdminLayout><RoomList /></AdminLayout></PrivateRoute>} />
                    <Route path="/admin/amenity" element={<PrivateRoute roles={[0, 1]}><AdminLayout><AmenityList /></AdminLayout></PrivateRoute>} />
                    <Route path="/admin/booking" element={<PrivateRoute roles={[0, 1]}><AdminLayout><BookingList /></AdminLayout></PrivateRoute>} />
                </Routes>
            </Router>
        </AuthProvider>
    );
};

export default App;
