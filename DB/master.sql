USE HospitalManagementSystem

-- CREATE TABLES
CREATE TABLE Roles
(
  id INT PRIMARY KEY IDENTITY(1001, 1),
  roleName VARCHAR(100) NOT NULL UNIQUE,
  roleCode VARCHAR(50) NOT NULL UNIQUE,
  description VARCHAR(255),
  is_active BIT DEFAULT 1,
  createdAt DATETIME DEFAULT GETDATE(),
  updatedAt DATETIME DEFAULT GETDATE()
)


CREATE TABLE UserDirectory
(
  id INT PRIMARY KEY IDENTITY(1, 1),
  userName VARCHAR(150) NOT NULL,
  email VARCHAR(255) NOT NULL UNIQUE,
  passwordHash VARCHAR(255) NOT NULL,
  roleCode VARCHAR(50) NOT NULL UNIQUE,
  createdAt DATETIME DEFAULT GETDATE(),
  updatedAt DATETIME DEFAULT GETDATE()
)


-- INSERT QUERIES
-- INSERT UserDirectory
INSERT INTO UserDirectory
  (userName, email, passwordHash, roleCode)
VALUES
  ('Admin user', 'admin@gmail.com', 'Admin@123', 'ADMIN')

--INSERT Roles
INSERT INTO Roles
  (roleName, roleCode, description)
VALUES
  ('Super Admin', 'SUPER_ADMIN', 'Super Admin role'),
  ('Admin', 'ADMIN', 'Admin role'),
  ('User', 'USER', 'User role'),
  ('Front Desk', 'FRONT_DESK', 'Front Desk role'),
  ('Doctor', 'DOCTOR', 'Doctor role'),
  ('Nurse', 'NURSE', 'Nurse role'),
  ('Patient', 'PATIENT', 'Patient role');






-- SELECT QUERIES
SELECT *
FROM UserDirectory;

SELECT *
FROM Roles;


-- DROP TABLES
DROP TABLE UserDirectory
DROP TABLE Roles