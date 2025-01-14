﻿using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class CustoMaterial : DomainEmpresaBase<CustoMaterial>
    {
        public virtual DateTime Data { get; set; }
        public virtual double CustoAquisicao { get; set; }
        public virtual double Custo { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual bool CadastroManual { get; set; }
        
        public virtual Pessoa Fornecedor { get; set; }
        public virtual Pessoa Funcionario { get; set; }
    }
}