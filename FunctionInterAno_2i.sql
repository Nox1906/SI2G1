USE L51NG1

CREATE FUNCTION dbo.IntervencaoAno (@year int)
RETURNS TABLE
AS
RETURN (SELECT i.id, i.descricao FROM dbo.Intervencao i
		WHERE YEAR(i.dtInicio) = @year AND YEAR(i.dtFim) = @year)

