CREATE OR ALTER PROCEDURE sp_get_doctor_details(
  @UserId INT
)
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    ud.Email,
    ud.Id AS UserId,
    ud.RoleId,
    ud.UserName,
    up.Firstname,
    up.Lastname,
    up.Gender,
    up.Age,
    up.CreatedBy,
    up.UpdatedBy,
    ucd.PhoneNumber,
    ucd.DoorFloorBuilding,
    ucd.AddressLine1,
    ucd.AddressLine2,
    ucd.State,
    ucd.City,
    ucd.Country,
    ucd.Pincode,
    dd.Id AS DoctorId,
    dd.DateOfAssociation,
    dd.LicenseNumber,
    dd.Qualification,
    mdq.Name AS QualificationName,
    dd.Specialisation,
    mds.Name AS SpecialisationName,
    dd.Designation,
    mdd.Name AS DesignationName,
    dd.ExperienceYears
  FROM UserDirectory AS ud
    INNER JOIN DoctorDetails AS dd ON dd.UserId = ud.Id
    INNER JOIN UserProfile AS up ON up.userId = ud.Id
    INNER JOIN UserContactDetails AS ucd ON ucd.userId = ud.Id
    INNER JOIN MasterData AS mdq ON mdq.Id = dd.Qualification
    INNER JOIN MasterData AS mds ON mds.Id = dd.Specialisation
    INNER JOIN MasterData AS mdd ON mdd.Id = dd.Designation
  WHERE ud.Id = @UserId;
END;

EXEC sp_get_doctor_details
  @UserId = 23;

UPDATE DoctorDetails
SET ExperienceYears = 15
WHERE UserId = 23;