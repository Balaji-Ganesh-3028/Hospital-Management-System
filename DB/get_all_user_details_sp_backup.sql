CREATE OR ALTER PROCEDURE sp_get_all_user_details(
  @searchTerm VARCHAR(255),
  @pageNumber INT = 1,
  @pageSize INT = 10,
  @SortByOrder VARCHAR(6) = 'ASC',
  @SortByColumn VARCHAR(50) = 'Firstname',
  @UserTypeFilter VARCHAR(20) = 'All'
)
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
  WHERE
    (
      @searchTerm IS NULL
    OR Firstname LIKE '%' + @searchTerm + '%'
    OR Email LIKE '%' + @searchTerm + '%'
    )
END;

EXEC sp_get_all_user_details;