﻿using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridColecaoProgramadaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Remessa")]
        public string RemessaProgramada { get; set; }

        [Display(Name = "Fichas Técnicas Programadas")]
        public long QtdeFichasTecnicasProgramadas { get; set; }
    }
}