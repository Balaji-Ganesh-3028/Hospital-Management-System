CREATE OR ALTER PROCEDURE sp_get_dashboard_details
AS
BEGIN
  SET NOCOUNT ON;

  -- Dashboard Summary
  SELECT
    (SELECT COUNT(Id)
    FROM PatientDetails) AS PatientCount,
    (SELECT COUNT(ID)
    FROM DoctorDetails) AS DoctorCount,
    (SELECT COUNT(ID)
    FROM AppointmentDirectory) AS AppointmentCount,
    (SELECT COUNT(Id)
    FROM UserDirectory) AS TotalUserCount,
    (SELECT COUNT(Id)
    FROM PatientDetails
    WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)) AS PatientAddedToday,
    (SELECT COUNT(ID)
    FROM DoctorDetails
    WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)) AS DoctorAddedToday,
    (SELECT COUNT(ID)
    FROM AppointmentDirectory
    WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE))
    AS TotalAppointmentsAddedToday;
END


EXECUTE sp_get_dashboard_details