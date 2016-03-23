﻿using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201603091702)]
    public class Migration201603091702 : Migration
    {
        public override void Up()
        {
            Create.Table("remessaproducao")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("numero").AsInt64()
                .WithColumn("ano").AsInt64()
                .WithColumn("descricao").AsString()
                .WithColumn("datainicio").AsDateTime()
                .WithColumn("datalimite").AsDateTime()
                .WithColumn("dataalteracao").AsDateTime()
                .WithColumn("observacao").AsString().Nullable()
                .WithColumn("colecao_id").AsInt64()
                .ForeignKey("FK_remessaproducao_colecao", "colecao", "id");
            
            Create.Table("remessaproducaocapacidadeprodutiva")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("quantidade").AsFloat()
                .WithColumn("remessaproducao_id").AsInt64()
                .ForeignKey("FK_remessaproducaocapacidadeprodutiva_remessaproducao", "remessaproducao", "id")
                .WithColumn("classificacaodificuldade_id").AsInt64()
                .ForeignKey("FK_remessaproducaocapacidadeprodutiva_classificacaodificuldade", "classificacaodificuldade", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201603091702.permissao.sql");
            
            Alter.Table("programacaoproducao")
                .AddColumn("remessaproducao_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_programacaoproducao_remessaproducao", "remessaproducao", "id");
            
            Execute.Sql(@"UPDATE programacaoproducao SET remessaproducao_id = colecao_id");

            Delete.ForeignKey("FK_programacaoproducao_colecao").OnTable("programacaoproducao");
            Delete.Column("colecao_id").FromTable("programacaoproducao");
        }

        public override void Down()
        {
            
        }
    }
}