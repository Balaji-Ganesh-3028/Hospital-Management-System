CREATE OR ALTER  PROCEDURE sp_get_roles
AS
BEGIN
  SET NOCOUNT ON;

  SELECT Id, RoleName, RoleCode
  FROM roles
  WHERE Is_active = 1;
END;

EXEC sp_get_roles;