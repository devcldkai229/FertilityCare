CREATE TABLE UserProfile (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	FirstName NVARCHAR(255),
	MiddleName NVARCHAR(255),
    LastName NVARCHAR(255),
    Gender INT,
    DateOfBirth DATE,
    Address NVARCHAR(MAX),
    AvatarUrl NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
);

CREATE TABLE PatientPartner (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FullName NVARCHAR(500) NOT NULL,
    Gender INT,
    DateOfBirth DATE,
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
);

CREATE TABLE Patient (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    MedicalHistory NTEXT,
    FertilityDiagnosis NVARCHAR(MAX),
    PatientParnerId UNIQUEIDENTIFIER NULL,
    Note NTEXT,
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id) ON DELETE CASCADE,
	FOREIGN KEY(PatientParnerId) REFERENCES PatientPartner(Id)
);

CREATE TABLE Doctor (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER NOT NULL,
    Degree NVARCHAR(255),
    Specialization NVARCHAR(255),
    YearsOfExperience INT,
    Biography NTEXT, 
    Rating DECIMAL(3,2) CHECK (Rating BETWEEN 1 AND 5),
    PatientsServed INT DEFAULT 0, 
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id) ON DELETE CASCADE
);

CREATE TABLE DoctorSchedule (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    DoctorId UNIQUEIDENTIFIER NOT NULL,
    WorkDate DATE,
    StartTime TIME,
    EndTime TIME,
	IsAcceptingPatients BIT DEFAULT 1,
    MaxAppointments INT,
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id) ON DELETE CASCADE
);

CREATE TABLE TreatmentCategory (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(255),
    Description NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
	UpdatedAt DATETIME
);

CREATE TABLE TreatmentService (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TreatmentCategoryId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(MAX),
    Description NTEXT,
    EstimatePrice DECIMAL(18,2),
    Duration INT, 
    SuccessRate DECIMAL(5,2), 
    RecommendedFor NVARCHAR(MAX),
    Contraindications NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(TreatmentCategoryId) REFERENCES TreatmentCategory(Id)
);

CREATE TABLE TreatmentStep (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    TreatmentServiceId UNIQUEIDENTIFIER NOT NULL,
    StepName NVARCHAR(MAX),
    Description NTEXT,
    StepOrder INT,
    EstimatedDurationDays INT NULL,
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id) ON DELETE CASCADE
);

CREATE TABLE Appointment (
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	PatientId UNIQUEIDENTIFIER,
	DoctorId UNIQUEIDENTIFIER,
	DoctorScheduleId BIGINT NULL,
	TreatmentServiceId UNIQUEIDENTIFIER,
	AppointmentDate DATETIME NULL,
	StartTime TIME NULL,
    EndTime TIME NULL,
	BookingEmail VARCHAR(255) NULL,
	BookingPhone VARCHAR(255) NULL,
	Status INT,
	CancellationReason NTEXT NULL,
	Note NTEXT,
	CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
	FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(DoctorScheduleId) REFERENCES DoctorSchedule(Id),
	FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id)
);

CREATE TABLE MedicalExamination (
	Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
	AppointmentId UNIQUEIDENTIFIER,
	Symptoms NTEXT,
	Diagnosis NTEXT,
	Indications NTEXT,
	Note NTEXT,
	FOREIGN KEY(AppointmentId) REFERENCES Appointment(Id)
);

CREATE TABLE AppointmentReminder (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,	
    AppointmentId UNIQUEIDENTIFIER,
	ToEmailAddress NVARCHAR(MAX),
	ToPhoneNumber NVARCHAR(20) NULL,
    ReminderDate DATETIME,
    ReminderMethod NVARCHAR(100),
    IsSent BIT DEFAULT 0,
    Status INT, -- Pending, Sent, Failed
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
	UpdatedAt DATETIME,
    FOREIGN KEY(AppointmentId) REFERENCES Appointment(Id) ON DELETE CASCADE
);

CREATE TABLE TreatmentPlan (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER,
    DoctorId UNIQUEIDENTIFIER,
    TreatmentServiceId UNIQUEIDENTIFIER,
    StartDate DATE,
    EndDate DATE NULL,
    Status INT, -- Planned, InProgress, Completed, Cancelled, Failed
    Note NTEXT,
	PaymentStatus INT,
	TotalPrice DECIMAL(18,2),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(TreatmentServiceId) REFERENCES TreatmentService(Id)
);

CREATE TABLE TreatmentPlanStep (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    TreatmentPlanId UNIQUEIDENTIFIER,
    TreatmentStepId BIGINT,
    StartDate DATETIME,
    EndDate DATETIME NULL,
    Status INT, -- Pending, InProgress, Completed, Skipped, Failed,
	StepCost DECIMAL(18,2), 
    Note NTEXT,
    IsComplete BIT DEFAULT 0,
	CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE,
    FOREIGN KEY(TreatmentStepId) REFERENCES TreatmentStep(Id)
);

CREATE TABLE TestResult (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    TreatmentPlanId UNIQUEIDENTIFIER,
	TreatmentPlanStepId BIGINT NULL,
    TestName NVARCHAR(255),
    Result NTEXT,
    Note NTEXT,
    TestDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE,
	FOREIGN KEY(TreatmentPlanStepId) REFERENCES TreatmentPlanStep(Id)
);

CREATE TABLE EggRetrievalCycle (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TreatmentPlanId UNIQUEIDENTIFIER,
    CycleNumber INT, -- Chu kỳ lấy trứng thứ mấy (1, 2, 3...)
    RetrievalDate DATETIME,
    TotalEggsRetrieved INT, -- Tổng số trứng thu được
    MatureEggs INT DEFAULT 0, -- Số trứng trưởng thành (M2)
    ImmatureEggs INT DEFAULT 0, -- Số trứng chưa trưởng thành (MI, GV)
    AbnormalEggs INT DEFAULT 0, -- Số trứng bất thường/thoái hóa
    Note NTEXT,
    FOREIGN KEY(TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE,
);

CREATE TABLE EmbryoFertilization (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EggRetrievalCycleId UNIQUEIDENTIFIER,
	TreatmentPlanId UNIQUEIDENTIFIER,
    FertilizationDate DATE,
    TotalEggsUsed INT, -- Số trứng được dùng để thụ tinh
    TotalEggsFertilized INT DEFAULT 0, -- Số trứng thụ tinh thành công
    TotalEmbryosFormed INT DEFAULT 0, -- Số phôi được hình thành
	FertilizationMethod NVARCHAR(30),
    CreatedAt DATETIME DEFAULT GETDATE(),
	UpdatedAt DATETIME,
    FOREIGN KEY(EggRetrievalCycleId) REFERENCES EggRetrievalCycle(Id),
	FOREIGN KEY (TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE
);

CREATE TABLE EmbryoDetail (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmbryoFertilizationId UNIQUEIDENTIFIER NOT NULL,
	TreatmentPlanId UNIQUEIDENTIFIER,
    Grade NVARCHAR(50), -- Cho Day 3: Grade 1,2,3,4 | Cho Day 5/6: AA, AB, BA, BB, BC, CB, CC
    IsViable BIT, -- Phôi có khả năng sống
    Status INT, -- Available, Transferred, Frozen, Discarded, Deteriorated
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(EmbryoFertilizationId) REFERENCES EmbryoFertilization(Id),
	FOREIGN KEY (TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE
);

CREATE TABLE FrozenEmbryoStorage (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmbryoDetailId UNIQUEIDENTIFIER,
	TreatmentPlanId UNIQUEIDENTIFIER,
    StorageStartDate DATETIME,
    StorageEndDate DATETIME ,
    StorageTank NVARCHAR(50), -- Tên bình chứa (Tank A, Tank B...)
    FreezeMethod INT, -- Vitrification, Slow Freeze
    MonthlyStorageFee DECIMAL(18,2), -- Phí lưu trữ hàng tháng
    Status INT DEFAULT 1, -- Active, Used, Discarded, Expired, Transferred
    SurvivalAfterThaw BIT, -- Phôi có sống sau khi rã đông không
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(EmbryoDetailId) REFERENCES EmbryoDetail(Id) 
	,FOREIGN KEY (TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE

);

CREATE TABLE EmbryoTransfer (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmbryoDetailId UNIQUEIDENTIFIER NOT NULL, -- Tham chiếu đến phôi (dù là fresh hay frozen)
    TreatmentPlanId UNIQUEIDENTIFIER,
	IsFrozenTransfer BIT DEFAULT 0, -- TRUE = Frozen, FALSE = Fresh
    TransferDate DATETIME NOT NULL,
    IsSuccessful BIT, -- Có thụ thai không
    PregnancyResultNote NTEXT, -- Ghi chú về tình trạng sau khi cấy: beta HCG, siêu âm, v.v.
    FeeCharged DECIMAL(10,2), -- Có tính tiền không?
    Note NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(EmbryoDetailId) REFERENCES EmbryoDetail(Id)
	,FOREIGN KEY (TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE
);

CREATE TABLE Prescription (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TreatmentPlanId UNIQUEIDENTIFIER,
    PrescriptionDate DATETIME DEFAULT GETDATE(),
    Note NTEXT NULL,
    FOREIGN KEY(TreatmentPlanId) REFERENCES TreatmentPlan(Id) ON DELETE CASCADE
);

CREATE TABLE PrescriptionItem (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    PrescriptionId UNIQUEIDENTIFIER,
    MedicationName NTEXT,
    Dosage NTEXT,
    Quantity INT DEFAULT 1,
    StartDate DATE DEFAULT GETDATE(),
    EndDate DATE NULL,
    SpecialInstructions NTEXT NULL,
    FOREIGN KEY(PrescriptionId) REFERENCES Prescription(Id) ON DELETE CASCADE
);

CREATE TABLE PaymentMethod (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100),
    Description NVARCHAR(MAX),
    IsActive BIT DEFAULT 1
);

CREATE TABLE Payment (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserProfileId UNIQUEIDENTIFIER,
    TreatmentPlanStepId BIGINT,
    PaymentCode NVARCHAR(MAX) UNIQUE,
    Amount DECIMAL(18,2),
    PaymentMethodId UNIQUEIDENTIFIER,
    TransactionCode NVARCHAR(255),
    PaymentDate DATETIME,
    Status INT, -- Pending, Partial, Completed, Failed, Refund
	OrderInfo NVARCHAR(MAX) NULL	,
	GatewayResponseCode NVARCHAR(255) NULL, 
    GatewayMessage NVARCHAR(255) NULL,   
    SignedHash NVARCHAR(MAX) NULL,
	IsConfirmed BIT DEFAULT 0, -- Xác nhận từ server VNPAY/MOMO chưa?
    ConfirmedAt DATETIME NULL,  -- Thời gian xác nhận
    Notes NTEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(UserProfileId) REFERENCES UserProfile(Id),
    FOREIGN KEY(TreatmentPlanStepId) REFERENCES TreatmentPlanStep(Id),
    FOREIGN KEY(PaymentMethodId) REFERENCES PaymentMethod(Id)
);

CREATE TABLE MediaFile (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PublicId NVARCHAR(MAX),
    Url NVARCHAR(MAX),
    SecureUrl NVARCHAR(MAX),
    Folder NVARCHAR(255),
    FileName NVARCHAR(255),
    FileType NVARCHAR(100), -- Document, Image, Video, etc.
    ResourceType NVARCHAR(50), -- image, video, raw 
    Format NVARCHAR(50),
    Size BIGINT,
    Width INT,
    Height INT,
    Duration DECIMAL(10,2), -- For videos
    Tags NVARCHAR(MAX),
    OwnerId UNIQUEIDENTIFIER NULL, -- User who uploaded
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(OwnerId) REFERENCES UserProfile(Id)
);

CREATE TABLE Feedback (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PatientId UNIQUEIDENTIFIER,
    DoctorId UNIQUEIDENTIFIER,
	TreatmentPlanId UNIQUEIDENTIFIER,
    Rating DECIMAL(3,1) CHECK (Rating BETWEEN 1 AND 5),
    TreatmentQualityRating DECIMAL(3,1) CHECK (TreatmentQualityRating BETWEEN 1 AND 5),
	Comment NTEXT,
    IsDisplayed BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY(PatientId) REFERENCES Patient(Id),
    FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
    FOREIGN KEY(TreatmentPlanId) REFERENCES TreatmentPlan(Id)
);

CREATE TABLE Blog (
    Id BIGINT IDENTITY(1000,1) PRIMARY KEY,
    UserProfileId UNIQUEIDENTIFIER,
    Title NVARCHAR(MAX),
    Summary NVARCHAR(MAX),
    Content NTEXT,
    FeaturedImageUrl NVARCHAR(MAX),
    MetaKeywords NVARCHAR(MAX),
    MetaDescription NVARCHAR(MAX),
    Status INT DEFAULT 2, -- Draft, Published, Archived
	FOREIGN KEY (UserProfileId) REFERENCES UserProfile (Id)
);


USE FertilityCareDB

SELECT * FROM DoctorSchedule

