CREATE OR ALTER PROCEDURE sp_update_user_contact_details(
  @UserId INT ,
  @phoneNumber NVARCHAR(15),
  @doorFloorBuilding NVARCHAR(255),
  @addressLine1 NVARCHAR(255),
  @addressLine2 NVARCHAR(255),
  @city NVARCHAR(255),
  @state NVARCHAR(255),
  @country NVARCHAR(255),
  @pincode NVARCHAR(15),
  @updatedAt DATETIME = NULL,
  @createdBy NVARCHAR(255) = 'SYSTEM',
  @updatedBy NVARCHAR(255) = 'SYSTEM'
)
AS
BEGIN

  SET NOCOUNT ON;

  IF @UpdatedAt IS NULL 
  SET @UpdatedAt = GETDATE();
  BEGIN TRANSACTION;

  -- Check if user contact record exists
  IF EXISTS (SELECT 1
  FROM UserContactDetails
  WHERE UserId = @UserId)
        BEGIN
    UPDATE UserContactDetails
          SET 
            PhoneNumber = @PhoneNumber,
            DoorFloorBuilding = @DoorFloorBuilding,
            AddressLine1 = @AddressLine1,
            AddressLine2 = @AddressLine2,
            City = @City,
            State = @State,
            Country = @Country,
            Pincode = @Pincode,
            UpdatedAt = @UpdatedAt,
            UpdatedBy = @UpdatedBy
          WHERE UserId = @UserId;
  END
        ELSE
        BEGIN
    INSERT INTO UserContactDetails
      (
      UserId,
      PhoneNumber,
      DoorFloorBuilding,
      AddressLine1,
      AddressLine2,
      City,
      State,
      Country,
      Pincode,
      CreatedAt,
      CreatedBy
      )
    VALUES
      (
        @UserId,
        @PhoneNumber,
        @DoorFloorBuilding,
        @AddressLine1,
        @AddressLine2,
        @City,
        @State,
        @Country,
        @Pincode,
        GETDATE(),
        @CreatedBy
          );
  END;
  COMMIT TRANSACTION;
END;

EXEC sp_update_user_contact_details 
  2,
  '1234567890',
  '12B, Sunshine Apartments',
  '123 Main St',
  'Apt 4C',
  'Springfield',
  'Illinois',
  'USA',
  '62704',
   NULL,
  'admin@gamil.com';