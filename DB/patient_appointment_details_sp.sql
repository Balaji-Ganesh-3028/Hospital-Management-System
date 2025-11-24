CREATE OR ALTER PROCEDURE sp_update_patient_appointment_details(
  @id INT,
  @appointmentDate DATE,
  @patientID INT,
  @doctorID INT,
  @purposeOfVisit INT,
  @illnessOrDisease VARCHAR(500),
  @proceduresOrMedication VARCHAR(500),
  @currentStatus INT,
  @updatedAt DATETIME = NULL,
  @createdBy NVARCHAR(255) = 'SYSTEM',
  @updatedBy NVARCHAR(255) = 'SYSTEM'
)
AS
BEGIN

  -- SET NOCOUNT ON;

  IF @UpdatedAt IS NULL 
  SET @UpdatedAt = GETDATE();
  BEGIN TRANSACTION;

  -- Check if user contact record exists
  -- UPDATE AppointmentDirectory if exists
  IF EXISTS (SELECT 1
  FROM AppointmentDirectory
  WHERE ID = @id)
  BEGIN
    UPDATE AppointmentDirectory
      SET 
        AppointmentDate = @appointmentDate,
        PatientID = @patientID,
        DoctorID = @doctorID,
        PurposeOfVisit = @purposeOfVisit,
        IllnessOrDisease = @illnessOrDisease,
        ProceduresOrMedication = @proceduresOrMedication,
        CurrentStatus = @currentStatus,
        UpdatedAt = @UpdatedAt,
        UpdatedBy = @UpdatedBy
      WHERE ID = @id;
  END;

  -- INSERT AppointmentDirectory if not exists
  ELSE
  BEGIN
    INSERT INTO AppointmentDirectory
      (
      AppointmentDate,
      PatientID,
      DoctorID,
      PurposeOfVisit,
      IllnessOrDisease,
      ProceduresOrMedication,
      CurrentStatus,
      CreatedBy
      )
    VALUES
      (
        @appointmentDate,
        @patientID,
        @doctorID,
        @purposeOfVisit,
        @illnessOrDisease,
        @proceduresOrMedication,
        @currentStatus,
        @CreatedBy
    );
  END;
  COMMIT TRANSACTION;
END;

EXEC sp_update_patient_appointment_details
  @id = 0,
  @appointmentDate = '2024-02-20 10:30:00',
  @patientID = 2000,
  @doctorID = 6002,
  @purposeOfVisit = 3012,
  @illnessOrDisease = 'Headache and fever',
  @proceduresOrMedication = 'Paracetamol 500mg',
  @currentStatus = 3076;