﻿DECLARE @PRODUCAOID AS BIGINT;
SET @PRODUCAOID = (SELECT id FROM permissao WHERE area = 'Producao' AND controller IS NULL AND [action] IS NULL AND [permissaopai_id] IS NULL);

DECLARE @BASICOSID AS BIGINT;
SET @BASICOSID = (SELECT id FROM permissao WHERE descricao = 'Básicos' AND area = 'Producao' AND permissaopai_id = @PRODUCAOID);

-- Remessa de produção
DECLARE @ID AS BIGINT;
INSERT INTO permissao (action, area, controller, descricao, exibenomenu, requerpermissao, ordem, permissaopai_id) VALUES ('Index', 'Producao', 'RemessaProducao', 'Remessa de Produção', 1, 1, 0, @BASICOSID);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Novo', 'Producao', 'RemessaProducao', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Editar', 'Producao', 'RemessaProducao', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('Excluir', 'Producao', 'RemessaProducao', 'Excluir', 0, 1, 0, @ID);

INSERT INTO remessaproducao(id, idtenant, idempresa, numero, ano, descricao, datainicio, datalimite, dataalteracao, observacao, colecao_id)
	SELECT id, 0, 0, id, 2016, descricao, GETDATE(), GETDATE(), GETDATE(), NULL, id	
		FROM COLECAO

----MIGRAÇÃO BOOK DA COLEÇÃO PROGRAMADA PARA REMESSA DE PRODUÇÃO
DECLARE @INDEXID AS BIGINT;
SET @INDEXID = (SELECT id FROM permissao WHERE action = 'Index' and controller = 'RemessaProducao');

UPDATE permissao SET controller = 'RemessaProducao', permissaopai_id = @INDEXID WHERE action = 'Book';

----CUSTO DO MATERIAL
DECLARE @INDEXMATERIALID AS BIGINT;
SET @INDEXMATERIALID = (SELECT id FROM permissao WHERE action = 'Index' and controller = 'Material');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, ordem, permissaopai_id) VALUES ('MaterialCusto', 'Almoxarifado', 'Material', 'Custo', 0, 1, 0, @INDEXMATERIALID);