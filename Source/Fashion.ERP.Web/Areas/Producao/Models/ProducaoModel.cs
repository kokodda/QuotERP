﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProducaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Lote/Ano")]
        public long? Lote { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Data")]
        public DateTime? Data { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Responsável")]
        [Required(ErrorMessage = "Informe o responsável")]
        public long? Funcionario { get; set; }
        
        [Display(Name = "Remessa")]
        [Required(ErrorMessage = "Informe a remessa de produção")]
        public long? RemessaProducao { get; set; }

        [Display(Name = "Situação")]
        public SituacaoProducao SituacaoProducao { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Informe o tipo")]
        public TipoProducao TipoProducao{ get; set; }

        public IList<ProducaoItemModel> GridProducaoItens { get; set; }
    }
}