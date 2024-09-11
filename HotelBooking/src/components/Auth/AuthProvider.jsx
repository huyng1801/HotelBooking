import React, { createContext, useContext, useState, useEffect, useCallback } from 'react';
import { setAuth, clearAuth, refreshToken as refreshAuthToken } from '../../api/tokenService';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [auth, setAuthState] = useState(() => JSON.parse(localStorage.getItem('auth')) || null);

    useEffect(() => {
        const storedAuth = localStorage.getItem('auth');
        if (storedAuth) {
            setAuthState(JSON.parse(storedAuth));
        }
    }, []);

    const login = (userData) => {
        console.log(userData);
        setAuthState(userData);
        setAuth(userData);
    };

    const logout = () => {
        setAuthState(null);
        clearAuth();
    };

    const refreshToken = useCallback(async () => {
        try {
            const newToken = await refreshAuthToken();
            if (newToken) {
                setAuthState((prevAuth) => ({
                    ...prevAuth,
                    token: newToken,
                }));
            }
            return newToken;
        } catch (error) {
            console.error('Failed to refresh token', error);
            logout();
        }
        return null;
    }, []);

    return (
        <AuthContext.Provider value={{ auth, login, logout, refreshToken }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    return useContext(AuthContext);
};
