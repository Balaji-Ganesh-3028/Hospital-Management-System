CREATE OR ALTER PROCEDURE sp_get_patient_details(
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
    pd.BloodGroup,
    pd.DOJ,
    pd.Allergies,
    pd.ChronicDiseases,
    pd.EmergencyContactName,
    pd.EmergencyContactNumber,
    pd.InsuranceProvider,
    pd.InsuranceProvider
  FROM UserDirectory AS ud
    INNER JOIN UserProfile AS up ON ud.Id = up.UserId
    INNER JOIN UserContactDetails AS ucd ON ud.Id = ucd.UserId
    INNER JOIN PatientDetails AS pd ON ud.Id = pd.UserId
  WHERE ud.Id = @UserId;
END;

EXEC sp_get_patient_details
  @UserId = 2;