import React from 'react';
import AdminNavigationBar from './AdminNavigationBar';
import Footer from '../Shared/Footer';

const AdminLayout = ({ children }) => {
    return (
        <div id="root">
            <AdminNavigationBar />
            <div className="main-content mt-3" style={{minHeight: '50vh'}}>
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

export default AdminLayout;
