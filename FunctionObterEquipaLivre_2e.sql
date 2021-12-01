USE L51NG1

CREATE FUNCTION dbo.obterEquipaLivre (@competencia varchar(50))
RETURNS int
AS
BEGIN
	declare @equipaId int
	SELECT distinct @equipaId= e.equipaId FROM dbo.EquipaFunc e 
						   JOIN dbo.FunCompet f ON e.funcId = f.idFunc 
						   JOIN dbo.Competencias c ON c.id = f.idComp					
WHERE c.descricao like @competencia AND e.equipaId IN (SELECT equipaId FROM (select top 1 ei.equipaId, i.dtFim 
																					FROM EquipaIntervencao ei
																					JOIN Intervencao i ON ei.idIntervencao = i.id
																				    WHERE i.estado <> 'concluído' AND i.estado <> 'em execucão'
																			        GROUP BY equipaId, i.dtFim 
																					HAVING count(*) <3
																					ORDER BY i.dtFim ASC
																		  )e
													)
RETURN @equipaId
END

DROP FUNCTION dbo.obterEquipaLivre



select * from dbo.Equipa e where e.id = dbo.obterEquipaLivre('Limpeza')

