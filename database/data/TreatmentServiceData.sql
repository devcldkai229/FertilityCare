USE FertilityCareDB

-- IVF Category
INSERT INTO TreatmentCategory (Id, Name, Description, IsActive, CreatedAt)
VALUES 
(NEWID(), N'IVF (Thụ tinh trong ống nghiệm)', N'Phương pháp hỗ trợ sinh sản bằng cách kết hợp trứng và tinh trùng ngoài cơ thể.', 1, GETDATE());

-- IUI Category
INSERT INTO TreatmentCategory (Id, Name, Description, IsActive, CreatedAt)
VALUES 
(NEWID(), N'IUI (Bơm tinh trùng vào buồng tử cung)', N'Phương pháp hỗ trợ sinh sản bằng cách bơm tinh trùng trực tiếp vào tử cung.', 1, GETDATE());

-- IVF Service
INSERT INTO TreatmentService (
    Id, TreatmentCategoryId, Name, Description, BasicPrice, Duration, SuccessRate,
    MinAge, MaxAge, RecommendedFor, Contraindications, IsActive, CreatedAt
)
VALUES (
    NEWID(),
    'EB5C4A0C-B2AD-4292-AA64-D7E1EF891368',
    N'IVF cơ bản',
    N'Dịch vụ thụ tinh trong ống nghiệm cơ bản, bao gồm kích thích buồng trứng, chọc hút trứng và chuyển phôi.',
    60000000,
    45,
    45.5,
    20,
    40,
    N'Phụ nữ hiếm muộn do tắc vòi trứng hoặc vô sinh không rõ nguyên nhân.',
    N'Không áp dụng cho phụ nữ có bệnh tim mạch nặng hoặc ung thư tiến triển.',
    1,
    GETDATE()
);

SELECT * FROM TreatmentCategory

SELECT * FROM TreatmentServices

-- IUI Service
INSERT INTO TreatmentService (
    Id, TreatmentCategoryId, Name, Description, BasicPrice, Duration, SuccessRate,
    MinAge, MaxAge, RecommendedFor, Contraindications, IsActive, CreatedAt
)
VALUES (
    NEWID(),
    'BB0B470B-FD54-40AC-B071-6CCFF97B09A8',
    N'IUI cơ bản',
    N'Dịch vụ bơm tinh trùng vào buồng tử cung, thích hợp cho các trường hợp tinh trùng yếu nhẹ hoặc cổ tử cung bất thường.',
    10000000,
    15,
    20.0,
    20,
    40,
    N'Các cặp vợ chồng hiếm muộn không rõ nguyên nhân hoặc rối loạn rụng trứng nhẹ.',
    N'Không áp dụng cho nữ bị tắc ống dẫn trứng hai bên.',
    1,
    GETDATE()
);


-- Các bước IVF
INSERT INTO TreatmentStep (TreatmentServiceId, StepName, Description, StepOrder, EstimatedDurationDays, IsOptional, IsActive)
VALUES 
('AAF4D868-E5F8-4811-A30D-7C78FC1576D1', N'Kích thích buồng trứng', N'Sử dụng hormone để kích thích phát triển nhiều nang trứng.', 1, 10, 0, 1),
('AAF4D868-E5F8-4811-A30D-7C78FC1576D1', N'Chọc hút trứng', N'Gây mê nhẹ và hút trứng từ buồng trứng.', 2, 1, 0, 1),
('AAF4D868-E5F8-4811-A30D-7C78FC1576D1', N'Thụ tinh và nuôi cấy phôi', N'Thụ tinh trứng và tinh trùng trong phòng lab và nuôi cấy phôi.', 3, 3, 0, 1),
('AAF4D868-E5F8-4811-A30D-7C78FC1576D1', N'Chuyển phôi', N'Chuyển phôi vào tử cung người mẹ.', 4, 1, 0, 1),
('AAF4D868-E5F8-4811-A30D-7C78FC1576D1', N'The dõi beta hCG', N'Xét nghiệm máu sau 2 tuần để kiểm tra có thai.', 5, 10, 0, 1);

-- Các bước IUI
INSERT INTO TreatmentStep (TreatmentServiceId, StepName, Description, StepOrder, EstimatedDurationDays, IsOptional, IsActive)
VALUES 
('343B203A-90B1-447D-A582-1360AA3A831E', N'Kích thích rụng trứng nhẹ', N'Sử dụng thuốc để kích thích rụng trứng đúng thời điểm.', 1, 5, 0, 1),
('343B203A-90B1-447D-A582-1360AA3A831E', N'Bơm tinh trùng vào tử cung', N'Đưa tinh trùng đã lọc rửa vào tử cung bằng ống nhỏ.', 2, 1, 0, 1),
('343B203A-90B1-447D-A582-1360AA3A831E', N'Theo dõi và thử thai', N'Theo dõi chu kỳ và xét nghiệm máu kiểm tra thai.', 3, 10, 0, 1);


SELECT * FROM TreatmentService WHERE ID = 'aaf4d868-e5f8-4811-a30d-7c78fc1576d1'

SELECT COUNT(*) FROM TreatmentServices;
