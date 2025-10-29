CREATE OR ALTER  PROCEDURE sp_GetMasterData(
  @CategoryName VARCHAR(150)
)
AS
BEGIN
  SET NOCOUNT ON;

  SELECT Id, Name, Value
  FROM MasterData
  WHERE Category = @CategoryName AND Is_active = 1;
END;

EXEC sp_GetMasterData 'designation';