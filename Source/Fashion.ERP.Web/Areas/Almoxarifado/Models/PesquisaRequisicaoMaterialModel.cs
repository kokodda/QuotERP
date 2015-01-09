﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class PesquisaRequisicaoMaterialModel
    {
        [Display(Name = "Origem")]
        public string Origem { get; set; }

        [Display(Name = "Unidade Requerente")]
        public long? UnidadeRequerente { get; set; }

        [Display(Name = "Requerente")]
        public long? Requerente { get; set; }

        [Display(Name = "Tipo do Material")]
        public long? TipoMaterial { get; set; }

        [Display(Name = "Material")]
        public long? Material { get; set; }

        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Data")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFim { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoRequisicaoMaterial? SituacaoRequisicaoMaterial { get; set; }
        
        public string ModoConsulta { get; set; }

        [Display(Name = "Tipo")]
        public string TipoRelatorio { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridRequisicaoMaterialModel> Grid { get; set; }
    }
}