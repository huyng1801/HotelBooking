import React from 'react';
import { Route, Navigate } from 'react-router-dom';
import { useAuth } from './AuthProvider';

const PrivateRoute = ({ children, roles }) => {
    const { auth } = useAuth();

    if (!auth) {
        return <Navigate to="/" />;
    }

    if (roles && roles.length > 0 && !roles.includes(auth.role)) {
        return <Navigate to="/admin" />;
    }

    return children;
};

export default PrivateRoute;
