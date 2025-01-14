﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.EngenhariaProduto.ObjetosRelatorio;
using Fashion.ERP.Domain.EngenhariaProduto.Views;
using Fashion.ERP.Reporting.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class RelatorioController : BaseController
    {
        #region Variaveis
        private readonly ILogger _logger;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<ProcessoOperacional> _naturezaRepository;
        /// /////////////////////////////////////////////////////////////////
        private readonly IRepository<Classificacao> _classificacaoRepository;
        private readonly IRepository<Marca> _marcaRepository;
        private readonly IRepository<ProdutoBase> _produtoBaseRepository;
        private readonly IRepository<Comprimento> _comprimentoRepository;
        private readonly IRepository<Barra> _barraRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Familia> _familiaRepository;
        //private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<ConsumoMaterialColecaoView> _consumoRepository;
        private readonly IRepository<Segmento> _segmentoRepository;
        private readonly IRepository<Artigo> _artigoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly string[] _tipoRelatorio = { "Detalhado", "Listagem", "Sintético" };
        #region ColunasListagemModelos
        private static readonly Dictionary<string, string> ColunasListagemModelos = new Dictionary<string, string>
        {
            {"Artigo", "Artigo.Descricao"},
            {"Barra", "Barra.Descricao"},
            {"Classificação", "Classificacao.Descricao"},
            {"Coleção", "Colecao.Descricao"},
            {"Complemento", "Complemento"},
            {"Comprimento", "Comprimento.Descricao"},
            {"Descrição", "Descricao"},
            {"Detalhamento", "Detalhamento"},
            {"Estilista", "Estilista.Nome"},
            {"Grade", "Grade.Descricao"},
            {"Lavada", "Lavada"},
            {"Linha casa", "LinhaCasa"},
            {"Marca", "Marca.Nome"},
            {"Modelista", "Modelista.Nome"},
            {"Natureza", "Natureza.Descricao"},
            {"Produto base", "ProdutoBase.Descricao"},
            {"Referência", "Referencia"},
            {"Segmento", "Segmento.Descricao"},
        };
        #endregion
        #region ColunasConsumoMaterialColecao
        private static readonly Dictionary<string, string> ColunasConsumoMaterialColecao = new Dictionary<string, string>
        {
            {"Referência", "Referencia"},
            {"Descrição", "Descricao"},
            {"UND", "Unidade"},
            {"Quantidade", "Quantidade"},
            {"Compras", "Compras"},
            {"Estoque", "Estoque"},
            {"Diferença", "Diferenca"},
            {"Categoria", "Categoria"},
            {"Data da previsão de envio", "DataPrevisaoEnvio"},
           // {"Subcategoria", "Subcategoria"},

        };
        #endregion        
        #region ColunasConsumoMaterialPorModelo
        private static readonly Dictionary<string, string> ColunasConsumoMaterialPorModelo = new Dictionary<string, string>
        {
            {"Referência", "Referencia"},
            {"Descrição", "Descricao"}
        };
        #endregion
        
        #endregion

        #region Construtores
        public RelatorioController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<ProcessoOperacional> naturezaRepository, IRepository<Classificacao> classificacaoRepository,
            IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Familia> familiaRepository, //IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<ConsumoMaterialColecaoView> consumoRepository, IRepository<Grade> gradeRepository,
            IRepository<Marca> marcaRepository, IRepository<ProdutoBase> produtoBaseRepository,
            IRepository<Comprimento> comprimentoRepository, IRepository<Barra> barraRepository, 
            IRepository<Segmento> segmentoRepository, IRepository<Artigo> artigoRepository,
            IRepository<DepartamentoProducao> departamentoProducaoRepository, IRepository<Material> materialRepository )
        {
            _logger = logger;
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _pessoaRepository = pessoaRepository;
            _naturezaRepository = naturezaRepository;
            _classificacaoRepository = classificacaoRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _familiaRepository = familiaRepository;
            //_fichaTecnicaRepository = fichaTecnicaRepository;
            _consumoRepository = consumoRepository;
            _gradeRepository = gradeRepository;
            _marcaRepository = marcaRepository;
            _produtoBaseRepository = produtoBaseRepository;
            _comprimentoRepository = comprimentoRepository;
            _barraRepository = barraRepository;
            _segmentoRepository = segmentoRepository;
            _artigoRepository = artigoRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _materialRepository = materialRepository;
        }

        #endregion

        #region Views

        #region ListagemModelos
        [PopulateViewData("PopulateListagemModelos")]
        public virtual ActionResult ListagemModelos()
        {
            var modelos = _modeloRepository.Find().OrderByDescending(m => m.DataAlteracao).Take(20);

            var model = new PesquisaModeloModel { ModoConsulta = "Listar" };

            model.Grid = modelos.Select(p => new GridModeloModel
            {
                Id = p.Id.GetValueOrDefault(),
                Referencia = p.Referencia,
                Descricao = p.Descricao,
                Colecao = p.Colecao.Descricao,
                Estilista = p.Estilista.Nome,
                Grade = p.Grade.Descricao,
                Marca = p.Marca.Nome,
                Segmento = p.Segmento != null ? p.Segmento.Descricao : null,

            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateListagemModelos")]
        public virtual ActionResult ListagemModelos(PesquisaModeloModel model)
        {
            var modelos = _modeloRepository.Find();

            try
            {
                #region Filtros
                var filtros = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(model.Referencia))
                {
                    modelos = modelos.Where(p => p.Referencia == model.Referencia);
                    filtros.AppendFormat("Referência: {0}, ", model.Referencia);
                }

                if (!string.IsNullOrWhiteSpace(model.Descricao))
                {
                    modelos = modelos.Where(p => p.Descricao.Contains(model.Descricao));
                    filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
                }

                if (model.Classificacao.HasValue)
                {
                    modelos = modelos.Where(p => p.Classificacao.Id == model.Classificacao);
                    filtros.AppendFormat("Classificação: {0}, ", _classificacaoDificuldadeRepository.Get(model.Classificacao.Value).Descricao);
                }

                if (model.Comprimento.HasValue)
                {
                    modelos = modelos.Where(p => p.Comprimento.Id == model.Comprimento);
                    filtros.AppendFormat("Comprimento: {0}, ", _comprimentoRepository.Get(model.Comprimento.Value).Descricao);
                }

                if (model.Marca.HasValue)
                {
                    modelos = modelos.Where(p => p.Marca.Id == model.Marca);
                    filtros.AppendFormat("Marca: {0}, ", _marcaRepository.Get(model.Marca.Value).Nome);
                }

                if (model.ProdutoBase.HasValue)
                {
                    modelos = modelos.Where(p => p.ProdutoBase.Id == model.ProdutoBase);
                    filtros.AppendFormat("ProdutoBase: {0}, ", _produtoBaseRepository.Get(model.ProdutoBase.Value).Descricao);
                }

                if (model.Natureza.HasValue)
                {
                    modelos = modelos.Where(p => p.Natureza.Id == model.Natureza);
                    filtros.AppendFormat("Natureza: {0}, ", _naturezaRepository.Get(model.Natureza.Value).Descricao);
                }

                if (model.Barra.HasValue)
                {
                    modelos = modelos.Where(p => p.Barra.Id == model.Barra);
                    filtros.AppendFormat("Barra: {0}, ", _barraRepository.Get(model.Barra.Value).Descricao);
                }

                if (model.Grade.HasValue)
                {
                    modelos = modelos.Where(p => p.Grade.Id == model.Grade);
                    filtros.AppendFormat("Grade: {0}, ", _gradeRepository.Get(model.Grade.Value).Descricao);
                }

                if (model.Colecao.HasValue)
                {
                    modelos = modelos.Where(p => p.Colecao.Id == model.Colecao);
                    filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
                }

                if (model.Funcionario.HasValue)
                {
                    modelos = modelos.Where(p => p.Estilista.Id == model.Funcionario);
                    filtros.AppendFormat("Estilista: {0}, ", _pessoaRepository.Get(model.Funcionario.Value).Nome);
                }

                if (model.Modelista.HasValue)
                {
                    modelos = modelos.Where(p => p.Modelista.Id == model.Modelista);
                    filtros.AppendFormat("Modelista: {0}, ", _pessoaRepository.Get(model.Modelista.Value).Nome);
                }

                if (model.Grade.HasValue)
                {
                    modelos = modelos.Where(p => p.Grade.Id == model.Grade);
                    filtros.AppendFormat("Grade: {0}, ", _gradeRepository.Get(model.Grade.Value).Descricao);
                }

                if (model.Marca.HasValue)
                {
                    modelos = modelos.Where(p => p.Marca.Id == model.Marca);
                    filtros.AppendFormat("Marca: {0}, ", _marcaRepository.Get(model.Marca.Value).Nome);
                }

                if (model.Segmento.HasValue)
                {
                    modelos = modelos.Where(p => p.Segmento.Id == model.Segmento);
                    filtros.AppendFormat("Segmento: {0}, ", _segmentoRepository.Get(model.Segmento.Value).Descricao);
                }

                if (model.Material.HasValue)
                {
                    modelos = modelos.Where(p => p.MateriaisConsumo.Any(material =>
                        material.Material.Id== model.Material));

                    filtros.AppendFormat("Referência do Material: {0}, ", _materialRepository.Get(model.Material).Referencia);
                }

                if (model.Natureza.HasValue)
                {
                    modelos = modelos.Where(p => p.Natureza.Id == model.Natureza);
                    filtros.AppendFormat("Natureza: {0}, ", _naturezaRepository.Get(model.Natureza.Value).Descricao);
                }

                if (model.Classificacao.HasValue)
                {
                    modelos = modelos.Where(p => p.Classificacao.Id == model.Classificacao);
                    filtros.AppendFormat("Classificação: {0}, ", _classificacaoRepository.Get(model.Classificacao.Value).Descricao);
                }

                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    modelos = modelos.Where(p => p.Referencia.Contains(model.Tag));
                    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                }

                if (model.Artigo.HasValue)
                {
                    modelos = modelos.Where(p => p.Artigo.Id == model.Artigo);
                    filtros.AppendFormat("Artigo: {0}, ", _artigoRepository.Get(model.Artigo.Value).Descricao);
                }

                if (model.ProdutoBase.HasValue)
                {
                    modelos = modelos.Where(p => p.ProdutoBase.Id == model.ProdutoBase);
                    filtros.AppendFormat("ProdutoBase: {0}, ", _produtoBaseRepository.Get(model.ProdutoBase.Value).Descricao);
                }

                if (model.Comprimento.HasValue)
                {
                    modelos = modelos.Where(p => p.Comprimento.Id == model.Comprimento);
                    filtros.AppendFormat("Comprimento: {0}, ", _comprimentoRepository.Get(model.Comprimento.Value).Descricao);
                }

                if (model.Barra.HasValue)
                {
                    modelos = modelos.Where(p => p.Barra.Id == model.Barra);
                    filtros.AppendFormat("Barra: {0}, ", _barraRepository.Get(model.Barra.Value).Descricao);
                }
                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        modelos = model.OrdenarEm == "asc"
                            ? modelos.OrderBy(model.OrdenarPor)
                            : modelos.OrderByDescending(model.OrdenarPor);
                    else
                        modelos = modelos.OrderByDescending(m => m.DataAlteracao);

                    model.Grid = modelos.Select(p => new GridModeloModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Referencia = p.Referencia,
                        Descricao = p.Descricao,
                        Colecao = p.Colecao.Descricao,
                        Estilista = p.Estilista.Nome,
                        Grade = p.Grade.Descricao,
                        Marca = p.Marca.Nome,
                        Segmento = p.Segmento != null ? p.Segmento.Descricao : null,

                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = modelos
                    .Fetch(p => p.Colecao).Fetch(p => p.Estilista).Fetch(p => p.Modelista).Fetch(p => p.Grade).Fetch(p => p.Marca).Fetch(p => p.Segmento)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                //Report report = null;

                var report = new ListagemModelosReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasListagemModelos.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { Error = message });

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        #endregion
        
        #region ConsumoMaterialColecao
        [PopulateViewData("PopulateConsumoMaterialColecao")]
        public virtual ActionResult ConsumoMaterialColecao()
        {
            var model = new ConsumoMaterialColecaoModel();
            return View(model);
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateConsumoMaterialColecao")]
        public virtual JsonResult ConsumoMaterialColecao(ConsumoMaterialColecaoModel model)
        {
            var query = _consumoRepository.Find();
           
            var filtros = new StringBuilder();

            if (model.Colecao.HasValue)
            {
                query = query.Where(p => p.Colecao == model.Colecao);
                filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
            }

            if (model.Categoria.HasValue)
            {
                query = query.Where(p => p.Categoria == model.Categoria);
                filtros.AppendFormat("Categoria: {0}, ", _categoriaRepository.Get(model.Categoria.Value).Nome);
            }

            if (model.Subcategoria.HasValue)
            {
                query = query.Where(p => p.Subcategoria == model.Subcategoria);
                filtros.AppendFormat("Subcategoria: {0}, ", _subcategoriaRepository.Get(model.Subcategoria.Value).Nome );
            }

            if (model.Familia.HasValue)
            {
                query = query.Where(p => p.Familia == model.Familia);
                filtros.AppendFormat("Família: {0}, ", _classificacaoRepository.Get(model.Familia.Value).Descricao);
            }

            if (model.ColecaoAprovada.HasValue)
            {
                query = query.Where(p => p.ColecaoAprovada == model.ColecaoAprovada);
                filtros.AppendFormat("Coleção Aprovada: {0}, ", _colecaoRepository.Get(model.ColecaoAprovada.Value).Descricao);
            }

            List<ConsumoMaterialColecaoView> result;
            if (model.AgruparPor != null && model.AgruparPor.Equals("DataPrevisaoEnvio"))
            {
                result = query
                    //Atualizar NHibernate para utilizar o GroupBy direto na consulta, possível problema de estouro de memória
                    .ToList()
                    .GroupBy(x => new
                    {
                        x.Referencia,
                        x.Descricao,
                        x.Unidade,
                        x.Categoria,
                        x.Subcategoria,
                        x.Estoque,
                        x.DataPrevisaoEnvio
                    })
                    .Select(x =>
                        new ConsumoMaterialColecaoView
                        {
                            Referencia = x.Key.Referencia,
                            Descricao = x.Key.Descricao,
                            Unidade = x.Key.Unidade,
                            Quantidade = x.Sum(y => y.Quantidade),
                            Categoria = x.Key.Categoria,
                            Compras = x.Sum(y => y.Compras),
                            Diferenca = x.Sum(y => y.Diferenca),
                            Estoque = x.Key.Estoque,
                            DataPrevisaoEnvio = x.Key.DataPrevisaoEnvio,
                            Subcategoria = x.Key.Subcategoria
                        }
                    )
                    .ToList();
            }
            else
            {
                result = query
                    //Atualizar NHibernate para utilizar o GroupBy direto na consulta
                    .ToList()
                    .GroupBy(x => new
                    {
                        x.Referencia,
                        x.Descricao,
                        x.Unidade,
                        x.Categoria,
                        x.Subcategoria,
                        x.Estoque
                    })
                    .Select(x =>
                        new ConsumoMaterialColecaoView
                        {
                            Referencia = x.Key.Referencia,
                            Descricao = x.Key.Descricao,
                            Unidade = x.Key.Unidade,
                            Quantidade = x.Sum(y => y.Quantidade),
                            Categoria = x.Key.Categoria,
                            Compras = x.Sum(y => y.Compras),
                            Diferenca = x.Sum(y => y.Diferenca),
                            Estoque = x.Key.Estoque,
                            Subcategoria = x.Key.Subcategoria
                        }
                    )
                    .ToList();
            }
        
            if (!result.Any())
                return Json(new { Error = "Nenhum item foi encontrado." });

            var report = new ConsumoMaterialColecaoReport { DataSource = result };

            if (filtros.Length > 2)
                report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

            if (model.AgruparPor != null)
            {
                grupo.Groupings.Add("=Fields." + model.AgruparPor);

                var key = ColunasConsumoMaterialColecao.First(p => p.Value == model.AgruparPor).Key;
                var titulo = string.Format("=\"{0}: \" + Fields.{1}", key, model.AgruparPor);
                grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
            }
            else
            {
                report.Groups.Remove(grupo);
            }

            if (model.OrdenarPor != null)
                report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #endregion
        
        #region Métodos
        
        #region PopulateListagemModelos
        protected void PopulateListagemModelos(IModeloDropdownModel model)
        {
            var grades = _gradeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Grade"] = grades.ToSelectList("Descricao", model.Grade);

            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);

            var classificacoes = _classificacaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Classificacao"] = classificacoes.ToSelectList("Descricao", model.Classificacao);

            var segmentos = _segmentoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Segmento"] = segmentos.ToSelectList("Descricao", model.Segmento);

            var naturezas = _naturezaRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Natureza"] = naturezas.ToSelectList("Descricao", model.Natureza);

            var barras = _barraRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Barra"] = barras.ToSelectList("Descricao", model.Barra);

            var comprimentos = _comprimentoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Comprimento"] = comprimentos.ToSelectList("Descricao", model.Comprimento);

            var produtosBase = _produtoBaseRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ProdutoBase"] = produtosBase.ToSelectList("Descricao", model.ProdutoBase);

            var artigos = _artigoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Artigo"] = artigos.ToSelectList("Descricao", model.Artigo);

            var marcas = _marcaRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["Marca"] = marcas.ToSelectList("Nome", model.Marca);

            // Se tela de pesquisa
            var pesquisaModel = model as PesquisaModeloModel;
            if (pesquisaModel != null)
            {
                var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                    && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                    .OrderBy(p => p.Nome).ToList();
                ViewData["Estilista"] = estilistas.ToSelectList("Nome", pesquisaModel.Funcionario);

                var modelistas = _pessoaRepository.Find(p => p.Funcionario != null
                    && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Modelista)
                    .OrderBy(p => p.Nome).ToList();
                ViewData["Modelista"] = modelistas.ToSelectList("Nome", pesquisaModel.Modelista);

                ViewBag.TipoRelatorio = new SelectList(_tipoRelatorio);
                ViewBag.OrdenarPor = new SelectList(ColunasListagemModelos, "value", "key");
                ViewBag.AgruparPor = new SelectList(ColunasListagemModelos, "value", "key");
            }
        }
        #endregion
        
        #region PopulateConsumoMaterialColecao
        protected void PopulateConsumoMaterialColecao(ConsumoMaterialColecaoModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);

            var colecaoAprovada = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecaoAprovada.ToSelectList("Descricao", model.Colecao);

            var categorias = _categoriaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Categoria"] = categorias.ToSelectList("Nome", model.Categoria);

            var subcategorias = _subcategoriaRepository.Find(p => p.Categoria.Id == model.Categoria && p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Subcategoria"] = subcategorias.ToSelectList("Nome", model.Subcategoria);

            var familias = _familiaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Familia"] = familias.ToSelectList("Nome", model.Familia);

            ViewBag.OrdenarPor = new SelectList(ColunasConsumoMaterialColecao, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasConsumoMaterialColecao, "value", "key");
        }
        #endregion
        
        #endregion
    }
}