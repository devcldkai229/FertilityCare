CREATE DATABASE FeritilyCare_DB_Test

USE FeritilyCare_DB_Test

-- 1. Hồ sơ người dùng (tích hợp Identity User)
CREATE TABLE UserProfile (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	FirstName NVARCHAR(500),
	MiddleName NVARCHAR(500),
    LastName NVARCHAR(500),
    Gender NVARCHAR(50),
    DateOfBirth DATE,
    Address NVARCHAR(MAX),
    City NVARCHAR(255),
    Province NVARCHAR(255),
    Country NVARCHAR(100) DEFAULT 'Vietnam',
    AvatarUrl NVARCHAR(MAX),
    PreferredLanguage NVARCHAR(50) DEFAULT 'vi-VN',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    IsActive BIT DEFAULT 1
);

-- 2. Hồ sơ bệnh nhân
CREATE TABLE Patient (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    MedicalHistory NTEXT,
    FertilityDiagnosis NVARCHAR(MAX), -- chuẩn đoán khả năng sinh sản
    AllergiesNotes NTEXT, -- ghi chú dị ứng
    BloodType NVARCHAR(20), -- loại máu
    Height DECIMAL(5,2), -- in cm
    Weight DECIMAL(5,2), -- in kg
    MaritalStatus NVARCHAR(50), -- tình trạng hôn nhân
    PartnerInfoId UNIQUEIDENTIFIER NULL,
    Notes NTEXT,
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_Patient_UserProfile UNIQUE(UserProfileId)
);

-- Bảng thông số yêu cầu tiêu chuẩn 
CREATE TABLE StandardTreatmentMetric (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    MetricName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    MetricUnit NVARCHAR(255),
    MinValue FLOAT DEFAULT 0,
    MaxValue FLOAT,
	Genre NVARCHAR(255),
    StandardNote NTEXT
);

-- Bảng test các thông số tiêu chuẩn trước khi điều trị hoặc trước mấy buổi điều trị
CREATE TABLE TestResultsTreatmentMetric (
	Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
	PatientId UNIQUEIDENTIFIER NULL,
	PatientPartnerId UNIQUEIDENTIFIER NULL,
	MetricName NVARCHAR(255), 
	MinValue FLOAT DEFAULT 0,
	MaxValue FLOAT,
	FOREIGN KEY (PatientId) REFERENCES Patient (Id),
	FOREIGN KEY (PatientPartnerId) REFERENCES PatientPartner (Id)
);

-- 3. Bồ bệnh nhân (người cho tt)
CREATE TABLE PatientPartner (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    FullName NVARCHAR(500) NOT NULL,
    Gender NVARCHAR(50),
    DateOfBirth DATE,
    BloodType NVARCHAR(20),
    MedicalHistory NTEXT, -- tiền sử bệnh án
    ContactNumber NVARCHAR(50),
    Email NVARCHAR(256),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id) ON DELETE CASCADE
);

-- Add the FK constraint after both tables exist
ALTER TABLE Patient
ADD CONSTRAINT FK_Patient_Partner FOREIGN KEY(PartnerInfoId) REFERENCES PatientPartner(Id);

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

-- 6. Lịch làm việc của bác sĩ
CREATE TABLE DoctorSchedule (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    WorkDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    IsAvailable BIT DEFAULT 1,
    MaxAppointments INT DEFAULT 10,
    Note NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id) ON DELETE CASCADE
);

-- 7. Lịch nghỉ phép của bác sĩ
CREATE TABLE DoctorLeave (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    LeaveType NVARCHAR(100), -- Vacation, Sick, Conference, etc.
    Reason NVARCHAR(MAX),
    ApprovedBy UNIQUEIDENTIFIER NULL, -- Manager chấp nhận
    ApprovedAt DATETIME,
    Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, Approved, Rejected
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id) ON DELETE CASCADE,
    FOREIGN KEY(ApprovedBy) REFERENCES UserProfile(Id)
);

-- 8. Đặt lịch 
CREATE TABLE Appointment (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    ScheduleId BIGINT,
    ServicePackagePlanId UNIQUEIDENTIFIER NULL,
    AppointmentDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    AppointmentType NVARCHAR(100), -- Initial Consultation, Follow-up, Treatment, etc.
    Purpose NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Scheduled', -- Scheduled, Completed, Cancelled, NoShow
    CancellationReason NVARCHAR(MAX),
    Notes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(ScheduleId) REFERENCES DoctorSchedule(Id)
);

-- 9. Thể loại dịch vụ điều trị 
CREATE TABLE TreatmentCategory (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(255) NOT NULL,
    Description NTEXT,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
);

-- 10. Dịch vụ điều trị
CREATE TABLE TreatmentService (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TreatmentCategoryId UNIQUEIDENTIFIER,
    Name NVARCHAR(MAX) NOT NULL,
    Description NTEXT,
    TreatmentType NVARCHAR(100),
    BasicPrice DECIMAL(18,2),
    Duration INT, -- ước tính ngày
    RequiresPartner BIT DEFAULT 1,
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

-- 11. Các bước điều trị
CREATE TABLE TreatmentStep (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    TreatmentServiceId UNIQUEIDENTIFIER NOT NULL,
    StepName NVARCHAR(255) NOT NULL,
    Description NTEXT,
    StepOrder INT NOT NULL,
    EstimatedDurationDays INT,
    IsOptional BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id) ON DELETE CASCADE
);

-- 12. Các câu hỏi thường gặp về các loại dịch vụ
CREATE TABLE TreatmentServiceFAQ (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TreatmentServiceId UNIQUEIDENTIFIER NOT NULL,
    Question NVARCHAR(MAX) NOT NULL,
    Answer NTEXT NOT NULL,
    DisplayOrder INT,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id) ON DELETE CASCADE
);

-- 13. Kế hoạch đều trị cho bệnh nhân
CREATE TABLE ServicePackagePlan (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    TreatmentServiceId UNIQUEIDENTIFIER NOT NULL,
    PlanCode NVARCHAR(50) UNIQUE,
    StartDate DATE,
    EstimatedEndDate DATE,
    ActualEndDate DATE,
    Status NVARCHAR(100) DEFAULT 'Planned', -- Planned, In Progress, Completed, Cancelled, Failed
    TotalCost DECIMAL(18,2),
    PaymentStatus NVARCHAR(50) DEFAULT 'Pending', -- Pending, Partial, Completed, Refunded
    Notes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id)
);

ALTER TABLE Appointment
ADD CONSTRAINT FK_Appointment_ServicePackagePlan 
FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id);

-- 14. Các bước trong kế hoạch điều trị cho bệnh nhân
CREATE TABLE ServicePackagePlanStep (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    TreatmentStepId BIGINT NOT NULL,
    ScheduledStartDate DATETIME,
    ScheduledEndDate DATETIME,
    ActualEndDate DATETIME,
    Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, In Progress, Completed, Skipped, Failed
    DoctorId UNIQUEIDENTIFIER, -- Bác sĩ điều trị
    Note NTEXT,
    IsComplete BIT DEFAULT 0,
    CompletedAt DATETIME,
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(TreatmentStepId) REFERENCES TreatmentStep(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
);

CREATE TABLE 

-- 15. Các bước bổ sung trong các bước của lịch trình điều trị cố định
CREATE TABLE ServicePackageStepExtension (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServicePackagePlanStepId BIGINT NOT NULL,
    StepName NVARCHAR(MAX) NOT NULL,
    Reason NVARCHAR(MAX),
    Note NTEXT,
    StartDate DATETIME,
    EndDate DATETIME,
    ExtraFee DECIMAL(18,2),
    IsComplete BIT DEFAULT 0,
    CompletedAt DATETIME,
    CreatedBy UNIQUEIDENTIFIER NOT NULL, -- bác sĩ thêm bước
    FOREIGN KEY(ServicePackagePlanStepId) REFERENCES ServicePackagePlanStep(Id) ON DELETE CASCADE,
    FOREIGN KEY(CreatedBy) REFERENCES Doctor(Id)
);

-- 16. Kết quả xét nghiệm
CREATE TABLE TestResult (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    ServicePackagePlanStepId BIGINT NOT NULL,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    TestName NVARCHAR(255) NOT NULL,
    TestCategory NVARCHAR(100), -- Blood, Hormone, Ultrasound, etc.
    ResultValue NVARCHAR(MAX),
    Unit NVARCHAR(50),
    ReferenceRangeLow NVARCHAR(50),
    ReferenceRangeHigh NVARCHAR(50),
    Note NTEXT,
    TestDate DATETIME NOT NULL,
    RecordedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(ServicePackagePlanStepId) REFERENCES ServicePackagePlanStep(Id) ON DELETE CASCADE,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id)
);

-- 17. Báo cáo lại tiến trình điều trị - của bác sĩ note lại
CREATE TABLE TreatmentProgressReport (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    ReportDate DATETIME DEFAULT GETDATE(),
    Progress NTEXT,
    Challenges NTEXT,
    NextSteps NTEXT,
    OverallAssessment NVARCHAR(MAX),
    PatientFeedback NTEXT,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id) ON DELETE CASCADE,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id)
);

-- 18. Dấu hiệu đến từ theo dõi bệnh nhân theo các phác đồ điều trị
CREATE TABLE CrucialSigns (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    ServicePackagePlanId UNIQUEIDENTIFIER,
    RecordDate DATETIME NOT NULL,
    Temperature DECIMAL(5,2),
    BloodPressureSystolic INT, -- huyết áp tâm thu
    BloodPressureDiastolic INT, -- huyết áp tâm trương
    HeartRate INT, -- nhịp tim
    Weight DECIMAL(5,2),
    Notes NTEXT,
    RecordedByDoctorId UNIQUEIDENTIFIER, -- ghi lại bởi bác sĩ điều trị 
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id),
    FOREIGN KEY(RecordedByDoctorId) REFERENCES Doctor(Id)
);

-- 20. Đơn thuốc
CREATE TABLE Prescription (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    ServicePackagePlanId UNIQUEIDENTIFIER,
    PrescriptionDate DATETIME NOT NULL,
    ExpiryDate DATETIME NULL,
    Note NTEXT,
    Status NVARCHAR(50) DEFAULT 'Active', -- Active, Completed, Cancelled
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id)
);

-- 21. Kế thuốc theo toa
CREATE TABLE PrescriptionMedication (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY DEFAULT NEWID(),
    PrescriptionId UNIQUEIDENTIFIER NOT NULL,
    MedicationName UNIQUEIDENTIFIER NOT NULL,
    Dosage NVARCHAR(255) NOT NULL, -- liều dùng
    Frequency NVARCHAR(255) NOT NULL,
    Duration NVARCHAR(100),
    Quantity INT,
    StartDate DATE,
    EndDate DATE,
    SpecialInstructions NTEXT, -- hướng dẫn kĩ hơn nếu có
    FOREIGN KEY(PrescriptionId) REFERENCES Prescription(Id) ON DELETE CASCADE
);
 
-- 22. Nhắc nhở giám sát cho kế hoạch điều trị
CREATE TABLE MonitorReminder (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    ServicePackagePlanId UNIQUEIDENTIFIER NOT NULL,
    TreatmentStepId BIGINT,
    ReminderType NVARCHAR(100), -- Medication, Test, Appointment, etc.
    Title NVARCHAR(255) NOT NULL,
    Description NTEXT,
    ReminderDate DATETIME NOT NULL,
    RecurrencePattern NVARCHAR(100), -- Daily, Weekly - lặp lại theo ngày, tuần...
    IsComplete BIT DEFAULT 0,
    CompletedAt DATETIME,
    CreatedBy UNIQUEIDENTIFIER, -- gửi bởi ai
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id),
    FOREIGN KEY(TreatmentStepId) REFERENCES TreatmentStep(Id),
    FOREIGN KEY(CreatedBy) REFERENCES UserProfile(Id)
);

-- 23. Nhắc nhở đến lịch hẹn khám, tư vấn 
CREATE TABLE AppointmentReminder (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    AppointmentId UNIQUEIDENTIFIER NOT NULL,
    PatientId UNIQUEIDENTIFIER NOT NULL,
    ReminderDate DATETIME NOT NULL,
    ReminderMethod NVARCHAR(50), -- Email, SMS, App notification
    IsSent BIT DEFAULT 0,
    SentAt DATETIME,
    Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, Sent, Failed
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(AppointmentId) REFERENCES Appointment(Id) ON DELETE CASCADE,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id)
);

-- 25. Các loại phương thức thanh toán
CREATE TABLE PaymentMethod (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    IsActive BIT DEFAULT 1
);

-- 26. Thanh toán
CREATE TABLE Payment (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    ServicePackagePlanId UNIQUEIDENTIFIER,
    PaymentCode NVARCHAR(255) UNIQUE,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentMethodId UNIQUEIDENTIFIER NOT NULL,
    TransactionCode NVARCHAR(255),
    PaymentDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- Pending, Completed, Failed, Refunded
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

-- 30. Image, Video
CREATE TABLE MediaFiles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PublicId NVARCHAR(MAX) NOT NULL,
    Url NVARCHAR(MAX) NOT NULL,
    SecureUrl NVARCHAR(MAX) NOT NULL,
    Folder NVARCHAR(255),
    FileName NVARCHAR(255) NOT NULL,
    FileType NVARCHAR(100) NOT NULL, -- Document, Image, Video, etc.
    MimeType NVARCHAR(100),
    ResourceType NVARCHAR(50) NOT NULL, -- image, video, raw (for Cloudinary)
    Format NVARCHAR(20),
    Size BIGINT,
    Width INT,
    Height INT,
    Duration DECIMAL(10,2), -- For videos
    Tags NVARCHAR(MAX),
    Context NVARCHAR(MAX), -- For metadata
    Transformation NVARCHAR(MAX), -- For applied transformations
    OwnerId UNIQUEIDENTIFIER NOT NULL, -- User who uploaded
    OwnerType NVARCHAR(50) NOT NULL, -- User, Patient, Doctor, System
    RelatedEntityId NVARCHAR(50), -- ID of related entity
    RelatedEntityType NVARCHAR(50), -- Type of related entity
    IsPublic BIT DEFAULT 0,
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(OwnerId) REFERENCES UserProfile(Id)
);

-- 31. Feedback từ user, patient
CREATE TABLE Feedback (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER NOT NULL,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    ServicePackagePlanId UNIQUEIDENTIFIER,
    Rating DECIMAL(3,1) CHECK (Rating BETWEEN 1 AND 5) NOT NULL,
    Comment NTEXT,
    TreatmentQualityRating DECIMAL(3,1) CHECK (TreatmentQualityRating BETWEEN 1 AND 5),
    PrivacyRating DECIMAL(3,1) CHECK (PrivacyRating BETWEEN 1 AND 5),
    IsDisplayed BIT DEFAULT 0,
    Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, Approved, Rejected
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(ServicePackagePlanId) REFERENCES ServicePackagePlan(Id)
);

-- 32. Blogs
CREATE TABLE Blog (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Slug NVARCHAR(300) UNIQUE NOT NULL,
    Summary NVARCHAR(MAX),
    Content NTEXT NOT NULL,
    FeaturedImageUrl NVARCHAR(MAX),
    MetaKeywords NVARCHAR(MAX),
    MetaDescription NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Draft', -- Draft, Published, Archived
    ViewCount INT DEFAULT 0,
	FOREIGN KEY (UserProfileId) REFERENCES UserProfile (Id)
);
