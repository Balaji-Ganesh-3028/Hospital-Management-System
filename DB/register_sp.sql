CREATE OR ALTER PROCEDURE sp_register
  (
  @userName VARCHAR(150),
  @email VARCHAR(255),
  @passwordHash VARCHAR(255),
  @roleId INT
)
AS

BEGIN

  SET NOCOUNT ON;

  IF EXISTS (SELECT 1
  FROM UserDirectory
  WHERE email = @email)
  BEGIN
    SELECT @email AS Email, 'Exists' AS message
  END;

  ELSE
  INSERT INTO UserDirectory
    (UserName, Email, PasswordHash, RoleId)
  VALUES
    (@userName, @email, @passwordHash, @roleId);

  SELECT @userName AS UserName, 'Registered' AS message;
END;


EXEC sp_register 'Balaji', 'balaji@gmail.com', 'Admin@123', 1001;