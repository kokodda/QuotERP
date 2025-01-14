﻿using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridProgramacaoProducaoRequisicaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Und.")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Gênero Categoria")]
        public GeneroCategoria GeneroCategoria { get; set; }
        
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Qtde.Reservada")]
        public double Quantidade { get; set; }
        
        public bool Requisitado { get; set; }
        public bool Editavel { get; set; }
        
        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Informe o departamento")]
        public string DepartamentoProducao { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade")]
        public long? Unidade { get; set; }
    }
}