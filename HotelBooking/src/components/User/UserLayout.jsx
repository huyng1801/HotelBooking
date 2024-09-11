import React from 'react';
import UserNavigationBar from './UserNavigationBar';
import Footer from '../Shared/Footer';

const UserLayout = ({ children }) => {
    return (
        <div>
            <UserNavigationBar />
            <div className="container mt-3">
                <div className="row">
                    <div className="col-12">
                        {children}
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    );
};

export default UserLayout;
