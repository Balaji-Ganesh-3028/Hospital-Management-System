CREATE OR ALTER PROCEDURE sp_get_user_profile_details(
  @UserId INT
)
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    ud.Id AS UserId,
    ud.Email,
    ud.RoleId,
    r.RoleName,
    ud.UserName,
    up.Firstname,
    up.Lastname,
    up.Gender,
    md.Name AS GenderValue,
    up.Age,
    up.DOB,
    up.CreatedBy,
    up.UpdatedBy,
    ucd.PhoneNumber,
    ucd.DoorFloorBuilding,
    ucd.AddressLine1,
    ucd.AddressLine2,
    ucd.State,
    ucd.City,
    ucd.Country,
    ucd.Pincode
  FROM UserDirectory AS ud
    INNER JOIN UserProfile AS up ON ud.Id = up.UserId
    INNER JOIN UserContactDetails AS ucd ON ud.Id = ucd.UserId
    INNER JOIN MasterData AS md ON up.Gender = md.Id
    INNER JOIN Roles AS r ON ud.RoleId = r.Id
  WHERE ud.Id = @UserId;
END;

EXEC sp_get_user_profile_details
  @UserId = 2;