CREATE DATABASE FeritilyCareDB

USE FeritilyCareDB

DROP DATABASE FeritilyCareDB

-- 1. Hồ sơ người dùng (tích hợp Identity User)
CREATE TABLE UserProfile (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	FirstName NVARCHAR(500),
	MiddleName NVARCHAR(500),
    LastName NVARCHAR(500),
    Gender INT DEFAULT 1,
    DateOfBirth DATE,
    Address NVARCHAR(MAX),
    City NVARCHAR(255),
    Province NVARCHAR(255),
    Country NVARCHAR(255) DEFAULT 'Vietnam',
    AvatarUrl NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
);

-- 2. Đối tác của bệnh nhân
CREATE TABLE PatientPartner (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FullName NVARCHAR(500) NOT NULL,
    Gender INT,
    DateOfBirth DATE,
    BloodType NVARCHAR(20),
    MedicalHistory NTEXT, -- tiền sử bệnh án
    ContactNumber NVARCHAR(20),
    Email NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
);

-- 3. Hồ sơ bệnh nhân
CREATE TABLE Patient (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER UNIQUE NOT NULL,
    MedicalHistory NTEXT,
    FertilityDiagnosis NVARCHAR(MAX), -- chuẩn đoán khả năng sinh sản
    AllergiesNotes NTEXT, -- ghi chú dị ứng
    BloodType NVARCHAR(20), -- loại máu
    Height DECIMAL(5,2), -- in cm
    Weight DECIMAL(5,2), -- in kg
    MaritalStatus NVARCHAR(50), -- tình trạng hôn nhân
    PatientParnerId UNIQUEIDENTIFIER NULL,
    Note NTEXT,
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id) ON DELETE CASCADE,
	FOREIGN KEY(PatientParnerId) REFERENCES PatientPartner(Id)
);

-- 4. Bác sĩ
CREATE TABLE Doctor (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    Degree NVARCHAR(255),
    Specialization NVARCHAR(255),
    YearsOfExperience INT,
    Biography NTEXT, -- tiểu sử 
    Education NTEXT,
    Rating DECIMAL(3,2) CHECK (Rating BETWEEN 1 AND 5),
    PatientsServed INT DEFAULT 0, -- số bệnh nhân đã chữa trị
    IsAcceptingPatients BIT DEFAULT 1, -- đang nhận bệnh nhân
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_Doctor_UserProfile UNIQUE(UserProfileId)
);

-- 5. Lịch làm việc của bác sĩ
CREATE TABLE DoctorSchedule (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    WorkDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    IsAvailable BIT DEFAULT 1,
    MaxAppointments INT,
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id) ON DELETE CASCADE
);

-- 6. Thể loại dịch vụ điều trị 
CREATE TABLE TreatmentCategory (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(255) NOT NULL,
    Description NTEXT,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
);

-- 7. Dịch vụ điều trị
CREATE TABLE TreatmentService (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TreatmentCategoryId UNIQUEIDENTIFIER,
    Name NVARCHAR(MAX) NOT NULL,
    Description NTEXT,
    BasicPrice DECIMAL(18,2),
    Duration INT, -- ước tính ngày
    SuccessRate DECIMAL(5,2), -- tỉ lệ thành công
    MinAge INT,
    MaxAge INT,
    RecommendedFor NVARCHAR(MAX), -- khuyến khích cho
    Contraindications NVARCHAR(MAX), -- chống chỉ định với gì
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(TreatmentCategoryId) REFERENCES TreatmentCategory(Id)
);

-- 8. Các bước điều trị
CREATE TABLE TreatmentStep (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    TreatmentServiceId UNIQUEIDENTIFIER NOT NULL,
    StepName NVARCHAR(255) NOT NULL,
    Description NTEXT,
    StepOrder INT NOT NULL,
    EstimatedDurationDays INT NULL,
    IsOptional BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id) ON DELETE CASCADE
);

-- 9. Đặt lịch 
CREATE TABLE Appointment (	
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    DoctorScheduleId BIGINT NULL,
    TreatmentServiceId UNIQUEIDENTIFIER NULL,
    AppointmentDate DATETIME NOT NULL,
    StartTime TIME NULL,
    EndTime TIME NULL,
    Purpose NVARCHAR(MAX),
    Status INT, -- Scheduled, Completed, Cancelled, NoShow
    CancellationReason NVARCHAR(MAX) NULL,
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(DoctorScheduleId) REFERENCES DoctorSchedule(Id),
	FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id)
);

-- 10. Kế hoạch đều trị cho bệnh nhân
CREATE TABLE ServicePackagePlan (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    TreatmentServiceId UNIQUEIDENTIFIER NOT NULL,
    StartDate DATE DEFAULT GETDATE(),
    EndDate DATE NULL,
    Status INT DEFAULT 1, -- Planned, In Progress, Completed, Cancelled, Failed
    TotalCost DECIMAL(18,2),
    PaymentStatus INT DEFAULT 1, -- Pending, Partial, Completed, Refunded
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id)
);

-- 11. Các bước trong kế hoạch điều trị cho bệnh nhân
CREATE TABLE ServicePackagePlanStep (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    TreatmentStepId BIGINT NOT NULL,
    StartDate DATETIME,
    EndDate DATETIME NULL,
    Status INT DEFAULT 1, -- Pending, In Progress, Completed, Skipped, Failed
    Note NTEXT NULL,
    IsComplete BIT DEFAULT 0,
    CompletedAt DATETIME NULL,
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE,
    FOREIGN KEY(TreatmentStepId) REFERENCES TreatmentStep(Id)
);

-- 12. Các bước bổ sung trong các bước của lịch trình điều trị cố định
CREATE TABLE ServicePackagePlanExtension (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    StepName NVARCHAR(MAX) NOT NULL,
    Reason NVARCHAR(MAX),
    Note NTEXT,
    StartDate DATETIME,
    EndDate DATETIME,
    ExtraFee DECIMAL(18,2),
    IsComplete BIT DEFAULT 0,
    CompletedAt DATETIME,
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE,
);

-- 13. Kết quả xét nghiệm
CREATE TABLE TestResult (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    TestName NVARCHAR(MAX) NOT NULL,
    TestCategory NVARCHAR(MAX) NULL, -- Blood, Hormone, Ultrasound, etc.
    ResultValue NVARCHAR(MAX) NULL,
    Note NTEXT NULL,
    TestDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE
);

-- 14. Báo cáo lại tiến trình điều trị - của bác sĩ note lại
CREATE TABLE TreatmentProgressReport (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
	DoctorId UNIQUEIDENTIFIER NOT NULL,
    ReportDate DATETIME DEFAULT GETDATE(),
    NextSteps NTEXT NOT NULL,
    OverallAssessment NVARCHAR(MAX) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id)
);

-- 15. Đơn thuốc
CREATE TABLE Prescription (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServicePackagePlanId UNIQUEIDENTIFIER NULL,
    PrescriptionDate DATETIME DEFAULT GETDATE(),
    ExpiryDate DATETIME NULL,
    Note NTEXT NULL,
    Status NVARCHAR(255) DEFAULT 'Active', -- Active, Completed, Cancelled
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE
);

-- 16. Kế thuốc chi tiết theo toa thuốc
CREATE TABLE PrescriptionItem  (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    PrescriptionId UNIQUEIDENTIFIER NOT NULL,
    MedicationName NVARCHAR(MAX) NOT NULL,
    Dosage NVARCHAR(255) NULL, -- liều dùng
    Quantity INT DEFAULT 1,
    StartDate DATE DEFAULT GETDATE(),
    EndDate DATE NULL,
    SpecialInstructions NTEXT NULL, -- hướng dẫn kĩ hơn nếu có
    FOREIGN KEY(PrescriptionId) REFERENCES Prescription(Id) ON DELETE CASCADE
);
 
-- 17. Nhắc nhở giám sát cho kế hoạch điều trị
CREATE TABLE MonitorReminder (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    PatientId UNIQUEIDENTIFIER,
    ServicePackagePlanId UNIQUEIDENTIFIER,
	SenderId UNIQUEIDENTIFIER, -- gửi bởi ai
    ReminderType NVARCHAR(100), -- Medication, Test, Appointment, etc.
    Title NVARCHAR(MAX),
    Description NTEXT,
    ReminderDate DATETIME,
    RecurrencePattern NVARCHAR(100), -- Daily, Weekly - lặp lại theo ngày, tuần...
    IsComplete BIT DEFAULT 0,
    CompletedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(PatientId) REFERENCES Patient(Id) ON DELETE CASCADE,
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id),
    FOREIGN KEY(SenderId) REFERENCES UserProfile(Id)
);

-- 18. Nhắc nhở đến lịch hẹn khám, tư vấn 
CREATE TABLE AppointmentReminder (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,	
    AppointmentId UNIQUEIDENTIFIER NOT NULL,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    ReminderDate DATETIME,
    ReminderMethod NVARCHAR(100), -- Email, SMS, App notification
    IsSent BIT DEFAULT 0,
    SentAt DATETIME,
    Status NVARCHAR(100) DEFAULT 'Pending', -- Pending, Sent, Failed
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(AppointmentId) REFERENCES Appointment(Id) ON DELETE CASCADE,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id)
);

-- 19. Các loại phương thức thanh toán
CREATE TABLE PaymentMethod (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    IsActive BIT DEFAULT 1
);

-- 20. Thanh toán
CREATE TABLE Payment (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    ServicePackagePlanId UNIQUEIDENTIFIER,
    PaymentCode NVARCHAR(255) UNIQUE,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentMethodId UNIQUEIDENTIFIER NOT NULL,
    TransactionCode NVARCHAR(255),
    PaymentDate DATETIME,
    Status NVARCHAR(100) NOT NULL, -- Pending, Completed, Failed, Refunded
    RefundAmount DECIMAL(18,2) DEFAULT 0, -- tiền hoàn lại
    RefundReason NVARCHAR(MAX), -- lí do hoàn
    RefundDate DATETIME, -- ngày hoàn
    Notes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id),
    FOREIGN KEY(PaymentMethodId) REFERENCES PaymentMethod(Id)
);

-- 21. Image, Video
CREATE TABLE MediaFile (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PublicId NVARCHAR(MAX),
    Url NVARCHAR(MAX),
    SecureUrl NVARCHAR(MAX),
    Folder NVARCHAR(255),
    FileName NVARCHAR(255),
    FileType NVARCHAR(100), -- Document, Image, Video, etc.
    MimeType NVARCHAR(100),
    ResourceType NVARCHAR(50), -- image, video, raw (for Cloudinary)
    Format NVARCHAR(50),
    Size BIGINT,
    Width INT,
    Height INT,
    Duration DECIMAL(10,2), -- For videos
    Tags NVARCHAR(MAX),
    Context NVARCHAR(MAX), -- For metadata
    Transformation NVARCHAR(MAX), -- For applied transformations
    OwnerId UNIQUEIDENTIFIER NULL, -- User who uploaded
    OwnerType NVARCHAR(100), -- User, Patient, Doctor, System
    RelatedEntityId NVARCHAR(255) DEFAULT '#NoData', -- ID of related entity
    RelatedEntityType NVARCHAR(255) DEFAULT '#NoData', -- Type of related entity
    IsPublic BIT DEFAULT 0,
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(OwnerId) REFERENCES UserProfile(Id)
);

-- 22. Feedback từ user, patient
CREATE TABLE Feedback (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER,
    DoctorId UNIQUEIDENTIFIER,
    ServicePackagePlanId UNIQUEIDENTIFIER,
    Rating DECIMAL(3,1) CHECK (Rating BETWEEN 1 AND 5),
    Comment NTEXT,
    TreatmentQualityRating DECIMAL(3,1) CHECK (TreatmentQualityRating BETWEEN 1 AND 5),
    PrivacyRating DECIMAL(3,1) CHECK (PrivacyRating BETWEEN 1 AND 5),
    IsDisplayed BIT DEFAULT 0,
    Status INT DEFAULT 2, -- Pending, Approved, Rejected
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id)
);

-- 23. Blogs
CREATE TABLE Blog (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(MAX) NOT NULL,
    Summary NVARCHAR(MAX),
    Content NTEXT NOT NULL,
    FeaturedImageUrl NVARCHAR(MAX),
    MetaKeywords NVARCHAR(MAX),
    MetaDescription NVARCHAR(MAX),
    Status INT DEFAULT 2, -- Draft, Published, Archived
    ViewCount INT DEFAULT 0,
	FOREIGN KEY (UserProfileId) REFERENCES UserProfile (Id)
);


-- 24. Bảng quản lý chu kỳ lấy trứng
CREATE TABLE EggRetrievalCycle (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    CycleNumber INT NOT NULL, -- Chu kỳ lấy trứng thứ mấy (1, 2, 3...)
    RetrievalDate DATETIME,
    TotalEggsRetrieved INT, -- Tổng số trứng thu được
    MatureEggs INT DEFAULT 0, -- Số trứng trưởng thành (MII)
    ImmatureEggs INT DEFAULT 0, -- Số trứng chưa trưởng thành (MI, GV)
    AbnormalEggs INT DEFAULT 0, -- Số trứng bất thường/thoái hóa
    DoctorId UNIQUEIDENTIFIER,
    DoctorNotes NTEXT,
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id)
);

-- 25. Bảng quản lý quá trình thụ tinh
CREATE TABLE EmbryoFertilization (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EggRetrievalCycleId UNIQUEIDENTIFIER NOT NULL,
    FertilizationDate DATE NOT NULL,
    TotalEggsUsed INT NOT NULL, -- Số trứng được dùng để thụ tinh
    TotalEggsFertilized INT DEFAULT 0, -- Số trứng thụ tinh thành công
    TotalEmbryosFormed INT DEFAULT 0, -- Số phôi được hình thành
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    EmbryologistNotes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
	UpdatedAt DATETIME NULL,
    FOREIGN KEY(EggRetrievalCycleId) REFERENCES EggRetrievalCycle(Id) ON DELETE CASCADE,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id)
);

-- 26. Bảng chi tiết từng phôi
CREATE TABLE EmbryoDetail (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmbryoFertilizationId UNIQUEIDENTIFIER NOT NULL,
    Grade NVARCHAR(20), -- Cho Day 3: Grade 1,2,3,4 | Cho Day 5/6: AA, AB, BA, BB, BC, CB, CC
    IsViable BIT DEFAULT 1, -- Phôi có khả năng sống
    Status INT DEFAULT 1, -- Available, Transferred, Frozen, Discarded, Deteriorated
    Notes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    FOREIGN KEY(EmbryoFertilizationId) REFERENCES EmbryoFertilization(Id) ON DELETE CASCADE
);

-- 27. Bảng quản lý phôi đông lạnh
CREATE TABLE FrozenEmbryoStorage (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmbryoDetailId UNIQUEIDENTIFIER NOT NULL,
    StorageStartDate DATETIME NOT NULL,
    StorageEndDate DATETIME NULL,
    StorageTank NVARCHAR(50) NOT NULL, -- Tên bình chứa (Tank A, Tank B...)
    FreezeMethod NVARCHAR(50), -- Vitrification, Slow Freeze
    MonthlyStorageFee DECIMAL(10,2), -- Phí lưu trữ hàng tháng
    Status INT DEFAULT 1, -- Active, Used, Discarded, Expired, Transferred
    SurvivalAfterThaw BIT, -- Phôi có sống sau khi rã đông không
    Notes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(EmbryoDetailId) REFERENCES EmbryoDetail(Id) ON DELETE CASCADE
);

CREATE TABLE EmbryoTransfer (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmbryoDetailId UNIQUEIDENTIFIER NOT NULL, -- Tham chiếu đến phôi (dù là fresh hay frozen)
    IsFrozenTransfer BIT DEFAULT 0, -- TRUE = Frozen, FALSE = Fresh
    TransferDate DATETIME NOT NULL,
    IsSuccessful BIT, -- Có thụ thai không
    PregnancyResultNote NTEXT, -- Ghi chú về tình trạng sau khi cấy: beta HCG, siêu âm, v.v.
    DoctorId UNIQUEIDENTIFIER,
    FeeCharged DECIMAL(10,2), -- Có tính tiền không?
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(EmbryoDetailId) REFERENCES EmbryoDetail(Id) ON DELETE CASCADE,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id)
);