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
    Id, TreamentCategoryId, Name, Description, BasicPrice, Duration, SuccessRate,
    MinAge, MaxAge, RecommendedFor, Contraindications, IsActive, CreatedAt
)
VALUES (
    NEWID(),
    '2B39DCAD-04D2-4533-9C61-90C6DB3819B7',
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

SELECT * FROM TreatmentService

-- IUI Service
INSERT INTO TreatmentService (
    Id, TreamentCategoryId, Name, Description, BasicPrice, Duration, SuccessRate,
    MinAge, MaxAge, RecommendedFor, Contraindications, IsActive, CreatedAt
)
VALUES (
    NEWID(),
    '7AFF4651-217C-4789-9933-1F802CAAE7C6',
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
('37C94DC0-ABF1-41B3-88E8-40509BA18D67', N'Kích thích buồng trứng', N'Sử dụng hormone để kích thích phát triển nhiều nang trứng.', 1, 10, 0, 1),
('37C94DC0-ABF1-41B3-88E8-40509BA18D67', N'Chọc hút trứng', N'Gây mê nhẹ và hút trứng từ buồng trứng.', 2, 1, 0, 1),
('37C94DC0-ABF1-41B3-88E8-40509BA18D67', N'Thụ tinh và nuôi cấy phôi', N'Thụ tinh trứng và tinh trùng trong phòng lab và nuôi cấy phôi.', 3, 3, 0, 1),
('37C94DC0-ABF1-41B3-88E8-40509BA18D67', N'Chuyển phôi', N'Chuyển phôi vào tử cung người mẹ.', 4, 1, 0, 1),
('37C94DC0-ABF1-41B3-88E8-40509BA18D67', N'The dõi beta hCG', N'Xét nghiệm máu sau 2 tuần để kiểm tra có thai.', 5, 10, 0, 1);

-- Các bước IUI
INSERT INTO TreatmentStep (TreatmentServiceId, StepName, Description, StepOrder, EstimatedDurationDays, IsOptional, IsActive)
VALUES 
('0444E079-2AE6-4A75-951B-6C6BD8F578BD', N'Kích thích rụng trứng nhẹ', N'Sử dụng thuốc để kích thích rụng trứng đúng thời điểm.', 1, 5, 0, 1),
('0444E079-2AE6-4A75-951B-6C6BD8F578BD', N'Bơm tinh trùng vào tử cung', N'Đưa tinh trùng đã lọc rửa vào tử cung bằng ống nhỏ.', 2, 1, 0, 1),
('0444E079-2AE6-4A75-951B-6C6BD8F578BD', N'Theo dõi và thử thai', N'Theo dõi chu kỳ và xét nghiệm máu kiểm tra thai.', 3, 10, 0, 1);


SELECT * FROM TreatmentService WHERE ID = '37C94DC0-ABF1-41B3-88E8-40509BA18D67'

SELECT * FROM TreatmentService

SELECT COUNT(*) FROM TreatmentService;
