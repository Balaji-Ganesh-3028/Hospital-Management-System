CREATE OR ALTER PROCEDURE sp_get_dashboard_details
AS
BEGIN
  SET NOCOUNT ON;

  -- Dashboard Summary
  SELECT
    -- PATIENT COUNTS
    (SELECT COUNT(Id)
    FROM PatientDetails) AS PatientCount,

    -- DOCTOR COUNTS
    (SELECT COUNT(ID)
    FROM DoctorDetails) AS DoctorCount,

    -- APPOINTMENT COUNTS
    (SELECT COUNT(ID)
    FROM AppointmentDirectory) AS AppointmentCount,

    -- TOTAL USER COUNTS
    (SELECT COUNT(Id)
    FROM UserDirectory) AS TotalUserCount,

    -- TODAY'S PATIENTS ADDED
    (SELECT COUNT(Id)
    FROM PatientDetails
    WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)) AS PatientAddedToday,

    -- TODAY'S DOCOTRS ADDED
    (SELECT COUNT(ID)
    FROM DoctorDetails
    WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)) AS DoctorAddedToday,

    -- TODAY'S APPOINTMENTS ADDED
    (SELECT COUNT(ID)
    FROM AppointmentDirectory
    WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE))
    AS TotalAppointmentsAddedToday,

    -- TODAY'S APPOINTMENT STATUS COUNTS
    -- COMPLETED APPOINTMENTS
    (SELECT COUNT(ID)
    FROM AppointmentDirectory
    WHERE CurrentStatus = (SELECT Id
      FROM MasterData
      WHERE Value = 'Completed' AND Category = 'appointment_status') AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)) AS CompletedAppointmentsToday,

    -- SCHEDULED APPOINTMENTS
    (SELECT COUNT(ID)
    FROM AppointmentDirectory
    WHERE CurrentStatus = (SELECT Id
      FROM MasterData
      WHERE Value = 'Scheduled' AND Category = 'appointment_status') AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)) AS ScheduledAppointmentsToday,

    -- CANCELLED APPOINTMENTS
    (SELECT COUNT(ID)
    FROM AppointmentDirectory
    WHERE CurrentStatus = (SELECT Id
      FROM MasterData
      WHERE Value = 'Cancelled' AND Category = 'appointment_status') AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)) AS CancelledAppointmentsToday,

    -- RESCHEDULED APPOINTMENTS
    (SELECT COUNT(ID)
    FROM AppointmentDirectory
    WHERE CurrentStatus = (SELECT Id
      FROM MasterData
      WHERE Value = 'Rescheduled' AND Category = 'appointment_status') AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)) AS ReScheduledAppointmentsToday;
END


EXECUTE sp_get_dashboard_details