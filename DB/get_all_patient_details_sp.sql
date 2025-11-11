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
    mdb.Name AS BloodGroupName,
    pd.id AS PatientId,
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
    INNER JOIN MasterData AS mdb ON mdb.Id = pd.BloodGroup
END;

EXEC sp_get_all_patient_details;