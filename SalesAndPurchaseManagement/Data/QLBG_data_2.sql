INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES
(N'Bàn'),                   -- Tables
(N'Ghế'),                   -- Chairs
(N'Tủ');				    -- Wardrobes

INSERT INTO [dbo].[Characteristics] (CharacteristicName) VALUES 
(N'Cố định'),
(N'Xoay'),
(N'Gấp'),
(N'Nâng hạ'),
(N'Bánh xe');

INSERT INTO [dbo].[Countries] ([CountryName]) VALUES
(N'Việt Nam'),          -- Vietnam
(N'Trung Quốc'),        -- China
(N'Nhật Bản'),          -- Japan
(N'Hàn Quốc'),          -- South Korea
(N'Đức'),               -- Germany
(N'Pháp'),              -- France
(N'Mỹ'),                -- United States
(N'Ý'),                 -- Italy
(N'Thái Lan'),          -- Thailand
(N'Ấn Độ');             -- India

INSERT INTO [dbo].[Customers] ([CustomerName], [Address], [PhoneNumber]) VALUES
(N'Nguyễn Văn An', N'123 Trần Hưng Đạo, Q.1, TP.HCM', N'0909123456'),
(N'Trần Thị Bích Ngọc', N'456 Lê Lợi, Q.3, TP.HCM', N'0918234567'),
(N'Lê Hoàng Nam', N'789 Nguyễn Đình Chiểu, Q.5, TP.HCM', N'0939456123'),
(N'Phạm Văn Toàn', N'234 Hai Bà Trưng, Q.1, TP.HCM', N'0908567890'),
(N'Ngô Thị Lan', N'567 Điện Biên Phủ, Q.3, TP.HCM', N'0912678912'),
(N'Trần Quốc Bảo', N'890 Võ Văn Kiệt, Q.5, TP.HCM', N'0923789123'),
(N'Nguyễn Thị Hồng', N'123 Hoàng Sa, Q.3, TP.HCM', N'0945897123'),
(N'Vũ Hoàng Sơn', N'456 Trường Chinh, Q.Tân Bình, TP.HCM', N'0956789123'),
(N'Đỗ Minh Hạnh', N'789 Cách Mạng Tháng 8, Q.10, TP.HCM', N'0977890123'),
(N'Phan Tấn Tài', N'123 Lê Văn Sỹ, Q.3, TP.HCM', N'0988901234'),
(N'Hoàng Văn Đông', N'234 Phan Đăng Lưu, Q.Phú Nhuận, TP.HCM', N'0990012345'),
(N'Nguyễn Thị Thanh', N'345 Nguyễn Trãi, Q.5, TP.HCM', N'0901123456'),
(N'Lê Minh Phú', N'456 Lý Thường Kiệt, Q.Tân Bình, TP.HCM', N'0912234567'),
(N'Võ Hồng Đào', N'567 Hoàng Văn Thụ, Q.Phú Nhuận, TP.HCM', N'0923345678'),
(N'Phạm Ngọc Tân', N'678 Cộng Hòa, Q.Tân Bình, TP.HCM', N'0934456789'),
(N'Lê Thị Thu Hằng', N'789 Nguyễn Văn Trỗi, Q.Phú Nhuận, TP.HCM', N'0945567890'),
(N'Trương Văn Kiên', N'890 Lý Tự Trọng, Q.1, TP.HCM', N'0956678901'),
(N'Phan Anh Tuấn', N'234 Pasteur, Q.1, TP.HCM', N'0967789012'),
(N'Nguyễn Thành Công', N'456 Nguyễn Thị Minh Khai, Q.3, TP.HCM', N'0978890123'),
(N'Đinh Văn Khánh', N'678 Võ Thị Sáu, Q.3, TP.HCM', N'0989901234');

INSERT INTO [dbo].[Jobs] ([JobTitle], [Salary]) VALUES
(N'Quản lý', 25000000),
(N'Nhân viên', 12000000);

INSERT INTO [dbo].[Employees] ([EmployeeName], [Gender], [DateOfBirth], [PhoneNumber], [Address], [Image], [JobId], [Password], [IsAdmin], [Email]) VALUES
(N'Trần Minh Đan', 0, '2004-09-04', N'0348580486', N'123 Đường XYZ, Quận 1, TP.HCM', 'user_default.png', 2, N'123456', 1, N'minhdan190904@gmail.com');

INSERT INTO [dbo].[Employees] ([EmployeeName], [Gender], [DateOfBirth], [PhoneNumber], [Address], [Image], [JobId], [Password], [IsAdmin], [Email]) VALUES
(N'Tổng Quản Trị', 1, '1980-01-01', N'0900000000', N'1 Đại lộ Chính, Quận 1, TP.HCM', 'user_default.png', 1, N'123456', 1, N'admin@gmail.com'); 

INSERT INTO [dbo].[Employees] ([EmployeeName], [Gender], [DateOfBirth], [PhoneNumber], [Address], [Image], [JobId], [Password], [IsAdmin], [Email]) VALUES
(N'Admin', 0, '1980-03-15', N'0901234567', N'123 Đường ABC, Quận 1, TP.HCM', 'user_default.png', 1, N'123456', 1, N'hungbgclone01@gmail.com'),

(N'Trần Văn Bảo', 1, '1980-03-15', N'0901234567', N'123 Đường ABC, Quận 1, TP.HCM', 'user_default.png', 1, N'AdminPass123', 1, N'ql1@example.com'),
(N'Nguyễn Thị Thu Hà', 0, '1982-06-21', N'0902345678', N'456 Đường DEF, Quận 2, TP.HCM', 'user_default.png', 1, N'AdminPass456', 1, N'ql2@example.com'),
(N'Lê Văn Thuận', 1, '1975-09-09', N'0903456789', N'789 Đường GHI, Quận 3, TP.HCM', 'user_default.png', 1, N'AdminPass789', 1, N'ql3@example.com'),
(N'Phạm Thị My', 0, '1978-12-12', N'0904567890', N'101 Đường JKL, Quận 4, TP.HCM', 'user_default.png', 1, N'AdminPass101', 1, N'ql4@example.com'),
(N'Hoàng Văn Trung', 1, '1985-04-18', N'0905678901', N'102 Đường MNO, Quận 5, TP.HCM', 'user_default.png', 1, N'AdminPass102', 1, N'ql5@example.com'),

(N'Vũ Thị Đào', 0, '1990-05-25', N'0906789012', N'103 Đường PQR, Quận 6, TP.HCM', 'user_default.png', 2, N'UserPass123', 0, N'nv1@example.com'),
(N'Nguyễn Văn Phú', 1, '1991-08-13', N'0907890123', N'104 Đường STU, Quận 7, TP.HCM', 'user_default.png', 2, N'UserPass456', 0, N'nv2@example.com'),
(N'Phan Thị Viên', 0, '1992-02-23', N'0908901234', N'105 Đường VWX, Quận 8, TP.HCM', 'user_default.png', 2, N'UserPass789', 0, N'nv3@example.com'),
(N'Bùi Văn Quý', 1, '1993-11-30', N'0909012345', N'106 Đường YZA, Quận 9, TP.HCM', 'user_default.png', 2, N'UserPass101', 0, N'nv4@example.com'),
(N'Trịnh Thị Hải', 0, '1994-01-05', N'0910123456', N'107 Đường BCD, Quận 10, TP.HCM', 'user_default.png', 2, N'UserPass102', 0, N'nv5@example.com'),

(N'Đặng Văn Lâm', 1, '1995-07-17', N'0911234567', N'108 Đường EFG, Quận 11, TP.HCM', 'user_default.png', 2, N'UserPass103', 0, N'nv6@example.com'),
(N'Phạm Văn Hoàng', 1, '1996-10-19', N'0912345678', N'109 Đường HIJ, Quận 12, TP.HCM', 'user_default.png', 2, N'UserPass104', 0, N'nv7@example.com'),
(N'Hoàng Thị Nhi', 0, '1997-03-22', N'0913456789', N'110 Đường KLM, Quận 1, TP.HCM', 'user_default.png', 2, N'UserPass105', 0, N'nv8@example.com'),
(N'Lê Thị Chu', 0, '1998-12-28', N'0914567890', N'111 Đường NOP, Quận 2, TP.HCM', 'user_default.png', 2, N'UserPass106', 0, N'nv9@example.com'),
(N'Phạm Văn Hải', 1, '1999-09-15', N'0915678901', N'112 Đường QRS, Quận 3, TP.HCM', 'user_default.png', 2, N'UserPass107', 0, N'nv10@example.com'),

(N'Ngô Văn Quyên', 1, '2000-06-11', N'0916789012', N'113 Đường TUV, Quận 4, TP.HCM', 'user_default.png', 2, N'UserPass108', 0, N'nv11@example.com'),
(N'Trần Thị Triệu', 0, '2001-04-20', N'0917890123', N'114 Đường WXY, Quận 5, TP.HCM', 'user_default.png', 2, N'UserPass109', 0, N'nv12@example.com'),
(N'Vũ Văn Nhân', 1, '2002-08-09', N'0918901234', N'115 Đường ZAB, Quận 6, TP.HCM', 'user_default.png', 2, N'UserPass110', 0, N'nv13@example.com'),
(N'Phạm Thị Như', 0, '2003-11-11', N'0919012345', N'116 Đường CDE, Quận 7, TP.HCM', 'user_default.png', 2, N'UserPass111', 0, N'nv14@example.com'),
(N'Nguyễn Văn Ngọc', 1, '2004-01-25', N'0920123456', N'117 Đường FGH, Quận 8, TP.HCM', 'user_default.png', 2, N'UserPass112', 0, N'nv15@example.com');

INSERT INTO [dbo].[Features] (FeatureName) VALUES 
(N'Phòng Khách'),
(N'Phòng Bếp'),
(N'Công Sở');

INSERT INTO [dbo].[Manufacturers] ([ManufacturerName]) VALUES 
(N'IKEA'),           -- (Sweden)
(N'Ashley Furniture'), -- (United States)
(N'Natuzzi'),        -- (Italy)
(N'Habitat'),        -- (United Kingdom)
(N'Tempur Sealy'),   -- (United States)
(N'Roche Bobois'),   -- (France)
(N'Herman Miller'),  -- (United States)
(N'Hülsta'),         -- (Germany)
(N'TOLICCI'),        -- (Turkey)
(N'LivArt');         -- (South Korea)

INSERT INTO [dbo].[Materials] (MaterialName) VALUES 
(N'Gỗ'),                      -- Wood
(N'Kim loại'),                -- Metal
(N'Nhựa'),                    -- Plastic
(N'Kính'),                    -- Glass
(N'Vải'),
(N'Tre'),
(N'Mây');

INSERT INTO [dbo].[Shapes] ([ShapeName])
VALUES 
(N'Hình chữ nhật'),
(N'Hình tròn'),
(N'Hình vuông'),
(N'Hình tam giác'),
(N'Hình bầu dục'),
(N'Hình lục giác'),
(N'Hình ngũ giác');

INSERT INTO [dbo].[Products] 
    (ProductName, Length, Width, Height, Color, CategoryId, ShapeId, MaterialId, CountryOfOriginId, ManufacturerId, FeatureId, CharacteristicId, Quantity, PurchasePrice, SellingPrice, WarrantyPeriod, Image, Notes)
VALUES 
    (N'Bàn Gỗ Chống Nước', 120, 60, 75, N'Nâu', 1, 1, 1, 1, 1, 1, 1, 10, 500000, 700000, 24, N'CallieDesk.png', N'Bàn gỗ có tính năng chống nước.'),
    (N'Ghế Kim Loại Gấp', 45, 45, 90, N'Xám', 2, 2, 2, 2, 2, 2, 2, 15, 200000, 350000, 12, N'NaiLarOutdoorChair.png', N'Ghế kim loại có thể gấp gọn.'),
    (N'Bàn Nhựa Bền Màu', 100, 50, 70, N'Trắng', 1, 1, 3, 1, 3, 3, 1, 20, 150000, 250000, 18, N'OscarTable.png', N'Bàn nhựa bền màu, dễ lau chùi.'),
    (N'Ghế Gỗ Xoay', 50, 50, 100, N'Đen', 2, 2, 1, 1, 1, 1, 2, 8, 300000, 450000, 24, N'GX001.png', N'Ghế gỗ có tính năng xoay.'),
    (N'Ghế Vải Thoải Mái', 60, 60, 100, N'Xanh đậm', 2, 2, 5, 4, 3, 3, 5, 7, 400000, 600000, 12, N'4.png', N'Ghế vải thoáng mát và thoải mái.'),
    (N'Bàn Gỗ Bền', 150, 70, 80, N'Nâu nhạt', 1, 1, 1, 2, 4, 1, 1, 12, 600000, 850000, 36, N'AfroditeTable.png', N'Bàn gỗ bền và chắc chắn.'),
    (N'Sofa Vải Dài', 180, 90, 80, N'Xám nhạt', 2, 2, 5, 4, 1, 2, 5, 2, 1200000, 1800000, 24, N'5.png', N'Sofa vải rộng rãi và thoải mái.'),
    (N'Bàn Kính Chống Nước', 100, 50, 75, N'Đen', 1, 1, 4, 1, 3, 3, 3, 8, 900000, 1300000, 18, N'AmbieTable.png', N'Bàn kính với bề mặt chống nước.'),
    (N'Ghế Kim Loại Kháng Bụi', 45, 45, 95, N'Đen', 2, 2, 2, 2, 2, 1, 4, 12, 250000, 400000, 12, N'GX002.png', N'Ghế kim loại chống bụi, dễ vệ sinh.'),
    (N'Ghế Nhựa Dẻo', 40, 40, 90, N'Vàng', 2, 2, 3, 3, 4, 2, 5, 25, 100000, 200000, 6, N'3.png', N'Ghế nhựa dẻo, dễ di chuyển.'),
    (N'Tủ Nhựa Chống Ẩm', 80, 40, 150, N'Trắng', 3, 3, 3, 2, 4, 1, 2, 18, 600000, 950000, 18, N'1.png', N'Tủ nhựa nhẹ, chống ẩm mốc.'),
    (N'Bàn Vải Đơn Giản', 60, 40, 50, N'Xám', 1, 1, 5, 4, 3, 3, 1, 20, 250000, 400000, 12, N'BluestoneTable.png', N'Bàn vải nhỏ gọn, phù hợp nhiều không gian.'),
    (N'Ghế Gỗ Đệm', 45, 45, 95, N'Nâu đỏ', 2, 2, 1, 1, 1, 1, 1, 15, 300000, 500000, 12, N'5.png', N'Ghế gỗ có đệm thoải mái.'),
    (N'Tủ Nhựa Đựng Đồ', 50, 40, 130, N'Xám', 3, 3, 3, 4, 2, 2, 4, 8, 500000, 750000, 12, N'1.png', N'Tủ nhựa tiện lợi, phù hợp mọi không gian.'),
    (N'Bàn Gỗ Mini', 80, 50, 40, N'Nâu sáng', 1, 1, 1, 1, 1, 3, 2, 10, 150000, 300000, 6, N'1.png', N'Bàn gỗ kích thước nhỏ gọn.'),
    (N'Ghế Kim Loại Đệm', 45, 45, 90, N'Đen', 2, 2, 2, 1, 3, 1, 4, 12, 220000, 370000, 12, N'AntonTable.png', N'Ghế kim loại có đệm ngồi thoải mái.'),
    (N'Tủ Nhựa Mini', 50, 40, 80, N'Trắng', 3, 4, 3, 2, 5, 3, 2, 30, 250000, 400000, 6, N'1.png', N'Tủ nhựa mini tiện lợi.'),
    (N'Bàn Kim Loại Cỡ Lớn', 140, 60, 75, N'Xám', 1, 1, 2, 1, 1, 3, 3, 7, 400000, 600000, 12, N'MarieDesk.png', N'Bàn kim loại bền bỉ, rộng rãi.'),
    (N'Ghế Gỗ Cao', 50, 50, 100, N'Nâu', 2, 4, 1, 4, 4, 2, 2, 25, 350000, 500000, 12, N'DC02DiningChair.png', N'Ghế gỗ cao, dễ ngồi.'),
    (N'Tủ Gỗ Cao', 60, 40, 170, N'Nâu đậm', 3, 4, 1, 1, 3, 1, 2, 8, 900000, 1300000, 24, N'1.png', N'Tủ gỗ đứng, có sức chứa lớn.'),
    (N'Bàn Vải Nhỏ Gọn', 60, 40, 50, N'Xám đen', 1, 1, 5, 4, 1, 1, 2, 5, 200000, 320000, 6, N'BluestoneTable.png', N'Bàn vải tiện dụng và nhỏ gọn.'),
    (N'Tủ Kim Loại Chống Trầy', 70, 40, 160, N'Xám đen', 3, 3, 2, 1, 2, 3, 4, 9, 800000, 1200000, 24, N'1.png', N'Tủ kim loại chống trầy và bền chắc.'),
    (N'Bàn Kim Loại Đơn Giản', 120, 60, 75, N'Xám', 1, 1, 2, 1, 2, 3, 5, 10, 500000, 750000, 18, N'MarieDesk.png', N'Bàn kim loại đơn giản, phù hợp mọi không gian.'),
    (N'Ghế Vải Bền', 50, 50, 90, N'Xanh', 2, 2, 5, 1, 5, 2, 3, 12, 300000, 450000, 12, N'NordicArmChair.png', N'Ghế vải bền và dễ di chuyển.'),
    (N'Bàn Gỗ Chống Nước', 120, 60, 75, N'Nâu', 1, 1, 1, 1, 1, 1, 1, 10, 500000, 700000, 24, N'TriceTable.png', N'Bàn gỗ có tính năng chống nước.'),
    (N'Ghế Kim Loại Gấp', 45, 45, 90, N'Xám', 2, 2, 2, 2, 2, 2, 2, 15, 200000, 350000, 12, N'DC23DiningChair.png', N'Ghế kim loại có thể gấp gọn.'),
    (N'Bàn Nhựa Bền Màu', 100, 50, 70, N'Trắng', 1, 1, 3, 1, 3, 3, 1, 20, 150000, 250000, 18, N'TobiTable.png', N'Bàn nhựa bền màu, dễ lau chùi.'),
    (N'Bàn Kính Chống Trầy', 120, 60, 75, N'Trong suốt', 1, 3, 4, 2, 1, 1, 3, 5, 1000000, 1500000, 12, N'AluminumTable.png', N'Bàn kính chống trầy, sang trọng.'),
    (N'Tủ Kim Loại Chống Bụi', 80, 40, 150, N'Xanh', 3, 4, 2, 3, 2, 1, 4, 6, 500000, 800000, 18, N'1.png', N'Tủ kim loại chống bụi, bảo vệ đồ đạc.'),
    (N'Ghế Vải Thoải Mái', 60, 60, 100, N'Xanh đậm', 2, 2, 5, 4, 3, 3, 5, 7, 400000, 600000, 12, N'2.png', N'Ghế vải thoáng mát và thoải mái.'),
    (N'Bàn Gỗ Bền', 150, 70, 80, N'Nâu nhạt', 1, 1, 1, 2, 4, 1, 4, 12, 600000, 850000, 36, N'AmbieTable.png', N'Bàn gỗ bền và chắc chắn.'),
    (N'Tủ Kính Cường Lực', 60, 40, 180, N'Trong suốt', 3, 3, 4, 2, 1, 2, 5, 3, 800000, 1200000, 24, N'1.png', N'Tủ kính bền và chịu lực.'),
    (N'Sofa Vải Dài', 180, 90, 80, N'Xám nhạt', 2, 2, 5, 4, 1, 2, 5, 2, 1200000, 1800000, 24, N'PellowSofa.png', N'Sofa vải rộng rãi và thoải mái.'),
    (N'Bàn Kính Chống Nước', 100, 50, 75, N'Đen', 1, 1, 4, 1, 3, 3, 3, 8, 900000, 1300000, 18, N'AluminumTable.png', N'Bàn kính với bề mặt chống nước.'),
    (N'Ghế Kim Loại Kháng Bụi', 45, 45, 95, N'Đen', 2, 2, 2, 2, 2, 1, 4, 12, 250000, 400000, 12, N'7.png', N'Ghế kim loại chống bụi, dễ vệ sinh.'),
    (N'Tủ Gỗ Cao', 80, 40, 200, N'Nâu', 3, 4, 1, 2, 3, 1, 1, 10, 1200000, 1800000, 24, N'1.png', N'Tủ gỗ cao, có sức chứa lớn.'),
    (N'Ghế Nhựa Dẻo', 40, 40, 90, N'Vàng', 2, 2, 3, 3, 4, 2, 2, 25, 100000, 200000, 6, N'DC01DiningChair.png', 'Ghế nhựa dẻo, dễ di chuyển.'),
    (N'Tủ Gỗ Cao', 80, 40, 200, N'Nâu', 3, 4, 1, 2, 3, 1, 4, 10, 1200000, 1800000, 24, N'1.png', N'Tủ gỗ cao, có sức chứa lớn.'),
    (N'Tủ Nhựa Chống Ẩm', 80, 40, 150, N'Trắng', 3, 3, 3, 2, 4, 2, 2, 18, 600000, 950000, 18, N'1.png', N'Tủ nhựa nhẹ, chống ẩm mốc.'),
    (N'Ghế Gỗ Đệm', 45, 45, 95, N'Nâu đỏ', 2, 2, 1, 1, 1, 1, 1, 15, 300000, 500000, 12, N'5.png', N'Ghế gỗ có đệm thoải mái.'),
    (N'Tủ Nhựa Đựng Đồ', 50, 40, 130, N'Xám', 3, 3, 3, 4, 2, 1, 4, 8, 500000, 750000, 12, N'1.png', N'Tủ nhựa tiện lợi, phù hợp mọi không gian.'),
    (N'Bàn Gỗ Mini', 80, 50, 40, N'Nâu sáng', 1, 1, 1, 1, 1, 1, 2, 10, 150000, 300000, 6, N'AntonTable.png', N'Bàn gỗ kích thước nhỏ gọn.'),
    (N'Ghế Kim Loại Đệm', 45, 45, 90, N'Đen', 2, 2, 2, 1, 3, 2, 4, 12, 220000, 370000, 12, N'GX003.png', N'Ghế kim loại có đệm ngồi thoải mái.'),
    (N'Tủ Nhựa Mini', 50, 40, 80, N'Trắng', 3, 4, 3, 2, 5, 2, 2, 30, 250000, 400000, 6, N'1.png', N'Tủ nhựa mini tiện lợi.'),
    (N'Ghế Gỗ Cao', 50, 50, 100, N'Nâu', 2, 4, 1, 4, 4, 1, 2, 25, 350000, 500000, 12, N'8.png', N'Ghế gỗ cao, dễ ngồi.'),
    (N'Tủ Gỗ Cao', 60, 40, 170, N'Nâu đậm', 3, 4, 1, 1, 3, 2, 2, 8, 900000, 1300000, 24, N'1.png', N'Tủ gỗ đứng, có sức chứa lớn.'),
    (N'Bàn Vải Nhỏ Gọn', 60, 40, 50, N'Xám đen', 1, 1, 5, 4, 1, 1, 4, 5, 200000, 320000, 6, N'UlticTable.png', N'Bàn vải tiện dụng và nhỏ gọn.'),
    (N'Tủ Kim Loại Chống Trầy', 70, 40, 160, N'Xám đen', 3, 3, 2, 1, 2, 1, 4, 9, 800000, 1200000, 24, N'1.png', N'Tủ kim loại chống trầy và bền chắc.'),
    (N'Bàn Kim Loại Đơn Giản', 120, 60, 75, N'Xám', 1, 1, 2, 1, 2, 2, 5, 10, 500000, 750000, 18, N'ZiggiTable.png', N'Bàn kim loại đơn giản, phù hợp mọi không gian.'),
    (N'Tủ Kính Cường Lực', 60, 40, 180, N'Trong suốt', 3, 3, 4, 2, 1, 3, 2, 3, 800000, 1200000, 24, N'1.png', N'Tủ kính bền và chịu lực.'),
    (N'Bàn Kính Chống Trầy', 120, 60, 75, N'Trong suốt', 1, 3, 4, 2, 1, 1, 3, 5, 1000000, 1500000, 12, N'AmbieTable.png', N'Bàn kính chống trầy, sang trọng.'),
    (N'Tủ Kim Loại Chống Bụi', 80, 40, 150, N'Xanh', 3, 4, 2, 3, 2, 2, 4, 6, 500000, 800000, 18, N'1.png', N'Tủ kim loại chống bụi, bảo vệ đồ đạc.'),
    (N'Ghế Vải Bền', 50, 50, 90, N'Xanh', 2, 2, 5, 1, 5, 2, 2, 12, 300000, 450000, 12, N'2.png', N'Ghế vải bền và dễ di chuyển.');


INSERT INTO [dbo].[Suppliers] ([SupplierName], [Address], [PhoneNumber])
VALUES 
(N'Nhà cung cấp A', N'123 Đường Lê Lợi, Quận 1, TP. Hồ Chí Minh', N'0901234567'),
(N'Nhà cung cấp B', N'45 Phố Huế, Quận Hai Bà Trưng, Hà Nội', N'0912345678'),
(N'Nhà cung cấp C', N'678 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh', N'0923456789'),
(N'Nhà cung cấp D', N'12 Trần Hưng Đạo, Quận 1, TP. Hồ Chí Minh', N'0934567890'),
(N'Nhà cung cấp E', N'90 Nguyễn Trãi, Quận Thanh Xuân, Hà Nội', N'0945678901'),
(N'Nhà cung cấp F', N'15 Nguyễn Thị Minh Khai, Quận 1, TP. Hồ Chí Minh', N'0956789012'),
(N'Nhà cung cấp G', N'78 Đinh Tiên Hoàng, Quận Bình Thạnh, TP. Hồ Chí Minh', N'0967890123'),
(N'Nhà cung cấp H', N'56 Trường Chinh, Quận Tân Bình, TP. Hồ Chí Minh', N'0978901234'),
(N'Nhà cung cấp I', N'102 Phạm Ngũ Lão, Quận 1, TP. Hồ Chí Minh', N'0989012345'),
(N'Nhà cung cấp J', N'67 Võ Văn Tần, Quận 3, TP. Hồ Chí Minh', N'0990123456');

INSERT INTO [dbo].[SalesInvoices] ([EmployeeId], [InvoiceDate], [CustomerId], [TotalAmount])
VALUES
(1, '2023-01-05 10:23:45', 1, 500000),
(2, '2023-01-06 11:15:20', 2, 1500000),
(3, '2023-01-07 14:45:35', 3, 750000),
(4, '2023-01-08 09:30:10', 4, 1200000),
(5, '2023-01-09 16:05:55', 5, 850000),
(6, '2023-01-10 08:25:50', 6, 900000),
(1, '2023-01-11 13:12:40', 7, 650000),
(2, '2023-01-12 15:50:15', 8, 1350000),
(3, '2023-01-13 10:05:25', 9, 1450000),
(4, '2023-01-14 17:45:30', 10, 1100000),
(5, '2023-01-15 11:23:40', 11, 920000),
(6, '2023-01-16 12:30:50', 12, 1300000),
(1, '2023-01-17 08:40:15', 13, 970000),
(2, '2023-01-18 09:55:25', 14, 1020000),
(3, '2023-01-19 13:05:35', 15, 1150000),
(4, '2023-01-20 14:35:45', 16, 990000),
(5, '2023-01-21 10:20:50', 17, 1450000),
(6, '2023-01-22 15:50:55', 18, 875000),
(1, '2023-01-23 16:05:05', 19, 780000),
(2, '2023-01-24 09:10:15', 20, 940000),
(3, '2023-01-25 11:20:25', 20, 1280000),
(4, '2023-01-26 17:35:35', 19, 1200000),
(5, '2023-01-27 13:45:50', 18, 1000000),
(6, '2023-01-28 16:50:10', 17, 1100000),
(1, '2023-01-29 08:05:25', 16, 820000),
(2, '2023-01-30 10:15:35', 15, 1400000),
(3, '2023-01-31 15:25:45', 14, 950000),
(4, '2023-02-01 09:30:50', 13, 1300000),
(5, '2023-02-02 14:05:15', 12, 1550000),
(6, '2023-02-03 11:12:25', 11, 890000),
(1, '2023-02-04 16:35:40', 10, 1230000),
(2, '2023-02-05 13:05:20', 9, 700000),
(3, '2023-02-06 10:45:15', 8, 1200000),
(4, '2023-02-07 12:50:55', 7, 830000),
(5, '2023-02-08 15:40:05', 6, 990000),
(6, '2023-02-09 17:55:30', 5, 1190000),
(1, '2023-02-10 09:25:10', 4, 680000),
(2, '2023-02-11 08:40:20', 3, 1400000),
(3, '2023-02-12 11:50:35', 2, 1040000),
(4, '2023-02-13 16:15:45', 1, 1350000);

INSERT INTO [dbo].[SalesInvoiceDetails] ([SalesInvoiceId], [ProductId], [Quantity], [UnitPrice], [Discount])
VALUES
-- InvoiceId 1 - Tổng tiền: 500000
(1, 1, 2, 150000, 0),
(1, 2, 1, 200000, 0),

-- InvoiceId 2 - Tổng tiền: 1500000
(2, 3, 3, 400000, 0),
(2, 4, 1, 300000, 0),
(2, 5, 1, 500000, 0),

-- InvoiceId 3 - Tổng tiền: 750000
(3, 6, 2, 250000, 0),
(3, 7, 1, 250000, 0),

-- InvoiceId 4 - Tổng tiền: 1200000
(4, 8, 3, 300000, 0),
(4, 9, 2, 150000, 0),

-- InvoiceId 5 - Tổng tiền: 850000
(5, 10, 1, 300000, 0),
(5, 11, 1, 400000, 0),
(5, 12, 1, 150000, 0),

-- InvoiceId 6 - Tổng tiền: 900000
(6, 13, 3, 300000, 0),

-- InvoiceId 7 - Tổng tiền: 650000
(7, 14, 2, 250000, 0),
(7, 15, 1, 150000, 0),

-- InvoiceId 8 - Tổng tiền: 1350000
(8, 16, 3, 400000, 0),
(8, 17, 1, 150000, 0),

-- InvoiceId 9 - Tổng tiền: 1450000
(9, 18, 2, 700000, 0),
(9, 19, 1, 50000, 0),

-- InvoiceId 10 - Tổng tiền: 1100000
(10, 20, 4, 275000, 0),

-- InvoiceId 11 - Tổng tiền: 920000
(11, 21, 2, 460000, 0),

-- InvoiceId 12 - Tổng tiền: 1300000
(12, 22, 4, 325000, 0),

-- InvoiceId 13 - Tổng tiền: 970000
(13, 23, 1, 470000, 0),
(13, 24, 2, 250000, 0),

-- InvoiceId 14 - Tổng tiền: 1020000
(14, 25, 3, 340000, 0),

-- InvoiceId 15 - Tổng tiền: 1150000
(15, 26, 5, 230000, 0),

-- InvoiceId 16 - Tổng tiền: 990000
(16, 27, 3, 330000, 0),

-- InvoiceId 17 - Tổng tiền: 1450000
(17, 28, 4, 362500, 0),

-- InvoiceId 18 - Tổng tiền: 875000
(18, 29, 2, 437500, 0),

-- InvoiceId 19 - Tổng tiền: 780000
(19, 30, 3, 260000, 0),

-- InvoiceId 20 - Tổng tiền: 940000
(20, 31, 4, 235000, 0),

-- InvoiceId 21 - Tổng tiền: 1280000
(21, 32, 2, 640000, 0),

-- InvoiceId 22 - Tổng tiền: 1200000
(22, 33, 3, 400000, 0),

-- InvoiceId 23 - Tổng tiền: 1000000
(23, 34, 5, 200000, 0),

-- InvoiceId 24 - Tổng tiền: 1100000
(24, 35, 4, 275000, 0),

-- InvoiceId 25 - Tổng tiền: 820000
(25, 36, 2, 410000, 0),

-- InvoiceId 26 - Tổng tiền: 1400000
(26, 37, 7, 200000, 0),

-- InvoiceId 27 - Tổng tiền: 950000
(27, 38, 3, 316667, 0),

-- InvoiceId 28 - Tổng tiền: 1300000
(28, 39, 4, 325000, 0),

-- InvoiceId 29 - Tổng tiền: 1550000
(29, 40, 3, 516667, 0),

-- InvoiceId 30 - Tổng tiền: 890000
(30, 1, 2, 445000, 0),

-- InvoiceId 31 - Tổng tiền: 1230000
(31, 2, 4, 307500, 0),

-- InvoiceId 32 - Tổng tiền: 700000
(32, 3, 2, 350000, 0),

-- InvoiceId 33 - Tổng tiền: 1200000
(33, 4, 3, 400000, 0),

-- InvoiceId 34 - Tổng tiền: 830000
(34, 5, 2, 415000, 0),

-- InvoiceId 35 - Tổng tiền: 990000
(35, 6, 2, 495000, 0),

-- InvoiceId 36 - Tổng tiền: 1190000
(36, 7, 7, 170000, 0),

-- InvoiceId 37 - Tổng tiền: 680000
(37, 8, 4, 170000, 0),

-- InvoiceId 38 - Tổng tiền: 1400000
(38, 9, 8, 175000, 0),

-- InvoiceId 39 - Tổng tiền: 1040000
(39, 10, 3, 346667, 0),

-- InvoiceId 40 - Tổng tiền: 1350000
(40, 11, 5, 270000, 0);


INSERT INTO [dbo].[PurchaseInvoices] ([EmployeeId], [SupplierId], [InvoiceDate], [TotalAmount])
VALUES
-- InvoiceId 1
(1, 1, '2024-01-10T08:00:00', 5000000),
-- InvoiceId 2
(1, 2, '2024-01-12T09:00:00', 7500000),
-- InvoiceId 3
(2, 3, '2024-01-15T10:30:00', 6000000),
-- InvoiceId 4
(2, 4, '2024-01-20T11:00:00', 8000000),
-- InvoiceId 5
(3, 5, '2024-01-22T14:00:00', 9000000),
-- InvoiceId 6
(3, 1, '2024-01-25T16:00:00', 3000000),
-- InvoiceId 7
(1, 2, '2024-02-01T10:00:00', 7000000),
-- InvoiceId 8
(1, 3, '2024-02-05T09:30:00', 5000000),
-- InvoiceId 9
(2, 4, '2024-02-10T11:15:00', 6500000),
-- InvoiceId 10
(2, 5, '2024-02-12T13:45:00', 4000000);

INSERT INTO [dbo].[PurchaseInvoiceDetails] ([PurchaseInvoiceId], [ProductId], [Quantity], [UnitPrice], [Discount])
VALUES
-- PurchaseInvoiceId 1
(1, 1, 2, 1000000, 10),
(1, 2, 1, 2000000, 5),
(1, 3, 1, 1500000, 15),
(1, 4, 3, 500000, 0),
(1, 5, 5, 300000, 20),

-- PurchaseInvoiceId 2
(2, 1, 3, 1000000, 10),
(2, 2, 2, 1500000, 5),
(2, 3, 1, 1800000, 0),
(2, 4, 4, 700000, 5),
(2, 5, 2, 500000, 10),

-- PurchaseInvoiceId 3
(3, 1, 1, 2000000, 15),
(3, 2, 3, 900000, 5),
(3, 3, 2, 1200000, 10),
(3, 4, 1, 800000, 0),
(3, 5, 5, 1000000, 10),

-- PurchaseInvoiceId 4
(4, 1, 2, 1500000, 5),
(4, 2, 4, 600000, 0),
(4, 3, 3, 2000000, 10),
(4, 4, 1, 1200000, 5),
(4, 5, 2, 500000, 0),

-- PurchaseInvoiceId 5
(5, 1, 2, 500000, 20),
(5, 2, 2, 1000000, 5),
(5, 3, 3, 1500000, 10),
(5, 4, 1, 400000, 5),
(5, 5, 1, 2000000, 0),

-- PurchaseInvoiceId 6
(6, 1, 3, 1200000, 15),
(6, 2, 2, 800000, 0),
(6, 3, 1, 900000, 5),
(6, 4, 2, 700000, 0),
(6, 5, 1, 300000, 10),

-- PurchaseInvoiceId 7
(7, 1, 1, 1100000, 10),
(7, 2, 3, 1300000, 0),
(7, 3, 1, 1500000, 5),
(7, 4, 2, 500000, 0),
(7, 5, 1, 600000, 10),

-- PurchaseInvoiceId 8
(8, 1, 2, 900000, 15),
(8, 2, 3, 1100000, 5),
(8, 3, 2, 1300000, 10),
(8, 4, 1, 400000, 5),
(8, 5, 1, 1000000, 0),

-- PurchaseInvoiceId 9
(9, 1, 3, 1500000, 20),
(9, 2, 1, 2000000, 0),
(9, 3, 2, 1700000, 15),
(9, 4, 3, 800000, 10),
(9, 5, 1, 600000, 5),

-- PurchaseInvoiceId 10
(10, 1, 2, 1300000, 5),
(10, 2, 2, 1400000, 0),
(10, 3, 1, 900000, 10),
(10, 4, 3, 600000, 0),
(10, 5, 1, 800000, 20);

UPDATE si
SET si.TotalAmount = t.TotalAmount
FROM SalesInvoices si
JOIN (
    SELECT SalesInvoiceId,
           SUM(Quantity * UnitPrice * (1 - Discount / 100.0)) AS TotalAmount
    FROM SalesInvoiceDetails
    GROUP BY SalesInvoiceId
) AS t ON si.SalesInvoiceId = t.SalesInvoiceId;

UPDATE pi
SET pi.TotalAmount = t.TotalAmount
FROM PurchaseInvoices pi
JOIN (
    SELECT PurchaseInvoiceId,
           SUM(Quantity * UnitPrice * (1 - Discount / 100.0)) AS TotalAmount
    FROM PurchaseInvoiceDetails
    GROUP BY PurchaseInvoiceId
) AS t ON pi.PurchaseInvoiceId = t.PurchaseInvoiceId;
