CREATE OR ALTER PROCEDURE sp_get_all_user_details(
  @searchTerm VARCHAR(255) = NULL,
  @pageNumber INT = 1,
  @pageSize INT = 10,
  @SortByOrder VARCHAR(6) = 'ASC',
  @SortByColumn VARCHAR(50) = 'Firstname',
  @UserTypeFilter VARCHAR(20) = 'All'
)
AS
BEGIN
  SET NOCOUNT ON;


  -- Validate input
  IF @pageNumber < 1 SET @pageNumber = 1;
  IF @pageSize < 1 SET @pageSize = 10;

  DECLARE @Offset INT = (@pageNumber - 1) * @pageSize;

  -- Main query with filtering and sorting
  WITH
    UserCTE
    AS
    (
      SELECT
        ud.Id AS UserId,
        ud.Email,
        up.Firstname,
        up.Lastname,
        up.Gender,
        ucd.PhoneNumber,
        CASE
          WHEN pd.Id IS NOT NULL THEN 'Patient'
          WHEN dd.Id IS NOT NULL THEN 'Doctor'
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
        OR up.Firstname LIKE '%' + @searchTerm + '%'
        OR ud.Email LIKE '%' + @searchTerm + '%'
        )
        AND (
          @UserTypeFilter = 'All'
        OR (@UserTypeFilter = 'Patient' AND pd.Id IS NOT NULL)
        OR (@UserTypeFilter = 'Doctor' AND dd.Id IS NOT NULL)
        )
    )

  SELECT
    UserId,
    Email,
    Firstname,
    Lastname,
    Gender,
    PhoneNumber,
    UserType
  FROM UserCTE
  ORDER BY
        CASE WHEN @SortByColumn = 'Firstname' AND @SortByOrder = 'ASC' THEN Firstname END ASC,
        CASE WHEN @SortByColumn = 'Firstname' AND @SortByOrder = 'DESC' THEN Firstname END DESC,
        CASE WHEN @SortByColumn = 'Lastname' AND @SortByOrder = 'ASC' THEN Lastname END ASC,
        CASE WHEN @SortByColumn = 'Lastname' AND @SortByOrder = 'DESC' THEN Lastname END DESC,
        CASE WHEN @SortByColumn = 'Email' AND @SortByOrder = 'ASC' THEN Email END ASC,
        CASE WHEN @SortByColumn = 'Email' AND @SortByOrder = 'DESC' THEN Email END DESC
    OFFSET @Offset ROWS
    FETCH NEXT @pageSize ROWS ONLY;
END;

EXEC sp_get_all_user_details
  @searchTerm = '',
  @pageNumber = 1,
  @pageSize = 10,
  @SortByOrder = 'ASC',
  @SortByColumn = 'Firstname',
  @UserTypeFilter = 'All';