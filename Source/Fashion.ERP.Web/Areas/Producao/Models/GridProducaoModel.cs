﻿using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridProducaoModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Lote/Ano")]
        public string LoteAno { get; set; }

        [Display(Name = "Remessa")]
        public string RemessaProducao { get; set; }
        
        [Display(Name = "Responsável")]
        public string Responsavel { get; set; }

        [Display(Name = "Data")]
        public DateTime Data{ get; set; }

        [Display(Name = "Qtde. Programada")]
        public long QtdeProgramada { get; set; }

        [Display(Name = "Qtde. Fichas Técnicas")]
        public long QtdeFichasTecnicas { get; set; }

        [Display(Name = "Situação")]
        public SituacaoProducao SituacaoProducao { get; set; }

        [Display(Name = "Tipo")]
        public TipoProducao TipoProducao { get; set; }
    }
}