﻿using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ReservaMaterialMap : EmpresaClassMap<ReservaMaterial>
    {
        public ReservaMaterialMap()
            : base("reservamaterial", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.DataProgramacao).Nullable();
            Map(x => x.Observacao).Nullable();
            Map(x => x.ReferenciaOrigem).Nullable();
            Map(x => x.SituacaoReservaMaterial).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();
            
            References(x => x.Unidade).Not.Nullable();
            References(x => x.Colecao).Nullable();
            References(x => x.Requerente).Not.Nullable();

            HasMany(x => x.ReservaMaterialItems)
                .KeyNullable()
                .Cascade.AllDeleteOrphan();
        }         
    }
}