import axiosInstance from './axiosConfig';

let auth = JSON.parse(localStorage.getItem('auth')) || null;

const getToken = () => {
    return auth ? auth.token : null;
};

const setAuth = (newAuth) => {
    auth = newAuth;
    localStorage.setItem('auth', JSON.stringify(auth));
};

const clearAuth = () => {
    auth = null;
    localStorage.removeItem('auth');
};

const refreshToken = async () => {
    if (auth && auth.refreshToken) {
        try {
            const response = await axiosInstance.post('/Auth/refresh-token', {
                accessToken: auth.token,
                refreshToken: auth.refreshToken,
            });

            const newAuthData = {
                ...auth,
                token: response.data.token,
                refreshToken: response.data.refreshToken,
            };

            setAuth(newAuthData);
            return response.data.token;
        } catch (error) {
            console.error('Failed to refresh token', error);
            clearAuth();
            throw error;
        }
    }
    return null;
};

export { getToken, setAuth, clearAuth, refreshToken };
