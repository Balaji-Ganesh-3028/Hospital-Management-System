USE HospitalManagementSystem

-- CREATE TABLES
-- Roles Table
-- List of roles
CREATE TABLE Roles
(
  Id INT PRIMARY KEY IDENTITY(1001, 1),
  RoleName VARCHAR(100) NOT NULL UNIQUE,
  RoleCode VARCHAR(50) NOT NULL UNIQUE,
  Description VARCHAR(255),
  Is_active BIT DEFAULT 1,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
)

-- User Directory Table
-- Stores user information
CREATE TABLE UserDirectory
(
  Id INT PRIMARY KEY IDENTITY(1, 1),
  UserName VARCHAR(150) NOT NULL,
  Email VARCHAR(255) NOT NULL UNIQUE,
  PasswordHash VARCHAR(255) NOT NULL,
  RoleId INT NOT NULL,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
)

-- User Profile Table
-- Stores addtional user profile information
CREATE TABLE UserProfile
(
  Id INT PRIMARY KEY IDENTITY(4000, 1),
  UserId INT FOREIGN KEY REFERENCES UserDirectory(id),
  Firstname VARCHAR(255) NOT NULL,
  Lastname VARCHAR(255) NULL,
  DOB DATE NOT NULL,
  Gender INT NOT NULL,
  Age INT NOT NULL,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
);

-- User Address Table 
-- Stores user address information
CREATE TABLE UserContactDetails
(
  ID INT PRIMARY KEY IDENTITY(5000,1),
  UserId INT NOT NULL FOREIGN KEY REFERENCES UserDirectory(ID),
  PhoneNumber NVARCHAR(15) NOT NULL,
  DoorFloorBuilding NVARCHAR(255) NOT NULL,
  AddressLine1 NVARCHAR(255) NOT NULL,
  AddressLine2 NVARCHAR(255) NULL,
  City NVARCHAR(255) NOT NULL,
  State NVARCHAR(255) NOT NULL,
  Country NVARCHAR(255) NOT NULL,
  Pincode NVARCHAR(15) NOT NULL,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
);

-- Patient Directory Table
-- Stores patient information
CREATE TABLE PatientDetails
(
  Id INT PRIMARY KEY IDENTITY(2000, 1),
  UserId INT FOREIGN KEY REFERENCES UserDirectory(id),
  DOJ DATE DEFAULT GETDATE(),
  BloodGroup INT NOT NULL,
  Allergies VARCHAR(255),
  ChronicDiseases VARCHAR(255),
  EmergencyContactName VARCHAR(150),
  EmergencyContactNumber VARCHAR(15),
  InsuranceProvider VARCHAR(255),
  InsuranceNumber VARCHAR(150),
  MedicalHistoryNotes NVARCHAR(255),
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
)

ALTER TABLE PatientDetails
ADD MedicalHistoryNotes NVARCHAR(255);


-- Doctor Directory Table
-- Stores doctor information
CREATE TABLE DoctorDetails
(
  ID INT PRIMARY KEY IDENTITY(6000,1),
  UserId INT NOT NULL FOREIGN KEY REFERENCES UserDirectory(ID),
  DateOfAssociation DATE NOT NULL,
  LicenseNumber NVARCHAR(100) NULL,
  Qualification VARCHAR(500) NOT NULL,
  Specialisation VARCHAR(500) NOT NULL,
  Designation INT NOT NULL,
  ExperienceYears INT NOT NULL,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
);


--Appointment Directory Table
-- Store appointment information
CREATE TABLE AppointmentDirectory
(
  ID INT PRIMARY KEY IDENTITY(7000,1),
  AppointmentDate DATETIME NOT NULL,
  PatientID INT NOT NULL FOREIGN KEY REFERENCES PatientDetails(ID),
  DoctorID INT NOT NULL FOREIGN KEY REFERENCES DoctorDetails(ID),
  PurposeOfVisit INT NOT NULL,
  IllnessOrDisease VARCHAR(500) NULL,
  ProceduresOrMedication VARCHAR(500) NULL,
  CurrentStatus INT NOT NULL,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
);

ALTER TABLE AppointmentDirectory
ALTER COLUMN AppointmentDate DATE NOT NULL;

EXEC sp_help 'AppointmentDirectory';

--HOW TO GET THE TABLE DETAILS IN DATATYPE
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'AppointmentDirectory';

-- Master Data Table
-- Stores master data for various categories
CREATE TABLE MasterData
(
  Id INT PRIMARY KEY IDENTITY(3000, 1),
  Name VARCHAR(255) NOT NULL UNIQUE,
  Value VARCHAR(255) NOT NULL UNIQUE,
  Category VARCHAR(150) NOT NULL,
  Description VARCHAR(255),
  Is_active BIT DEFAULT 1,
  CreatedAt DATETIME DEFAULT GETDATE(),
  UpdatedAt DATETIME DEFAULT GETDATE(),
  CreatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
  UpdatedBy NVARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
)

-- INSERT QUERIES
-- INSERT UserDirectory
INSERT INTO UserDirectory
  (UserName, Email, PasswordHash, RoleId)
VALUES
  ('Admin user', 'admin@gmail.com', 'Admin@123', 1001)

--INSERT Roles
INSERT INTO Roles
  (RoleName, RoleCode, Description)
VALUES
  ('Super Admin', 'SUPER_ADMIN', 'Super Admin role'),
  ('Admin', 'ADMIN', 'Admin role'),
  ('User', 'USER', 'User role'),
  ('Front Desk', 'FRONT_DESK', 'Front Desk role'),
  ('Doctor', 'DOCTOR', 'Doctor role'),
  ('Nurse', 'NURSE', 'Nurse role'),
  ('Patient', 'PATIENT', 'Patient role');

-- INSERT MasterData
INSERT INTO MasterData
  (Name, Value, Category, Description)
VALUES
  ('Male', 'M', 'gender', 'Male gender'),
  ('Female', 'F', 'gender', 'Female gender'),
  ('Others', 'Others', 'gender', 'Other genders'),
  ('A+', 'A+', 'blood_group', 'Blood group A+'),
  ('A-', 'A-', 'blood_group', 'Blood group A-'),
  ('B+', 'B+', 'blood_group', 'Blood group B+'),
  ('B-', 'B-', 'blood_group', 'Blood group B-'),
  ('AB+', 'AB+', 'blood_group', 'Blood group AB+'),
  ('AB-', 'AB-', 'blood_group', 'Blood group AB-'),
  ('O+', 'O+', 'blood_group', 'Blood group O+'),
  ('O-', 'O-', 'blood_group', 'Blood group O-'),
  ('Follow-up', 'FOLLOW_UP', 'appointment_type', 'Follow-up appointment'),
  ('New Patient', 'NEW_PATIENT', 'appointment_type', 'New patient appointment'),
  ('Consultation', 'CONSULTATION', 'appointment_type', 'Consultation appointment'),
  ('Emergency', 'EMERGENCY', 'appointment_type', 'Emergency appointment'),
  ('Routine Check-up', 'ROUTINE_CHECKUP', 'appointment_type', 'Routine check-up appointment'),

  ('Cardiology', 'CARD', 'specialisation', 'Heart and cardiovascular system'),
  ('Dermatology', 'DERM', 'specialisation', 'Skin, hair, and nail disorders'),
  ('Endocrinology', 'ENDO', 'specialisation', 'Hormonal and gland disorders (e.g., diabetes)'),
  ('Gastroenterology', 'GAST', 'specialisation', 'Digestive system and related organs'),
  ('General Medicine', 'GENMED', 'specialisation', 'Primary care and internal medicine'),
  ('General Surgery', 'GENSURG', 'specialisation', 'Common surgical procedures'),
  ('Neurology', 'NEUR', 'specialisation', 'Brain and nervous system'),
  ('Nephrology', 'NEPH', 'specialisation', 'Kidney and urinary system'),
  ('Orthopedics', 'ORTHO', 'specialisation', 'Bones, joints, muscles, and spine'),
  ('Pediatrics', 'PED', 'specialisation', 'Child and infant healthcare'),
  ('Psychiatry', 'PSY', 'specialisation', 'Mental health and behavioral disorders'),
  ('Pulmonology', 'PULM', 'specialisation', 'Lungs and respiratory system'),
  ('Radiology', 'RAD', 'specialisation', 'Imaging and diagnostic procedures'),
  ('Oncology', 'ONCO', 'specialisation', 'Cancer diagnosis and treatment'),
  ('Ophthalmology', 'OPHTH', 'specialisation', 'Eye and vision care'),
  ('Obstetrics & Gynecology (OB-GYN)', 'OBGYN', 'specialisation', 'Women’s reproductive health'),
  ('Urology', 'URO', 'specialisation', 'Urinary tract and male reproductive system'),
  ('ENT (Otolaryngology)', 'ENT', 'specialisation', 'Ear, Nose, and Throat disorders'),
  ('Anesthesiology', 'ANES', 'specialisation', 'Pain management and surgical anesthesia'),
  ('Dentistry', 'DENT', 'specialisation', 'Teeth and oral health'),


  ('MBBS', 'MBBS', 'qualification', 'Bachelor of Medicine and Bachelor of Surgery'),
  ('BDS', 'BDS', 'qualification', 'Bachelor of Dental Surgery'),
  ('MD (Doctor of Medicine)', 'MD', 'qualification', 'Postgraduate in general medicine or specialization'),
  ('MS (Master of Surgery)', 'MS', 'qualification', 'Postgraduate surgical specialization'),
  ('DM', 'DM', 'qualification', 'Super-specialization after MD (e.g., Cardiology, Neurology)'),
  ('MCh', 'MCH', 'qualification', 'Super-specialization after MS (e.g., Neurosurgery)'),
  ('DNB (Diplomate of National Board)', 'DNB', 'qualification', 'Equivalent to MD/MS, awarded by NBE'),
  ('BAMS', 'BAMS', 'qualification', 'Bachelor of Ayurvedic Medicine and Surgery'),
  ('BHMS', 'BHMS', 'qualification', 'Bachelor of Homeopathic Medicine and Surgery'),
  ('BPT', 'BPT', 'qualification', 'Bachelor of Physiotherapy'),
  ('MPT', 'MPT', 'qualification', 'Master of Physiotherapy'),
  ('PhD (Medical Science)', 'PHD', 'qualification', 'Doctorate in medical or health science'),
  ('PG Diploma in Clinical Medicine', 'PGDIP', 'qualification', 'Specialized postgraduate training'),
  ('MDS', 'MDS', 'qualification', 'Master of Dental Surgery'),
  ('MSc Nursing', 'MSC_NUR', 'qualification', 'Master of Science in Nursing'),
  ('BSc Nursing', 'BSC_NUR', 'qualification', 'Bachelor of Science in Nursing'),
  ('MBBS + MD', 'MBBS_MD', 'qualification', 'Combined degree with specialization'),
  ('MBBS + MS', 'MBBS_MS', 'qualification', 'Combined medical and surgical qualification'),
  ('MBBS + DNB', 'MBBS_DNB', 'qualification', 'Combined degree with board certification'),
  ('Diploma in Orthopedics / Pediatrics / etc.', 'DIP_MED', 'qualification', 'Short-term specialization diploma'),

  ('Intern', 'INTERN', 'designation', 'A medical graduate undergoing practical training under supervision'),
  ('Junior Resident', 'JR', 'designation', 'A postgraduate trainee doctor working under a senior consultant'),
  ('Senior Resident', 'SR', 'designation', 'A senior trainee doctor with experience after post-graduation'),
  ('Consultant', 'CONSULT', 'designation', 'A fully qualified specialist doctor responsible for patient care'),
  ('Senior Consultant', 'SR_CONSULT', 'designation', 'A highly experienced specialist overseeing junior doctors'),
  ('Attending Physician', 'ATTEND', 'designation', 'A physician responsible for all patient care decisions in a hospital'),
  ('Chief Consultant', 'CHIEF_CONSULT', 'designation', 'A lead specialist overseeing a medical department'),
  ('Medical Officer', 'MO', 'designation', 'A doctor responsible for providing general healthcare services'),
  ('Senior Medical Officer', 'SR_MO', 'designation', 'A senior doctor supervising junior medical officers'),
  ('Resident Medical Officer', 'RMO', 'designation', 'A doctor who manages inpatient care in hospitals or clinics'),
  ('Visiting Consultant', 'VISIT_CONSULT', 'designation', 'A specialist who provides periodic consultation services'),
  ('Assistant Professor', 'ASST_PROF', 'designation', 'A teaching doctor involved in academics and clinical practice'),
  ('Associate Professor', 'ASSOC_PROF', 'designation', 'A senior academic doctor with clinical and teaching responsibilities'),
  ('Professor', 'PROF', 'designation', 'A senior teaching doctor heading a medical department'),
  ('Head of Department', 'HOD', 'designation', 'A senior-most doctor managing and supervising a medical department'),
  ('Medical Superintendent', 'MED_SUPT', 'designation', 'A senior doctor responsible for hospital administration'),
  ('Chief Medical Officer', 'CMO', 'designation', 'The top medical executive overseeing clinical operations'),
  ('Director of Medical Services', 'DIR_MED', 'designation', 'The senior-most executive overseeing all medical departments'),
  ('Surgeon', 'SURG', 'designation', 'A doctor specialized in performing surgical operations'),
  ('Senior Surgeon', 'SR_SURG', 'designation', 'An experienced surgeon responsible for complex surgeries and mentoring juniors');

INSERT INTO MasterData
  (Name, Value, Category, Description)
VALUES
  ('Scheduled', 'Scheduled', 'appointment_status', 'The appointment is scheduled and confirmend.'),
  ('Completed', 'Completed', 'appointment_status', 'The appointment has been successfully completed.'),
  ('Cancelled', 'Cancelled', 'appointment_status', 'The appointment has been cancelled by either party.'),
  ('No-Show', 'No-Show', 'appointment_status', 'The patient did not show up for the scheduled appointment.'),
  ('Rescheduled', 'Rescheduled', 'appointment_status', 'The appointment has been rescheduled to a different date/time.');

INSERT INTO MasterData
  (Name, Value, Category, Description)
VALUES
  ('Patient', 'Patient', 'user_type', 'The user is a patient.'),
  ('Doctor', 'Doctor', 'user_type', 'The user is a doctor.'),
  ('Nurse', 'Nurse', 'user_type', 'The user is a nurse.');

-- SELECT QUERIES
SELECT *
FROM UserDirectory;

SELECT *
FROM UserProfile;

SELECT *
FROM UserContactDetails;

SELECT *
FROM PatientDetails;

SELECT *
FROM DoctorDetails;

SELECT *
FROM AppointmentDirectory;

SELECT *
FROM Roles;

SELECT *
FROM MasterData;

SELECT *
FROM MasterData
WHERE Category = 'designation';

SELECT *
FROM MasterData
WHERE Category = 'appointment_type';

SELECT *
FROM MasterData
WHERE Category = 'appointment_status';

SELECT *
FROM MasterData
WHERE Category = 'gender';

SELECT *
FROM MasterData
WHERE Category = 'blood_group';


SELECT *
FROM MasterData
WHERE Category = 'blood_group';


-- DROP TABLES
DROP TABLE UserDirectory
DROP TABLE Roles
DROP TABLE UserProfile
DROP TABLE UserContactDetails
DROP TABLE PatientDetails
DROP TABLE DoctorDetails
DROP TABLE MasterData
DROP TABLE AppointmentDirectory

TRUNCATE TABLE PatientDetails;


DELETE FROM PatientDetails;

UPDATE UserDirectory 
SET
  RoleId = 1007
WHERE id = 2;