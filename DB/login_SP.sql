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
    SELECT Email, ud.Id, roleId, UserName, RoleName, 'Login successful' AS message
    FROM UserDirectory AS ud
      INNER JOIN Roles ON ud.roleId = Roles.id
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

EXEC sp_login 'admin@gmail.com', 'Admin@123';