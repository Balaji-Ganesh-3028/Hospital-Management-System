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


  DECLARE @InsertedId INT;

  IF EXISTS (
      SELECT 1
  FROM UserDirectory
  WHERE Email = @email
  )
  BEGIN
    SELECT
      Id,
      Email,
      'Exists' AS Message
    FROM UserDirectory
    WHERE Email = @email;
  END;

  ELSE
  BEGIN
    INSERT INTO UserDirectory
      (UserName, Email, PasswordHash, RoleId)
    VALUES
      (@userName, @email, @passwordHash, @roleId);

    SET @InsertedId = SCOPE_IDENTITY();

    SELECT
      @InsertedId AS Id,
      @userName AS UserName,
      'Registered' AS Message;
  END;
END;


EXEC sp_register 'Balaji', 'balaji@gmail.com', 'Admin@123', 1001;