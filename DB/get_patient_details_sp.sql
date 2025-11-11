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
    pd.Id AS PatientId,
    pd.UserId,
    pd.BloodGroup,
    pd.DOJ,
    pd.Allergies,
    pd.ChronicDiseases,
    pd.EmergencyContactName,
    pd.EmergencyContactNumber,
    pd.InsuranceProvider,
    pd.InsuranceNumber,
    pd.MedicalHistoryNotes
  FROM UserDirectory AS ud
    INNER JOIN PatientDetails AS pd ON pd.UserId = ud.Id
    INNER JOIN UserProfile AS up ON up.UserId = ud.Id
    INNER JOIN UserContactDetails AS ucd ON ucd.UserId = ud.Id
  WHERE ud.Id = @UserId;
END;

EXEC sp_get_patient_details
  @UserId = 7;