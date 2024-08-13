CREATE DATABASE LTW_QLLK
go
USE LTW_QLLK
go
SET DATEFORMAT DMY
CREATE TABLE KHACH
(
	MAKH INT IDENTITY(1,1) PRIMARY KEY,
	TENTK NVARCHAR(50),
	HOTEN NVARCHAR(50),
	GIOITINH NVARCHAR(20),
	DIACHI NVARCHAR(MAX),
	NGAYSINH DATE,
	GHICHU NVARCHAR(MAX),
	SODT VARCHAR(20),
	EMAIL VARCHAR(50),
	PASSWORD_USER VARCHAR(50),
	ISADMIN INT
)
CREATE TABLE LOAISP
(
	MALOAI INT IDENTITY(1,1) PRIMARY KEY,
	TENLOAI NVARCHAR(50)
)
CREATE TABLE HANGSX
(
	MANSX INT IDENTITY(1,1) PRIMARY KEY,
	TENNSX NVARCHAR(50),
	QUOCGIA NVARCHAR(50)
)
CREATE TABLE SANPHAM
(
	MASP INT IDENTITY(1,1) PRIMARY KEY,
	TENSP NVARCHAR(100),
	BAOHANH INT,
	IMGSP1 VARCHAR(MAX),
	IMGSP2 VARCHAR(MAX), --CÓ THỂ CÓ HOẶC KHÔNG
	IMGSP3 VARCHAR(MAX), --CÓ THỂ CÓ HOẶC KHÔNG
	MIEUTA NVARCHAR(MAX),
	GIA FLOAT,
	SOLUONGSP INT,	
	TRANGTHAI NVARCHAR(MAX) DEFAULT N'Còn hàng',
	MANSX INT,
	MALOAI INT,	
	FOREIGN KEY (MALOAI) REFERENCES LOAISP(MALOAI),
	FOREIGN KEY (MANSX) REFERENCES HANGSX(MANSX)
)
CREATE TABLE GIOHANG --CHỨA DANH SÁCH SẢN PHẨM KHÁCH ĐÃ ẤN MUA
( 
    MAGH INT IDENTITY(1,1),
	MAKH INT,
	PRIMARY KEY(MAGH),
	FOREIGN KEY (MAKH) REFERENCES KHACH(MAKH)
)
CREATE TABLE CT_GIOHANG
(
	MAGH INT,
	MASP INT,
	SOLUONG INT,
	TONGTIEN FLOAT,
	PRIMARY KEY(MAGH,MASP),
    FOREIGN KEY (MAGH) REFERENCES GIOHANG(MAGH),
	FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)
CREATE TABLE DONHANG --SẼ HIỆN THỊ CHO KHÁCH HÀNG TRONG MỤC ĐƠN HÀNG VÀ TOÀN BỘ ĐƠN HÀNG CỦA KHÁCH Ở QUẢN TRỊ
(
	MADH INT IDENTITY(1,1),
	MAKH INT,
	NGAYTAO DATE, --HIỂN THỊ NGÀY KHÁCH HÀNG MUA HÀNG
	NGAYGIAO DATE, --RANDOM TRƯỚC 2 3 NGÀY HIỂN THỊ NGÀY GIAO DỰ ĐOÁN
	TINHTRANG NVARCHAR(20) --SẼ HIỂN THỊ ĐANG CHỜ GIAO HAY HOÀN THÀNH
	PRIMARY KEY(MADH),
    FOREIGN KEY (MAKH) REFERENCES KHACH(MAKH),
)
CREATE TABLE CHITIETDONHANG
(
    MADH INT,
    MASP INT,
    SOLUONG INT,
    THANHTIEN FLOAT,
    PRIMARY KEY (MADH),
    FOREIGN KEY (MADH) REFERENCES DONHANG(MADH) ON DELETE CASCADE,
    FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)

----------------------KHÁCH HÀNG------------------------------------
--INSERT INTO KHACH(HOTEN, GIOITINH, DIACHI, NGAYSINH, GHICHU, SODT, EMAIL,PASSWORD_USER,ISADMIN)
--VALUES
--(N'Nguyễn Thị Thu Hà', N'NỮ', N'Quận 12 TPHCM', '16-12-2003', NULL, '0988558043', 'thuhanguyeen@gmail.com','123abc',null),
--(N'Trần Văn An', N'Nam', N'Quận 10 TPHCM', '20-05-1995', NULL, '0909123456', 'vanan@gmail.com','123abc',null),
--(N'Lê Thị Bình', N'Nữ', N'Quận 5 TPHCM', '02-08-1988', NULL, '0938223344', 'binhle@gmail.com','123abc',null),
--(N'Nguyễn Văn Nam', N'Nam', N'Quận 2 TPHCM', '10-03-2000', NULL, '0978123456', 'namnguyen@gmail.com','123abc',null),
--(N'Trần Thị Mai', N'Nữ', N'Quận 7 TPHCM', '05-11-1992', NULL, '0912347211','maitran@gmail.com','123abc',null),
--(N'Lê Văn Quang', N'Nam', N'Quận Bình Thạnh TPHCM', '18-07-1987', NULL, '0978234567', 'quangle@gmail.com','123abc',null),
--(N'Trần Thị Lan', N'Nữ', N'Quận Gò Vấp TPHCM', '25-11-1990', NULL, '0909988776', 'lantran@gmail.com','123abc',null),
--(N'Phạm Văn Đức', N'Nam', N'Quận Tân Bình TPHCM', '10-06-2002', NULL, '0988777666', 'ducpham@gmail.com','123abc',null),
--(N'Hoàng Thị Hồng', N'Nữ', N'Quận Thủ Đức TPHCM', '12-09-1994', NULL, '0938123456', 'honghoang@gmail.com','123abc',null),
--(N'Lê Bá Duy', N'Nam', N'Quận Thủ Đức TPHCM', '12-09-1994', NULL, '0928908761', 'duyle@gmail.com','123abc',1)
-------------------------LOẠI SẢN PHẨM---------------------------
INSERT INTO LOAISP (TENLOAI)
VALUES
(N'CPU - Bộ vi xử lý'),
(N'RAM - Bộ nhớ Ram'),
(N'Mainboard- Bo mạch chủ'),
(N'VGA- Card màn hình'),
(N'PSU - Nguồn máy tính')

----------------------HÃNG SẢN XUẤT------------------

INSERT INTO HANGSX
VALUES
(N'INTEL', N'United States'),
(N'AMD', N'United States'),
(N'NVIDIA', N'United States'),
(N'GIGABYTE', N'United States'),
(N'MSI',N'United States')

----------------------SẢN PHẨM----------------------------

--------------------------INTEL-----

INSERT INTO dbo.SANPHAM ( TENSP, BAOHANH,IMGSP1,IMGSP2, IMGSP3,MIEUTA,GIA,SOLUONGSP,MANSX,MALOAI)
VALUES
(N'CPU Intel Core I3 10105F', 3,'https://product.hstatic.net/200000420363/product/1_f4d575565fe24bc4b07d4e47646eabbf_master.jpg', NULL,NULL,N'Intel Core I3-10105F là bộ xử lý dành cho máy tính để bàn với 4 nhân 8 luồng, ra mắt vào tháng 3 năm 2021. Nó là một phần của dòng Core i3, sử dụng kiến ​​trúc Comet Lake với Socket 1200. Nhờ Intel Hyper-Threading, số lượng lõi được tăng gấp đôi một cách hiệu quả.', 1000.690, 100, 1, 1),
(N'CPU Intel Core I3 12100F', 3,'https://product.hstatic.net/200000420363/product/1_c07a1f4049214638935a5168913867f8_master.jpg',null,null, N'CPU Intel Core i3-12100F là CPU thế hệ thứ 12 của Intel (Alder Lake) trên nền Socket LGA 1700 với kiến trúc hoàn toàn mới cho hiệu năng vượt trội.', 2200.290, 100, 1, 1),
(N'CPU Intel Pentium G6400', 2,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__df2db9d328e249899835f9d5f5762bce_master.jpg',NULL,NULL, N'CPU intel Pentium Gold G6400 với 2 nhân 4 luồng thuộc dòng Comet Lake và được sản xuất trên tiến trình xử lý 14nm của hãng. CPU Intel Pentium Gold G6400 ra mắt trong quý 2/2020, có sẵn GPU Onboard Intel UHD Graphics 610.', 1750.000, 100, 1, 1),
(N'CPU Intel Pentium G6405', 2,'https://product.hstatic.net/200000420363/product/1_f198a79307fd40c2b352427c1fbba037_master.jpg',NULL,NULL,N'Intel Pentium Gold G6405 là bộ vi xử lý dành cho máy tính để bàn có 2 lõi, ra mắt vào tháng 3 năm 2021. Nó là một phần của dòng sản phẩm Pentium Gold, sử dụng kiến ​​trúc Comet Lake với Socket 1200.', 17900.00, 100, 1, 1),
(N'CPU Intel Core I5 10400F',2, 'https://product.hstatic.net/200000420363/product/cpu-intel-core-i5-10400f_8f7abf96f397415a90d776f93107b6ec_master.jpg',NULL,NULL, N'CPU intel Core I5-10400 với 6 nhân 12 luồng thuộc dòng Comet Lake và được sản xuất trên tiến trình xử lý 14nm của hãng. CPU Intel Core i5-10400F ra mắt trong quý 2/2020, không hỗ trợ GPU Onboard, do bị vô hiệu trên những CPU dòng F', 24900.00, 100, 1, 1),
(N'CPU Intel Core I3 10100F', 1,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__a85655f9565843ed9bdfbc93c4b52fc7_master.jpg', NULL,NULL,N'CPU intel Core I3 10100F với 4 nhân 8 luồng thuộc dòng Comet Lake và được sản xuất trên tiến trình xử lý 14nm của hãng. CPU Intel Core i3 10100F ra mắt trong quý 4/2020, không tích hợp GPU Onboard Intel UHD Graphics. CPU Intel Core i3 10100F hướng đến phục vụ các đối tượng sử dụng cho việc giải trí, Game thủ và công việc liên quan đến đồ họa.', 15500.00, 100, 1, 1),
(N'CPU Intel Core I5 12400F',2, 'https://product.hstatic.net/200000420363/product/1_3771f4a6092f486d86d838a8f7049b78_master.jpg',NULL,NULL, N'CPU Intel Core I5 12400F là bộ xử lý dành cho máy tính để bàn với 6 nhân 12 luồng, ra mắt vào tháng 1 năm 2021. Nó là một phần của dòng Core i3, sử dụng kiến ​​trúc Comet Lake với Socket 1700. Nhờ Intel Hyper-Threading, số lượng lõi được tăng gấp đôi một cách hiệu quả.', 36900.00, 100, 1, 1),
(N'CPU Intel Core I5 12400', 2,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__4c5353914dfb4f9faa641f238a0737ac_master.jpg',NULL,NULL, N'CPU Intel Core I5 12400 là bộ xử lý dành cho máy tính để bàn với 6 nhân 12 luồng, ra mắt vào tháng 9 năm 2021. Nó là một phần của dòng Core i5, sử dụng kiến ​​trúc Comet Lake với FCLGA1700. Nhờ Intel Hyper-Threading, số lượng lõi được tăng gấp đôi một cách hiệu quả.', 43900.00, 100, 1, 1),
(N'CPU Intel Core I7 10700K',2, 'https://product.hstatic.net/200000420363/product/i7.10700k.b.ch_10e0dd2093174ba5ab72154eeeea8334_master.jpg', NULL,NULL,N'CPU Intel Core I7 10700K là bộ xử lý dành cho máy tính để bàn với 8 nhân 16 luồng, ra mắt vào tháng 3 năm 2021. Nó là một phần của dòng Core i7, sử dụng kiến ​​trúc bộ xử lý I7 10700K – Comet Lake, số lượng lõi được tăng gấp đôi một cách hiệu quả.', 89900.00, 100, 1, 1),
(N'CPU Intel Core I9 10900F', 2,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__3555f27567c6451a8dc3e306999721f2_master.jpg',NULL,NULL, N'Intel Core I9 10900F là bộ xử lý dành cho máy tính để bàn với 10 nhân 20 luồng, ra mắt vào tháng 8 năm 2022. Nó là một phần của dòng Core I9, sử dụng kiến ​​trúc Comet Lake với Socket FCLGA1200. Nhờ Intel Hyper-Threading, số lượng lõi được tăng gấp đôi một cách hiệu quả.', 99900.00, 100, 1, 1),
(N'CPU Intel Core I7 13700K', 2,'https://product.hstatic.net/200000420363/product/i7.13700k.b.ct_d8ab66ef3add420eabe7c74cc0aa958e_master.jpg',NULL,NULL,N'Intel Core i7-13700K là bộ xử lý dành cho máy tính để bàn với 16 nhân 24 luồng, ra mắt vào tháng 12 năm 2022. Nó là một phần của dòng Core I7, sử dụng kiến ​​trúc I7-13700K . Nhờ Intel Hyper-Threading, số lượng lõi được tăng gấp đôi một cách hiệu quả.', 95500.00, 100, 1, 1),
(N'CPU Intel Celeron G5900',2, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__b87b3878d493406d9540000811970c59_master.jpg',NULL,NULL, N'CPU intel Celeron G5900 với 2 nhân 2 luồng thuộc dòng Comet Lake và được sản xuất trên tiến trình xử lý 14nm của hãng. CPU Intel Celeron G5900 ra mắt trong quý 2/2020, có sẵn GPU Onboard Intel UHD Graphics 610. CPU Intel Celeron G5900 hướng đến phục vụ các đối tượng sử dụng cho việc giải trí, Game thủ và công việc liên quan đến văn phòng', 10500.00, 100, 1, 1),
(N'CPU Intel Core I5 13600KF',2, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__10539a98641b47a0a9356a26237a9597_master.jpg',NULL,NULL, N'CPU Intel Core i5-13600KF là CPU thế hệ thứ 13 của Intel (Raptor Lake) trên nền Socket LGA 1700 với kiến trúc hoàn toàn mới mang lại hiệu năng vượt trội hơn so với thế hệ tiền nhiệm.', 66900.00, 100, 1, 1),
(N'CPU Intel Core I7 12700KF',2, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__59646419c2c840c9850f025f8a13b7b5_master.jpg',NULL,NULL, N'CPU Intel Core i7-12700KF là CPU thế hệ thứ 12 của Intel (Alder Lake) trên nền Socket LGA 1700 với kiến trúc hoàn toàn mới cho hiệu năng vượt trội so với người tiền nhiệm.', 64900.00, 100, 1, 1),
(N'CPU Intel Pentium Gold G7400',3, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__d97fdac959884f6fb2046fb4d2c27e69_master.jpg',NULL,NULL, N'CPU Intel Pentium Gold G7400 (3.7GHz, 6MB Cache, 2 Cores 4 Threads) Box Chính Hãng (BX80715G7400SRL66)', 23200.00, 100, 1, 1);
--------------------------AMD-------
INSERT INTO dbo.SANPHAM ( TENSP, BAOHANH,IMGSP1,IMGSP2, IMGSP3,MIEUTA,GIA,SOLUONGSP,MANSX,MALOAI)
VALUES
(N'CPU AMD A8 7680',2, 'https://product.hstatic.net/200000420363/product/a.a8.7680.b.ch_c7a7b7491f394bbaa65ac84f52ccd229_master_c24a19f9b4e1409dbab14bc915bd3ba8_master.jpg',NULL,NULL, N'AMD A8-7680 là bộ vi xử lý dành cho máy tính để bàn với 4 nhân 4 luồng, ra mắt vào tháng 10 năm 2018. Nó là một phần của dòng sản phẩm A8, sử dụng kiến ​​trúc Godaveri với Socket FM2 + .A8-7680', 490.99, 100, 2, 1),
(N'CPU AMD Ryzen 5 4600G',3, 'https://product.hstatic.net/200000420363/product/cpu-amd-ryzen-5-4600g_1ab5edb12f664daf80d368753dad7436_master.jpg', NULL,NULL,N'Ryzen 5 4600G là một bước đột phá của AMD ở hạng mục CPU tầm trung. Với kết cấu 6 nhân 12 luồng và sở hữu cấu trúc Zen 2, Ryzen 5 4600G hoạt động tốt với hiệu năng vượt trội.', 2600.000, 100, 2, 1),
(N'CPU AMD Ryzen 5 3500', 2,'https://product.hstatic.net/200000420363/product/_new_-anh-sp-web_fec6907deb084db0a059622759074fb2_master.jpg',NULL,NULL, N'CPU AMD RYZEN 5 3500 là bộ vi xử lý thế hệ mới dành cho các game thủ và người dùng cần giải quyết khối lượng lớn công việc. Sản phẩm mang lại khả năng xử lý mạnh mẽ và tốc độ truy xuất dữ liệu cực nhanh do được trang bị 6 nhân 6 luồng với tốc độ xử lý khi ép xung đạt 4.1GHz.', 1840.00, 100, 2, 1),
(N'CPU AMD Athlon 3000G',2, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__a38ce6b9eb7b472ea27b828fb616ed95_master.jpg',NULL,NULL, N'AMD Athlon 3000G là sản phẩm  giá rẻ dành cho nhóm người dùng phổ thông có nhu cầu sử dụng không quá lớn như những người làm văn phòng hoặc giải trí đơn giản.', 11500.00, 100, 2, 1),
(N'CPU AMD Ryzen 5 5600', 2,'https://product.hstatic.net/200000420363/product/cpu-amd-ryzen-5-5600_8c3d8026aa3242d5ba9ff3683acafac7_master.jpg',NULL,NULL, N'AMD Ryzen 5 5600 sở hữu cấu trúc Zen 3 mạnh mẽ, có nhiều cải tiến mới giúp hiệu năng hoạt động mạnh hơn thế hệ tiền nhiệm. Cấu trúc Zen 3 của AMD đã thay đổi với nhiều cấu trúc đa dạng ', 357500.00, 100, 2, 1),
(N'CPU AMD Ryzen 7 5700X',3, 'https://product.hstatic.net/200000420363/product/cpu-amd-ryzen-7-5800x_1e0375f72b584a47881b782ce3c95a73_master.jpg',NULL,NULL, N'AMD Ryzen 7 5700X được coi là cải tiến của thế hệ chip xử lý CPU hiện đại. Thiết kế Zen 3 độc đáo, mạnh mẽ, công nghệ AMD VR Ready Processors tân tiến  cùng hiệu suất hoạt động tối đa nhờ AMD StoreMI. Tất cả tạo nên con chip CPU Ryzen 7 5700X với sức mạnh chiến binh sẵn sàng đồng hành cùng bạn trong mọi tựa Game gay cấn ', 48500.00, 100, 2, 1),
(N'CPU AMD Ryzen 5 7600', 3,'https://product.hstatic.net/200000420363/product/cpu-amd-ryzen-5-7600-2__1__2bff056824094cf6833924c9fc5cf0fc_master.jpg',NULL,NULL, N'Được ra mắt với nhiều cải tiến, thế hệ Ryzen 7000 Series mang lại những nguồn sức mạnh mới dành những bộ PC AMD. Với phân khúc cận cao cấp, đội đỏ mang đến cho người dùng chúng ta model AMD Ryzen 5 7600 / 3.8GHz Boost 5.1GHz / 6 nhân 12 luồng / 38MB / AM5 ', 50900.00, 100, 2, 1),
(N'CPU AMD RYZEN 3 3200G',3, 'https://product.hstatic.net/200000420363/product/1_b02cdb65d4ca421a953de0085ab3c19b_master.jpg',NULL,NULL, N'CPU Ryzen 3 3200G Dòng vi xử lý đầu tiên RYZEN có đồ họa tích hợp. Tích hợp Card Đồ Họa Radeon Vega 8 với xung nhịp tối đa 1100Mhz hiệu năng gần ngang ngửa 1 chiếc VGA GT1030. Điểm nhấn chính là nhân đồ họa tích hợp AMD Radeon VEGA. ', 22300.00, 100, 2, 1),
(N'CPU AMD Ryzen 7 5800X',2, 'https://product.hstatic.net/200000420363/product/amd_ryzen7-600x600_16bfc33c6c924a80a6e035f80dfc5324_master.png',NULL,NULL, N'AMD Ryzen 7 5800X được sản xuất trên nền tảng socket AM4 sử dụng kiến trúc zen 3 mới nhất và dựa trên dây chuyền sản xuất 7nm. Sở hữu thông số ấn tượng, 8 nhân, 16 luồng xử lý, bộ nhớ đệm lên tới 32MB, và đây sẽ là đối thủ của Intel Core i7-10700K nhưng với mức giá tốt hơn nhiều. ', 62500.00, 100, 2, 1),
(N'CPU AMD Ryzen 9 5950X',1, 'https://product.hstatic.net/200000420363/product/r9-gen-5_d97e850df6b443efb8209ee007a4f243_master.jpg',NULL,NULL, N'CPU AMD RYZEN 9 5950X dẫn đầu hiệu suất đa nhiệm và khả năng chơi game đồ họa cao do được sản xuất trên nền tảng AM4, 16 nhân 32 luồng với mức xung nhịp tối đa 4.9Ghz.', 135000.00, 100, 2, 1);

--------------------------MAINBOARD--------
INSERT INTO dbo.SANPHAM ( TENSP, BAOHANH,IMGSP1,IMGSP2, IMGSP3,MIEUTA,GIA,SOLUONGSP,MANSX,MALOAI)
VALUES
(N'Mainboard Gigabyte H510M H V2',3,'https://product.hstatic.net/200000420363/product/1000__1__d2b44dd84bca4539b882f5e84e313515_master.png',NULL,NULL,N'Mainboard Gigabyte H510M H V2 | Intel H470, Socket 1200, Micro ATX, 2 khe DDR4 Được ra mắt với nhiều cải tiến',1690.00,100,4,3),
(N'Mainboard Gigabyte B560M DS3H V3 (rev. 1.0)',3,'https://product.hstatic.net/200000420363/product/41_210b16f1c7034127ab86d3f209b1caa6_master.jpg',NULL,NULL,N'Mainboard Gigabyte B560M DS3H V3 (rev. 1.0) là sản phẩm  giá rẻ dành cho nhóm người dùng phổ thông có nhu cầu sử dụng không quá lớn như những người làm văn phòng hoặc giải trí đơn giản',2998.00,100,4,3),
(N'Mainboard MSI PRO B760M-E DDR4',1,'https://product.hstatic.net/200000420363/product/1000__1__d2b44dd84bca4539b882f5e84e313515_master.png',NULL,NULL,N'Mainboard Gigabyte H510M H V2 | Intel H470, Socket 1200, Micro ATX, 2 khe DDR4 Được ra mắt với nhiều cải tiến',5789.00,100,4,3),
(N'Mainboard Gigabyte B560M DS3H V3 (rev. 1.0)',3,'https://product.hstatic.net/200000420363/product/pro-b760m-e-ddr4-4_dd45c96a308148408f3631cd4a4442c2_master.jpg',NULL,NULL,N'Mainboard Gigabyte B560M DS3H V3 (rev. 1.0) là sản phẩm  giá rẻ dành cho nhóm người dùng phổ thông có nhu cầu sử dụng không quá lớn như những người làm văn phòng hoặc giải trí đơn giản',89021.00,100,4,3),
(N'Mainboard Gigabyte B760M Aorus Elite DDR5',1,'https://product.hstatic.net/200000420363/product/m.gg.b760m.ds3h.d4_ac8d739f596f4cc68745491365a8f8e5_master.jpg',NULL,NULL,N'Mainboard Gigabyte H510M H V2 | Intel H470, Socket 1200, Micro ATX, 2 khe DDR4 Được ra mắt với nhiều cải tiến',1242.00,100,4,3),
(N'Mainboard Gigabyte B560M DS3H V3 (rev. 1.0)',3,'https://product.hstatic.net/200000420363/product/pro-b760m-e-ddr4-4_dd45c96a308148408f3631cd4a4442c2_master.jpg',NULL,NULL,N'Mainboard Gigabyte B560M DS3H V3 (rev. 1.0) là sản phẩm  giá rẻ dành cho nhóm người dùng phổ thông có nhu cầu sử dụng không quá lớn như những người làm văn phòng hoặc giải trí đơn giản',8906.00,100,4,3)


------------------------VGA-----
INSERT INTO dbo.SANPHAM ( TENSP, BAOHANH,IMGSP1,IMGSP2, IMGSP3,MIEUTA,GIA,SOLUONGSP,MANSX,MALOAI)
VALUES
(N'VGA Asus Radeon RX 6900XT',2, 'https://product.hstatic.net/200000420363/product/1_ec32ea43e90445099e4ddf4a4433e661_master.jpg',NULL,NULL, N'TUF GAMING Radeon™ RX 6900 XT là một con quái vật khủng với lớp vỏ ngoài bằng kim loại cứng cáp, khả năng làm mát siêu hiệu quả và các thành phần linh kiện mang đến độ bền nâng cao. Một lớp vỏ ngoài hoàn toàn bằng kim loại có ba quạt công nghệ hướng trục Axial mạnh mẽ có thêm nhiều cánh và ổ trục quạt bi kép.', 16000.00, 100, 2, 4),
(N'VGA ASRock Radeon RX 6600',2, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__1b8a253c228c446292f11e37d83acbaf_master.jpg', NULL,NULL,N'Hai quạt cung cấp hiệu suất làm mát mạnh mẽ và giúp giàn máy chơi game của bạn luôn mát mẻ. Nó được tối ưu hóa để mang lại trải nghiệm chơi game tuyệt vời với thiết kế thời trang và hợp lý. Được thiết kế để tránh uốn cong PCB. Giao diện lạ mắt làm cho thẻ đồ họa trở nên huyền thoại hơn trong hình ảnh. Nó cũng giúp tăng cường khả năng làm mát với các tấm tản nhiệt cao cấp được trang bị ở mặt sau.', 4900.00, 100, 2, 4),
(N'VGA ASRock Radeon RX 5600', 3,'https://product.hstatic.net/200000420363/product/ar-rx5600-pt-d2-1_ea29799231554816a226570e9b26a829_master.jpg', NULL,NULL,N'TVGA ASRock Radeon RX 5600 6GB GDDR6 PGD2 (No Box).', 49900.00, 100, 2, 4),
(N'VGA ASRock Radeon RX 6600',2, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__1b8a253c228c446292f11e37d83acbaf_master.jpg',NULL,NULL, N'VGA Radeon RX6600 ASRock Challenger D (RX6600 CLD 8G). Thiết kế quạt kép. Hai quạt cung cấp hiệu suất làm mát mạnh mẽ và giúp giàn máy chơi game của bạn luôn mát mẻ. Nó được tối ưu hóa để mang lại trải nghiệm chơi game tuyệt vời với thiết kế thời trang và hợp lý.', 45900.00, 100, 2, 4),
(N'VGA Asus Phoenix RX550',2, 'https://product.hstatic.net/200000420363/product/v.rx550.4g.as.pn.evo.1f_cea8625266b94c4f9d185cded532adfe_master.jpg', NULL,NULL,N'VGA ASUS Phoenix Radeon RX 550 4GB GDDR5 (PH-RX550-4G-EVO). Bạn muốn xây dựng cho mình một dàn máy tính chơi game nhỏ gọn, thì card đồ họa Asus Phoenix Radeon RX 550 4GB GDDR5 (PH-RX550-4G-EVO) chắc chắn là một công cụ không thể thiếu với mức giá tương đối "dễ chịu"', 19700.00, 100, 2, 4),
(N'VGA Radeon RX 6600',2, 'https://product.hstatic.net/200000420363/product/1_828e4915702042ebb644ac689b5488d5_master.jpg', NULL,NULL,N'VGA Radeon RX 6600 8GB GDDR6 Gigabyte EAGLE (GV-R66EAGLE-8GBD)', 50500.00, 100, 2, 4),
(N'VGA Radeon RX 550',2, 'https://product.hstatic.net/200000420363/product/11_466767ffa7d840778826a4921dee11b9_master.jpg',NULL,NULL, N'VGA Radeon RX 550 2GB Extra Low Profile (1500Mhz/ GDDR5/ 64 bit/ ATX bracket)', 14900.00, 100, 2, 4),
(N'VGA MSI RX 5600XT',3, 'https://product.hstatic.net/200000420363/product/2_45b0b4f9d2084a2e86c1361d847564ed_master.jpg', NULL,NULL,N'Trải nghiệm chơi game tuyệt vời được tạo ra bằng cách bẻ cong các quy tắc. Radeon RX 5600 series hoàn toàn mới của RDNA cho hiệu năng cao và chơi game với độ trung thực cao. Trải nghiệm Radeon RX 5600 series mạnh mẽ, tăng tốc game cho bạn', 17000.00, 100, 2, 4),
(N'VGA Radeon RX 580 Esonic ', 3,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__62c6bbd9f80c4ba3aa5166bf023b1313_master.jpg',NULL,NULL, N'VGA Radeon RX 580 Esonic là một con quái vật khủng với lớp vỏ ngoài bằng kim loại cứng cáp, khả năng làm mát siêu hiệu quả và các thành phần linh kiện mang đến độ bền nâng cao. Một lớp vỏ ngoài hoàn toàn bằng kim loại có ba quạt công nghệ hướng trục Axial mạnh mẽ có thêm nhiều cánh và ổ trục quạt bi kép.', 1700.00, 100, 2, 4),
(N'VGA Galax RTX 3050',2, 'https://product.hstatic.net/200000420363/product/3050_ex_box_gx_2ef5f615a93d407b8d1e4e6da7e2706a_master.png', NULL,NULL,N'Không còn giới hạn trong những trò chơi đồ họa ở mức trung bình, chiếc VGA Galax RTX 3050 8G GDDR6 EX (1-Click OC) tân tiến với những tính năng AI hiện đại cùng thiết kế Ampere đảm bảo hiệu năng mạnh mẽ cho mọi hoạt động từ Gaming tới phát sóng trực tuyến đem đến cho bạn trải nghiệm tuyệt vời nhất.', 5900.99, 100, 3, 4),
(N'VGA Colorful RTX 4060',2, 'https://product.hstatic.net/200000420363/product/card-man-hinh-vga-colorful-geforce-rtx-4060-nb-duo-8gb-v-6_f92a674271ac418384f16416d69c1dee_master.jpg', NULL,NULL,N'Card màn hình Colorful GeForce RTX 4060 Ti NB DUO 8GB-V sở hữu hiệu suất ấn tượng nhờ nhân dò tia và nhân Tensor được nâng cao, bộ đa xử lý phát trực tiếp mới và bộ nhớ G6 tốc độ cao. Được thiết kế dành cho những người yêu game và yêu cầu hiệu năng tốt nhất cho bộ PC Gaming, PC Đồ Họa, PC Thiết kế của bạn.', 11600.99, 100, 3, 4),
(N'VGA NVIDIA Quadro T400',3, 'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__f3cf0de8716d4a2d8612bf9f5c8e3f22_master.jpg', NULL,NULL,N'Card Màn Hình NVIDIA Quadro T400 4GB là giải pháp tối ưu cho các máy trạm tiên tiến nhất trên thế giới, nó cung cấp sức mạnh tính toán hình ảnh theo yêu cầu của hàng triệu chuyên gia như một phần của quy trình làm việc hàng ngày của họ. Tất cả các giai đoạn của quy trình làm việc chuyên nghiệp, từ tạo, chỉnh sửa và xem các mô hình và video 2D và 3D, đến làm việc với nhiều ứng dụng trên nhiều màn hình, đều được hưởng lợi từ sức mạnh của GPU cấp chuyên nghiệp.', 39500.00, 100, 3, 4),
(N'VGA NVIDIA QUADRO P2000 5G GDDR5', 2,'https://product.hstatic.net/200000420363/product/quadro-p2000_cb1915e0fa9c41a2be6734bb6bc620ef_master.jpg',NULL,NULL, N'P2000 là thiết kế mới nhất phân khúc này của nVIDIA với 1024 nhân CUDA, 5GB GDDR5, chiếm một khe PCIe duy nhất. P2000 phù hợp cho nhưng người sáng tạo nội dung 3D, CAD, hình ảnh y tế. Card này có sức mạnh tuyệt vời trong việc tạo bóng, họa tiết, pha trộn và lọc để tạo ra độ rõ nét hình ảnh chất lượng cao.', 5900.00, 100, 3, 4),
(N'VGA NVIDIA QUADRO P620 2GB GDDR5',3, 'https://product.hstatic.net/200000420363/product/quadro-p620_fb094d2242124ff287cc9a38704aa1ce_master.jpg',NULL,NULL, N'Card đồ họa chuyên dụng NVIDIA Quadro K620 được hãng giới thiệu vào khoảng nửa đầu năm 2018, là model card đồ họa chuyên dụng mới nhất ở phân khúc phổ thông. NVIDIA Quadro P620 tích hợp GPU Pascal 512 nhân CUDA, bộ nhớ lớn trên bo mạch và các công nghệ hiển thị tiên tiến để mang lại hiệu suất tuyệt vời cho một loạt các quy trình xử lý công việc chuyên nghiệp.', 37900.00, 100, 3, 4),
(N'VGA NVIDIA QUADRO P400 2GB GDDR5', 2,'https://product.hstatic.net/200000420363/product/quadro_p400_dc220de2041c473ba4c2874dee9a7194_master.jpg', NULL,NULL,N'NVIDIA Quadro P400 mới kết hợp kiến ​​trúc GPU Pascal mới nhất với 2GB bộ nhớ trên bo mạch cực nhanh để mang lại hiệu năng tuyệt vời cho một loạt các ứng dụng chuyên nghiệp. Một yếu tố hình thức một khe, cấu hình thấp làm cho nó tương thích với ngay cả khung gầm bị hạn chế về không gian và năng lượng nhất. Hỗ trợ ba màn hình 4K (4096 x 2160 ở 60Hz) với màu HDR mang đến cho bạn không gian làm việc trực quan rộng với độ phân giải cực cao.', 25900.00, 100, 3, 4),
(N'VGA NVIDIA QUADRO P2200 5G GDDR5X',2, 'https://product.hstatic.net/200000420363/product/nvidia-quadro-p2200-new_ef9b85fbd8e840deb77efd41bdaa52f2_master.jpg',NULL,NULL, N'Sau khi kiến trúc Pascal đầu tiên được NVIDIA giới thiệu vào năm 2017, chính xác là sau 2 năm ra mắt Quadro P2000, giờ đây NVIDIA đã ra mắt P2200 thế hệ mới với sự cân bằng hoàn hảo về hiệu năng, tính năng hấp dẫn và yếu tố hình thức nhỏ gọn. So với P2000, P2200 có nhiều lõi CUDA hơn với 1280 nhân CUDA, bộ nhớ trong 5 GB GDDR5X lớn và băng thông bộ nhớ cao hơn lên tới 200 GB/s và khả năng điều khiển tối đa bốn màn hình 5K (5120×2880 @ 60Hz).', 98900.00, 100, 3, 4),
(N'VGA NVIDIA QUADRO P1000 4GB GDDR5', 2,'https://product.hstatic.net/200000420363/product/quadro_p1000_027d95c5a4954d1c9655402ac30539c3_master.jpg',NULL,NULL, N'NVIDIA Quadro P1000 là card đồ họa chuyên nghiệp với kiến trúc Pascal GPU của NVIDIA. So với người anh em tầm trung của nó là Quadro P4000 thì Quadro P1000 thật sự ấn tượng với mức giá của nó. Quadro P1000 là dòng card sử dụng nguồn trực tiếp từ khe PCI nên sẽ phù hợp cho những máy trạm hạn chế về công suất nguồn, không gian và số lượng khe PCI. Mặc dù Quadro P1000 không thể đáp ứng một số nhu cầu đòi hỏi phải sử dụng các dòng card cao cấp như Quadro P4000, P5000 và P6000, tuy nhiên P1000 sẽ có thể giải quyết hầu hết các trường hợp sử dụng.', 72900.00, 100, 3, 4),
(N'VGA INNO3D GTX 1650 4GB',2, 'https://product.hstatic.net/200000420363/product/v.1650.4g.in.twin.oc.x2.2f_f3fbc84cb0aa492c8644442e3bff79f3_master.jpg', NULL,NULL,N'VGA INNO3D GTX1650 4G GDDR6 Twin X2 OC V2', 34500.00, 100, 3, 4),
(N'VGA Zotac Gaming RTX 3060 Twin Edge 12GB DDR6 Ver', 2,'https://product.hstatic.net/200000420363/product/vga-zotac-gaming-rtx-3060-twin-edge-12gb-ddr6-ver-2-zt-a30600e-10m_5351c2e9f1094679a514c9f7fecab408_master.jpg',NULL,NULL,N'Card màn hình VGA Zotac Gaming RTX 3060 Twin Edge 12GB DDR6 Ver 2 (ZT-A30600E-10M)', 68500.00, 100, 3, 4);
------------------------PSU------------
INSERT INTO dbo.SANPHAM ( TENSP, BAOHANH,IMGSP1,IMGSP2, IMGSP3,MIEUTA,GIA,SOLUONGSP,MANSX,MALOAI)
VALUES
(N'Nguồn Asus Tuf Gaming 650W Bronze 80 Plus',4,'https://product.hstatic.net/200000420363/product/1_a1091a5b42354ef9bfc74e260fd0a006_master.jpg',NULL,NULL,N'Nguồn Asus Tuf Gaming 650W Bronze 80 Plus mới ra mắt vào tháng 8 năm 2023 với nhiều đặc điểm',3456.00,100,5,5),
(N'Nguồn Asus ROG Thor 1200P2 Gaming 1200W 80Plus',4,'https://product.hstatic.net/200000420363/product/1_b3d9e24c195044e88c021cf193ae3a90_master.jpg',NULL,NULL,N'Nguồn Asus ROG Thor 1200P2 Gaming 1200W 80Plus Platinum mới ra mắt vào tháng 10 năm 2023 với nhiều đặc điểm',1425.00,100,5,5),
(N'Nguồn Asus ROG Thor 1200P2 Gaming 1200W 80Plus',1,'https://product.hstatic.net/200000420363/product/1_b3d9e24c195044e88c021cf193ae3a90_master.jpg',NULL,NULL,N'Nguồn Asus ROG Thor 1200P2 Gaming 1200W 80Plus Platinum mới ra mắt vào tháng 5 năm 2021 với nhiều đặc điểm',1425.00,100,5,5),
(N'Nguồn Asus ROG Thor 1000 P2 1000W Platinum ',3,'https://product.hstatic.net/200000420363/product/1_b3d9e24c195044e88c021cf193ae3a90_master.jpg',NULL,NULL,N'Nguồn Asus ROG Thor 1000 P2 1000W Platinum | PCIE 5.0 Ready mới ra mắt vào tháng 9 năm 2020 với nhiều đặc điểm',3456.00,100,5,5),
(N'Nguồn ASUS ROG Thor 1600T Gaming | 1600W',10,'https://product.hstatic.net/200000420363/product/1_b3d9e24c195044e88c021cf193ae3a90_master.jpg',NULL,NULL,N'Nguồn ASUS ROG Thor 1600T Gaming | 1600W, 80 Plus Titanium mới ra mắt vào tháng 10 năm 2019 với nhiều đặc điểm',6731.00,100,5,5)

INSERT INTO dbo.SANPHAM ( TENSP, BAOHANH,IMGSP1,IMGSP2, IMGSP3,MIEUTA,GIA,SOLUONGSP,MANSX,MALOAI)
VALUES
(N'Ram PC Kingmax HEATSINK Horizon 16GB DDR5 5600MHz',3,'https://product.hstatic.net/200000420363/product/king-max_4a59361912df45cc8bd0ccd194639492_master.jpg',NULL,NULL,N'Ram PC Kingmax HEATSINK Horizon 16GB DDR5 5600MHz | 1x 16GB, Tản Nhiệt được thiết kế vào năm 2022',2790.00,100,1,2),
(N'Ram PC Corsair Vengeance RGB 32GB DDR5 5200Mhz',6,'https://product.hstatic.net/200000420363/product/1_c96efe8947124eb991533bd0f25e8ae1_master.jpg',NULL,NULL,N'Ram PC Corsair Vengeance RGB 32GB DDR5 5200Mhz (CMH32GX5M2B5200C40W) ( Tản nhiệt, 2x16GB, white)',6790.00,100,1,2),
(N'Ram TeamGroup T-Force Delta RGB White',3,'https://product.hstatic.net/200000420363/product/tai_xuong__94__e6840ef622904216b4c86dccfdd2aa5f_master.png',NULL,NULL,N'Ram TeamGroup T-Force Delta RGB White 64GB | 2 x 32GB, DDR5, 5600MHz màu trắng có Dung lượng: 2 x 16GB',4790.00,100,1,2),
(N'Ram TeamGroup T-Force Delta RGB Black 64GB',10,'https://product.hstatic.net/200000420363/product/tai_xuong__91__6d079e63c1d04ef0be7a0bb625216b4b_master.png',NULL,NULL,N'Ram TeamGroup T-Force Delta RGB Black 64GB | 2 x 32GB, DDR5, 5600MHz màu đen',5630.00,100,1,2),
(N'Ram Laptop Kingmax 8GB',9,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__dde6a386fd724d5698dca0840564c16a_master.jpg',NULL,NULL,N'Ram Laptop Kingmax 8GB 4800Mhz DDR5 (KM-SD5-4800-08GS)',9790.00,100,1,2),
(N'Ram DDR5 32GB/5200Mhz Kingston Fury Beast',7,'https://product.hstatic.net/200000420363/product/_2023_-khung-sp-_1__39dad772e852462490aab97766afbb2d_master.jpg',NULL,NULL,N'Ram DDR5 32GB/5200Mhz Kingston Fury Beast RGB 2x16GB',2790.00,100,1,2)

INSERT INTO KHACH(TENTK,HOTEN,GIOITINH,DIACHI,NGAYSINH,GHICHU,SODT,EMAIL,PASSWORD_USER,ISADMIN) VALUES
('KhoiTran',N'Anh Khôi','Nam',N'140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, TP Hồ Chí Minh','2023-11-26',N'Tài Khoản thử nghiệm cho đồ án lập trình web.','0707349055','hoangkhoi230@gmail.com','khoi123456',1),
('ThuHa',N'Thu Hà','Nam',N'140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, TP Hồ Chí Minh','2023-11-26',N'Tài Khoản thử nghiệm cho đồ án lập trình web.','0707349056','thuhanguyeen16122003@gmail.com','ha123456',0),
('PhuongDien',N'Phương Điền','Nam',N'140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, TP Hồ Chí Minh','2023-11-26',N'Tài Khoản thử nghiệm cho đồ án lập trình web.','0707349057','diennguyenphuong9@gmail.com','dien123456',0)



select * from khach