DROP DATABASE IF EXISTS [HOMOLOGACAOTESTE]
GO
CREATE DATABASE HOMOLOGACAOTESTE
GO

USE HOMOLOGACAOTESTE

CREATE TABLE tb_tarefa (
    cd_tarefa INT IDENTITY (1,1) PRIMARY KEY,
    nome_tarefa VARCHAR(100) NOT NULL UNIQUE,
    vlr_custo DECIMAL(12, 2) DEFAULT 0.00,
    dt_limite DATE,
    nr_ordem_apresentacao INT
)

INSERT INTO tb_tarefa (nome_tarefa,vlr_custo,dt_limite) values ('Tela', 4000.00, '2020-04-01 14:20:00')
INSERT INTO tb_tarefa (nome_tarefa,vlr_custo,dt_limite) values ('Desenvolver Classe', 2.00, '2020-04-01 14:20:00')


SELECT * FROM tb_tarefa order by nr_ordem_apresentacao asc

--update tb_tarefa set nr_ordem_apresentacao = 4 where cd_tarefa = 4