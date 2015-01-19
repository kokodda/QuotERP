﻿declare @estoquematerialId as bigint, @id as bigint;
set @estoquematerialId = (Select Id from permissao where area='Almoxarifado' and descricao='Estoque de Material' and controller is null)

-- Cria o menu Requisição de Material
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Index', 'Almoxarifado', 'RequisicaoMaterial', 'Requisição de Material', 1, 1, 0, @estoquematerialId);
SET @ID = SCOPE_IDENTITY()
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Novo', 'Almoxarifado', 'RequisicaoMaterial', 'Novo', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Editar', 'Almoxarifado', 'RequisicaoMaterial', 'Editar', 0, 1, 0, @ID);
INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) VALUES ('Cancelar', 'Almoxarifado', 'RequisicaoMaterial', 'CancelamentoRequisicaoMaterial', 0, 1, 0, @ID);