CREATE OR ALTER PROCEDURE sp_delete_user_details
  (
  @UserId INT
)
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @UserType VARCHAR(100);
  DECLARE @PatientId INT;
  DECLARE @DoctorId INT;

  BEGIN TRANSACTION;

  SELECT @UserType = Roles.RoleName, @PatientId = pd.id, @DoctorId = dd.id
  FROM UserDirectory AS ud
    INNER JOIN Roles ON ud.RoleId = Roles.Id
    INNER JOIN PatientDetails AS pd ON pd.UserId = ud.Id
    INNER JOIN DoctorDetails AS dd ON dd.UserId = ud.Id
  WHERE ud.id = @UserId;

  -- Check if user exists
  IF @UserType IS NULL
  BEGIN
    ROLLBACK TRANSACTION;
    SELECT
      'User not found' AS Message;
    RETURN;
  END;


  -- Delete from the Appointment table
  DELETE FROM AppointmentDirectory
  WHERE PatientID = @PatientId AND DoctorID = @DoctorId;

  -- Delete from Patient table if user is a patient
  IF @UserType = 'Patient'
    BEGIN
    DELETE FROM PatientDetails 
      WHERE UserId = @UserId;
  END;

  -- Delete from Doctor table if user is a doctor
  IF @UserType = 'Doctor'
    BEGIN
    DELETE FROM DoctorDetails 
      WHERE UserId = @UserId;
  END;

  -- Delete from UserContactDetails table
  DELETE FROM UserContactDetails 
  WHERE UserId = @UserId;

  -- Delete from UserProfile table
  DELETE FROM UserProfile 
  WHERE UserId = @UserId;

  -- Delete from UserDirectory table
  DELETE FROM UserDirectory
  WHERE id = @UserId;

  SELECT
    'Success' AS Message,
    @UserId AS DeletedUserId,
    @UserType AS UserType;

  COMMIT TRANSACTION;
END;

EXEC sp_delete_user_details @UserId = 2