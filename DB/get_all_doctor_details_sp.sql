CREATE OR ALTER PROCEDURE sp_get_all_doctor_details
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
    dd.id AS DoctorId,
    dd.DateOfAssociation,
    dd.LicenseNumber,
    dd.Qualification,
    mdq.Name AS QualificationName,
    dd.Specialisation,
    mds.Name AS SpecialisationName,
    dd.Designation,
    mdd.Name AS DesignationName,
    dd.ExperienceYears
  FROM DoctorDetails AS dd
    INNER JOIN UserDirectory AS ud ON ud.Id = dd.UserId
    INNER JOIN UserProfile AS up ON up.userId = dd.UserId
    INNER JOIN UserContactDetails AS ucd ON ucd.userId = dd.UserId
    INNER JOIN MasterData AS mdq ON mdq.Id = dd.Qualification
    INNER JOIN MasterData AS mds ON mds.Id = dd.Specialisation
    INNER JOIN MasterData AS mdd ON mdd.Id = dd.Designation
END;

EXEC sp_get_all_doctor_details;