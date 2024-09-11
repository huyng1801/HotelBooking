import axiosInstance from './axiosConfig';

export const getStatistics = async () => {
    const [totalHotels, totalRooms, totalUsers, totalBookings, dailyRevenue, monthlyRevenue, yearlyRevenue] = await Promise.all([
        axiosInstance.get('/statistics/total/hotels').then(res => res.data),
        axiosInstance.get('/statistics/total/rooms').then(res => res.data),
        axiosInstance.get('/statistics/total/blogPosts').then(res => res.data),
        axiosInstance.get('/statistics/total/bookings').then(res => res.data),
        axiosInstance.get('/statistics/revenue/daily', { params: { date: new Date().toISOString().split('T')[0] } }).then(res => res.data),
        axiosInstance.get('/statistics/revenue/monthly', { params: { month: new Date().getMonth() + 1, year: new Date().getFullYear() } }).then(res => res.data),
        axiosInstance.get('/statistics/revenue/yearly', { params: { year: new Date().getFullYear() } }).then(res => res.data)
    ]);

    return {
        totalHotels,
        totalRooms,
        totalUsers,
        totalBookings,
        dailyRevenue,
        monthlyRevenue,
        yearlyRevenue
    };
};
