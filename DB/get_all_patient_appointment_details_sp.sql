CREATE OR ALTER PROCEDURE sp_get_all_appointments
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    ad.ID AS AppointmentId,
    ad.AppointmentDate,
    mdpv.Name AS PurposeOfVisitName,
    ad.PurposeOfVisit,
    ad.IllnessOrDisease,
    ad.ProceduresOrMedication,
    ad.CurrentStatus,
    mdst.Name AS Status,

    -- PATIENT DETAILS
    pd.Id AS PatientId,
    pd.UserId AS PatientUserId,
    upp.FirstName AS PatientFirstName,
    upp.LastName AS PatientLastName,
    pd.BloodGroup,

    --DOCTOR DETAILS
    ad.DoctorID AS DoctorId,
    dd.UserId AS DoctorUserId,
    upd.FirstName AS DoctorFirstName,
    upd.LastName AS DoctorLastName,
    dd.Designation

  FROM AppointmentDirectory ad
    -- TO GET PATIENT DETAILS
    INNER JOIN PatientDetails AS pd ON pd.Id = ad.PatientID
    INNER JOIN UserProfile AS upp ON upp.UserId = pd.UserId

    -- TO GET DOCTOR DETAILS
    INNER JOIN DoctorDetails AS dd ON dd.ID = ad.DoctorID
    INNER JOIN UserProfile AS upd ON upd.UserId = dd.UserId

    -- MASTER DATA VALUE
    INNER JOIN MasterData AS mdst ON mdst.Id = ad.CurrentStatus
    INNER JOIN MasterData AS mdpv ON mdpv.Id = ad.PurposeOfVisit
  -- assuming DoctorDetails has UserId FK
  ORDER BY ad.AppointmentDate ASC;
END;

EXEC sp_get_all_appointments