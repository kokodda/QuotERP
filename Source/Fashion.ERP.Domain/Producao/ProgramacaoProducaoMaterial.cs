﻿using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducaoMaterial : DomainEmpresaBase<ProgramacaoProducaoMaterial>
    {
        public virtual double Quantidade { get; set; }
        public virtual ReservaMaterial ReservaMaterial { get; set; }
        public virtual Material Material { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}