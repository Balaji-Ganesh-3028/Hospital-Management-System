CREATE OR ALTER PROCEDURE sp_update_user_profile(
  @userId INT,
  @firstName VARCHAR(255),
  @lastName VARCHAR(255),
  @DOB DATE,
  @gender INT,
  @age INT,
  @UserType INT,
  @CreatedBy NVARCHAR(255) = 'SYSTEM',
  @UpdatedAt DATETIME = NULL,
  @UpdatedBy NVARCHAR(255) = 'SYSTEM'
)
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @CreatedAt DATETIME = GETDATE();

  IF @UpdatedAt IS NULL 
      SET @UpdatedAt = GETDATE();

  -- Procedure logic goes here
  BEGIN TRANSACTION;

  IF EXISTS (SELECT 1
  FROM UserProfile
  WHERE UserId = @userId)
      BEGIN
    -- If record exists, update it
    UPDATE UserProfile
        SET 
          FirstName = @firstName,
          LastName = @lastName,
          DOB = @DOB,
          Gender = @gender,
          Age = @age,
          UpdatedAt = @UpdatedAt,
          UpdatedBy = @UpdatedBy
        WHERE UserId = @userId;
  END
      ELSE
      BEGIN
    -- If record doesn’t exist, insert new
    INSERT INTO UserProfile
      (
      UserId,
      FirstName,
      LastName,
      DOB,
      Gender,
      Age,
      CreatedAt,
      CreatedBy,
      UpdatedAt,
      UpdatedBy
      )
    VALUES
      (
        @userId,
        @firstName,
        @lastName,
        @DOB,
        @gender,
        @age,
        @CreatedAt,
        @CreatedBy,
        @UpdatedAt,
        @UpdatedBy
        );
  END;

  COMMIT TRANSACTION;
END;

EXEC sp_update_user_profile
  @userId = 2,
  @firstName = 'John',
  @lastName = 'Doe Updated 1',
  @DOB = '1990-01-01',
  @gender = 3000,
  @age = 34;