import React from 'react';

const Footer = () => {
    return (
        <footer className="text-white text-center text-lg-start" style={{ backgroundColor: '#23242a' }}>
            <div className="container p-4">
                <div className="row mt-4">
                    <div className="col-lg-4 col-md-12 mb-4 mb-md-0">
                        <h5 className="text-uppercase mb-4">Hệ thống đặt phòng khách sạn</h5>
                        <p>Hệ thống của chúng tôi cung cấp dịch vụ đặt phòng cho nhiều khách sạn sang trọng với tiêu chuẩn cao, bao gồm các loại phòng từ Phòng tiêu chuẩn đến Phòng cao cấp và Phòng sang trọng, phục vụ khách hàng 24/7.</p>
                        <div className="mt-4">
                            <a className="btn btn-floating btn-warning btn-lg">
                                <i className="fab fa-facebook-f"></i>
                            </a>
                            <a className="btn btn-floating btn-warning btn-lg ml-2">
                                <i className="fab fa-dribbble"></i>
                            </a>
                            <a className="btn btn-floating btn-warning btn-lg ml-2">
                                <i className="fab fa-twitter"></i>
                            </a>
                            <a className="btn btn-floating btn-warning btn-lg ml-2">
                                <i className="fab fa-google-plus-g"></i>
                            </a>
                        </div>
                    </div>
                    <div className="col-lg-4 col-md-6 mb-4 mb-md-0">
                        <h5 className="text-uppercase mb-4">Thông tin liên lạc</h5>
                        <ul className="fa-ul">
                            <li className="mb-3">
                                <span className="fa-li"><i className="fas fa-home"></i></span> Đường ABC, Quận XYZ, TP.HCM
                            </li>
                            <li className="mb-3">
                                <span className="fa-li"><i className="fas fa-envelope"></i></span> abc@gmail.com
                            </li>
                            <li className="mb-3">
                                <span className="fa-li"><i className="fas fa-phone"></i></span> 0123 456 789
                            </li>
                        </ul>
                    </div>
                    <div className="col-lg-4 col-md-6 mb-4 mb-md-0">
                        <h5 className="text-uppercase mb-4">Giờ mở cửa</h5>
                        <table className="table table-dark text-center">
                            <tbody>
                                <tr>
                                    <td>Thứ Hai - Thứ Năm:</td>
                                    <td>8:00 - 21:00</td>
                                </tr>
                                <tr>
                                    <td>Thứ Sáu - Thứ Bảy:</td>
                                    <td>8:00 - 13:00</td>
                                </tr>
                                <tr>
                                    <td>Chủ Nhật:</td>
                                    <td>9:00 - 22:00</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div className="text-center p-3" style={{ backgroundColor: 'rgba(0, 0, 0, 0.2)' }}>
                © 2024 Bản quyền thuộc về hệ thống đặt phòng khách sạn
            </div>
        </footer>
    );
};

export default Footer;
