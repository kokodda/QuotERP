﻿using System;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloAprovacao : DomainEmpresaBase<ModeloAprovacao>
    {
        public virtual String Observacao { get; set; }
        public virtual long Quantidade { get; set; }
        public virtual String Referencia { get; set; }
        public virtual String Descricao { get; set; }
        public virtual double? MedidaBarra { get; set; }
        public virtual double? MedidaComprimento { get; set; }
        public virtual Comprimento Comprimento { get; set; }
        public virtual Barra Barra { get; set; }
        public virtual ProdutoBase ProdutoBase { get; set; }
        public virtual FichaTecnica FichaTecnica { get; set; }
        public virtual Grade Grade { get; set; }
    }
}