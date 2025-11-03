CREATE OR ALTER PROCEDURE sp_get_all_patient_details
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    ud.Email,
    ud.Id AS UserId,
    ud.UserName,
    up.Firstname,
    up.Lastname,
    up.Gender,
    up.Age,
    up.CreatedBy,
    up.UpdatedBy,
    ucd.PhoneNumber,
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
    INNER JOIN UserProfile AS up ON ud.Id = up.UserId
    INNER JOIN UserContactDetails AS ucd ON ud.Id = ucd.UserId
    INNER JOIN PatientDetails AS pd ON ud.Id = pd.UserId
END;

EXEC sp_get_all_patient_details;