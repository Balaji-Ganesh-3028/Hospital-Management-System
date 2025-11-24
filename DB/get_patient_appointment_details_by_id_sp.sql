CREATE OR ALTER PROCEDURE sp_get_appointment_details_by_id(
  @AppointmentId INT
)
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
    mdbg.Name AS BloodGroupName,

    --DOCTOR DETAILS
    ad.DoctorID AS DoctorId,
    dd.UserId AS DoctorUserId,
    upd.FirstName AS DoctorFirstName,
    upd.LastName AS DoctorLastName,
    mdd.Name AS Designation,
    mds.Name AS Specialisation

  FROM AppointmentDirectory ad
    -- TO GET PATIENT DETAILS
    INNER JOIN PatientDetails AS pd ON pd.Id = ad.PatientID
    INNER JOIN UserProfile AS upp ON upp.UserId = pd.UserId

    -- TO GET DOCTOR DETAILS
    INNER JOIN DoctorDetails AS dd ON dd.ID = ad.DoctorID
    INNER JOIN UserProfile AS upd ON upd.UserId = dd.UserId
    INNER JOIN MasterData AS mdd ON mdd.Id = dd.Designation
    INNER JOIN MasterData AS mds ON mds.Id = dd.Specialisation
    INNER JOIN MasterData AS mdbg ON mdbg.Id = pd.BloodGroup
    INNER JOIN MasterData AS mdst ON mdst.Id = ad.CurrentStatus
    INNER JOIN MasterData AS mdpv ON mdpv.Id = ad.PurposeOfVisit
  -- assuming DoctorDetails has UserId FK
  WHERE ad.id = @AppointmentId
  ORDER BY ad.AppointmentDate ASC;
END;

EXEC sp_get_appointment_details_by_id
  @AppointmentId = 7000;
