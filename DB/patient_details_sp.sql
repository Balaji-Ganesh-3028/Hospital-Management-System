CREATE OR ALTER PROCEDURE sp_update_patient_details(
  @UserId INT ,
  @DOJ DATE,
  @bloodGroup INT,
  @allergies VARCHAR(255),
  @chronicDiseases VARCHAR(255),
  @emergencyContactName VARCHAR(150),
  @emergencyContactNumber VARCHAR(15),
  @insuranceProvider VARCHAR(255),
  @insuranceNumber VARCHAR(150),
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
  -- UPDATE PatientDetails if exists
  IF EXISTS (SELECT 1
  FROM PatientDetails
  WHERE UserId = @UserId)
  BEGIN
    UPDATE PatientDetails
      SET 
        DOJ = @DOJ,
        BloodGroup = @bloodGroup,
        Allergies = @allergies,
        ChronicDiseases = @chronicDiseases,
        EmergencyContactName = @emergencyContactName,
        EmergencyContactNumber = @emergencyContactNumber,
        InsuranceProvider = @insuranceProvider,
        InsuranceNumber = @insuranceNumber,
        UpdatedAt = @UpdatedAt,
        UpdatedBy = @UpdatedBy
      WHERE UserId = @UserId;
  END;

  -- INSERT PatientDetails if not exists
  ELSE
  BEGIN
    INSERT INTO PatientDetails
      (
      UserId,
      DOJ,
      BloodGroup,
      Allergies,
      ChronicDiseases,
      EmergencyContactName,
      EmergencyContactNumber,
      InsuranceProvider,
      InsuranceNumber,
      CreatedBy
      )
    VALUES
      (
        @UserId,
        @DOJ,
        @bloodGroup,
        @allergies,
        @chronicDiseases,
        @emergencyContactName,
        @emergencyContactNumber,
        @insuranceProvider,
        @insuranceNumber,
        @CreatedBy
    );
  END;
  COMMIT TRANSACTION;
END;

EXEC sp_update_patient_details
  @UserId = 3,
  @DOJ = '2024-01-15',
  @bloodGroup = 3005,
  @allergies = 'Peanuts',
  @chronicDiseases = 'Asthma',
  @emergencyContactName = 'Jane Doe',
  @emergencyContactNumber = '9876543210',
  @insuranceProvider = 'HealthSecure',
  @insuranceNumber = 'HS123456789',
  @CreatedBy = 'admin@gmail.com';