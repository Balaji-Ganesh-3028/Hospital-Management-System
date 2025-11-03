CREATE OR ALTER PROCEDURE sp_get_all_user_details
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    ud.Id AS userId,
    ud.Email,
    up.Firstname,
    up.Lastname,
    up.Gender,
    ucd.PhoneNumber,
    CASE
      WHEN pd.id IS NOT NULL THEN 'Patient'
      WHEN dd.id IS NOT NULL THEN 'Doctor'
      ELSE 'User'
    END AS UserType
  FROM UserDirectory AS ud
    INNER JOIN UserProfile AS up ON ud.Id = up.UserId
    INNER JOIN UserContactDetails AS ucd ON ud.Id = ucd.UserId
    LEFT JOIN PatientDetails AS pd ON ud.Id = pd.UserId
    LEFT JOIN DoctorDetails AS dd ON ud.Id = dd.UserId
END;

EXEC sp_get_all_user_details;