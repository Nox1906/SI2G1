USE [L51NG1]
GO
/****** Object:  Trigger [dbo].[trg_UpdateEquipaIntervencao]    Script Date: 29/11/2021 12:10:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insere na tabela equipaFun um funcionario sem equipa
create OR ALTER TRIGGER [dbo].[trg_membroEquipa] 
ON [dbo].Funcionarios
FOR INSERT 
AS 
BEGIN
			DECLARE @funcId int
			SELECT @funcId = id FROM inserted
			INSERT INTO EquipaFunc VALUES (@funcId, null,null)
END

GO

--elimina na tabela equipaFun um funcionario 
CREATE OR ALTER TRIGGER [dbo].[trg_DeleteFuncEquipa] 
ON [dbo].Funcionarios
FOR DELETE
AS 
BEGIN
			DECLARE @funcId int
			SELECT @funcId = id FROM deleted
			Delete from EquipaFunc where funcId = @funcId
			delete from FunCompet where FunCompet.idFunc = @funcId
END

GO

--Contagem de nFunc por equipa a em cada update da tabela equipa Func
create OR ALTER TRIGGER [dbo].[trg_updateNfunc] 
ON [dbo].EquipaFunc
after update
AS 
BEGIN
			DECLARE @funcId int,
			@equipaID int,
			@nFunc INT ,
			@nFuncSup INT

			SELECT @funcId = funcId , @equipaID = equipaID , @nFunc = ef.CONT , @nFuncSup = CONTS
			FROM inserted
				LEFT JOIN (SELECT EquipaId as eid,COUNT(1) AS CONT from EquipaFunc GROUP BY EquipaId )ef on ef.eid = equipaId
				LEFT JOIN (SELECT EquipaId as eids,COUNT(DISTINCT supervisor) AS CONTS from EquipaFunc GROUP BY EquipaId ) efs on efs.eids = equipaId
			Update Equipa set nFunc = ISNULL(@nFunc,0) + iSNULL(@nFuncSup,0) where id = @equipaID

			SELECT @funcId = funcId , @equipaID = equipaID , @nFunc = ef.CONT , @nFuncSup = CONTS
			FROM deleted
				LEFT JOIN (SELECT EquipaId as eid,COUNT(1) AS CONT from EquipaFunc GROUP BY EquipaId )ef on ef.eid = equipaId
				LEFT JOIN (SELECT EquipaId as eids,COUNT(DISTINCT supervisor) AS CONTS from EquipaFunc GROUP BY EquipaId ) efs on efs.eids = equipaId
			Update Equipa set nFunc = ISNULL(@nFunc,0) + iSNULL(@nFuncSup,0) where id = @equipaID

END
GO

--Contagem de nFunc por equipa a em cada delete da tabela equipa Func
create OR ALTER TRIGGER [dbo].[trg_DeleteNfunc] 
ON [dbo].EquipaFunc
FOR DELETE
AS 
BEGIN
			DECLARE @funcId int,
			@equipaID int

			SELECT @funcId = funcId , @equipaID = equipaID
			FROM deleted
			Update Equipa set nFunc = 0 where id = @equipaID
			delete from FunCompet where FunCompet.idFunc = @funcId
			delete from Funcionarios where id = @funcId
END
