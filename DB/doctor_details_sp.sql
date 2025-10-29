CREATE OR ALTER PROCEDURE sp_update_doctor_details(
  @UserId INT ,
  @dateOfAssociation DATE,
  @licenseNumber NVARCHAR(100),
  @qualification VARCHAR(500),
  @specialisation VARCHAR(500),
  @designation INT,
  @experienceYears INT,
  @updatedAt DATETIME = NULL,
  @createdBy NVARCHAR(255) = 'SYSTEM',
  @updatedBy NVARCHAR(255) = 'SYSTEM'
)
AS
BEGIN

  SET NOCOUNT ON;

  IF @UpdatedAt IS NULL 
  SET @UpdatedAt = GETDATE();
  BEGIN TRANSACTION;

  -- Check if user contact record exists
  -- UPDATE DoctorDetails if exists
  IF EXISTS (SELECT 1
  FROM DoctorDetails
  WHERE UserId = @UserId)
  BEGIN
    UPDATE DoctorDetails
      SET 
        DateOfAssociation = @dateOfAssociation,
        LicenseNumber = @licenseNumber,
        Qualification = @qualification,
        Specialisation = @specialisation,
        Designation = @designation,
        ExperienceYears = @experienceYears,
        UpdatedAt = @UpdatedAt,
        UpdatedBy = @UpdatedBy
      WHERE UserId = @UserId;
  END;

  -- INSERT DoctorDetails if not exists
  ELSE
  BEGIN
    INSERT INTO DoctorDetails
      (
      UserId,
      DateOfAssociation,
      LicenseNumber,
      Qualification,
      Specialisation,
      Designation,
      ExperienceYears,
      CreatedBy
      )
    VALUES
      (
        @UserId,
        @dateOfAssociation,
        @licenseNumber,
        @qualification,
        @specialisation,
        @designation,
        @experienceYears,
        @CreatedBy
    );
  END;
  COMMIT TRANSACTION;
END;

EXEC sp_update_doctor_details
  @UserId = 2,
  @DateOfAssociation = '2024-01-15',
  @licenseNumber = 3005,
  @qualification = 3036,
  @specialisation = 3016,
  @designation = 3060,
  @experienceYears = 5,
  @CreatedBy = 'admin@gmail.com';