CREATE OR ALTER PROCEDURE sp_delete_user_details
  (
  @UserId INT
)
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @UserType VARCHAR(100);
  DECLARE @PatientId INT = NULL;
  DECLARE @DoctorId INT = NULL;

  BEGIN TRY
    BEGIN TRANSACTION;

    -- Get the user type (Doctor / Patient / Other)
    SELECT
    @UserType = R.RoleName
  FROM UserDirectory AS ud
    INNER JOIN Roles AS R ON ud.RoleId = R.Id
  WHERE ud.Id = @UserId;

    -- If no user found
    IF @UserType IS NULL
    BEGIN
    ROLLBACK TRANSACTION;
    SELECT 'User not found' AS Message;
    RETURN;
  END;

    -- Get IDs from respective tables if exist
    SELECT @PatientId = Id
  FROM PatientDetails
  WHERE UserId = @UserId;
    SELECT @DoctorId  = Id
  FROM DoctorDetails
  WHERE UserId = @UserId;

    -- Delete related appointments
    IF @UserType = 'Patient'
    BEGIN
    DELETE FROM AppointmentDirectory WHERE PatientID = @PatientId;
    DELETE FROM PatientDetails WHERE UserId = @UserId;
  END;

    IF @UserType = 'Doctor'
    BEGIN
    DELETE FROM AppointmentDirectory WHERE DoctorID = @DoctorId;
    DELETE FROM DoctorDetails WHERE UserId = @UserId;
  END;

    -- Delete from contact and profile
    DELETE FROM UserContactDetails WHERE UserId = @UserId;
    DELETE FROM UserProfile WHERE UserId = @UserId;

    -- Finally delete the user
    DELETE FROM UserDirectory WHERE Id = @UserId;

    COMMIT TRANSACTION;

    SELECT
    'Success' AS Message,
    @UserId AS DeletedUserId,
    @UserType AS UserType;
  END TRY

  BEGIN CATCH
    ROLLBACK TRANSACTION;

    SELECT
    'Error' AS Message,
    ERROR_MESSAGE() AS ErrorMessage;
  END CATCH;
END;


EXECUTE sp_delete_user_details @UserId = 9