﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class FichaTecnicaBasicosModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Tag/Ano")]
        [Required(ErrorMessage = "Informe o tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Informe o ano")]
        public long? Ano { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Natureza")]
        [Required(ErrorMessage = "Informe a natureza")]
        public long? Natureza { get; set; }

        [Display(Name = "Coleção")]
        [Required(ErrorMessage = "Informe a coleção")]
        public long? Colecao { get; set; }
        
        [Display(Name = "Artigo")]
        [Required(ErrorMessage = "Informe o artigo")]
        public long? Artigo { get; set; }

        [Display(Name = "Segmento")]
        [Required(ErrorMessage = "Informe o segmento")]
        public long? Segmento { get; set; }

        [Display(Name = "Classificação")]
        [Required(ErrorMessage = "Informe a classificação")]
        public long? Classificacao { get; set; }

        [Display(Name = "Dificuldade")]
        [Required(ErrorMessage = "Informe a dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }

        [Display(Name = "Marca")]
        public long? Marca { get; set; }
        
        [Display(Name = "Silk")]
        public string Silk { get; set; }

        [Display(Name = "Bordado")]
        public string Bordado { get; set; }

        [Display(Name = "Pedraria")]
        public string Pedraria { get; set; }

        [Display(Name = "Detalhamento")]
        public string Detalhamento { get; set; }

        [Display(Name = "Grade")]
        [Required(ErrorMessage = "Informe a grade")]
        public long? Grade { get; set; }

        [Display(Name = "Prazo Máximo")]
        public long? PrazoMaximo { get; set; }

        [Display(Name = "Quantidade Aprovada")]
        public int? QuantidadeAprovada { get; set; }
        
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Variações")]
        public IList<GridFichaTecnicaVariacaoModel> GridFichaTecnicaVariacao { get; set; }

        #region Jeans
        [Display(Name = "Base")]
        public long? ProdutoBase { get; set; }

        [Display(Name = "Comprimento")]
        public long? Comprimento { get; set; }

        [Display(Name = "Passante")]
        public string Passante { get; set; }

        [Display(Name = "Barra")]
        public long? Barra { get; set; }

        [Display(Name = "Boca")]
        public string Boca { get; set; }

        [Display(Name = "Lavada")]
        public string Lavada { get; set; }
        
        [Display(Name = "Cos")]
        public string Cos { get; set; }

        [Display(Name = "Pesponto")]
        public string Pesponto { get; set; }

        [Display(Name = "EntrePernas")]
        public string Entrepernas { get; set; }

        #endregion
    }
}