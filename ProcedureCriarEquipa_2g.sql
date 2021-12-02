USE L51NG1
CREATE PROCEDURE CriaEquipa (@idEquipa int, @localizacao varchar(50))
AS
BEGIN
		INSERT INTO dbo.Equipa VALUES (@idEquipa, @localizacao, 0)
END