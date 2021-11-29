USE L51NG1
SET XACT_ABORT ON
begin transaction

DROP TABLE dbo.HistAlteracaoEqInterv
DROP TABLE dbo.EquipaIntervencao
DROP TABLE dbo.IntervencaoPeriodica
DROP TABLE dbo.Intervencao
DROP TABLE dbo.EquipaFunc
DROP TABLE dbo.Equipa
DROP TABLE dbo.Historico
DROP TABLE dbo.Ativos
DROP TABLE dbo.Tipos
DROP TABLE dbo.FunCompet
DROP TABLE dbo.Funcionarios
DROP TABLE dbo.Competencias

commit

SET XACT_ABORT OFF
