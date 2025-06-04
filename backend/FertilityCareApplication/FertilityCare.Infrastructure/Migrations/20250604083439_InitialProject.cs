using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientPartner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FullName = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPartner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                    Description = table.Column<string>(type: "NTEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    MiddleName = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TreatmentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Description = table.Column<string>(type: "NTEXT", nullable: true),
                    EstimatePrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    Duration = table.Column<int>(type: "INT", nullable: true),
                    SuccessRate = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: true),
                    RecommendedFor = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Contraindications = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentService_TreatmentCategory",
                        column: x => x.TreatmentCategoryId,
                        principalTable: "TreatmentCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_UserProfile",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "DECIMAL(3,2)", nullable: true),
                    PatientsServed = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_UserProfile",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Format = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFile_UserProfile",
                        column: x => x.OwnerId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalHistory = table.Column<string>(type: "NTEXT", nullable: true),
                    PatientParnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Partner",
                        column: x => x.PatientParnerId,
                        principalTable: "PatientPartner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patient_UserProfile",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentStep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    TreatmentServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepName = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "NVARCHAR(255)"),
                    Description = table.Column<string>(type: "NTEXT", nullable: true),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    EstimatedDurationDays = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentStep_TreatmentService",
                        column: x => x.TreatmentServiceId,
                        principalTable: "TreatmentService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    MaxAppointments = table.Column<int>(type: "int", nullable: true, defaultValue: 10),
                    IsAcceptingPatients = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedule_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "GETDATE()"),
                    EndDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentPlan_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentPlan_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreatmentPlan_TreatmentService",
                        column: x => x.TreatmentServiceId,
                        principalTable: "TreatmentService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    TreatmentServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingEmail = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    BookingPhone = table.Column<string>(type: "NVARCHAR(12)", maxLength: 12, nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CancellationReason = table.Column<string>(type: "NVARCHAR(512)", maxLength: 512, nullable: true),
                    Note = table.Column<string>(type: "NTEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointment_DoctorSchedule",
                        column: x => x.DoctorScheduleId,
                        principalTable: "DoctorSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointment_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_TreatmentService",
                        column: x => x.TreatmentServiceId,
                        principalTable: "TreatmentService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EggRetrievalCycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CycleNumber = table.Column<int>(type: "int", nullable: false),
                    RetrievalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalEggsRetrieved = table.Column<int>(type: "int", nullable: false),
                    MatureEggs = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ImmatureEggs = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AbnormalEggs = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EggRetrievalCycle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EggRetrievalCycle_ServicePackagePlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    TreatmentQualityRating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    Note = table.Column<string>(type: "NTEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescription_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    Note = table.Column<string>(type: "NTEXT", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResult_ServicePackagePlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlanStep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentStepId = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: false),
                    StepPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    StartDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    EndDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlanStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentPlanStep_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreatmentPlanStep_TreatmentStep",
                        column: x => x.TreatmentStepId,
                        principalTable: "TreatmentStep",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentReminder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReminderDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ReminderMethod = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentReminder_Appointment",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalExamination",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symptoms = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, defaultValue: ""),
                    Diagnosis = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, defaultValue: ""),
                    Indications = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, defaultValue: ""),
                    Note = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalExamination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalExamination_Appointment",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbryoFertilization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EggRetrievalCycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FertilizationMethod = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    FertilizationDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalEggsUsed = table.Column<int>(type: "int", nullable: false),
                    TotalEggsFertilized = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    TotalEmbryosFormed = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbryoFertilization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbryoFertilization_EggRetrievalCycle",
                        column: x => x.EggRetrievalCycleId,
                        principalTable: "EggRetrievalCycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbryoFertilization_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Dosage = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    StartDate = table.Column<DateOnly>(type: "DATE", nullable: false, defaultValueSql: "GETDATE()"),
                    EndDate = table.Column<DateOnly>(type: "DATE", nullable: true),
                    SpecialInstructions = table.Column<string>(type: "NTEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionItem_Prescription",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanStepId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentPayment_PaymentMethod",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentPayment_TreatmentPlanStep",
                        column: x => x.TreatmentPlanStepId,
                        principalTable: "TreatmentPlanStep",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TreatmentPayment_UserProfile",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmbryoDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmbryoFertilizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    IsViable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbryoDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbryoDetail_EmbryoFertilization",
                        column: x => x.EmbryoFertilizationId,
                        principalTable: "EmbryoFertilization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmbryoDetail_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmbryoTransfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmbryoDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFrozenTransfer = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TransferDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PregnancyResultNote = table.Column<string>(type: "NTEXT", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeCharged = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    Note = table.Column<string>(type: "NTEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbryoTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbryoTransfer_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmbryoTransfer_EmbryoDetail",
                        column: x => x.EmbryoDetailId,
                        principalTable: "EmbryoDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmbryoTransfer_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FrozenEmbryoStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmbryoDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StorageEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StorageTank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FreezeMethod = table.Column<int>(type: "int", nullable: false),
                    MonthlyStorageFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SurvivalAfterThaw = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrozenEmbryoStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrozenEmbryoStorage_EmbryoDetail",
                        column: x => x.EmbryoDetailId,
                        principalTable: "EmbryoDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FrozenEmbryoStorage_TreatmentPlan",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorScheduleId",
                table: "Appointment",
                column: "DoctorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_TreatmentServiceId",
                table: "Appointment",
                column: "TreatmentServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentReminder_AppointmentId",
                table: "AppointmentReminder",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileId",
                table: "AspNetUsers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_UserProfileId",
                table: "Blog",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserProfileId",
                table: "Doctor",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_DoctorId",
                table: "DoctorSchedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_EggRetrievalCycle_TreatmentPlanId",
                table: "EggRetrievalCycle",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoDetail_EmbryoFertilizationId",
                table: "EmbryoDetail",
                column: "EmbryoFertilizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoDetail_TreatmentPlanId",
                table: "EmbryoDetail",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoFertilization_EggRetrievalCycleId",
                table: "EmbryoFertilization",
                column: "EggRetrievalCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoFertilization_TreatmentPlanId",
                table: "EmbryoFertilization",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoTransfer_DoctorId",
                table: "EmbryoTransfer",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoTransfer_EmbryoDetailId",
                table: "EmbryoTransfer",
                column: "EmbryoDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbryoTransfer_TreatmentPlanId",
                table: "EmbryoTransfer",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_DoctorId",
                table: "Feedback",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_PatientId",
                table: "Feedback",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_TreatmentPlanId",
                table: "Feedback",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FrozenEmbryoStorage_EmbryoDetailId",
                table: "FrozenEmbryoStorage",
                column: "EmbryoDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FrozenEmbryoStorage_TreatmentPlanId",
                table: "FrozenEmbryoStorage",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFile_OwnerId",
                table: "MediaFile",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExamination_AppointmentId",
                table: "MedicalExamination",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientParnerId",
                table: "Patient",
                column: "PatientParnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserProfileId",
                table: "Patient",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_TreatmentPlanId",
                table: "Prescription",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItem_PrescriptionId",
                table: "PrescriptionItem",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TreatmentPlanId",
                table: "TestResult",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPayment_PaymentCode",
                table: "TreatmentPayment",
                column: "PaymentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPayment_PaymentMethodId",
                table: "TreatmentPayment",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPayment_TreatmentPlanStepId",
                table: "TreatmentPayment",
                column: "TreatmentPlanStepId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPayment_UserProfileId",
                table: "TreatmentPayment",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlan_DoctorId",
                table: "TreatmentPlan",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlan_PatientId",
                table: "TreatmentPlan",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlan_TreatmentServiceId",
                table: "TreatmentPlan",
                column: "TreatmentServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlanStep_TreatmentPlanId",
                table: "TreatmentPlanStep",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlanStep_TreatmentStepId",
                table: "TreatmentPlanStep",
                column: "TreatmentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentService_TreatmentCategoryId",
                table: "TreatmentService",
                column: "TreatmentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentStep_TreatmentServiceId",
                table: "TreatmentStep",
                column: "TreatmentServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentReminder");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "EmbryoTransfer");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FrozenEmbryoStorage");

            migrationBuilder.DropTable(
                name: "MediaFile");

            migrationBuilder.DropTable(
                name: "MedicalExamination");

            migrationBuilder.DropTable(
                name: "PrescriptionItem");

            migrationBuilder.DropTable(
                name: "TestResult");

            migrationBuilder.DropTable(
                name: "TreatmentPayment");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EmbryoDetail");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "TreatmentPlanStep");

            migrationBuilder.DropTable(
                name: "EmbryoFertilization");

            migrationBuilder.DropTable(
                name: "DoctorSchedule");

            migrationBuilder.DropTable(
                name: "TreatmentStep");

            migrationBuilder.DropTable(
                name: "EggRetrievalCycle");

            migrationBuilder.DropTable(
                name: "TreatmentPlan");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "TreatmentService");

            migrationBuilder.DropTable(
                name: "PatientPartner");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "TreatmentCategory");
        }
    }
}
