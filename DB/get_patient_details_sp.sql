CREATE OR ALTER PROCEDURE sp_get_patient_details(
  @PatientId INT
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
    pd.Id,
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
  FROM PatientDetails AS pd
    INNER JOIN UserDirectory AS ud ON ud.Id = pd.UserId
    INNER JOIN UserProfile AS up ON up.UserId = pd.UserId
    INNER JOIN UserContactDetails AS ucd ON ucd.UserId = pd.UserId
  WHERE pd.Id = @PatientId;
END;

EXEC sp_get_patient_details
  @PatientId = 2001;