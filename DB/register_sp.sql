CREATE OR ALTER PROCEDURE sp_register
  (
  @userName VARCHAR(150),
  @email VARCHAR(255),
  @passwordHash VARCHAR(255),
  @roleCode VARCHAR(50)
)
AS

BEGIN

  SET NOCOUNT ON;

  IF EXISTS (SELECT 1
  FROM UserDirectory
  WHERE email = @email)
  BEGIN
    SELECT @email AS email, 'Exists' AS message
  END;

  ELSE
  INSERT INTO UserDirectory
    (userName, email, passwordHash, roleCode)
  VALUES
    (@userName, @email, @passwordHash, @roleCode);

  SELECT @userName AS userName, 'Registered' AS message;
END;


EXEC sp_register 'Test User', 'testuser@gmail.com', 'Test@123', 'USER';