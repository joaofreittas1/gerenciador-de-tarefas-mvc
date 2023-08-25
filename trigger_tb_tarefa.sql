use HOMOLOGACAOTESTE
GO

CREATE TRIGGER tr_insert_tb_tarefa
ON tb_tarefa
AFTER INSERT
AS
BEGIN
    DECLARE @NextOrder INT
    SELECT @NextOrder = ISNULL(MAX(nr_ordem_apresentacao), 0) + 1
    FROM tb_tarefa

    UPDATE tb_tarefa
    SET nr_ordem_apresentacao = @NextOrder
    WHERE cd_tarefa IN (SELECT cd_tarefa FROM INSERTED)
END
GO
