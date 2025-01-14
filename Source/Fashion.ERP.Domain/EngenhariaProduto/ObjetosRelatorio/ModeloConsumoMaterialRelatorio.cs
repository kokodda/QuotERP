﻿namespace Fashion.ERP.Domain.EngenhariaProduto.ObjetosRelatorio
{
    public class ModeloConsumoMaterialRelatorio
    {
        public virtual string Tag { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual double QuantidadeAprovada { get; set; }
        public virtual double QuantidadeMaterial { get; set; }
        public virtual double QuantidadeTotalMaterial { get; set; } 
    }
}