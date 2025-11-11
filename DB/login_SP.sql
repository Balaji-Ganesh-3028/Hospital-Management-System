CREATE OR ALTER PROCEDURE sp_login
  (
  @email VARCHAR(255),
  @password VARCHAR(255)
)
AS
BEGIN
  SET NOCOUNT ON

  IF EXISTS ( SELECT 1
  FROM UserDirectory
  WHERE Email = @email AND PasswordHash = @password)

  BEGIN
    SELECT Email, ud.Id, roleId, UserName, RoleName, 'Login successful' AS message,
      CASE
      WHEN pd.id IS NOT NULL THEN 'Patient'
      WHEN dd.id IS NOT NULL THEN 'Doctor'
      ELSE 'User'
    END AS UserType,
      CASE
      WHEN pd.id IS NOT NULL THEN pd.Id
      WHEN dd.id IS NOT NULL THEN dd.ID
      ELSE 0
    END AS UserTypeId
    FROM UserDirectory AS ud
      INNER JOIN Roles ON ud.roleId = Roles.id
      LEFT JOIN PatientDetails AS pd ON pd.UserId = ud.Id
      LEFT JOIN DoctorDetails AS dd ON dd.UserId = ud.Id
    WHERE Email = @email AND PasswordHash = @password
  END

  -- If email exist, but password is mismatch
  ELSE IF EXISTS ( SELECT 1
  FROM UserDirectory
  WHERE email = @email)
  
  BEGIN
    SELECT @email AS Email, 'Mismatch' AS message;
  END
  
  -- If email is not found 
  ELSE
  BEGIN
    SELECT @email AS Email, 'NotFound' AS message;
  END
END;

EXEC sp_login 'balaji@gmail.com', 'Admin@123';