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
  WHERE email = @email AND passwordHash = @password)

  BEGIN
    SELECT 'Login successful' AS message
  END

  -- If email exist, but password is mismatch
  ELSE IF EXISTS ( SELECT 1
  FROM UserDirectory
  WHERE email = @email)
  
  BEGIN
    SELECT @email AS email, 'Mismatch' AS message;
  END
  
  -- If email is not found 
  ELSE
  BEGIN
    SELECT @email AS email, 'NotFound' AS message;
  END
END;

EXEC sp_login 'admin@gmail.com', 'Admin@123';